const Main = resolve => require(['../layouts/Main'], resolve) // 首页
const message = resolve => require(['../layouts/Message'], resolve) // 即时通讯

import member from './member' // 登录注册
import IM from './IM' // 通讯录
import admin from './admin' // 我的(个人中心)
import work from './work' // 工作
/*
  分为 Main 和 member 两部分
  Main 为项目主内容
  member 为用户登录内容
*/
const routes = [{
  path: '/',
  name: 'Index',
  component: Main,
  children: [{
    path: '',
    component: message,
    name: '消息助手'
  }]
}]
routes.push(member)
routes[0].children.push(admin) // 我的(个人中心)
routes[0].children.push(IM) // 通讯录
routes[0].children.push(work) // 工作
export default routes