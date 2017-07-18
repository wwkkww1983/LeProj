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
	//���˰�ɫͼ��
	cvtColor(sourceFrame,imgHsv,CV_BGR2HSV);
	inRange(imgHsv,Scalar(0,0,221),Scalar(180,30,255),imgThreshold);

	
		imshow("��ɫ����", imgThreshold);
		moveWindow("��ɫ����",500,0);

	
	morphologyEx(imgThreshold,imgThreshold,MORPH_OPEN,shapeOperateKernal);	
	morphologyEx(imgThreshold,imgThreshold,MORPH_CLOSE,shapeOperateKernal);
	Canny(imgThreshold,imgThreshold,50,200,3);
	
		imshow("��ֵ����", imgThreshold);
		moveWindow("��ֵ����",0,500);

	//�ҳ�����
	//HoughLinesP(binary,lines,deltaRho,deltaTheta,minVote, minLength, maxGap); 
	double deltaRho=1;	//�����뾶�ֱ���
	double deltaTheta = CV_PI/180;	//�Ƕȷֱ���
	int minLinePoints = 50;	//ֱ������С�ĵ���
    double minLineLen = 50;	//��С�߶γ���  
    double maxLineGap = 50;	//���ֱ�߼�϶ 
	HoughLinesP(imgThreshold,lines,deltaRho,deltaTheta,minLinePoints,minLineLen,maxLineGap);
	//��������
	for(size_t i=0;i<lines.size();i++){
		Vec4i point = lines[i];
		line( sourceFrame, Point(point[0],point[1]),Point(point[2],point[3]), Scalar(255));  
	}

		imshow("lines", sourceFrame);
		moveWindow("lines",500,500);
}


//һ��DemoͼƬ
void ImageHandler::DemoImage(void){
	//����ͼƬ��ע��ͼƬ·��
	Mat image=imread("img.jpg");

	//ͼƬ����ɹ�����ж�
	if(!image.data)		return ;

	//��ʾͼ��
	imshow("image1",image);	
	moveWindow("����ʶ��",800,500);
	
	//�ȴ�����
	waitKey();
}