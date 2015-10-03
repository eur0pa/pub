#include <stdio.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <errno.h>
#include <setjmp.h>
#include <signal.h>

jmp_buf alarm_ret;

void alarm_func(int a)
{
  longjmp(alarm_ret, 1);
}

int main(int argc, char **argv)
{
  int connect_fd, ident_fd, len;
  char buffer[256];

  struct sockaddr_in connect_sa, ident_sa;

  if(argc < 3) {
    printf("%s <ip> <port>\n", argv[0]);
    return 1;
  }

  signal(SIGALRM, alarm_func);

  // establish a connection to that port

  connect_sa.sin_addr.s_addr=inet_addr(argv[1]);
  connect_sa.sin_port=htons(atoi(argv[2]));
  connect_sa.sin_family=AF_INET;

  if((connect_fd=socket(AF_INET, SOCK_STREAM, 0)) == -1) {
    printf("cannot create socket: %s", strerror(errno));
    return 1;
  }

  if(connect(connect_fd, (struct sockaddr*) &connect_sa, sizeof(connect_sa)) == -1) {
    printf("cannot connect to %s port %s: %s\n", argv[1], argv[2], strerror(errno));
    return 1;
  }

  // find local port

  len=sizeof(connect_sa);

  if(getsockname(connect_fd, (struct sockaddr*)&connect_sa, &len) == -1) {
    printf("getsockname() failed: %s\n", strerror(errno));
    return 1;
  }

  // ident request

  ident_sa.sin_addr.s_addr=inet_addr(argv[1]);
  ident_sa.sin_port=htons(113);
  ident_sa.sin_family=AF_INET;

  if((ident_fd=socket(AF_INET, SOCK_STREAM, 0)) == -1) {
    printf("cannot create socket: %s", strerror(errno));
    return 1;
  }

  if(connect(ident_fd, (struct sockaddr*) &ident_sa, sizeof(ident_sa)) == -1) {
    printf("cannot connect to identd on %s port 113: %s\n", argv[1], strerror(errno));
    return 1;
  }

  snprintf(buffer, 256, "%s, %d\n", argv[2], htons(connect_sa.sin_port));
  send(ident_fd, buffer, strlen(buffer), 0);

  len=recv(ident_fd, buffer, sizeof(buffer)-1, 0);

  if(len <= 0) {
    printf("recv() failed: %s\n", len == -1 ? strerror(errno) : "no data");
    return 1;
  }

  buffer[len-1]='\0';

  printf("ident reply: %s\n", buffer);

  // print banner

  len=0;

  if(!setjmp(alarm_ret)) {
    alarm(10);
    len=recv(connect_fd, buffer, sizeof(buffer)-1, 0);

     if(len > 0) {
       buffer[len-1]='\0';
       printf("banner: %s\n", buffer);
    }

    alarm(0);
  }

  if(len <= 0)
    printf("no banner found\n");

  close(connect_fd);
  close(ident_fd);

  return 0;
}

