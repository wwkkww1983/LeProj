import apiCall from './apiCall'

// 根据手机号码查询公司员工(设置系统管理员的)
export const findStaffByphone = function (opt) {
  return apiCall({
    type: 'post',
    url: '/v1/authChange/findStaffByphone',
    init: {
      adminId: '',
      leaderId: '',
      phone: '',
      tokenId: sessionStorage.getItem('tokenId'),
      companyId: sessionStorage.getItem('companyId')
    }
  }, opt)
}
// 设置为系统管理员
export const setSysAdmin = function (opt) {
  return apiCall({
    type: 'post',
    url: '/v1/authChange/setSystemAdmin',
    init: {
      comId: sessionStorage.getItem('companyId'),
      tokenId: sessionStorage.getItem('tokenId'),
      userId: '',
      leaderName: '',
      companyName: '公司名称'
    }
  }, opt)
}
// 取消系统管理员
export const delSystemAdmin = function (opt) {
   return apiCall({
    type: 'post',
    url: '/v1/authChange/delSystemAdmin',
    init: {
      comId: sessionStorage.getItem('companyId'),
      tokenId: sessionStorage.getItem('tokenId'),
      userId: '',
      leaderName: '',
      companyName: '公司名称'
    }
  }, opt)
}
// 设置店长或管理员时的员工数据
export const shopStaff = function (opt) {
  return apiCall({
    url: '/v1/authChange/showStaffInShop',
    init: {
      shopId: '1', // 店铺Id
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
// 设置为店铺店长或管理员
export const setShopManager = function (opt) {
  return apiCall({
    type: 'post',
    url: '/v1/authChange/setShopAdmin',
    init: {
      comId: sessionStorage.getItem('companyId'),
      shopId: '1', // 部门id
      name: '', // 部门名称(希望后台可以删除)
      preAdminId: '0', // 前任主管的id,无则为0
      tokenId: sessionStorage.getItem('tokenId'),
      type: '5', // 店长:5;管理员: 6
      userId: '' // 需要被设置的id
    }
  }, opt)
}
