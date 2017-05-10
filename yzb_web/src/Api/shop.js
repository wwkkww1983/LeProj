import apiCall from './apiCall'

// 修改店铺信息
export const changeShopInfo = function (opt) {
  return apiCall({
    url: "/v1/shop",
    init: {
      shopId: '',
      fullName: '',
      shortName: '',
      address: '',
      linkman: '',
      phone: '',
      groupId: '', // 群组id
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
// 展示店铺相关默认应用
export const shopManagerApp = function (opt) {
  return apiCall({
    url: "/v1/shop/shopMangerApply",
    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      shopId: '',
      shopType: 'SHOPMANGER', // 店铺:SHOPMANGER,管理员:SHOPADMIN,默认员工应用:SHOPDEFSTAFF，员工应用:SHOPSTAFF
      userId: null // 员工用户id
    }
  }, opt)
}
// 店铺设置展示页
export const shopSetInfo = function (opt) {
  return apiCall({
    url: "/v1/shop/shopSetShow",

    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      shopId: ''
    }
  }, opt)
}
