'''
#=============================================================================
#     FileName: __init__.py
#         Desc: 
#       Author: Suo Xudong
#        Email: suoxd123@126.com
#     HomePage: http://suoxd.name
#      Version: 0.0.1
#   LastChange: 2016-12-06 10:00:01
#      History:
#=============================================================================
'''
import sqlite3
from flask import Flask, request, session, g, redirect, url_for, \
abort, render_template, flash
from contextlib import closing

app = Flask(__name__)
app.config.from_object('pi_car.config.Development')

def connect_db():
  return sqlite3.connect(app.config['DATABASE'])

def init_db():
  with closing(connect_db()) as db:
    with app.open_resource('init.sql',mode='r') as f:
      ad.cursor().executescript(f.read())
    db.commit()

#@app.before_request
#def before_request():
#  g.db = connect_db()
#@app.teardown_request
#  def teardown_request(exception):
#  db = getattr(g, 'db', None)
#  if db is not None:
#    db.close()
#    g.db.close()

import  pi_car.views

if __name__=='__main__':
  app.run()
