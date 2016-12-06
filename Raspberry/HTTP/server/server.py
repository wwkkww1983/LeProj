#!/usr/bin/python
'''
#=============================================================================
#     FileName: server.py
#         Desc: 
#       Author: Suo Xudong
#        Email: suoxd123@126.com
#     HomePage: http://suoxd.name
#      Version: 0.0.1
#   LastChange: 2016-12-06 10:00:01
#      History:
#=============================================================================
'''
from pi_car import app

app.run(host='0.0.0.0',port=9001)
