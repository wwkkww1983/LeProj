#pragma once
#include <cv.h>
//#include "serialPort.h"


using namespace cv;

class MotionCalc
{
public:
	MotionCalc(int,int);
	~MotionCalc(void);
	//���ݺ�����ֵ�����Ӧ�Ƕ�
	double CalcAngleByLocation(int xValue);
	//��Ŀ���ƶ�һ��ʱ�䵥λ
	double CalcAngleNextStep();
	//���ݷ���ģʽ����Ŀ���ƶ�һ��ʱ�䵥λ
	void CalcAngleNextStepBySection(int);	
	//����
	void MoveOrigin();
	//��Ұ��Χ�Ƕȣ��ٶȸı�ĽǶ���ֵ
	const int MAX_VISION_ANGLE;

private:
	//���²���
	void UpdateParams(string);

	//ͼ���ܿ��
	int videoImageWidth;
	//����ͷ��ͼ���������룬���ڼ���Ŀ��Ƕ�
	double verticalDistance;
	//�ƶ��ٶ�
	double moveSpeed, visionMaxRadian;
	//��ǰ��Ŀ��Ƕȣ�ʵʱ��
	double currentAngle, targetAngle;
	//��������� �Ƕ�
	double sectionAngle, sectionBoundAngle;
	
	Scalar colorDemoResult;
	Point camPosDemoResult, objPosDemoResult;
	//�򴮿ڷ�����Ϣ
//	serialPort serial;
	char angleChar[2];
};

