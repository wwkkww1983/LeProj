#include <iostream>
#include <cv.h>

#include "ImageHandler.h"

using namespace std;

int main()
{
	
	//////////////////////��������
	const int CAMERA_WIDTH = 640, CAMERA_HIGHT = 480, COMPRESS_WIDTH = 400;
	int objXValue = -1, compressHight;
	bool distance,setFlag = false;
	ImageHandler imageTool;	
	Mat sourceFrame, compressFrame;	
	//////////////////////������ʼ��
	compressHight = 1.0 * CAMERA_HIGHT * COMPRESS_WIDTH / CAMERA_WIDTH;
	VideoCapture capture(0);
	if (!capture.isOpened()) return 0;	
	
	bool stopFlag(false);
	while (!stopFlag)
	{
		//////////////////////ͼ��Ԥ����
		if (!capture.read(sourceFrame))
		{
			capture.open(0);
			cout<<endl<<capture.isOpened()<<"Camera Read Fail;"<<endl;
			if (!capture.isOpened() || !capture.read(sourceFrame)) break;
		}
		resize(sourceFrame,compressFrame,Size(COMPRESS_WIDTH, compressHight));
		imshow("Source Image", sourceFrame);
		moveWindow("Source Image",0,0);
		//////////////////////�����㷨
		distance = imageTool.RecognitionHumanFace(compressFrame);
		// distance = True ʱ����������		

		int userKey = waitKey(10);
		if(userKey == 27)
		{//ESC�������˳�
			stopFlag = true;
		}else if(userKey == 13)
		{//Enter���û�����
			while(true)
			{
			cout<<"\r\n���ø�ʽ���س��ύ�������=ֵ��";
			cout<<"\r\n�˳����ã�0";
			cout<<"\r\n��С���루���ף���ǰ"<<imageTool.minDistance<<"����1";
			cout<<"\r\n�����루���ף���ǰ"<<imageTool.maxDistance<<"����2";
			cout<<"\r\n�������루���ף���ǰ"<<imageTool.warningDistance<<"����3";
			cout<<"\r\n�������(���ף���ǰ"<<imageTool.cameraFocus<<"����4\r\n";
			char str[100];
			cin>>str;

			int intKey = str[0]-'0';
			if(intKey == 0) break;
			char* strValue = strchr(str,'=');	
			strValue = strValue++;
			if(strValue){
				int intValue=atoi(strValue);
				switch(intKey){
				case 1:imageTool.minDistance=intValue;break;
				case 2:imageTool.maxDistance=intValue;break;
				case 3:imageTool.warningDistance=intValue;break;
				case 4:imageTool.cameraFocus=intValue;break;
				default:break;
				}
			}
			}
		}
	}
	return 0;
}

