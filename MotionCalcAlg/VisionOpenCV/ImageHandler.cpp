#include "ImageHandler.h"
#include "RecordLog.h"
#include <numeric>
#include <fstream>

using namespace std;

#define  FREE_VALID_COUNT		5
#define  EXISTS_VALID_COUNT		5
#define  MAX_VERTICAL_HEIGHT	50
#define  MIN_HORIZON_WIDTH		50
#define  CONSTANT_MULTIPLY		3500

ImageHandler::ImageHandler(void)
{
	InitialVariable();

	ifstream fileParam("Config.ini",ios::in);
	if(!fileParam.is_open()){
		cout<<"CANNOT open config file, Params use DEFAULT."<<endl; 	
		minDistance = 20;
		maxDistance = 80;
		return;
	}
	char tmpChar[256];
	while(!fileParam.eof())
	{
		fileParam.getline(tmpChar,256);
		string tmpString(tmpChar);
		UpdateParams(tmpString);
	}
}

void ImageHandler::InitialVariable()
{
	namedWindow("Control",WINDOW_NORMAL);

	iLowH = 100; 
	iHighH = 140; 
	iLowS = 90; 
	iHighS = 255; 
	iLowV = 90;
	iHighV = 255;

	createTrackbar("LowH", "Control", &iLowH, 179); //Hue (0 - 179) 
	createTrackbar("HighH", "Control", &iHighH, 179); 

	createTrackbar("LowS", "Control", &iLowS, 255); //Saturation (0 - 255) 
	createTrackbar("HighS", "Control", &iHighS, 255); 

	createTrackbar("LowV", "Control", &iLowV, 255); //Value (0 - 255) 
	createTrackbar("HighV", "Control", &iHighV, 255);
}

ImageHandler::~ImageHandler(void)
{
}

//����ʶ��
void ImageHandler::RecognitionColor(Mat sourceFrame){
	cvtColor(sourceFrame, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV
	//��Ϊ���Ƕ�ȡ���ǲ�ɫͼ��ֱ��ͼ���⻯��Ҫ��HSV�ռ��� 
	//split(imgHSV, hsvSplit); 
	//equalizeHist(hsvSplit[2],hsvSplit[2]); 
	//merge(hsvSplit,imgHSV); 
	Mat imgThresholded; 
	inRange(imgHSV, Scalar(iLowH, iLowS, iLowV), Scalar(iHighH, iHighS, iHighV), imgThresholded); //Threshold the image 
	//������ (ȥ��һЩ���) 
	Mat element = getStructuringElement(MORPH_RECT, Size(2, 2)); 
	morphologyEx(imgThresholded, imgThresholded, MORPH_OPEN,element); 
	//�ղ��� (����һЩ��ͨ��) 
	morphologyEx(imgThresholded, imgThresholded, MORPH_CLOSE, element); 
	imshow("Thresholded Image", imgThresholded); //show the thresholded image 
	moveWindow("Thresholded Image",700,0);
}



//ʹ�������ļ����²���ֵ
void ImageHandler::UpdateParams(string keyValue){
	if(keyValue.substr(0,2) == "//") return;//ע��
	size_t pos= keyValue.find('=');
	if(pos == string::npos) return;//û�ҵ��Ⱥ�

	string tmpKey = keyValue.substr(0,pos);
	transform(tmpKey.begin(), tmpKey.end(), tmpKey.begin(), (int(*)(int))toupper);
	if(tmpKey == "MINDISTANCE"){ //��ֵ�˲�֡��		
		minDistance = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MAXDISTANCE"){//��ֵ�˲�֡��
		maxDistance = atoi(keyValue.substr(pos + 1).c_str());
	}
}