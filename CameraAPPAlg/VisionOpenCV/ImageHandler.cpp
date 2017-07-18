#include "ImageHandler.h"
#include <numeric>
#include <fstream>

using namespace std;

#define  AREAS_MOTION	100
#define  MIN_TARGET_AREAR		500
#define  MAX_TARGET_AREAR		10000 

ImageHandler::ImageHandler(void)
{	
	shapeOperateKernal = getStructuringElement(MORPH_CROSS, Size(3, 3));
}


ImageHandler::~ImageHandler(void)
{
}

void ImageHandler::RecognitionLines(Mat sourceFrame)
{
	//过滤白色图案
	cvtColor(sourceFrame,imgHsv,CV_BGR2HSV);
	inRange(imgHsv,Scalar(0,0,221),Scalar(180,30,255),imgThreshold);

	
		imshow("白色过滤", imgThreshold);
		moveWindow("白色过滤",500,0);

	
	morphologyEx(imgThreshold,imgThreshold,MORPH_OPEN,shapeOperateKernal);	
	morphologyEx(imgThreshold,imgThreshold,MORPH_CLOSE,shapeOperateKernal);
	Canny(imgThreshold,imgThreshold,50,200,3);
	
		imshow("阈值过滤", imgThreshold);
		moveWindow("阈值过滤",0,500);

	//找出线条
	//HoughLinesP(binary,lines,deltaRho,deltaTheta,minVote, minLength, maxGap); 
	double deltaRho=1;	//线条半径分辨率
	double deltaTheta = CV_PI/180;	//角度分辨率
	int minLinePoints = 50;	//直线上最小的点数
    double minLineLen = 50;	//最小线段长度  
    double maxLineGap = 50;	//最大直线间隙 
	HoughLinesP(imgThreshold,lines,deltaRho,deltaTheta,minLinePoints,minLineLen,maxLineGap);
	//绘制线条
	for(size_t i=0;i<lines.size();i++){
		Vec4i point = lines[i];
		line( sourceFrame, Point(point[0],point[1]),Point(point[2],point[3]), Scalar(255));  
	}

		imshow("lines", sourceFrame);
		moveWindow("lines",500,500);
}


//一个Demo图片
void ImageHandler::DemoImage(void){
	//读入图片，注意图片路径
	Mat image=imread("img.jpg");

	//图片读入成功与否判定
	if(!image.data)		return ;

	//显示图像
	imshow("image1",image);	
	moveWindow("人脸识别",800,500);
	
	//等待按键
	waitKey();
}