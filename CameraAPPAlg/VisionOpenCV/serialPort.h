#pragma once
#include	 <iostream>
#include     <stdio.h>      /*��׼�����������*/
#include     <stdlib.h>     /*��׼�����ⶨ��*/
#include     <unistd.h>     /*Unix ��׼��������*/
#include     <sys/types.h>  
#include     <sys/stat.h>   
#include     <fcntl.h>      /*�ļ����ƶ���*/
#include     <termios.h>    /*PPSIX �ն˿��ƶ���*/
#include     <errno.h>      /*����Ŷ���*/
#include	 <string.h>

#define BAUDRATE B115200 //Baud rate : 115200
#define DEVICE "/dev/ttyAMA0"
#define SIZE 1024

class serialPort
{
public:
	serialPort(void);
	~serialPort(void);
	
	//�շ���Ϣ
	void sendMsg(char *);
	void receiveMsg(void);

private:
	int OpenDevice(char *);
	int nFd;
};

