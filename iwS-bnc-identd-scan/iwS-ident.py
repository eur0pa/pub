#!/usr/bin/env python
# priv8


import sys
import argparse
import socket
import errno


def main():
    parser = argparse.ArgumentParser(
        formatter_class=argparse.RawDescriptionHelpFormatter,
        description='''
+++ bnc identd trick +++ mtx . 2o
+++ ++ + priv8! + ++ +++ mon . o7
''',
        epilog='''
this tool abuses an ident daemon to obtain either a list of user:port tuples,
or the port for a specific user. it uses a nifty trick to determine if any given
local port is bound or not.
            ''')

    parser.add_argument(
            '-m', '--min-port',
            default=1024,
            type=int,
            help='min port to scan from (default: %(default)s)',
            dest='min_port')

    parser.add_argument(
            '-M', '--max-port',
            default=65535,
            type=int,
            help='max port to scan to (default: %(default)s)',
            dest='max_port')

    parser.add_argument(
            '-i', '--ident',
            default='any',
            help='ident to scan for (default: %(default)s)',
            dest='ident')

    parser.add_argument(
            '-t', '--target',
            default='127.0.0.1',
            help='remote/local host to scan (default: %(default)s)',
            dest='host')

    args = parser.parse_args()

    enumerate_host(args)


# in case of local enumeration we'll try to bind() instead of connecting - it's
# faster and less noticeable. if we can't go through it means the port is bound
# to someone.
def enumerate_host(args):
    for port in xrange(args.min_port, args.max_port):
        try:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.setblocking(0)
            if args.host == '127.0.0.1' or args.host == 'localhost':
                s.bind(('127.0.0.1', port))
            else:
                s.connect((args.host, port))
            s.shutdown(2)
        except socket.error, ex:
            (error_number, error_message) = ex
            if error_number == 98:
                whois = enumerate_ident(args.host, port)
                if args.ident and args.ident == whois:
                    print "user %s is bound to port %d" % (whois, port)
                elif args.ident == 'any' and whois:
                    print "port %d is bound to user %s" % (port, whois)
        s.close()


def enumerate_ident(host, port):
    tricksock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    identsock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    try:
        tricksock.connect((host, port))
    except socket.error, ex:
        (error_number, error_message) = ex
        if error_number == 111:
            tricksock.close()
            return

    try:
        identsock.connect((host, 113))
    except socket.error, ex:
        (error_number, error_message) = ex
        identsock.close()
        print "identd not available"
        sys.exit()

    trickaddr = tricksock.getsockname()
    trickport = trickaddr[1]

    identsock.send(str(port) + "," + str(trickport) + "\n")
    whois = identsock.recv(128)

    tricksock.shutdown(2)
    identsock.shutdown(2)

    tricksock.close()
    identsock.close()

    return whois.split(':')[3].rstrip('\r\n')


if __name__ == "__main__":
        main()

