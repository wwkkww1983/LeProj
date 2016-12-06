#准备工作
***

###MJPG-Steamer安装
用于视频流的显示

    wget https://github.com/jacksonliam/mjpg-streamer
    或者直接到网站下载zip安装包mjpg-streamer-master.zip，使用unzip mjpg-streamer-master.zip
    sudo apt-get install cmake  
    sudo  apt-get  install  libjpeg8-dev 
    cd /mjpg-streamer-master/mjpg-streamer-experimental
    make clean all

### GPIO 库安装

    sudo apt-get install git-core
    sudo apt-get update
    git clone git://git.drogon.net/wiringPi
    cd wiringPi
    git pull origin（更新非必需）
    ./build

### 编译服务器C程序（仅用于TCP程序）
gcc car_server.c –o car_server  -lwiringPi -lwiringPiDev

###Flask安装
需要设备可以联网，使用Flask框架，默认端口为 9001，启动程序需要先安装框架。

    sudo apt-get install python-pip
    sudo pip install flask

### 设置程序开机启动
可以不设置，如果不自动启动则每次通电后需要连上树莓，然后手动启动这两个程序（视频流和服务端程序）
    
    sudo vi /etc/rc.local
    在exit前加入下面两个程序的调用命令
    home/pi/mjpg-streamer/mjpg-streamer-experimental/mjpg-streamer -i "input_raspicam.so" -o "output_http.so -w ./www"
    python home/pi/server/server.py

#视频流模块
***
###代码修改

    打开pi_car\templates\home.html文件
    看到IP地址192.168.1.123，用设备IP替换

###服务器程序启动
不启动的话，网页端不显示视频

    cd mjpg-streamer/mjpg-streamer-experimental
    sudo ./mjpg-streamer -i "input_raspicam.so" -o "output_http.so -w ./www"（单引号双引号都可以）

#控制模块
***
###启动程序

    cd server
    sudo python server.py

###客户端访问控制
在浏览器输入下面内容

    设备IP:9001