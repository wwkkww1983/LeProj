import apiCall from './apiCall'

// let port = ':9081'
// 用户注册
export const userReg = function (opt) {
  let URL = "/v1/auth/";
  return apiCall(opt, URL);
  // return apiCall({
  //   type: 'post',
  //   port: port,
  //   url: '/v1/auth/',
  //   init: {
  //     avatarUrl: '', // 头像
  //     code: '', // 验证码
  //     nickName: '', // 昵称
  //     password: '', // 加密过的密码
  //     phone: '', // 手机号
  //     type: '', // 1:qq注册 2:微信注册 6666：其他注册
  //     uid: null // 第三方的uid,如果type不是1或者2的时候,填写null或者不填
  //   }
  // }, opt)
}
// 用户登录
export const userLogin = function (opt) {
    let URL = "/v1/auth/login"
    return apiCall(opt, URL);
}
// 绑定手机号码
export const validateCode = function (parm) {
    let URL = "/v1/auth/validateCode"
    return apiCall(parm, URL);
}
// 获取验证码
export const getVcCode = function (opt) {
  let URL = "/v1/auth/code";
  return apiCall(opt, URL);
  // return apiCall({
  //   url: '/v1/auth/code',
  //   port: ':9081',
  //   init: {
  //     phone: '',
  //      // type 类型
  //      //  GET_REGISTER_CODE注册,
  //      //  GET_UPDATAPWD_CODE忘记密码,
  //      //  GET_UPDATAPHONE_CODE 更换手机号,
  //      //  GET_LOGIN_CODE 登录验证码
  //     type: 'GET_REGISTER_CODE'
  //   }
  // }, opt)
}
// 手机验证
export const validateCodeForPhone = function (opt) {
  return apiCall({
    url: '/v1/auth/loginByCode',
    port: ':9081',
    init: {
      phone: '',
      code: ''
    }
  }, opt)
}
