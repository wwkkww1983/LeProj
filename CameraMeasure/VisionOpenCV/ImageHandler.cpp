#include <Windows.h>
#include "ImageHandler.h"
#include "RecordLog.h"
#include <numeric>
#include <fstream>
#include <mmsystem.h>
#pragma comment(lib,"winmm.lib")

using namespace std;

#define  FREE_VALID_COUNT		5
#define  EXISTS_VALID_COUNT		5
#define  MAX_VERTICAL_HEIGHT	50
#define  MIN_HORIZON_WIDTH		50
#define  CONSTANT_MULTIPLY		3500

ImageHandler::ImageHandler(void)
{
	hasAlarmFlag = false;
	isExists = false;
	existCount=0;
	freeCount=0;	
	lengthSum=0;
	lengthCount=0;

	//使用人脸识别时使用（加载匹配模型）
	string faceCascadeName = "haarcascade_mcs_righteye.xml";
	if(!faceCascade.load(faceCascadeName)){printf("--(!)Error loading\n");}

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

//过滤无效瞳孔
bool FilterInvalidEyes(vector<Rect> faces,vector<Rect> &result)
{
	bool flag = false;
	int iCount = faces.size();
	if(iCount < 2) return flag;

	Rect one,other;
	int minDiff = 10000,tempDiff=0;
	for(vector<Rect>::const_iterator r=faces.begin();r!=faces.end();r++)
	{
		for(vector<Rect>::const_iterator t=faces.begin();t!=faces.end();t++)
		{
			if(r == t) continue;
			tempDiff = abs(r->y - t->y);
			if(tempDiff < minDiff && tempDiff <= MAX_VERTICAL_HEIGHT && abs(r->x - t->x) >= MIN_HORIZON_WIDTH)
			{
				flag = true;
				one = *r;
				other = *t;
				minDiff = tempDiff;
			}
		}
	}

	if(flag)
	{
		result.clear();
		result.push_back(one);
		result.push_back(other);
//		printf("上下：%d ，左右：%d \n",abs(one.y - other.y),abs(one.x - other.x));
	}

	return flag;
}

//距离计算
int ImageHandler::CalculateDistance(vector<Rect> eyes)
{
	int cameraDistance = abs(eyes[0].x - eyes[1].x);
	int distance = cvRound(CONSTANT_MULTIPLY / cameraDistance);

	lengthSum += distance;
	lengthCount ++;
	printf("距离：%d \n",distance);
	return distance;
}

ImageHandler::~ImageHandler(void)
{
}

//人脸识别
bool ImageHandler::RecognitionHumanFace(Mat sourceFrame){
	vector<Rect> faces;
	Mat faceGray;
	//灰度处理（彩色图像变为黑白）
	cvtColor(sourceFrame, faceGray, CV_RGB2GRAY);
	//灰度图象直方图均衡化（归一化图像亮度和增强对比度）
	equalizeHist( faceGray, faceGray );
	//瞳孔识别
	faceCascade.detectMultiScale(faceGray, faces, 1.1, 3, 0|CV_HAAR_SCALE_IMAGE, Size(30,30),Size(300,300));
	
	vector<Rect> eyes(2);
	bool invalidFlag = FilterInvalidEyes(faces,eyes);
	if(!invalidFlag)
	{
		if(freeCount >= FREE_VALID_COUNT)
		{
			if(isExists)
			{
				timeDetect =double((getTickCount()-timeDetect)/getTickFrequency());
				int length = lengthSum/lengthCount;
				bool validFlag = length > minDistance && length < maxDistance;
				RecordLog::Inst()->Log(validFlag,to_string(length) + "（厘米）,持续时间：" + to_string(timeDetect) + "（秒）");
				freeTimeDetect = (double)getTickCount();
				hasAlarmFlag = false;
			}
			isExists = false;
			existCount = 0;
		}
		freeCount++;
		if(freeCount >= 35500) freeCount = FREE_VALID_COUNT;
		return -1;
	}
	
	if(existCount >= EXISTS_VALID_COUNT)
	{
		if(!isExists)
		{ 
			lengthSum=0;
			lengthCount=0;
			timeDetect = (double)getTickCount();
			RecordLog::Inst()->Log(false, "未检测到人,持续时间：" + to_string(double((timeDetect-freeTimeDetect)/getTickFrequency())) + "（秒）");
		}
		isExists = true;
		freeCount=0;
		if(lengthCount > 0)
		{
			int length = lengthSum/lengthCount;
			bool validFlag = length > minDistance && length < maxDistance;
			if(validFlag && !hasAlarmFlag)
			{
				PlaySound("warning.wav",NULL, SND_FILENAME|SND_ASYNC );
				hasAlarmFlag = true;
			}
		}
	}
	existCount++;
	

	for(vector<Rect>::const_iterator r=eyes.begin();r!=eyes.end();r++)
	{
		Point center(cvRound(r->x+r->width*0.5),cvRound(r->y+r->height*0.5));
		circle(sourceFrame,center,3,Scalar( 255, 0, 255 ),3);
	}

	int distance = CalculateDistance(eyes);


	imshow("瞳孔",sourceFrame);
	moveWindow("瞳孔",700,0);
	bool needWarning = false;
	if(isExists && double((timeDetect-freeTimeDetect)/getTickFrequency()) > 5)
		needWarning = true;
	return needWarning;
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