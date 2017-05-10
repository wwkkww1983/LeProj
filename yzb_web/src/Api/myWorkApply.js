import apiCall from './apiCall'

// 获取个人应用
export const getUserApp = function (opt) {
  return apiCall({
    port: ':9083',
    type: 'get',
    url: '/v1/myWorkApply',
    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      companyId: sessionStorage.getItem('companyId'),
      userId: sessionStorage.getItem('userId'),
      companyRole: ''
    }
  }, opt)
}

// 应用编辑
export const editApp = function (opt) {
  return apiCall({
    port: ':9083',
    url: '/v1/myWorkApply/editApply',
    init: {
      organizeType: 'DEP', // DEP || SHOP
      degreeType: '1', // 主管应用: 1, 管理员应用: 2, 默认员工应用: 3，员工应用: 4
      delApplyIds: '',
      depshopId: '',
      newApplyIds: '',
      tokenId: sessionStorage.getItem('tokenId'),
      uersId: null
    }
  }, opt)
}
