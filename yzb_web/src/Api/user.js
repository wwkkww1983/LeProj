import apiCall from './apiCall'
import apiCallClean from './apiCallClean'

// 获取用户信息
export const getSelfInfo = function (opt) {
    let URL = "/v1/user/getThisUser"
    return apiCallClean(opt, URL);
  // return apiCall({
  //     url: "/v1/user/getThisUser",
  //     init: {
  //       tokenId: sessionStorage.getItem('tokenId')
  //     }
  // }, opt)
}
// 根据id获取用户信息
export const getUserById = function (opt) {
  console.log("根据id获取用户信息");
  return apiCall({
      url: "/v1/user/getOneUser",
      init: {
        id: '',
        tokenId: sessionStorage.getItem('tokenId')
      }
  }, opt)
}
// 通过手机或昵称搜索公司内成员
export const getInfoByPN = function (opt) {
  console.log("通过手机或昵称搜索公司内成员");
  return apiCall({
    url: '/v1/user/findUserByPhoneOrName',
    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      companyId: sessionStorage.getItem('companyId'),
      PN: '电话或名字'
    }
  }, opt)
}
