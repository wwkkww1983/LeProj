#准备工作
***
###设置设备IP与静态网关
1. 备份并打开文件/etc/network/interfaces
2. 添加以下几行  
>iface eth0 inet static  
\#your static IP  
address 192.168.1.123    
\#your gateway IP  
gateway 192.168.1.1  
netmask 255.255.255.0  
\#your network address "family"  
network 192.168.1.0
3. 改为无线方式  
建议先使用有线方式连接树莓测试通过后再改，避免网络不稳定。  
>>iface eth0 inet dhcp  
auto wlan0  
allow-hotplug wlan0  
iface wlan0 inet dhcp  
wpa-ssid “你的wifi名称”  
wpa-psk “你的wifi密码”  

###Flask安装
需要设备可以联网，使用Flask框架，默认端口为 9001，启动程序需要先安装框架。

    sudo apt-get install python-pip
    sudo pip install flask


#视频流模块
***
###代码修改

    打开pi_car\templates\home.html文件
    看到IP地址192.168.1.123，用设备IP替换

###服务器程序启动
不启动的话，网页端不显示视频

    mjpg-streamer/mjpg-streamer-experimental
    sudo ./mjpg-streamer -i "input_raspicam.so" -o "output_http.so -w ./www"（单引号双引号都可以）

#控制模块
***
###启动程序

    sudo python server.py

###客户端访问
在浏览器输入下面内容

    设备IP:9001