#include <string.h>
#include <assert.h>
#define SHA2_UNROLL_TRANSFORM
#define SHA2_USE_INTTYPES_H
//#define BYTE_ORDER LITTLE_ENDIAN
#ifndef __SHA2_H__
#define __SHA2_H__
#ifdef __cplusplus
extern "C" {
#endif
#include <sys/types.h>
#ifdef SHA2_USE_INTTYPES_H
#include <inttypes.h>
#endif
#define SHA256_BLOCK_LENGTH     64
#define SHA256_DIGEST_LENGTH        32
#define SHA256_DIGEST_STRING_LENGTH (SHA256_DIGEST_LENGTH * 2 + 1)
#if 0
typedef unsigned char u_int8_t;
typedef unsigned int u_int32_t;
typedef unsigned long long u_int64_t;
#endif
#ifdef SHA2_USE_INTTYPES_H
typedef struct _SHA256_CTX {
    uint32_t    state[8];
    uint64_t    bitcount;
    uint8_t buffer[SHA256_BLOCK_LENGTH];
} SHA256_CTX;
#else
typedef struct _SHA256_CTX {
    u_int32_t   state[8];
    u_int64_t   bitcount;
    u_int8_t    buffer[SHA256_BLOCK_LENGTH];
} SHA256_CTX;
#endif
#ifndef NOPROTO
#ifdef SHA2_USE_INTTYPES_H
void SHA256_Init(SHA256_CTX *);
void SHA256_Update(SHA256_CTX*, const uint8_t*, size_t);
void SHA256_Final(uint8_t[SHA256_DIGEST_LENGTH], SHA256_CTX*);
#else
void SHA256_Init(SHA256_CTX *);
void SHA256_Update(SHA256_CTX*, const u_int8_t*, size_t);
void SHA256_Final(u_int8_t[SHA256_DIGEST_LENGTH], SHA256_CTX*);
#endif
#else
void SHA256_Init();
void SHA256_Update();
void SHA256_Final();
#endif
#ifdef  __cplusplus
}
#endif
#endif
#ifdef SHA2_USE_INTTYPES_H
typedef uint8_t  sha2_byte;
typedef uint32_t sha2_word32;
typedef uint64_t sha2_word64;
#else
typedef u_int8_t  sha2_byte;
typedef u_int32_t sha2_word32;
typedef u_int64_t sha2_word64;
#endif
#define SHA256_SHORT_BLOCK_LENGTH   (SHA256_BLOCK_LENGTH - 8)
#if BYTE_ORDER == LITTLE_ENDIAN
#define REVERSE32(w,x)  { \
    sha2_word32 tmp = (w); \
    tmp = (tmp >> 16) | (tmp << 16); \
    (x) = ((tmp & 0xff00ff00UL) >> 8) | ((tmp & 0x00ff00ffUL) << 8); \
}
#define REVERSE64(w,x)  { \
    sha2_word64 tmp = (w); \
    tmp = (tmp >> 32) | (tmp << 32); \
    tmp = ((tmp & 0xff00ff00ff00ff00ULL) >> 8) | \
          ((tmp & 0x00ff00ff00ff00ffULL) << 8); \
    (x) = ((tmp & 0xffff0000ffff0000ULL) >> 16) | \
          ((tmp & 0x0000ffff0000ffffULL) << 16); \
}
#endif
#define ADDINC128(w,n)  { \
    (w)[0] += (sha2_word64)(n); \
    if ((w)[0] < (n)) { \
        (w)[1]++; \
    } \
}
#if !defined(SHA2_USE_MEMSET_MEMCPY) && !defined(SHA2_USE_BZERO_BCOPY)
#define SHA2_USE_MEMSET_MEMCPY  1
#endif
#if defined(SHA2_USE_MEMSET_MEMCPY) && defined(SHA2_USE_BZERO_BCOPY)
#error Define either SHA2_USE_MEMSET_MEMCPY or SHA2_USE_BZERO_BCOPY, not both!
#endif
#ifdef SHA2_USE_MEMSET_MEMCPY
#define MEMSET_BZERO(p,l)   memset((p), 0, (l))
#define MEMCPY_BCOPY(d,s,l) memcpy((d), (s), (l))
#endif
#ifdef SHA2_USE_BZERO_BCOPY
#define MEMSET_BZERO(p,l)   bzero((p), (l))
#define MEMCPY_BCOPY(d,s,l) bcopy((s), (d), (l))
#endif
#define R(b,x)      ((x) >> (b))
#define S32(b,x)    (((x) >> (b)) | ((x) << (32 - (b))))
#define Ch(x,y,z)   (((x) & (y)) ^ ((~(x)) & (z)))
#define Maj(x,y,z)  (((x) & (y)) ^ ((x) & (z)) ^ ((y) & (z)))
#define Sigma0_256(x)   (S32(2,  (x)) ^ S32(13, (x)) ^ S32(22, (x)))
#define Sigma1_256(x)   (S32(6,  (x)) ^ S32(11, (x)) ^ S32(25, (x)))
#define sigma0_256(x)   (S32(7,  (x)) ^ S32(18, (x)) ^ R(3 ,   (x)))
#define sigma1_256(x)   (S32(17, (x)) ^ S32(19, (x)) ^ R(10,   (x)))
void SHA256_Transform(SHA256_CTX*, const sha2_word32*);
const static sha2_word32 K256[64] = {
    0x428a2f98UL, 0x71374491UL, 0xb5c0fbcfUL, 0xe9b5dba5UL,
    0x3956c25bUL, 0x59f111f1UL, 0x923f82a4UL, 0xab1c5ed5UL,
    0xd807aa98UL, 0x12835b01UL, 0x243185beUL, 0x550c7dc3UL,
    0x72be5d74UL, 0x80deb1feUL, 0x9bdc06a7UL, 0xc19bf174UL,
    0xe49b69c1UL, 0xefbe4786UL, 0x0fc19dc6UL, 0x240ca1ccUL,
    0x2de92c6fUL, 0x4a7484aaUL, 0x5cb0a9dcUL, 0x76f988daUL,
    0x983e5152UL, 0xa831c66dUL, 0xb00327c8UL, 0xbf597fc7UL,
    0xc6e00bf3UL, 0xd5a79147UL, 0x06ca6351UL, 0x14292967UL,
    0x27b70a85UL, 0x2e1b2138UL, 0x4d2c6dfcUL, 0x53380d13UL,
    0x650a7354UL, 0x766a0abbUL, 0x81c2c92eUL, 0x92722c85UL,
    0xa2bfe8a1UL, 0xa81a664bUL, 0xc24b8b70UL, 0xc76c51a3UL,
    0xd192e819UL, 0xd6990624UL, 0xf40e3585UL, 0x106aa070UL,
    0x19a4c116UL, 0x1e376c08UL, 0x2748774cUL, 0x34b0bcb5UL,
    0x391c0cb3UL, 0x4ed8aa4aUL, 0x5b9cca4fUL, 0x682e6ff3UL,
    0x748f82eeUL, 0x78a5636fUL, 0x84c87814UL, 0x8cc70208UL,
    0x90befffaUL, 0xa4506cebUL, 0xbef9a3f7UL, 0xc67178f2UL
};
const static sha2_word32 sha256_initial_hash_value[8] = {
    0x6a09e667UL,
    0xbb67ae85UL,
    0x3c6ef372UL,
    0xa54ff53aUL,
    0x510e527fUL,
    0x9b05688cUL,
    0x1f83d9abUL,
    0x5be0cd19UL
};
static const char *sha2_hex_digits = "0123456789abcdef";
void SHA256_Init(SHA256_CTX* context) {
    if (context == (SHA256_CTX*)0) {
        return;
    }
    MEMCPY_BCOPY(context->state, sha256_initial_hash_value, SHA256_DIGEST_LENGTH);
    MEMSET_BZERO(context->buffer, SHA256_BLOCK_LENGTH);
    context->bitcount = 0;
}
#ifdef SHA2_UNROLL_TRANSFORM
#if BYTE_ORDER == LITTLE_ENDIAN
#define ROUND256_0_TO_15(a,b,c,d,e,f,g,h)   \
    REVERSE32(*data++, W256[j]); \
    T1 = (h) + Sigma1_256(e) + Ch((e), (f), (g)) + \
             K256[j] + W256[j]; \
    (d) += T1; \
    (h) = T1 + Sigma0_256(a) + Maj((a), (b), (c)); \
    j++
