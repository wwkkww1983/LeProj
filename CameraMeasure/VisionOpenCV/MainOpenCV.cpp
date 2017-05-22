#include <iostream>
#include <cv.h>

#include "ImageHandler.h"

using namespace std;

int main()
{
	//////////////////////变量定义
	const int CAMERA_WIDTH = 640, CAMERA_HIGHT = 480, COMPRESS_WIDTH = 400;
	int objXValue = -1, compressHight;
	bool distance;
	ImageHandler imageTool;	
	Mat sourceFrame, compressFrame;	
	//////////////////////变量初始化
	compressHight = 1.0 * CAMERA_HIGHT * COMPRESS_WIDTH / CAMERA_WIDTH;
	VideoCapture capture(0);
	if (!capture.isOpened()) return 0;
	bool stopFlag(false);
	while (!stopFlag)
	{
		//////////////////////图像预处理
		if (!capture.read(sourceFrame))
		{
			capture.open(0);
			cout<<endl<<capture.isOpened()<<"Camera Read Fail;"<<endl;
			if (!capture.isOpened() || !capture.read(sourceFrame)) break;
		}
		resize(sourceFrame,compressFrame,Size(COMPRESS_WIDTH, compressHight));
		imshow("Source Image", compressFrame);
		moveWindow("Source Image",0,0);
		//////////////////////核心算法
		distance = imageTool.RecognitionHumanFace(compressFrame);
		// distance = True 时，触发警报		

		//程序结束开关
		if (waitKey(10) == 27)
		{//监听到ESC退出
			stopFlag = true;
		}
	}
	return 0;
}

