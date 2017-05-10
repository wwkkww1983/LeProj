/* eslint-disable */
import apiCall from './apiCall'

// 创建部门
export const createDep = function (opt) {
  return apiCall({
    type: 'post',
    url: "/v1/department",
    init: {
      name: "未命名部门",
      companyId: sessionStorage.getItem('companyId'),
      tokenId: sessionStorage.getItem('tokenId'),
      parentId: '0',
      applyId: ''
    }
  }, opt)
}
// 修改部门名称
export const changeDepName = function (opt) {
  return apiCall({
  type: 'post',
    url: '/v1/department/modifyDepartment',
    parameterType: 'body',
    init: {
      name: "未命名部门",
      tokenId: sessionStorage.getItem('tokenId'),
      departmentId: '', // 部门id
      groupId: '' // 群组id
    }
  }, opt)
}
// 部门设置所需信息
export const depSetInfo = function (opt) {
  return apiCall({
    url: '/v1/department/depSetShow',

    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      departmentId: '0'
    }
  }, opt)
}
// 删除部门
export const delDep = function (opt) {
  return apiCall({
    url: '/v1/department/delDepartment',

    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      departmentId: '0'
    }
  }, opt)
}
// 部门可用应用
export const depAvailableApp = function (opt) {
  return apiCall({
    url: '/v1/department/showCreateDepartmentApply',

    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      depId: '0'
    }
  }, opt)
}
// 部门默认应用
export const depDefApp = function (opt) {
  return apiCall({
    url: '/v1/department/defDepartmentApply',

    init: {
      tokenId: sessionStorage.getItem('tokenId'),
      depId: '0'
    }
  }, opt)
}
// 部门应用编辑
export const editDepApp = function (opt) {
  return apiCall({
    port: ':9083',
    url: '/v1/myWorkApply/editApply',
    init: {
      organizeType: 'DEP', // DEP || SHOP
      degreeType: '1',
      delApplyIds: '',
      depshopId: '',
      newApplyIds: '',
      tokenId: sessionStorage.getItem('tokenId'),
      uersId: ''
    }
  }, opt)
}
// 显示设置主管时的部门员工信息
export const depStaff = function (opt) {
  return apiCall({
    url: '/v1/authChange/showStaffInDepartment',

    init: {
      departmentId: '0',
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
// 显示主管
export const depManager = function (opt) {
  return apiCall({
    url: '/v1/authChange/showStaffInDepartment',

    init: {
      departmentId: '0', // 部门id
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
// 设置主管
export const setDepManager = function (opt) {
  return apiCall({
    url: '/v1/authChange/setDepartmentAdmin',
    type: 'post',
    init: {
      comId: sessionStorage.getItem('companyId'),
      departmentId: '1', // 部门id
      name: '', // 部门名称(希望后台可以删除)
      preAdminId: '0', // 前任主管的id,无则为0
      tokenId: sessionStorage.getItem('tokenId'),
      type: '3', // 3: 部门主管；4：部门管理员
      userId: '11' // 需要被设置的id
    }
  }, opt)
}
/* eslint-enable */
