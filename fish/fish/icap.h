#include "stdafx.h"

#define ICAP_LOGIN1_REQ							502
#define ICAP_LOGIN1_RESP						510
#define ICAP_LOGIN2_REQ							521
#define ICAP_LOGIN2_RESP						533
#define ICAP_KEEP_ALIVE							542
#define ICAP_GET_PROPERTIES_REQ					550
#define ICAP_GET_PROPERTIES_RESP				561
#define ICAP_GET_PARAMS_REQ						574
#define ICAP_GET_PARAMS_RESP					583
#define ICAP_SET_PARAMS_REQ						590
#define ICAP_SET_PARAMS_RESP					501
#define ICAP_MONITOR_STATUS_REQ					118
#define ICAP_MONITOR_STATUS_RESP				124
#define ICAP_STATUS_CHANGED_NOTIFY				135
#define ICAP_PLAY_VIDEO_REQ						147
#define ICAP_PLAY_VIDEO_RESP					151
#define ICAP_STOP_VIDEO_REQ						164
#define ICAP_VIDEO_DATA							176
#define ICAP_PLAY_AUDIO_REQ						188
#define ICAP_PLAY_AUDIO_RESP					195
#define ICAP_STOP_AUDIO_REQ						202
#define ICAP_AUDIO_DATA							214
#define ICAP_START_TALK_REQ						226
#define ICAP_START_TALK_RESP					234
#define ICAP_STOP_TALK_REQ						247
#define ICAP_TALK_DATA							253
#define ICAP_PTZ_CONTROL_REQ					264
#define ICAP_PTZ_CONTROL_RESP					277
#define	ICAP_SET_VIDEO_PERFORMANCE				284
#define ICAP_CAN_SET_VIDEO_PERFORMANCE_NOTIFY	294
#define ICAP_SERIAL_DATA_SEND_REQ				307
#define ICAP_SERIAL_DATA_SEND_RESP				315
#define ICAP_SERIAL_DATA_RECEIVE_REQ			323
#define ICAP_SERIAL_DATA_RECEIVE_RESP			334
#define ICAP_SERIAL_MONOPOLISM_REQ				345
#define ICAP_SERIAL_MONOPOLISM_RESP				356
#define ICAP_SERIAL_MONOPOLISM_RELEASE			367
#define ICAP_VIDEO_DATA2						374

#define ICAP_PLAY_RECORD_REQ                    419
#define ICAP_PLAY_RECORD_RESP                   424
#define ICAP_PLAY_RECORD_NOTIFY                                        432
#define ICAP_STOP_PLAY_RECORD_REQ                    443
#define ICAP_PAUSE_PLAY_RECORD_REQ                   454
#define ICAP_CONTINUE_PLAY_RECORD_REQ                465


int icap_encode(char * data, unsigned int len, const char * key);
int icap_decode(char * data, unsigned int len, const char * key);