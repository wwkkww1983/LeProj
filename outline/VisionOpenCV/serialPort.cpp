#include "serialPort.h"
#include <iostream>

using namespace std;


//���ô�����������İ������������ã�У��λ��ֹͣλ���á����ڵ�������
//Ҫ������struct termios�ṹ��ĸ���Աֵ
//struct termio
//{	unsigned short  c_iflag;	/* ����ģʽ��־ */	
//	unsigned short  c_oflag;		/* ���ģʽ��־ */	
//	unsigned short  c_cflag;		/* ����ģʽ��־*/	
//	unsigned short  c_lflag;		/* local mode flags */	
//	unsigned char  c_line;		    /* line discipline */	
//	unsigned char  c_cc[NCC];    /* control characters */
//};

serialPort::serialPort(void)
{
	//�򿪴���
	nFd = OpenDevice(DEVICE);
	if(-1 == nFd)	return;
	//���ô��ڲ���
	struct termios stOption;
	if(tcgetattr(nFd,&stOption) != 0) {        
		perror("tcgetattr error\n");  
		return;     
	}
	//�����ʣ�115200
	cfsetispeed(&stOption, BAUDRATE);//115200
	cfsetospeed(&stOption, BAUDRATE);
	//����λ����8λ  
	stOption.c_cflag |= (CLOCAL|CREAD); //�������Ӻͽ���ʹ��
	stOption.c_cflag &= ~CSIZE;
	stOption.c_cflag |= CS8;
	//У��λ����
	stOption.c_cflag &= ~PARENB;   //Clear parity enable
	stOption.c_iflag &= ~INPCK;    // Enable parity checking
	//ֹͣλ��1
	stOption.c_cflag &= ~CSTOPB;
	//�ȴ�ʱ��:1��
	stOption.c_cc[VTIME]=10;	//ָ����ȡ��һ���ַ��ĵȴ�ʱ�䣬ʱ��ĵ�λΪn*100ms
	//��Ҫ��ȡ�ַ�����С����	//�������VTIME=0�������ַ�����ʱread�������������ڵ�����
	stOption.c_cc[VMIN]=1;	
	//����δ�����ַ�
	tcflush(nFd,TCIFLUSH);	//����ն�δ��ɵ�����/�����������
	//����������
	if( tcsetattr(nFd,TCSANOW,&stOption) != 0 )
	{
		printf("tcsetattr Error!\n");
	}
}


serialPort::~serialPort(void)
{
}

int serialPort::OpenDevice(char *Dev)
{
	//	 Open�����г���ͨ�����⣬������������O_NOCTTY��O_NDELAY��
	//  O_NOCTTY: ֪ͨlinixϵͳ��������򲻻��Ϊ����˿ڵĿ����նˡ�
	//  O_NDELAY: ֪ͨlinuxϵͳ������DCD�ź���������״̬���˿ڵ���һ���Ƿ񼤻����ֹͣ����
	int	fd = open( Dev, O_RDWR|O_NOCTTY|O_NDELAY ); 
	if (-1 == fd)	
	{ 			
		cout << "Can't Open Serial Port" <<endl;
		return -1;		
	}	
	//�ָ����ڵ�״̬Ϊ����״̬
	if( (fcntl(fd, F_SETFL, 0)) < 0 )
	{
		cout << "Fcntl F_SETFL Error!\n" <<endl;
		return -1;
	}
	////���Դ򿪵��ļ��������Ƿ�����һ���ն��豸���Խ�һ��ȷ�ϴ����Ƿ���ȷ��
	////��Ϊ�ն��豸�򷵻�1���棩�����򷵻�0���٣�
	//if(isatty(STDIN_FILENO)==0)
	//{
	//	cout << "standard input is not a terminal device\n" <<endl;
	//	return -1;
	//}

	return fd;
}

void serialPort::sendMsg(char *msg){
	int nLen = strlen(msg);	
	int nCount = write(nFd,msg,nLen);
//	printf("\nSend %d of %d  => %s\n", nCount, nLen, msg);
}

void serialPort::receiveMsg(void){
	int nread;
	char * msg = new char[512];
	while(true)
	{
		while((nread = read(nFd,msg,512)) > 0)
		{
			printf("\nRecieve %d\n", nread);
			msg[nread+1] = '\0';
			printf("\n%s",msg);
		}
	}
}