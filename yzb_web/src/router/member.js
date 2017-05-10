const Member = resolve => require(['../layouts/Member'], resolve)
const Login = resolve => require(['../layouts/member/Login'], resolve)
const signUp = resolve => require(['../layouts/member/SignUp'], resolve)
const bindingPhone = resolve => require(['../layouts/member/BindingPhone'], resolve) // 绑定qq和手机
const member = {
  path: '/member',
  component: Member,
  name: '登录',
  redirect: '/member/login',
  children: [
    {path: 'login', component: Login},
    {path: 'signup', component: signUp},
    {path: 'bindingPhone', component: bindingPhone}

  ]
}
export default member
