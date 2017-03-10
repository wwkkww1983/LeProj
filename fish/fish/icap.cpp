#include "stdafx.h"
#include "icap.h"
#include "utility.h"
#include "malloc.h"

typedef unsigned char UINT8;
typedef unsigned short UINT16;
typedef unsigned int UINT32;

#define ICAP_MAGIC								0x20130809
#define MAGIC_OFFSET							0
#define MAGIC_LENGTH							sizeof(UINT32)
#define COMMAND_OFFSET							MAGIC_LENGTH
#define COMMAND_LENGTH							sizeof(UINT16)
#define CONTENT_LENGTH_OFFSET					(MAGIC_LENGTH + COMMAND_LENGTH)
#define CONTENT_LENGTH_LENGTH					sizeof(UINT32)
#define HEADER_LENGTH							(MAGIC_LENGTH + COMMAND_LENGTH + CONTENT_LENGTH_LENGTH + 2)
#define CONTENT_OFFSET							HEADER_LENGTH

#define ICAP_OK									0
#define ICAP_BAD_AUTH							1
#define ICAP_TOO_MANY_SESSIONS					2
#define ICAP_INTERNAL_ERROR						3
#define ICAP_BAD_PARAM							4
#define ICAP_FORBIDDEN							5
#define ICAP_OPERATION_FAIL						6
#define ICAP_BAD_STATUS							7

#define ICAP_KEY								"hello kitty and kgb/cia 2011 COPYRIGHT@REECAM 5460"

#define ICAP_SPEAK_PACKETS_THRESHOLD			25


int icap_encode(char * data, unsigned int len, const char * key)
{
	blf_ctx * c;
	unsigned long xl=0L, xr=0L;
	unsigned int i;
	
	if (len % 8)
		return 0;
	if (NULL == (c = (blf_ctx *)malloc(sizeof(blf_ctx))))
		return 0;
	
	InitBlowfish(c, (unsigned char*)key, strlen(key));
	for (i = 0;i < len;i += 8)
	{
		memcpy(&xl, data + i, sizeof(UINT32));
		memcpy(&xr, data + i + 4, sizeof(UINT32));
		Blowfish_encipher(c, &xl, &xr);
		memcpy(data + i, &xl, sizeof(UINT32));
		memcpy(data + i + 4, &xr, sizeof(UINT32));
	}
	free(c);
	return 1;
}

int icap_decode(char * data, unsigned int len, const char * key)
{
	blf_ctx * c;
	unsigned long xl, xr;
	unsigned int i;
	
	if (len % 8)
		return 0;
	if (NULL == (c = (blf_ctx *)malloc(sizeof(blf_ctx))))
		return 0;
	
	InitBlowfish(c, (unsigned char*)key, strlen(key));
	for (i = 0;i < len;i += 8)
	{
		memcpy(&xl, data + i, sizeof(UINT32));
		memcpy(&xr, data + i + 4, sizeof(UINT32));
		Blowfish_decipher(c, &xl, &xr);
		memcpy(data + i, &xl, sizeof(UINT32));
		memcpy(data + i + 4, &xr, sizeof(UINT32));
	}
	
	free(c);
	return 1;
}
