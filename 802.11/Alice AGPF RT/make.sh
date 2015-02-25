#!/bin/bash

if [ $# -lt 2 ]; then
    echo -e "\n\n\tmake <lin|w32|w64> <native|atom|core2|xeon|amd> [static] [mt]\n\n"
    exit 0
fi


case $1 in
    "lin") CC="gcc" ; STRIP="strip" ; EXT="" ; if [ "$4" == "mt" ]; then THREADS="-lpthread"; else THREADS=""; fi ;;
    "w32") CC="i586-mingw32msvc-gcc" ; STRIP="i586-mingw32msvc-strip" ; EXT="_w32.exe" ; if [ "$4" == "mt" ]; then THREADS="pthreadVSE2.dll"; else THREADS=""; fi ;;
    "w64") CC="amd64-mingw32msvc-gcc" ; STRIP="amd64-mingw32msvc-strip" ; EXT="_w64.exe"; if [ "$4" == "mt" ]; then THREADS="pthreadVSE2.dll"; else THREADS=""; fi ;;
        *) exit 0 ;;
esac

case $2 in
    "native") CFLAGS="-O3 -march=native   -mtune=native   -msse4.2 -mfpmath=sse" ;;
      "atom") CFLAGS="-O3 -march=prescott -mtune=prescott -mssse3  -mfpmath=sse" ;;
     "core2") CFLAGS="-O3 -march=core2    -mtune=core2    -msse4.1 -mfpmath=sse" ;;
      "xeon") CFLAGS="-O3 -march=core2    -mtune=generic  -msse4.2 -mfpmath=sse" ;;
       "amd") CFLAGS="-O3 -march=k8       -mtune=k8       -msse3   -mfpmath=sse" ;;
           *) exit 0 ;;
esac

COPT="-fomit-frame-pointer -funroll-loops -pipe"

[ "$3" == "static" ] && COPT="$COPT -static"

rm -f alice_agpf_brute*.exe alice_agpf_brute
$CC -o alice_agpf_brute$EXT $CFLAGS $COPT $THREADS  alice_agpf_brute_MT.c
$STRIP -s alice_agpf_brute$EXT
