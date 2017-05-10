import apiCall from './apiCall'

// 获取免费店铺数量
export const getFreeShop = function (opt) {
  return apiCall({
    port: ':9083',
    url: "/v1/pay",
    init: {
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
// 创建免费店铺
export const createShopForFree = function (opt) {
  return apiCall({
    port: ':9083',
    type: 'post',
    url: "/v1/pay/freeCreateShop",
    init: {
      companyId: sessionStorage.getItem('companyId'),
      tokenId: sessionStorage.getItem('tokenId'),
      userId: sessionStorage.getItem('id')
    }
  }, opt)
}
// 支付创建
export const createShop = function (opt) {
  return apiCall({
    port: ':9083',
    type: 'post',
    url: "/v1/pay",
    init: {
      amount: 1, // 金额
      channel: 'upacp_pc', // 支付渠道,支付宝: alipay;微信: '?'
      shopNum: 3, // 店铺数量
      type: '1', // 买断: 1;年费: 2; 免费：3
      companyId: sessionStorage.getItem('companyId'),
      tokenId: sessionStorage.getItem('tokenId'),
      userId: sessionStorage.getItem('id')
    }
  }, opt)
}
