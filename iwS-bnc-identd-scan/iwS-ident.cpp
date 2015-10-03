#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <errno.h>
#include <string>
#include <list>
#include <vector>
#include <ifaddrs.h>

#define HAVE_GETIFADDRS
#define HAVE_IPV6

using namespace std;

class IPlist
{
 public:
  class entry
  {
   public:
    string str;
    string iface;
  };

  list<entry *> ipv4;
  list<entry *>::iterator current4;
#ifdef HAVE_IPV6
  list<entry *> ipv6;
  list<entry *>::iterator current6;
#endif
  void scan();
  string *getNext(int);

  int count() {
    int cnt=ipv4.size();
#ifdef HAVE_IPV6
    cnt+=ipv6.size();
#endif
    return cnt;
  }

  int count(int proto) {
    if(proto == AF_INET)
      return ipv4.size();
#ifdef HAVE_IPV6
   else if(proto == AF_INET6)
     return ipv6.size();
#endif
   return 0;
  }
};

void try_listen(const char *ip, int port);

int main(int argc, char **argv)
{
  int endport, i;
  string *ip;
  IPlist iplist;

  if(argc < 3) {
    printf("%s <start-port> <end-port>\n", argv[0]);
    return 1;
  }

  iplist.scan();
  ip=&((*iplist.current4)->str);

  while(ip)
  {
    i=atoi(argv[1]);
    endport=atoi(argv[2]);

    for(; i <= endport; i++)
      try_listen(ip->c_str(), i);

    ip=iplist.getNext(AF_INET);
  }

  return 0;
}

void try_listen(const char *ip, int port)
{
  int opt=1, socketfd;
  struct sockaddr_in sa;

  sa.sin_addr.s_addr=inet_addr(ip);
  sa.sin_port=htons(port);
  sa.sin_family=AF_INET;

  if((socketfd=socket(AF_INET, SOCK_STREAM, 0)) == -1) {
    printf("cannot create a socket (%s)", strerror(errno));
    return;
  }

  setsockopt(socketfd, SOL_SOCKET, SO_REUSEADDR, (char *)&opt, sizeof(opt));
  setsockopt(socketfd, SOL_SOCKET, SO_KEEPALIVE, (char *)&opt, sizeof(opt));

  if(bind(socketfd, (struct sockaddr*)&sa, sizeof(sa)) == -1)
    printf("%s %d (cannot bind: %s)\n", ip, port, strerror(errno));

  else if(listen(socketfd, 10) == -1)
    printf("%s %d (cannot listen: %s)\n", ip, port, strerror(errno));

  close(socketfd);
}

///////////////////////////////////////

void IPlist::scan()
{
#ifdef HAVE_GETIFADDRS
  char srcip[256];

  struct ifaddrs *ifap, *ifa;

  memset(srcip, 0, sizeof(srcip));

  if(getifaddrs(&ifap) != 0) {
    printf("*** getifaddrs() failed\n");
    return;
  }

  for(ifa=ifap; ifa; ifa=ifa->ifa_next) {
    if(ifa->ifa_addr == NULL)
      continue;
#ifdef HAVE_IPV6
    if(ifa->ifa_addr->sa_family == AF_INET6) {
      inet_ntop(ifa->ifa_addr->sa_family, &((struct sockaddr_in6 *)ifa->ifa_addr)->sin6_addr, srcip, sizeof(srcip)-1);

//      if(strcmp(srcip, "::1") == 0)
//        continue;

//      if(strncmp(srcip, "fe80", 4) == 0)
//        continue;

      entry *node=new entry;
      node->str=srcip;
      node->iface=ifa->ifa_name;
      ipv6.push_back(node);
    }

    else
#endif
    if(ifa->ifa_addr->sa_family==AF_INET) {
      inet_ntop(ifa->ifa_addr->sa_family, &((struct sockaddr_in *)ifa->ifa_addr)->sin_addr, srcip, sizeof(srcip)-1);

//      if(strncmp(srcip, "127", 3) == 0)
//        continue;

//      if(strncmp(srcip, "192", 3) == 0 || strncmp(srcip, "10.", 3) == 0)
//        continue;

      entry *node=new entry;
      node->str=srcip;
      node->iface=ifa->ifa_name;
      ipv4.push_back(node);
      }
    }
#endif

  current4=ipv4.begin();
#ifdef HAVE_IPV6
  current6=ipv6.begin();
#endif
}

string *IPlist::getNext(int proto)
{
  if(proto == AF_INET) {
    if(ipv4.size() == 0)
      return NULL;

    else {
      current4++;

      if(current4 == ipv4.end())
        return NULL;
      return &((*current4)->str);
    }
  }
#ifdef HAVE_IPV6
  else if(proto == AF_INET6) {
    if(ipv6.size() == 0)
      return NULL;

    else {
      current6++;

      if(current6 == ipv6.end())
        return NULL;

      return &((*current6)->str);
    }
  }
#endif
  return NULL;
}

