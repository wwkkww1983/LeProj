#ifndef _UTILITY_H_
#define _UTILITY_H_

#include "stdafx.h"
#include <string.h>
#include "malloc.h"


#ifdef MIN
#undef MIN
#endif
#define MIN(x,y) (((x)<(y))?(x):(y))

#ifdef MAX
#undef MAX
#endif
#define MAX(x,y) (((x)<(y))?(y):(x))

static __inline unsigned char * mem_new(int value, unsigned int len)
{
	unsigned char * p = (unsigned char *)malloc(len);
	if (! p)
		return NULL;
	memset(p, value, len);
	return p;
}

static __inline unsigned char * mem_assign(const unsigned char * value, unsigned int len)
{
	unsigned char * p = (unsigned char *)malloc(len);
	if (! p)
		return NULL;
	memcpy(p, value, len);
	return p;
}

static __inline unsigned char * mem_combine(const unsigned char * src1, unsigned int src1_len, const unsigned char * src2, unsigned int src2_len)
{
	unsigned char * p = (unsigned char *)malloc(src1_len + src2_len);
	if (! p)
		return NULL;
	memcpy(p, src1, src1_len);
	memcpy(p + src1_len, src2, src2_len);
	return p;
}

static __inline unsigned char * mem_append(unsigned char * src, unsigned int src_len, const unsigned char * append, unsigned int append_len)
{
	unsigned char * p = mem_combine(src, src_len, append, append_len);
	free(src);
	return p;
}

static __inline char * str_assign(const char * value)
{
	return (char *)mem_assign((unsigned char *)value, strlen(value) + 1);
}

static __inline char * str_append(char * src, const char * append)
{
	return (char *)mem_append((unsigned char *)src, strlen(src), (const unsigned char *)append, strlen(append) + 1);
}

static __inline char * str_combine(const char * src1, const char * src2)
{
	return (char *)mem_combine((const unsigned char *)src1, strlen(src1), (const unsigned char *)src2, strlen(src2) + 1);
}

typedef struct {
	unsigned long S[4][256],P[18];
} blf_ctx;

void Blowfish_encipher(blf_ctx *c,unsigned long *xl,unsigned long *xr);

void Blowfish_decipher(blf_ctx *c,unsigned long * xl, unsigned long *xr);

void InitBlowfish (blf_ctx *c,unsigned char* key,unsigned int key_len);


#ifdef ANDROID
#include <android/log.h>
#define LOG_TAG "rc_sdk"
//#define LOGD(...) __android_log_print(ANDROID_LOG_DEBUG, LOG_TAG, __VA_ARGS__)
//#define LOGI(...) __android_log_print(ANDROID_LOG_INFO, LOG_TAG, __VA_ARGS__)
#define LOGE(...) //__android_log_print(ANDROID_LOG_ERROR, LOG_TAG, __VA_ARGS__)  
#else
//#define LOGD printf
//#define LOGI printf
#define LOGE printf
#endif

#endif
