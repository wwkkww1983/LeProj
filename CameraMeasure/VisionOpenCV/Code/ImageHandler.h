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
	bool RecognitionHumanFace(Mat);

private:

	bool isExists, tempExistsFlag, hasAlarmFlag;
	int existCount,freeCount;
	int lengthSum, lengthCount;
	int minDistance, maxDistance,warningDistance;
	double timeDetect,freeTimeDetect;
	CascadeClassifier faceCascade;
	int CalculateDistance(vector<Rect>);
	void UpdateParams(string keyValue);
};

