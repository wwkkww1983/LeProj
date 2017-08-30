#include <iostream>
#include <cv.h>

#include "ImageHandler.h"
#include "MotionCalc.h"

using namespace std;

int main()
{
	ImageHandler imageTool;	
	Mat sourceFrame;
	VideoCapture capture(0);//"D:/2.avi"
	if (!capture.isOpened()) return 0;

	bool stopFlag(false);
	int idx=0;
	while (!stopFlag)
	{
		idx ++;
		if (!capture.read(sourceFrame))
		{
			capture.open(0);
			cout<<endl<<capture.isOpened()<<"Camera Read Fail;"<<endl;
			if (!capture.isOpened() || !capture.read(sourceFrame)) break;
		}

		//��ʾԭʼͼ��
		imshow("Source Image", sourceFrame);
		moveWindow("Source Image",0,0);
		
		imageTool.edgeSobel(sourceFrame);
		imageTool.edgeLaplacian(sourceFrame);
		imageTool.edgeCanny(sourceFrame);

		//�����������
		if (waitKey(10) == 27)
		{//������ESC�˳�
			stopFlag = true;
		}
	}
	return 0;
}

