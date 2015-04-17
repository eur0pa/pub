#!/usr/bin/env python2
# -*- coding: utf-8 -*

import sys
import time
import string
import socket
import threading

import praw


reddit_user = ''
reddit_pass = ''
subs = ['pcmasterrace',
        'steamgameswap',
        'steamgiveaway',
        'randomactsofgaming',
        'pcgaming',
        'giftofgames']
super_subs = ['steamgiveaway',
              'giftofgames']
includes = ['giveaway']
includes_urls = ['humblebundle.com']
excludes = ['giveaway over',
            'request',
            'thanks',
            'thank you',
            '[request]',
            '[thanks]',
            '[thank]']
excludes_urls = []
dump = "giveaway.txt"

irc_host = ""
irc_port = 6667
irc_nick = ""
irc_chan = "#"


class IRC(threading.Thread):
    def __init__(self, host, port, nick, chan):
        threading.Thread.__init__(self)
        self.host = host
        self.port = port
        self.nick = nick
        self.chan = chan
        self.s = socket.socket()


    def run(self):
        self.__login()
        self.__idle()


    def stop(self):
        self.__quit()
        self._Thread__stop()


    def announce(self, dest, sub, title, link):
        self.s.send("PRIVMSG %s :[/r/%s] %s: %s\r\n" % (dest, sub, title, link))


    def __login(self):
        self.s.connect((self.host, self.port))
        self.s.send("NICK %s\r\n" % self.nick)
        self.s.send("USER %s %s 0: %s\r\n" % (self.nick, self.host, self.nick))
        self.s.send("JOIN %s\r\n" % self.chan)
        time.sleep(1)


    def __idle(self):
        while True:
            ircbuffer = ""
            ircbuffer = ircbuffer + self.s.recv(1024)
            temp = string.split(ircbuffer, "\n")
            ircbuffer = temp.pop()

            for line in temp:
                line = string.rstrip(line)
                line = string.split(line)

            try:
                if line[0] == "PING":
                    self.s.send("PONG %s\r\n" % line[1])
            except:
                _print("irc exception: " + line)
            time.sleep(1)


    def __quit(self):
        self.s.send("QUIT\r\n")
        self.s.close()


class LURKER(threading.Thread):
    def __init__(self, irc, user, passw):
        self.thread = threading.Thread.__init__(self)
        self.irc = irc
        self.r = praw.Reddit('PRAW Test')
        self.user = user
        self.passw = passw
        self.stack = []


    def run(self):
        self.__login(self.user, self.passw)
        self.__lurk()


    def stop(self):
        self._Thread__stop()


    def __login(self, user, passw):
        try:
            self.r.login(user, passw)
        except Exception as e:
            print e
            raise


    def __lurk(self):
        while True:
            for sub in subs:
                subreddit = self.r.get_subreddit(sub)
                _print("lurking  /r/" + sub, 1)

                for post in subreddit.get_new(limit=5):
                    if post.id not in self.stack:
                        self.stack.append(post.id)
                        _print("checking /r/" + sub + " post #" + post.id)
                        if self.__check_submission(post):
                            _print(post.title)
                            post.upvote()
                            t = threading.Thread(target=self.irc.announce, args=(irc_chan, sub, post.title, post.short_link))
                            t.daemon = True
                            t.start()
                            with open(dump, 'a') as f:
                                f.write(post.id + '\n')
                time.sleep(3)


    def __check_submission(self, post):
        if post.id in open(dump).read():
            return 0

        for exclude in excludes:
            if unicode(exclude) in unicode(post.title).lower():
                return 0

        for exclude_url in excludes_urls:
            if unicode(exclude_url) in unicode(post.url).lower():
                return 0

        for super_sub in super_subs:
            if unicode(super_sub) in post.domain:
                return 1

        if not post.is_self:
            for include_url in includes_urls:
                if unicode(include_url) in unicode(post.url).lower():
                    return 1

        for include in includes:
            if unicode(include) in unicode(post.title).lower() or \
               unicode(include) in unicode(post.selftext).lower() or \
               unicode(include) in unicode(post.link_flair_text).lower():
               return 1


def _print(msg, status=0):
    clock = "[" + time.asctime(time.localtime()) + "] "
    sys.stdout.write("\r" + clock + msg)
    sys.stdout.write("\033[K")
    sys.stdout.flush()

def main():
    irc = None
    lurk = None
    threads = []

    _print("starting irc thread...", 1)
    irc = IRC(irc_host, irc_port, irc_nick, irc_chan)
    irc.daemon = True
    threads.append(irc)
    irc.start()

    time.sleep(1)

    while threading.active_count() > 0:
        if not lurk or not lurk.is_alive():
            _print("starting reddit thread...", 1)
            lurk = LURKER(irc, reddit_user, reddit_pass)
            lurk.daemon = True
            threads.append(lurk)
            lurk.start()
        lurk.join(1)


if __name__ == '__main__':
    print("reddit giveaway grabber")

    try:
        with open(dump) as f:
            pass
    except IOError:
        file(dump, 'w').close()

    main()
