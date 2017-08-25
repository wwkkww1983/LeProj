#include <iostream>
#include <cv.h>

#include "ImageHandler.h"

using namespace std;

int main()
{
	
	//////////////////////变量定义
	const int CAMERA_WIDTH = 640, CAMERA_HIGHT = 480, COMPRESS_WIDTH = 400;
	int objXValue = -1, compressHight;
	bool distance,setFlag = false;
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
		imshow("Source Image", sourceFrame);
		moveWindow("Source Image",0,0);
		//////////////////////核心算法
		distance = imageTool.RecognitionHumanFace(compressFrame);
		// distance = True 时，触发警报		

		int userKey = waitKey(10);
		if(userKey == 27)
		{//ESC，程序退出
			stopFlag = true;
		}else if(userKey == 13)
		{//Enter，用户输入
			while(true)
			{
			cout<<"\r\n设置格式，回车提交：（序号=值）";
			cout<<"\r\n退出设置：0";
			cout<<"\r\n最小距离（厘米，当前"<<imageTool.minDistance<<"）：1";
			cout<<"\r\n最大距离（厘米，当前"<<imageTool.maxDistance<<"）：2";
			cout<<"\r\n报警距离（厘米，当前"<<imageTool.warningDistance<<"）：3";
			cout<<"\r\n相机焦距(毫米，当前"<<imageTool.cameraFocus<<"）：4\r\n";
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

