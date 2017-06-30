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


class ImageHandler
{
public:
	ImageHandler(void);
	~ImageHandler(void);
	void RecognitionColor(Mat);
	void InitialVariable();

private:

	int minDistance, maxDistance;

	int iLowH, iHighH , iLowS ,iHighS ,iLowV , iHighV;
	Mat imgHSV; 
	vector<Mat> hsvSplit;

	
	vector<vector<Point> > contourAll;
	vector<Vec4i>hierarchy;	

	void UpdateParams(string keyValue);
};

