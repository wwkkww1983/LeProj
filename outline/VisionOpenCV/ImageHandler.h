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
	
	PointXY TrackMotionTargetRange(Mat,Mat);
	int TrackMotionTarget(Mat,Mat);
	//ʶ���˶�����
	void RecognitionMotionTarget(Mat);
	Rect RecognitionMotionTargetRange(Mat);
	//����ʶ��
	int RecognitionHumanFace(Mat);

	//ͼ�����
	void DemoImage(void);

	enum enumReconStatus
	{
		SET_TARGET = 0,
		TARGET_CAMSHIFT
	};
	Mat foreground;
	Rect selection, moveRange, moveRangeAlg;
	int MAX_VISION;

private:
	//���²���
	void UpdateParams(string);

	bool findTargetFlag;
	//�״�ʶ��������Ҫ��Ƶͼ��֡����ͬһ�������ľ�����Χ���ı�Ŀ����Ϊ��֡����ֵ�����¶�λǰ����������֡����
	const int FIRST_FRAME_COUNT, MIN_SIZE_PIXEL, CHANGE_FACE_JUMP_FALG, CHANGE_FACE_MIN_COUNT;
	//ʶ���������Ч�����Χ, ��Ч���곤�ȣ�ѹ��Ϊ������ʱ��
	int MIN_RECT_AREA, MAX_RECT_AREA, MIN_X_DISTANCE, MAX_X_DISTANCE;
	//��ֵ�˲�������, ��ֵ�˲�������
	int FILTER_MIDDLE_COUNT, FILTER_MEAN_COUNT;
	//��С�˶���Ч�������������Ч�˶��������������Ч���пռ�
	int MIN_POINT_COUNT, MAX_POINT_COUNT, VALID_INTERVAL;
	//��֡ͳ��
	int jumpFrameCount;
	int hsize, moveFrameCount;
	//���������涨ʱ��
	int stayCount, stayMaxCount ;
	//hsvת�����ݷ�Χ�˲�
	int vmin , vmax , smin ;
	float hranges[2] ;
	const float* phranges;
	int ch[2] ;
	vector<int> midFiltArray,meanFiltArray, sourceFiltArray;
	vector<int> midFiltArrayY,meanFiltArrayY, sourceFiltArrayY;
	//��ǰ׷�ٵ�Ŀ�꣬��һ���ƶ�Ŀ���
	Rect nextTarget;
	RotatedRect nextTargetRotate, trackBox;
	//�м����
	Mat hsv, hue, mask, hist, backproj,srcImage,tmpImage, shapeOperateKernal;
	//�ɱ�ռ�����
	vector<vector<Point> > contourAll;
	vector<Vec4i>hierarchy;	
	//����ʶ��
	CascadeClassifier faceCascade;
	vector<vector<Rect> > allFaceLatest;
};

