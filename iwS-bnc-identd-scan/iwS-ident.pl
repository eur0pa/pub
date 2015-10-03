#!/usr/bin/perl -w
#####################
#  Ident Discovery  #
#  Code by: matrix  #
#       priv8       #
#####################

use IO::Socket::INET;
use Proc::Queue size => '100', debug => '0', trace => '0', delay => '0.02';

sub usage() {
        print "[*] -=[ Ident Discovery - Coded by: matrix - Priv8 ]=- [*]\n";
        print "[*] -=[ Uso: $0 <ip> <startport> <endport> ]=- [*]\n";
}

sub mtx {
          my $port = $_[0];
          my $temp = new IO::Socket::INET(PeerAddr => $ARGV[0], PeerPort => $port, Proto => 'tcp', Timeout => '1');
          if ($temp) {
          my $ident = new IO::Socket::INET(PeerAddr => $ARGV[0], PeerPort => '113', Proto => 'tcp', Timeout => '5');
          my $localport = $temp->sockport();
          $request = "$port, $localport\n";
          $ident->send($request);
          $ident->recv($text, 64);
          close $ident;
          close $temp;
          print "[+] Host: $ARGV[0] - Port: $port - Ident: $text";
          }
}

if (@ARGV < 3) {
        usage();
        exit;
}

        print "[*] -=[  Ident Discovery  ]=- [*]\n";
        print "[*] -=[  Code by: matrix  ]=- [*]\n";
        print "[*] -=[       Priv8       ]=- [*]\n";
        my $test = new IO::Socket::INET(PeerAddr => $ARGV[0], PeerPort => '113', Proto => 'tcp', Timeout => '10') or die("[!] Identd Not Running!\n");
        close $test;
        $n = $ARGV[1];
        while ($n<$ARGV[2]) {
        $pid = fork();
        if( $pid == 0 ){ mtx($n); exit(0); }
          $n++;
        }
        print "[*] Discovery Terminato [*]\n";


