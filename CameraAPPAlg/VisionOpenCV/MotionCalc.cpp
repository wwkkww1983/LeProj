#include "MotionCalc.h"
#include <iostream>
#include <math.h>
#include <stdlib.h>
#include <highgui/highgui.hpp>



using namespace std;
#define M_PI       3.14159265358979323846
#define MOVE_MAX_SPEED 0.05	//3��
#define MOVE_NORMAL_SPEED 0.02	//1��
#define MOVE_EACH_TIME	10		//ÿ���ƶ�ʱ�䣨��������
#define	SPEED_THRESHOLD_ANGLE 0.7	//40��
#define VISION_SECTION_COUNT	5	//�ӽ� ������ ����
#define ONE_SIDE_SECTION_BOUND	0.1	//�߽绺������ ռ��
#define	DEMO_RESULT_RADIUS		500 // ָ�� �뾶

MotionCalc::MotionCalc(int imageWidth,int maxVision):MAX_VISION_ANGLE(maxVision)
{
	videoImageWidth = imageWidth;
	verticalDistance = (imageWidth / 2.0) / tan(MAX_VISION_ANGLE / 2 * M_PI / 180);
	sectionAngle = MAX_VISION_ANGLE / VISION_SECTION_COUNT * M_PI / 180;
	sectionBoundAngle = sectionAngle * ONE_SIDE_SECTION_BOUND;
	visionMaxRadian = MAX_VISION_ANGLE / 2.0 / 180 * M_PI;

	currentAngle = 0;
	angleChar[1] = '\0';//�ֶ���ӽ�������������Զ���0x02��Ϊ������
	camPosDemoResult = Point(DEMO_RESULT_RADIUS/2,0);
	objPosDemoResult = Point(DEMO_RESULT_RADIUS/2,DEMO_RESULT_RADIUS/2);
	colorDemoResult = Scalar(255,255,255);
}


MotionCalc::~MotionCalc(void)
{
}


double MotionCalc::CalcAngleByLocation(int xValue)
{
	//����������߾���
	double horiLen = xValue - videoImageWidth / 2.0;
	//ͨ�������м��㻡��
	double angle = atan2(fabs(horiLen),verticalDistance);
	//�Ƕȵķ�������ͷ�ԳƳ�������������������
	angle *= horiLen>0?-1:1;
	//ת��Ϊ�Ƕ�
	return angle;
}

double  MotionCalc::CalcAngleNextStep()
{//ֱ������Ŀ��λ��
	double tmpDiff = fabs(targetAngle - currentAngle);
	int tmpFlag = targetAngle > currentAngle ? 1 : -1;

	if(tmpDiff > MOVE_NORMAL_SPEED)
	{
		if(tmpDiff < SPEED_THRESHOLD_ANGLE)
			currentAngle += MOVE_NORMAL_SPEED * tmpFlag;
		else
			currentAngle += MOVE_MAX_SPEED * tmpFlag;
	}
	return currentAngle;
}

//���㲢��ʾ���ս��
void  MotionCalc::CalcAngleNextStepBySection(int xValue)
{
	//����ת��
	targetAngle = CalcAngleByLocation(xValue);
	//�����ٶ�
	int currentSection = currentAngle / sectionAngle, targetSection = targetAngle / sectionAngle;
	int sectionDiff = targetSection - currentSection;
	int sectionCount = abs(sectionDiff);
	double overLen = currentAngle - sectionAngle * currentSection;
	double tmpValue = targetAngle - max(currentSection, targetSection) * sectionAngle;
	if(sectionCount > 1 || sectionCount == 1 && abs(tmpValue) > sectionBoundAngle)
		moveSpeed = sectionCount * sectionAngle / MOVE_EACH_TIME * (sectionDiff>0?1:-1);
	else if(currentSection == 0 && targetSection ==0 && currentAngle * targetAngle < 0 && abs(targetAngle) > sectionBoundAngle)
		moveSpeed = 1 * sectionAngle / MOVE_EACH_TIME * (targetAngle>0?1:-1);
	else
		moveSpeed = 0;

	while(((currentAngle - targetAngle) * moveSpeed) < 0){//����Ŀ�ĵ�
		if(abs(currentAngle + moveSpeed) >= visionMaxRadian) break;//��ȫ����
		currentAngle += moveSpeed;
		//�����豸
		angleChar[0] = int(currentAngle * 180 * 2 / M_PI)+90;//+90תΪ����
		//serial.sendMsg(angleChar);
		////����ָ��ģ��ͷ��
		//Mat demoResultInfo = Mat::zeros(DEMO_RESULT_RADIUS/2,DEMO_RESULT_RADIUS,CV_8UC1);
		////��ע����ͷλ��
		//circle(demoResultInfo,camPosDemoResult,6,colorDemoResult,5);
		////���㵱ǰ�Ƕ�
		//objPosDemoResult.x = DEMO_RESULT_RADIUS / 2.0 * (1.0 - sin(currentAngle));
		//objPosDemoResult.y = DEMO_RESULT_RADIUS / 2.0 * cos(currentAngle);
		//line(demoResultInfo, camPosDemoResult, objPosDemoResult,colorDemoResult,3);
		//if(xValue > 0)
		//{//-1Ϊû��׽���˶�����
		//	circle(demoResultInfo,Point(DEMO_RESULT_RADIUS - xValue * 1.2,0), 7,colorDemoResult,2);
		//}
		//imshow("Result", demoResultInfo);
		//moveWindow("Result",500,0);
		waitKey(1000/MOVE_EACH_TIME); //����������		
	}
}

void MotionCalc::MoveOrigin()
{
	currentAngle = 0;
}
	