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
	////////////////////ͼ���ȡ
	VideoCapture capture(0);
	if (!capture.isOpened()) return 0;
	if (!capture.read(matSourceFrame))
	{		
		capture.open(0);
		cout<<endl<<capture.isOpened()<<"Camera Read Fail;"<<endl;
		if (!capture.isOpened() || !capture.read(matSourceFrame)) return -1;
	}
	////////////////////�۾����
	cvtColor(matSourceFrame, matTemp, CV_RGB2GRAY);	//�Ҷȴ�����ɫͼ���Ϊ�ڰף�
	equalizeHist( matTemp, matFace );//�Ҷ�ͼ��ֱ��ͼ���⻯����һ��ͼ�����Ⱥ���ǿ�Աȶȣ�
	faceCascade.detectMultiScale(matFace, allFaces, 1.1, 3, 0|CV_HAAR_SCALE_IMAGE, Size(60,60));
	if(allFaces.size() < 1) return -1;
	////////////////////ͫ��ʶ��
	cv::findContours(matThreshold, contours,hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, cv::Point(0, 0));
	int shapeCount = contours.size();
	for(int i=0;i<shapeCount;i++){
	float tmparea =(float)cv::contourArea(contours[i]); //�������������
	cv::RotatedRect rect =cv::minAreaRect(contours[i]);//������㼯��С����ľ���
 
	cv::Rect aRect =cv::boundingRect(contours[i]);//����㼯�������棨up-right�����α߽�
 
	cv::vector<cv::vector<cv::Point>>hull(contours.size());
	convexHull(cv::Mat(contours[i]), hull[j],false, true); //�ܵõ�������͹����
	}
	////////////////////�������
}