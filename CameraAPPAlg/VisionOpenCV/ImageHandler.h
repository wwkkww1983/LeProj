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

	//图像测试
	void DemoImage(void);

	Mat foreground;
	Rect selection, moveRange, moveRangeAlg;
	int MAX_VISION;

private:
	Mat kernel, imgHsv, imgThreshold,tmpImage, shapeOperateKernal;
	//可变空间数组
	vector<vector<Point> > contourAll;
	vector<Vec4i>hierarchy, lines;	
};

