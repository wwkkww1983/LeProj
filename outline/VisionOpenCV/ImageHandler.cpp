#include "ImageHandler.h"
#include <numeric>
#include <fstream>

using namespace std;

#define  AREAS_MOTION	100
#define  MIN_TARGET_AREAR		500
#define  MAX_TARGET_AREAR		10000 

ImageHandler::ImageHandler(void)
{
}


ImageHandler::~ImageHandler(void)
{
}

void ImageHandler::edgeSobel(Mat sourceFrame){
	Mat grayImage, resultImage;
	cvtColor(sourceFrame, grayImage, CV_RGB2GRAY);     //תΪ�Ҷ�ͼ
    Mat ax, ay;        
    Mat axx, ayy;
    Sobel(grayImage, ax, CV_16S, 1, 0,-1);       
    Sobel(grayImage, ay, CV_16S, 0, 1,-1);
    convertScaleAbs(ax, axx);      //��CV_16SתΪCV_8U
    convertScaleAbs(ay, ayy);
    addWeighted(axx, 0.5, ayy, 0.5, 0,resultImage);     //����ͼ���
    imshow("Sobel", resultImage);	
	moveWindow("Sobel",0,500);
}

void ImageHandler::edgeLaplacian(Mat sourceFrame){
	Mat grayImage, resultImage;
	cvtColor(sourceFrame, grayImage, CV_RGB2GRAY);     //תΪ�Ҷ�ͼ
    Laplacian(grayImage, resultImage, CV_16S,3);
    convertScaleAbs(resultImage, resultImage);     //��CV_16SתΪCV_8U
    imshow("Laplacian", resultImage);	
	moveWindow("Laplacian",600,500);
}

void ImageHandler::edgeCanny(Mat sourceFrame){
	Mat grayImage, resultImage;
	cvtColor(sourceFrame, grayImage, CV_RGB2GRAY);     //תΪ�Ҷ�ͼ
    Canny(grayImage, resultImage, 100, 300, 3);
    imshow("Canny", resultImage);
	moveWindow("Canny",1200,500);
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