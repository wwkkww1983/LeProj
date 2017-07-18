#pragma once
#include <iostream>
#include <core/core.hpp>
#include <highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/video/video.hpp>
#include <opencv2/objdetect/objdetect.hpp>
#include <opencv.hpp>
#include <cv.h>

using namespace cv;

struct PointXY
{
	int X;
	int Y;
	PointXY(int x,int y):X(x),Y(y){};
};

class ImageHandler
{
public:

	ImageHandler(void);
	~ImageHandler(void);
	

	void RecognitionLines(Mat);

	//ͼ�����
	void DemoImage(void);

	Mat foreground;
	Rect selection, moveRange, moveRangeAlg;
	int MAX_VISION;

private:
	Mat kernel, imgHsv, imgThreshold,tmpImage, shapeOperateKernal;
	//�ɱ�ռ�����
	vector<vector<Point> > contourAll;
	vector<Vec4i>hierarchy, lines;	
};

