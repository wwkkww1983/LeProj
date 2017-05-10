/* eslint-disable */
const Admin = resolve => require(['../layouts/Admin'], resolve)
const MyCompany = resolve => require(['../layouts/Admin/MyCompany'], resolve)
const ChangeCompanyInfo = resolve => require(['../layouts/Admin/ChangeCompanyInfo'], resolve)
const ChangeOwner = resolve => require(['../layouts/Admin/ChangeOwner'], resolve)
const Dissolve = resolve => require(['../layouts/Admin/DissolveCompany'], resolve)
const Join = resolve => require(['../layouts/Admin/JoinCompany'], resolve)
const Create = resolve => require(['../layouts/Admin/CreateCompany'], resolve)
const Setting = resolve => require(['../layouts/Admin/Setting'], resolve)
const Logout = resolve => require(['../layouts/Admin/Logout'], resolve)
/* 我的公司子页面 */
const MyCompanyDetail = resolve => require(['../layouts/Admin/mycompany/MyCompanyDetail'], resolve)
const Admins = resolve => require(['../layouts/Admin/mycompany/Admins'], resolve)
const Unallocated = resolve => require(['../layouts/Admin/mycompany/Unallocated'], resolve)
const DepartmentAdmin = resolve => require(['../layouts/Admin/mycompany/DepartmentAdmin'], resolve)
const DepartmentDetail = resolve => require(['../layouts/Admin/mycompany/DepartmentDetail'], resolve)
const AddShop = resolve => require(['../layouts/Admin/mycompany/AddShop'], resolve)
const ShopDetail = resolve => require(['../layouts/Admin/mycompany/ShopDetail'], resolve)
const ShopAdmin = resolve => require(['../layouts/Admin/mycompany/ShopAdmin'], resolve)

export default {
  path: 'admin',
  component: Admin,
  name: '我的',
  redirect: 'admin/MyCompany',
  children: [
    {
      path: '/admin/MyCompany',
      component: MyCompany,
      name: '我的公司',
      redirect: '/admin/MyCompany/index',
      children: [
        {path: '/admin/MyCompany/index', component: MyCompanyDetail, name: '我的公司'},
        {path: '/admin/MyCompany/admins', component: Admins, name: '系统管理员列表'},
        {path: '/admin/MyCompany/department/:id', component: DepartmentDetail},
        {path: '/admin/MyCompany/departmentAdmin/:id', component: DepartmentAdmin},
        {path: '/admin/MyCompany/unallocated', component: Unallocated, name: '未分配人员列表'},
        {path: '/admin/MyCompany/addShop', component: AddShop, name: '添加店铺'},
        {path: '/admin/MyCompany/shop/:id', component: ShopDetail, name: '店铺详情'},
        {path: '/admin/MyCompany/shopAdmin/:id', component: ShopAdmin, name: '店铺设置'},
      ]
    },
    {path: '/admin/ChangeCompanyInfo', component: ChangeCompanyInfo, name: '修改公司信息'},
    {path: '/admin/ChangeOwner', component: ChangeOwner, name: '转让创建者'},
    {path: '/admin/DissolveCompany', component: Dissolve, name: '解散公司'},
    {path: '/admin/JoinCompany', component: Join, name: '加入公司'},
    {path: '/admin/CreateCompany', component: Create, name: '创建公司'},
    {path: '/admin/Setting', component: Setting, name: '设置'},
    {path: '/admin/Logout', component: Logout, name: '登出'}    
  ]
}
/* eslint-enable */
