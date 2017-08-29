#include "ImageHandler.h"
#include <numeric>
#include <fstream>

using namespace std;

#define  AREAS_MOTION	100
#define  MIN_TARGET_AREAR		500
#define  MAX_TARGET_AREAR		10000 

ImageHandler::ImageHandler(void):FIRST_FRAME_COUNT(10),MIN_SIZE_PIXEL(10),CHANGE_FACE_JUMP_FALG(200), CHANGE_FACE_MIN_COUNT(5)
{
	vmin = 10;
	vmax = 256;
	smin = 30;

	findTargetFlag = false;
	jumpFrameCount=0;
	hsize = 16;
	hranges[0]=0;
	hranges[1]=180;
	phranges = hranges;
	stayCount = 0;
	stayMaxCount = 100;
	ch[0]=0;
	ch[1] =0;
	
	shapeOperateKernal = getStructuringElement(MORPH_RECT, Size(3, 3));

	//ʹ������ʶ��ʱʹ�ã�����ƥ��ģ�ͣ�
	string faceCascadeName = "haarcascade_frontalface_alt.xml";
	if(!faceCascade.load(faceCascadeName)){printf("--(!)Error loading\n");}

	ifstream fileParam("Config.ini",ios::in);
	if(!fileParam.is_open()){
		cout<<"CANNOT open config file, Params use DEFAULT."<<endl; 	
		MIN_RECT_AREA = 200;
		MAX_RECT_AREA = 30000;
		MIN_X_DISTANCE = 30;
		MAX_X_DISTANCE = 200;
		FILTER_MIDDLE_COUNT = 5;
		FILTER_MEAN_COUNT = 3;
		MAX_VISION = 60;
		MIN_POINT_COUNT = 20;
		MIN_POINT_COUNT = 280;
		VALID_INTERVAL = 15;

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


ImageHandler::~ImageHandler(void)
{
}

PointXY ImageHandler::TrackMotionTargetRange(Mat souceFrame,Mat foreground)
{
	PointXY xy = PointXY(0,0);
	Rect targetRect = RecognitionMotionTargetRange(foreground);

	if(targetRect.width < MIN_X_DISTANCE || targetRect.width > MAX_X_DISTANCE || targetRect.height < MIN_X_DISTANCE || targetRect.height > MAX_X_DISTANCE) return xy;
	double xValue = targetRect.x + targetRect.width/2.0;
	double yValue = targetRect.y + targetRect.height/2.0;
	midFiltArray.push_back(xValue);
	sourceFiltArray.push_back(xValue);
	midFiltArrayY.push_back(yValue);
	sourceFiltArrayY.push_back(yValue);
	if(midFiltArray.size() < FILTER_MIDDLE_COUNT || midFiltArrayY.size() < FILTER_MIDDLE_COUNT) return xy;
	sort(midFiltArray.begin(), midFiltArray.end());
	sort(midFiltArrayY.begin(), midFiltArrayY.end());
	meanFiltArray.push_back(midFiltArray[FILTER_MIDDLE_COUNT/2]);
	meanFiltArrayY.push_back(midFiltArrayY[FILTER_MIDDLE_COUNT/2]);
	if(meanFiltArray.size() > FILTER_MEAN_COUNT)
	{
		meanFiltArray.erase(meanFiltArray.begin ());
		meanFiltArrayY.erase(meanFiltArrayY.begin ());
	}
	midFiltArray.erase(std::find(midFiltArray.begin(),midFiltArray.end(),sourceFiltArray[0]));
	sourceFiltArray.erase(sourceFiltArray.begin());
	xy.X = std::accumulate(meanFiltArray.begin(),meanFiltArray.end(),0)/meanFiltArray.size();	
	midFiltArrayY.erase(std::find(midFiltArrayY.begin(),midFiltArrayY.end(),sourceFiltArrayY[0]));
	sourceFiltArrayY.erase(sourceFiltArrayY.begin());
	xy.Y = std::accumulate(meanFiltArrayY.begin(),meanFiltArrayY.end(),0)/meanFiltArrayY.size();

	return xy;
}

int ImageHandler::TrackMotionTarget(Mat souceFrame,Mat foreground)
{
	//����˶�Ŀ��
	RecognitionMotionTarget(foreground);

	if(moveRange.width < MIN_X_DISTANCE || moveRange.width > MAX_X_DISTANCE) return -1;//�˶�����̫С��̫�������
	//��ֵ + ��ֵ ����
	double xValue = moveRange.x + moveRange.width/2.0;
	midFiltArray.push_back(xValue);
	sourceFiltArray.push_back(xValue);
	if(midFiltArray.size() < FILTER_MIDDLE_COUNT) return -1;
	sort(midFiltArray.begin(), midFiltArray.end());
	meanFiltArray.push_back(midFiltArray[FILTER_MIDDLE_COUNT/2]);
	if(meanFiltArray.size() > FILTER_MEAN_COUNT)
	{
		meanFiltArray.erase(meanFiltArray.begin ());
	}
	midFiltArray.erase(std::find(midFiltArray.begin(),midFiltArray.end(),sourceFiltArray[0]));
	sourceFiltArray.erase(sourceFiltArray.begin());
	xValue = std::accumulate(meanFiltArray.begin(),meanFiltArray.end(),0)/meanFiltArray.size();
	return xValue;

	//��֡ ������
	//if(!findTargetFlag){//�״�û�ö�֡ȡ��ֵ
	//	nextTargetRotate = trackBox;
	//	findTargetFlag = true;
	//	return nextTarget.x;
	//}
	//if(abs(trackBox.center.x - nextTargetRotate.center.x) > CHANGE_FACE_JUMP_FALG || abs(trackBox.center.y - nextTargetRotate.center.y) > CHANGE_FACE_JUMP_FALG || 
	//	abs(trackBox.size.width - nextTargetRotate.size.width) > CHANGE_FACE_JUMP_FALG || abs(trackBox.size.height - nextTargetRotate.size.height) > CHANGE_FACE_JUMP_FALG)
	//{//��֡���
	//	jumpFrameCount++;
	//	if(jumpFrameCount >= CHANGE_FACE_MIN_COUNT)//û����֡����ֻ��ȡ�����һ֡
	//		nextTargetRotate = trackBox;
	//}else{
	//	jumpFrameCount = 0;
	//	nextTargetRotate = trackBox;
	//}
	////ellipse(dstImage,nextTargetRotate, Scalar(0,0,255), 3, CV_AA);
	////imshow("Move Obj", dstImage);
	////moveWindow("Move Obj",700,500);
	//return nextTargetRotate.center.x + nextTargetRotate.size.width / 2.0;
}

Rect ImageHandler::RecognitionMotionTargetRange(Mat foreground)
{
	morphologyEx(foreground,tmpImage,MORPH_OPEN,shapeOperateKernal);	
	morphologyEx(tmpImage,srcImage,MORPH_CLOSE,shapeOperateKernal);
	findContours(srcImage, contourAll, hierarchy, RETR_EXTERNAL , CHAIN_APPROX_SIMPLE);
	int shapeCount = contourAll.size(), maxAreaValue=0, maxAreaIdx = -1;
	vector<vector<Point> >contoursAppr(shapeCount);
	vector<Rect> boundRect(shapeCount);
	Rect maxRect = Rect(0,0,0,0);
	for(int i = 0; i < shapeCount; i ++)
	{
		approxPolyDP(Mat(contourAll[i]), contoursAppr[i], 5, true);
		boundRect[i] = boundingRect(Mat(contoursAppr[i]));
		if(boundRect[i].area() < MIN_RECT_AREA || boundRect[i].area() > MAX_RECT_AREA) continue;
		maxRect = boundRect[i].area()>maxRect.area()?boundRect[i]:maxRect;
		drawContours(srcImage,contourAll,i,Scalar(255), CV_FILLED);
	}
	return maxRect;
}


//ȷ���˶�����
void ImageHandler::RecognitionMotionTarget(Mat foreground)
{
	//���ղ���
	morphologyEx(foreground,tmpImage,MORPH_OPEN,shapeOperateKernal);	
	morphologyEx(tmpImage,srcImage,MORPH_CLOSE,shapeOperateKernal);
	//�ҵ���������
	findContours(srcImage, contourAll, hierarchy, RETR_EXTERNAL , CHAIN_APPROX_SIMPLE);
	int shapeCount = contourAll.size(), maxAreaValue=0, maxAreaIdx = -1;
	vector<vector<Point> >contoursAppr(shapeCount);
	vector<Rect> boundRect(shapeCount);
	//�ҵ������ͨ��
	for(int i = 0; i < shapeCount; i ++)
	{//�ҵ���������
		approxPolyDP(Mat(contourAll[i]), contoursAppr[i], 5, true);
		boundRect[i] = boundingRect(Mat(contoursAppr[i]));
		if(boundRect[i].area() < MIN_RECT_AREA || boundRect[i].area() > MAX_RECT_AREA) continue;
		//���ն�
		drawContours(srcImage,contourAll,i,Scalar(255), CV_FILLED);
	}	
	int imageWidth = srcImage.cols, imageHeight = srcImage.rows;
	uchar * imageData = srcImage.data, *tmpRow;
	ushort *moveCount = new ushort[imageWidth];
	for(int i=0;i<imageWidth;i++) 
		moveCount[i] = 0;
	for (int i = 0; i < imageHeight; i++)
	{//ͼ��ѹ����һά��X�᷽��
		tmpRow = srcImage.ptr<uchar>(i);
		for(int j=0;j<imageWidth;j++){
			//127�Ļ�ɫ�����ʾ��������߽磨���ԣ�
			moveCount[j] += ((int)tmpRow[j] == 255?1:0);
		}
	}
	bool validFlag = false;
	int invalidCount=0;
	vector<int> validStartEnd;
	for(int i=0;i<imageWidth;i++)
	{//ͳ����Ч�˶�����
		if(moveCount[i] < MIN_POINT_COUNT || moveCount[i] > MAX_POINT_COUNT)
		{//���˲�����Χ����ǰ������Ч��			
			if(validFlag)
			{
				if(invalidCount < VALID_INTERVAL){//С�����Ч
					invalidCount++;
				}
				else
				{
					validStartEnd.push_back(i - VALID_INTERVAL - 1);//�յ㣬��һ��ʾ��ǰҲ����Ч
					validFlag = false;
					invalidCount = 0;
				}
			}
		}
		else if(!validFlag)
		{//�¿�ʼ��¼
			validStartEnd.push_back(i);//���
			validFlag = true;
			invalidCount = 0;
		}
	}
	if(validStartEnd.size() % 2 != 0)
	{//���һ�α�����Ч
		validStartEnd.push_back(imageWidth);
	}
	delete(moveCount);
	int segmentCount = validStartEnd.size(),tmpLen;
	int maxValue = 0, maxIdx=-1;
	for(int i=0;i<segmentCount;i+=2)
	{//��������
		tmpLen = validStartEnd[i+1] - validStartEnd[i];
		if(tmpLen > maxValue){
			maxIdx = i;
			maxValue = tmpLen;
		}
	}
	if(maxIdx >= 0)
	{//�ҵ���������
		moveRange.x = validStartEnd[maxIdx]; 
		moveRange.y = 0; 
		moveRange.width = validStartEnd[maxIdx + 1] - validStartEnd[maxIdx]; 
		moveRange.height = imageHeight;

		//rectangle(srcImage, Point(moveRange.x, moveRange.y), Point(moveRange.x + moveRange.width, moveRange.y + moveRange.height), Scalar(255,0,0), 2);
		//circle(srcImage, Point(moveRange.x + moveRange.width / 2, moveRange.y + moveRange.height /2 ),7, Scalar(255,0,0),2);
		//imshow("Move", srcImage);
		//moveWindow("Move",500,400);
	}	
}

int findMostSimilarRect(Rect target, vector<Rect> selectList);
void FindTheFirstFace(vector<vector<Rect> > , int , Rect&);

//����ʶ��
int ImageHandler::RecognitionHumanFace(Mat sourceFrame){
	vector<Rect> faces;
	Mat faceGray;
	//�Ҷȴ�����ɫͼ���Ϊ�ڰף�
	cvtColor(sourceFrame, faceGray, CV_RGB2GRAY);
	//�Ҷ�ͼ��ֱ��ͼ���⻯����һ��ͼ�����Ⱥ���ǿ�Աȶȣ�
	equalizeHist( faceGray, faceGray );
	//����ʶ��
	faceCascade.detectMultiScale(faceGray, faces, 1.1, 3, 0|CV_HAAR_SCALE_IMAGE, Size(60,60));
	if(faces.size() < 1) return -1;
	if(!findTargetFlag)
	{//�״�ʶ��
		int faceCollectCount = allFaceLatest.size();
		if(faceCollectCount < FIRST_FRAME_COUNT)
		{//�״βɼ�
			allFaceLatest.push_back(faces);
		}
		if(faceCollectCount >= FIRST_FRAME_COUNT)
		{//�״�ʶ������� -> �����
			FindTheFirstFace(allFaceLatest,MIN_SIZE_PIXEL,nextTarget);
			findTargetFlag = true;
		}
		return -1;
	}
	//�����ϴ��������
	int similarIdx=findMostSimilarRect(nextTarget , faces);
	if(abs(faces[similarIdx].x - nextTarget.x) > CHANGE_FACE_JUMP_FALG || abs(faces[similarIdx].y - nextTarget.y) > CHANGE_FACE_JUMP_FALG || 
		abs(faces[similarIdx].width - nextTarget.width) > CHANGE_FACE_JUMP_FALG || abs(faces[similarIdx].height - nextTarget.height) > CHANGE_FACE_JUMP_FALG)
	{//��֡���
		jumpFrameCount++;
		if(jumpFrameCount >= CHANGE_FACE_MIN_COUNT){
			findTargetFlag = false;
			allFaceLatest.clear();
		}
	}else{
		jumpFrameCount = 0;
		nextTarget = faces[similarIdx];
	}
	//ͼ�����
	rectangle(sourceFrame,nextTarget,Scalar( 255, 0, 255 ),2, 8, 0 );	
	//Point center(nextTarget.x +nextTarget.width/2,nextTarget.y + nextTarget.height/2 );
	//ellipse(sourceFrame,center,Size(nextTarget.width/2,nextTarget.height/2),0,0,360,Scalar( 255, 0, 255 ), 2, 8, 0 );	
	imshow("Hunman Face",sourceFrame);
	moveWindow("Hunman Face",700,0);

	return nextTarget.x +nextTarget.width/2;
}

//�ҵ���ʼ���ĵ�һ����
void FindTheFirstFace(vector<vector<Rect> > allFrameFaces, int maxInter,Rect &faceNearest)
{//�״�ʶ������� -> �����
	char* strRectFormat = "%d_%d_%d_%d", *tmpStrRect=new char[100];
	map<string,int> faceStrCountList;
	map<string,Rect> faceStrRectList;
	vector<Rect> faceList = allFrameFaces[0];
	for(vector<Rect>::iterator istep=faceList.begin();istep != faceList.end();++istep)
	{//�洢��һ֡�е�����
		sprintf(tmpStrRect, strRectFormat,istep->x,istep->y,istep->width,istep->height);
		faceStrCountList[tmpStrRect]=1;
		faceStrRectList[tmpStrRect] = *istep;
	}
	int frameCount = allFrameFaces.size(), faceNum;
	for(int i=1;i<frameCount;i++)
	{//�Ѽ�����λ����������
		faceNum = allFrameFaces[i].size();
		for(int j=0;j<faceNum;j++)
		{
			sprintf(tmpStrRect, strRectFormat,allFrameFaces[i][j].x,allFrameFaces[i][j].y,allFrameFaces[i][j].width,allFrameFaces[i][j].height);
			int nearIdx = findMostSimilarRect(allFrameFaces[i][j],faceList);
			if(abs(allFrameFaces[i][j].x - faceList[nearIdx].x) > maxInter || abs(allFrameFaces[i][j].y - faceList[nearIdx].y) > maxInter|| 
				abs(allFrameFaces[i][j].width - faceList[nearIdx].width) > maxInter|| abs(allFrameFaces[i][j].height - faceList[nearIdx].height) > maxInter)
			{//�·��ֵľ��Σ�����ľ��β���ͬһ����
				faceList.push_back(allFrameFaces[i][j]);
				faceStrCountList[tmpStrRect]=1;
				faceStrRectList[tmpStrRect] = allFrameFaces[i][j];
			}
			else
			{
				faceStrCountList[tmpStrRect] ++;
			}
		}
	}
	int maxRectNum = 0;
	for(map<string,int>::iterator istep = faceStrCountList.begin();istep != faceStrCountList.end();istep++)
	{//������֡ͼ���У�������ľ���λ�ã�ʶ�����ߵ���Ϊ��������
		if(istep->second > maxRectNum)
			maxRectNum = istep->second;
	}
	int maxFaceSize = 0;
	for(map<string,int>::iterator istep = faceStrCountList.begin();istep != faceStrCountList.end();istep++)
	{//������� �е������������һ���������������Ҿ�������ͷ����ģ�
		if(istep->second < maxRectNum) continue;
		if(faceStrRectList[istep->first].height * faceStrRectList[istep->first].width > maxFaceSize)
		{
			faceNearest = faceStrRectList[istep->first];
			maxFaceSize = faceNearest.height * faceNearest.width;
		}		
	}
}

//���������ε�����ֵ
int calcTwoRectSimilar(Rect one, Rect two)
{
	//���Ƽ��㹫ʽ����Сֵ��������� + �����������ƽ��
	return abs(one.width * one.height - two.height*two.width) + 
		   (int)abs(pow(one.x + one.width/2 ,2) + pow(one.y + one.height/2,2) -pow(two.x + two.width/2,2)-pow(two.y+two.height/2,2));
}

//��������Ŀ��
int findMostSimilarRect(Rect target, vector<Rect> selectList)
{
	int faceCount = selectList.size(),similarIdx=0, mostSimilar = 0xFFFFFF, tmpSimilar;
	for(int i=1;i<faceCount;i++)
	{//�ҵ�����������
		tmpSimilar = calcTwoRectSimilar(selectList[i], target);
		if(tmpSimilar < mostSimilar)
		{
			similarIdx = i;
			mostSimilar = tmpSimilar;
		}
	}
	return similarIdx;
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

//ʹ�������ļ����²���ֵ
void ImageHandler::UpdateParams(string keyValue){
	if(keyValue.substr(0,2) == "//") return;//ע��
	size_t pos= keyValue.find('=');
	if(pos == string::npos) return;//û�ҵ��Ⱥ�

	string tmpKey = keyValue.substr(0,pos);
	transform(tmpKey.begin(), tmpKey.end(), tmpKey.begin(), (int(*)(int))toupper);
	if(tmpKey == "MIDFILTERCOUNT"){ //��ֵ�˲�֡��		
		FILTER_MIDDLE_COUNT = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MEANFILTERCOUNT"){//��ֵ�˲�֡��
		FILTER_MEAN_COUNT = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "IGNOREAREAMIN"){ //������� ��Сֵ
		MIN_RECT_AREA = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "IGNOREAREAMAX"){ //������� ���ֵ
		MAX_RECT_AREA = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MAXVISION"){ //����ӽ�
		MAX_VISION = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MINPOINTCOUNT"){ //�����˶���Ч������
		MIN_POINT_COUNT = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "MAXPOINTCOUNT"){ //����˶���Ч������
		MAX_POINT_COUNT = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "VALIDINTERVAL"){ //�����Ч���м��
		VALID_INTERVAL = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "IGNOREXDISTANCEMIN"){ //��С��Ч���곤��
		MIN_X_DISTANCE = atoi(keyValue.substr(pos + 1).c_str());
	}
	else if(tmpKey == "IGNOREXDISTANCEMAX"){ //�����Ч���곤��
		MAX_X_DISTANCE = atoi(keyValue.substr(pos + 1).c_str());
	}
}