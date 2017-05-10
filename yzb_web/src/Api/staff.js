import apiCall from './apiCall'

// 邀请员工
export const inviteStaff = function (opt) {
  return apiCall({
    url: '/v1/staff',
    type: 'post',
    init: {
      comId: sessionStorage.getItem('companyId'),
      comName: '公司名称',
      depId: '0',
      depName: '部门名称',
      shopId: '0',
      shopName: '店铺名称',
      tokenId: sessionStorage.getItem('tokenId'),
      type: '3',
      userId: '123',
      userName: '邀请人',
      groupId: '群组Id'
    }
  }, opt)
}

// 批量邀请员工
export const inviteStaffs = function (opt) {
  return apiCall({
    type: 'post',
    url: '/v1/excel/staffImportExcel',
    parameterType: 'formData',
    init: {
      excelFile: '', // 文件
      comId: sessionStorage.getItem('companyId'),
      depShopId: '0', // 部门或者店铺id
      depShopName: '', // 公司店铺名称
      msg: '邀请您加入', // 邀请消息
      type: '1', // 部门: 1, 店铺: 2, 未分配: 3
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}

// 同意加入公司的邀请
export const agreeJoin = function (opt) {
  return apiCall({
    url: '/v1/staff/agreeJoin',
    type: 'post',
    init: {
      comId: sessionStorage.getItem('companyId'),
      comName: '公司名称',
      depId: '0',
      depName: '部门名称',
      shopId: '0',
      shopName: '店铺名称',
      tokenId: sessionStorage.getItem('tokenId'),
      type: '3',
      userId: '123',
      userName: '邀请人',
      groupId: '群组id'
    }
  }, opt)
}

// 申请加入公司
export const applyJoin = function (opt) {
  return apiCall({
    type: 'post',
    init: {
      comId: sessionStorage.getItem('companyId'),
      comShopDepName: '公司或者店铺名称',
      shopDepId: '0',
      tokenId: sessionStorage.getItem('tokenId'),
      type: '1',
      userId: '',
      userName: '申请人的名称',
      validationMessage: "请求加入公司"
    }
  }, opt)
}
// 同意加入公司的申请
export const auditJoin = function (opt) {
  return apiCall({
    url: '/v1/staff/auditApplyJoin',
    type: 'post',
    init: {
      comId: sessionStorage.getItem('companyId'),
      comShopDepName: '公司或者部门名称',
      shopDepId: '0',
      tokenId: sessionStorage.getItem('tokenId'),
      type: 1,
      userId: '申请人的id',
      userName: '申请人的名称',
      validationMessage: '验证信息',
      groupId: ''
    }
  }, opt)
}
// 停职员工
export const stopStaff = function (opt) {
  return apiCall({
    url: '/v1/staff/stopStaff',
    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      userId: '员工id',
      shopDepId: '',
      companyId: '',
      gourpId: ''
    }
  }, opt)
}
// 离职员工
export const delStaff = function (opt) {
  return apiCall({
    url: '/v1/staff/delLiZhiStaff',
    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      userId: '员工id',
      shopDepId: '0', // 部门id或者店铺的id,未分配传0
      type: '3', // 1:部门 2:店铺 3:公司下未分配
      companyId: sessionStorage.getItem('companyId'),
      groupId: '0' // 群组id
    }
  }, opt)
}
/* eslint-enable */
