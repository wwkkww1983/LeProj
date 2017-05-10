import apiCall from './apiCall'
import apiCallClean from './apiCallClean'

// 公司列表
export const companyList = function (opt) {
  console.log("公司列表");
  console.log(opt);
  console.log("公司列表");
  let URL = "/v1/company"
  return apiCall(opt, URL);
  // return apiCall({
  //   url: '/v1/company',
  //   init: {
  //     tokenId: sessionStorage.getItem('tokenId')
  //   }
  // }, opt)
}
// 公司信息
export const getConstruct = function (opt) {
  let URL = "/v1/company/comShow"
  let obj = {
    companyId: sessionStorage.getItem('companyId'),
    tokenId: sessionStorage.getItem('tokenId'),
    type: 'ORGANIZATION' // 组织架构：OGRANIZATION； 公司管理: COMMANAGE
  }
  return apiCallClean(obj, URL);
  // return apiCall({
  //   url: '/v1/company/comShow',
  //   init: {
  //     companyId: sessionStorage.getItem('companyId'),
  //     tokenId: sessionStorage.getItem('tokenId'),
  //     type: 'ORGANIZATION' // 组织架构：OGRANIZATION； 公司管理: COMMANAGE
  //   }
  // }, opt)
}
// 切换公司
export const updUserInCompany = function (opt) {
    console.log("切换公司");
    console.log(opt);
    console.log("切换公司");
    let URL = "/v1/company/updUserInCompany"
    return apiCall(opt, URL);
  // return apiCall({
  //   url: '/v1/company/updUserInCompany',
  //   init: {
  //     comId: '',
  //     comName: '公司名称',
  //     tokenId: sessionStorage.getItem('tokenId')
  //   }
  // }, opt)
}
// 获取公司部门店铺的主管列表
export const companyManagers = function (opt) {
    let URL = "/v1/company/depAndShopAdminByComId"
    let obj = {
        companyId: sessionStorage.getItem('companyId'),
        tokenId: sessionStorage.getItem('tokenId')
    }
    return apiCallClean(obj, URL);
  // console.log("获取公司部门店铺的主管列表");
  // return apiCall({
  //   url: '/v1/company/depAndShopAdminByComId',
  //   init: {
  //     companyId: sessionStorage.getItem('companyId'),
  //     tokenId: sessionStorage.getItem('tokenId')
  //   }
  // }, opt)
}

// 获取单个部门或店铺信息
export const getDepShopInfo = function (opt) {
  console.log("获取单个部门或店铺信息");
  return apiCall({
    type: 'get',
    port: ':9082',
    url: '/v1/company/getOneDepOrShop/',

    init: {
      companyId: sessionStorage.getItem('companyId'),
      depShopId: '0',
      tokenId: sessionStorage.getItem('tokenId'),
      type: 'DEP' // 部门: DEP, 店铺: SHOP
    }
  }, opt)
}
// 转让创建者
export const transCreater = function (opt) {
  console.log("转让创建者");
  return apiCall({
    url: '/v1/company/transCreater',
    init: {
      companyId: sessionStorage.getItem('companyId'),
      companyName: "", // 公司名称
      newUserId: '',
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
