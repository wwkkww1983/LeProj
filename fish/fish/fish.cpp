// fish.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "icap.h"
#include<iostream>

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	char  name[8] = {'a','d','m','i','n','4','5','6'};
	
	cout<<name;
 unsigned	int len=8;
 char * key = "hello kitty and kgb/cia 2011 COPYRIGHT@REECAM 5460";
	int eF = icap_encode(name,len,key);
	cout<<"===================="<<endl;
	for(int i=0;i<8;i++){
		printf("%x ",name[i]);
		cout<<endl;
	}
	
	int de = icap_decode(name,len,key);

	cout<<"===================="<<endl;
	for(int i=0;i<8;i++){
		printf("%x ",name[i]);
		cout<<endl;
	}
	int a;
	cin>>a;

	return 0;
}
