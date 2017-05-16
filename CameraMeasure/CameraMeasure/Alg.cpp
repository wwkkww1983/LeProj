#include <iostream>
#include <iostream>
#include <core/core.hpp>
#include <highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/video/video.hpp>
#include <opencv2/objdetect/objdetect.hpp>
#include <opencv.hpp>
#include <cv.h>

using namespace std;
using namespace cv;



int main()
{
	Mat matSourceFrame,matTemp,matThreshold,matFace;
	vector<vector<Point> > contours;
	vector<Vec4i>hierarchy;	
	vector<Rect> allFaces;
	CascadeClassifier faceCascade;

	string faceCascadeName = "haarcascade_frontalface_alt.xml";
	if(!faceCascade.load(faceCascadeName)){printf("--(!)Error loading\n");}
	////////////////////图像读取
	VideoCapture capture(0);
	if (!capture.isOpened()) return 0;
	if (!capture.read(matSourceFrame))
	{		
		capture.open(0);
		cout<<endl<<capture.isOpened()<<"Camera Read Fail;"<<endl;
		if (!capture.isOpened() || !capture.read(matSourceFrame)) return -1;
	}
	////////////////////眼睛检测
	cvtColor(matSourceFrame, matTemp, CV_RGB2GRAY);	//灰度处理（彩色图像变为黑白）
	equalizeHist( matTemp, matFace );//灰度图象直方图均衡化（归一化图像亮度和增强对比度）
	faceCascade.detectMultiScale(matFace, allFaces, 1.1, 3, 0|CV_HAAR_SCALE_IMAGE, Size(60,60));
	if(allFaces.size() < 1) return -1;
	////////////////////瞳孔识别
	cv::findContours(matThreshold, contours,hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, cv::Point(0, 0));
	int shapeCount = contours.size();
	for(int i=0;i<shapeCount;i++){
	float tmparea =(float)cv::contourArea(contours[i]); //计算轮廓的面积
	cv::RotatedRect rect =cv::minAreaRect(contours[i]);//求包含点集最小面积的矩形
 
	cv::Rect aRect =cv::boundingRect(contours[i]);//计算点集的最外面（up-right）矩形边界
 
	cv::vector<cv::vector<cv::Point>>hull(contours.size());
	convexHull(cv::Mat(contours[i]), hull[j],false, true); //能得到轮廓的凸包。
	}
	////////////////////距离计算
}