#else
#define ROUND256_0_TO_15(a,b,c,d,e,f,g,h)   \
    T1 = (h) + Sigma1_256(e) + Ch((e), (f), (g)) + \
         K256[j] + (W256[j] = *data++); \
    (d) += T1; \
    (h) = T1 + Sigma0_256(a) + Maj((a), (b), (c)); \
    j++
#endif
#define ROUND256(a,b,c,d,e,f,g,h)   \
    s0 = W256[(j+1)&0x0f]; \
    s0 = sigma0_256(s0); \
    s1 = W256[(j+14)&0x0f]; \
    s1 = sigma1_256(s1); \
    T1 = (h) + Sigma1_256(e) + Ch((e), (f), (g)) + K256[j] + \
         (W256[j&0x0f] += s1 + W256[(j+9)&0x0f] + s0); \
    (d) += T1; \
    (h) = T1 + Sigma0_256(a) + Maj((a), (b), (c)); \
    j++
void SHA256_Transform(SHA256_CTX* context, const sha2_word32* data) {
    sha2_word32 a, b, c, d, e, f, g, h, s0, s1;
    sha2_word32 T1, *W256;
    int     j;
    W256 = (sha2_word32*)context->buffer;
    a = context->state[0];
    b = context->state[1];
    c = context->state[2];
    d = context->state[3];
    e = context->state[4];
    f = context->state[5];
    g = context->state[6];
    h = context->state[7];
    j = 0;
    do {
        ROUND256_0_TO_15(a,b,c,d,e,f,g,h);
        ROUND256_0_TO_15(h,a,b,c,d,e,f,g);
        ROUND256_0_TO_15(g,h,a,b,c,d,e,f);
        ROUND256_0_TO_15(f,g,h,a,b,c,d,e);
        ROUND256_0_TO_15(e,f,g,h,a,b,c,d);
        ROUND256_0_TO_15(d,e,f,g,h,a,b,c);
        ROUND256_0_TO_15(c,d,e,f,g,h,a,b);
        ROUND256_0_TO_15(b,c,d,e,f,g,h,a);
    } while (j < 16);
    do {
        ROUND256(a,b,c,d,e,f,g,h);
        ROUND256(h,a,b,c,d,e,f,g);
        ROUND256(g,h,a,b,c,d,e,f);
        ROUND256(f,g,h,a,b,c,d,e);
        ROUND256(e,f,g,h,a,b,c,d);
        ROUND256(d,e,f,g,h,a,b,c);
        ROUND256(c,d,e,f,g,h,a,b);
        ROUND256(b,c,d,e,f,g,h,a);
    } while (j < 64);
    context->state[0] += a;
    context->state[1] += b;
    context->state[2] += c;
    context->state[3] += d;
    context->state[4] += e;
    context->state[5] += f;
    context->state[6] += g;
    context->state[7] += h;
    a = b = c = d = e = f = g = h = T1 = 0;
}
#else
void SHA256_Transform(SHA256_CTX* context, const sha2_word32* data) {
    sha2_word32 a, b, c, d, e, f, g, h, s0, s1;
    sha2_word32 T1, T2, *W256;
    int     j;
    W256 = (sha2_word32*)context->buffer;
    a = context->state[0];
    b = context->state[1];
    c = context->state[2];
    d = context->state[3];
    e = context->state[4];
    f = context->state[5];
    g = context->state[6];
    h = context->state[7];
    j = 0;
    do {
#if BYTE_ORDER == LITTLE_ENDIAN
        REVERSE32(*data++,W256[j]);
        T1 = h + Sigma1_256(e) + Ch(e, f, g) + K256[j] + W256[j];
#else
        T1 = h + Sigma1_256(e) + Ch(e, f, g) + K256[j] + (W256[j] = *data++);
#endif
        T2 = Sigma0_256(a) + Maj(a, b, c);
        h = g;
        g = f;
        f = e;
        e = d + T1;
        d = c;
        c = b;
        b = a;
        a = T1 + T2;
        j++;
    } while (j < 16);
    do {
        s0 = W256[(j+1)&0x0f];
        s0 = sigma0_256(s0);
        s1 = W256[(j+14)&0x0f];
        s1 = sigma1_256(s1);
        T1 = h + Sigma1_256(e) + Ch(e, f, g) + K256[j] +
             (W256[j&0x0f] += s1 + W256[(j+9)&0x0f] + s0);
        T2 = Sigma0_256(a) + Maj(a, b, c);
        h = g;
        g = f;
        f = e;
        e = d + T1;
        d = c;
        c = b;
        b = a;
        a = T1 + T2;
        j++;
    } while (j < 64);
    context->state[0] += a;
    context->state[1] += b;
    context->state[2] += c;
    context->state[3] += d;
    context->state[4] += e;
    context->state[5] += f;
    context->state[6] += g;
    context->state[7] += h;
    a = b = c = d = e = f = g = h = T1 = T2 = 0;
}
#endif
void SHA256_Update(SHA256_CTX* context, const sha2_byte *data, size_t len) {
    unsigned int    freespace, usedspace;
    if (len == 0) {
        return;
    }
    assert(context != (SHA256_CTX*)0 && data != (sha2_byte*)0);
    usedspace = (context->bitcount >> 3) % SHA256_BLOCK_LENGTH;
    if (usedspace > 0) {
        freespace = SHA256_BLOCK_LENGTH - usedspace;
        if (len >= freespace) {
            MEMCPY_BCOPY(&context->buffer[usedspace], data, freespace);
            context->bitcount += freespace << 3;
            len -= freespace;
            data += freespace;
            SHA256_Transform(context, (sha2_word32*)context->buffer);
        } else {
            MEMCPY_BCOPY(&context->buffer[usedspace], data, len);
            context->bitcount += len << 3;
            usedspace = freespace = 0;
            return;
        }
    }
    while (len >= SHA256_BLOCK_LENGTH) {
        SHA256_Transform(context, (sha2_word32*)data);
        context->bitcount += SHA256_BLOCK_LENGTH << 3;
        len -= SHA256_BLOCK_LENGTH;
        data += SHA256_BLOCK_LENGTH;
    }
    if (len > 0) {
        MEMCPY_BCOPY(context->buffer, data, len);
        context->bitcount += len << 3;
    }
    usedspace = freespace = 0;
}
void SHA256_Final(sha2_byte digest[], SHA256_CTX* context) {
    sha2_word32 *d = (sha2_word32*)digest;
    unsigned int    usedspace;
    assert(context != (SHA256_CTX*)0);
    if (digest != (sha2_byte*)0) {
        usedspace = (context->bitcount >> 3) % SHA256_BLOCK_LENGTH;
#if BYTE_ORDER == LITTLE_ENDIAN
        REVERSE64(context->bitcount,context->bitcount);
#endif
        if (usedspace > 0) {
            context->buffer[usedspace++] = 0x80;
            if (usedspace <= SHA256_SHORT_BLOCK_LENGTH) {
                MEMSET_BZERO(&context->buffer[usedspace], SHA256_SHORT_BLOCK_LENGTH - usedspace);
            } else {
                if (usedspace < SHA256_BLOCK_LENGTH) {
                    MEMSET_BZERO(&context->buffer[usedspace], SHA256_BLOCK_LENGTH - usedspace);
                }
                SHA256_Transform(context, (sha2_word32*)context->buffer);
                MEMSET_BZERO(context->buffer, SHA256_SHORT_BLOCK_LENGTH);
            }
        } else {
            MEMSET_BZERO(context->buffer, SHA256_SHORT_BLOCK_LENGTH);
            *context->buffer = 0x80;
        }
        *(sha2_word64*)&context->buffer[SHA256_SHORT_BLOCK_LENGTH] = context->bitcount;
        SHA256_Transform(context, (sha2_word32*)context->buffer);
#if BYTE_ORDER == LITTLE_ENDIAN
        {
            int j;
            for (j = 0; j < 8; j++) {
                REVERSE32(context->state[j],context->state[j]);
                *d++ = context->state[j];
            }
        }
#else
        MEMCPY_BCOPY(d, context->state, SHA256_DIGEST_LENGTH);
#endif
    }
    MEMSET_BZERO(context, sizeof(context));
    usedspace = 0;
}
