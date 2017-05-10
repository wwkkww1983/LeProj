import apiCall from './apiCall.js'

// qq检测是否登入(第三方登录)
export const loginByOther = (parm) => {
    let URL = "/v1/auth/loginByOther";
    return apiCall(parm, URL);
  // return apiCall({
  //    url: '/v1/auth/loginByOther',
  //    port: ':9081'
  // }, opt)
}
// qq绑定手机号
export const validateCode = (parm) => {
    let URL = "/v1/auth/validateCode"
    return apiCall(parm, URL);
}
// QQ获取用户个人信息
export const getQqInfo = (opt) => {
  return apiCall({
     url: '/v1/qqApp/getUserInfo',
     port: ':9081'
  })
}
