#include <iostream>
#include <cv.h>

#include "ImageHandler.h"

using namespace std;

int main()
{
	//////////////////////��������
	const int CAMERA_WIDTH = 640, CAMERA_HIGHT = 480, COMPRESS_WIDTH = 400;
	int objXValue = -1, compressHight, angleMin,angleMax, distance;
	ImageHandler imageTool;	
	Mat sourceFrame, compressFrame;	
	//////////////////////������ʼ��
	compressHight = 1.0 * CAMERA_HIGHT * COMPRESS_WIDTH / CAMERA_WIDTH;
	//VideoCapture capture(0);
	VideoCapture capture("20170629155101_A.avi");
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
		//resize(sourceFrame,compressFrame,Size(COMPRESS_WIDTH, compressHight));
		imshow("Source Image", sourceFrame);
		moveWindow("Source Image",0,0);
		//////////////////////�����㷨
		
		imageTool.RecognitionColor(sourceFrame);
		

		//�����������
		if (waitKey(10) == 27)
		{//������ESC�˳�
			stopFlag = true;
		}
	}
	return 0;
}

