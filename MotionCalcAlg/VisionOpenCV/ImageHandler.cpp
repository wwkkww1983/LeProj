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

//人脸识别
void ImageHandler::RecognitionColor(Mat sourceFrame){
	cvtColor(sourceFrame, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV
	//因为我们读取的是彩色图，直方图均衡化需要在HSV空间做 
	//split(imgHSV, hsvSplit); 
	//equalizeHist(hsvSplit[2],hsvSplit[2]); 
	//merge(hsvSplit,imgHSV); 
	Mat imgThresholded; 
	inRange(imgHSV, Scalar(iLowH, iLowS, iLowV), Scalar(iHighH, iHighS, iHighV), imgThresholded); //Threshold the image 
	//开操作 (去除一些噪点) 
	Mat element = getStructuringElement(MORPH_RECT, Size(2, 2)); 
	morphologyEx(imgThresholded, imgThresholded, MORPH_OPEN,element); 
	//闭操作 (连接一些连通域) 
	morphologyEx(imgThresholded, imgThresholded, MORPH_CLOSE, element); 
	imshow("Thresholded Image", imgThresholded); //show the thresholded image 
	moveWindow("Thresholded Image",700,0);
}



//使用配置文件更新参数值
void ImageHandler::UpdateParams(string keyValue){
	if(keyValue.substr(0,2) == "//") return;//注释
	size_t pos= keyValue.find('=');
	if(pos == string::npos) return;//没找到等号

	string tmpKey = keyValue.substr(0,pos);
	transform(tmpKey.begin(), tmpKey.end(), tmpKey.begin(), (int(*)(int))toupper);
	if(tmpKey == "MINDISTANCE"){ //中值滤波帧数		
		minDistance = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MAXDISTANCE"){//均值滤波帧数
		maxDistance = atoi(keyValue.substr(pos + 1).c_str());
	}
}