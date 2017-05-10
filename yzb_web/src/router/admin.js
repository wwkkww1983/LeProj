const Admin = resolve => require(['../layouts/Admin'], resolve)
const PersonalInfo = resolve => require(['../layouts/Admin/PersonalInfo'], resolve)
const MyCompany = resolve => require(['../layouts/Admin/MyCompany'], resolve)
const ShopManage = resolve => require(['../layouts/Admin/ShopManage'], resolve)
const PawdSetting = resolve => require(['../layouts/Admin/PawdSetting'], resolve)

export default {
  path: 'admin',
  component: Admin,
  name: '我的',
  redirect: '/admin/personalInfo',
  children: [
    {path: '/admin/personalInfo', component: PersonalInfo},
    {path: '/admin/myCompany', component: MyCompany},
    {path: '/admin/shopManage', component: ShopManage},
    {path: '/admin/pawdSetting', component: PawdSetting}
  ]
}