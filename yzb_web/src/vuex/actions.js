import Vue from 'vue'
import router from './../router'
import * as types from './mutation-types.js'
// import { getMyFriends } from '../IM/friend'
import {getMyFriends} from '../Api/IM/friend/index'
import { getMyGroup } from '../IM/group/'
import { receiptAddOrChecks } from '../Api/commonality/operate'
import { seekProductDetail, seekDepartmentList, seekShowCounterList, seekRepositoryList, getProductTypeList, seekShowProviderList, seekGetShopListByCo } from '../Api/commonality/seek.js'

import {getSelfInfo, getUserById} from '../Api/user'
import {getConstruct, companyManagers} from '../Api/company'
/* eslint-disable */

// 获取用户信息
export const getUserInfo = ({ commit }, cb) => {
    let obj = {
        "tokenId": sessionStorage.getItem("tokenId")
    };
    getSelfInfo(obj).then((res) => {
        console.log(res.data);
        if (res.data.state === 200) {
            sessionStorage.setItem("获取用户信息+", JSON.stringify(res.data.data));
            sessionStorage.setItem("role", res.data.data.role)
            sessionStorage.setItem("nickname", res.data.data.nickname);
            commit(types.SET_USER_INFO, res.data.data);
            // 用于QQ验证
            loginInfo.identifier = sessionStorage.getItem('id');
            loginInfo.userSig = sessionStorage.getItem('sig');
            loginInfo.headurl = res.data.data.avatarUrl
            if (cb instanceof Function) cb(res.data.data);
        } else {
            console.log('获取个人数据是失败' + res.data.state + "-" + res.data.msg);
        }
    }, (err) => {
        alert('获取个人数据失败：' + err);
    })
}
// 获取公司信息
export const getComInfo = ({ commit }, cb) => {
  getConstruct().then((res) => {
    if (res.data.state === 200) {
      companyManagers().then((mRes) => {
        if (mRes.state === 200) {
          let deps = mRes.data.data
          console.log('获取主管信息成功', mRes);
          res.data.data.depList = res.data.data.depList.map((item) => {
            for (let dep of deps) {
              if (item.id === dep.depShopId) {
                Object.assign(item, dep)
              }
            }
            return item
          })
          res.data.data.shopList = res.data.data.shopList.map((item) => {
            for (let dep of deps) {
              if (item.id === dep.depShopId) {
                Object.assign(item, dep)
              }
            }
            return item
          })
          commit(types.SET_COM_INFO, res.data.data);
          if (cb instanceof Function) cb(res.data.data);
        } else {
          console.log('获取主管信息错误');
        }
      }, (err) => {
        console.log('获取主管信息失败', err);
      })
    } else {
      console.log('获取公司数据失败：' + res.data.state + "-" + res.data.msg)
    }
  }, (err) => {
    alert('获取公司数据失败：' + err.status)
  })
}

/*---------------IM 新-------------*/
// 状态类
export const IMonMsgNotify = ({commit}, parm) => { // 监听新消息时间
    console.log("小华测试新消息来了");
    console.log(parm);
    // 获取所有聊天会话
    var sessMap = webim.MsgStore.sessMap();
    console.log("获取所有聊天会话");
    console.log(sessMap);
    for (var i in sessMap) {
        var sess = sessMap[i];
        console.log(sess.type());
          console.log(sess.id());
          console.log(sess.name());
          console.log(sess.unread());
        if (selToID != sess.id()) {//更新其他聊天对象的未读消息数
          console.log(sess.type());
          console.log(sess.id());
          console.log(sess.name());
          console.log(sess.unread());
          // stopPolling = true;
        }
    }
    commit(types.IM_ON_MSG_NOTIFY, parm)
}
export const IMCutFriendGroup = ({commit}, parm) => { // 监听切换好友或群
    commit(types.IM_CUT_FRIEND_GROUP, parm)
}
/* IM部分旧 */
// 获取好友列表
export const getFriendList = ({ commit }, cb) => {
  getMyFriends((parm) => {
    const tokenId = sessionStorage.getItem('tokenId')
    const newArr = []
    parm.map((item) => { // 【待修改】
      getUserById({id: item.id}).then((res) => {
       // res.data.data.friendOperation = "发送消息"
       console.time('timeMun')
        newArr.push(res.data.data)
      }, (err) => {
        console.log(err)
      })
    })
     console.timeEnd('timeMun')
     sessionStorage.setItem("好友列表", JSON.stringify(newArr));
    commit(types.SET_FRIEND_LIST, newArr)
    if (cb instanceof Function) cb(newArr);
  })
}

// 获取群组列表
export const getGroupList = ({commit}, cb) => {
  getMyGroup(parm => {
    commit(types.SET_GROUP_LIST, parm)
    console.log('群组列表', parm);
    if (cb instanceof Function) cb(parm)
  })
}
// 最近联系人
export const getRecentContacts = ({commit, state}, parm) => {
  // console.log(parm)
  let recentData = parm
  let pinnedList = state.im.stick
  if (pinnedList.length) {
     for (let j = 0; j < recentData.length; j++) {
      for (let i = 0; i < pinnedList.length; i++) {
          console.log(recentData[j].id === pinnedList[i])
          if (recentData[j].id === pinnedList[i]) {
            recentData.unshift(recentData.splice(j, 1)[0])
            break
          }
      }
     }
  }
  commit(types.SET_RECENT_CONTACTS, recentData)
}

// 获取系统消息列表 之后细分通知公告审批等
export const getSysMess = ({commit}, parm) => {
  commit(types.SET_SYS_MESS, parm)
  // console.log(parm);
}

// 消息免打扰
export const setDndList = ({commit}, parm) => {
  commit(types.SET_IM_DNDLIST, parm)
}
// 消息置顶
export const setPinnedList = ({commit}, parm) => {
    commit(types.SET_IM_PINNEDLIST, parm)
}

// ********************** work ****************
// ----------状态类---------
export const workPopupAudit = ({commit}, parm) => { // 审核弹窗
    commit(types.WORK_POPUP_AUDIT, parm)
}
export const workPopupAffirm = ({commit}, parm) => { // 审核的确认弹窗
    commit(types.WORK_POPUP_AFFIRM, parm);
}
export const workPopupError = ({commit}, parm) => { // 错误弹窗
    commit(types.WORK_POPUP_ERROR, parm);
}
// 监听编辑权限
export const workUserType = ({commit}, parm) => {
    commit(types.WORK_USER_TYPE, parm)
}
// 审核状态
export const workProductStatus = ({commit}, parm) => {
    commit(types.WORK_PRODUCT_STATUS, parm)
}
// 应用用户列表
export const workApplyUser = ({commit}, parm) => {
    var url = INTERFACE_URL_9083 + "/v1/apply/userList"
    var data = {
        "data": {
            "type": parm.type, // 1部门2 公司 3 店铺 4 班组成员 5 审核人
            "objId": parm.objId,
            "applyId": sessionStorage.getItem("applyId")//应用ID
        },
        "unit": {
            "channel": 3,
            "os": "string",
            "userId": sessionStorage.getItem("id"),
            "companyId": sessionStorage.getItem("companyId"),
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    // sessionStorage.setItem("kkkkkk", JSON.stringify(data));
    Vue.http.post(url, data).then((response) => {
        console.log("获取用户列表");
        if (response.data.state === 200) {
            commit(types.WORK_APPLY_USER, response.data.data)
        } else {
            console.log("获取用户列表失败");
            console.log(response);
        }
    }, (response) => {
        console.log(response)
    })
}
// 产品类别（商品类型列表）
export const workProductClass = ({commit}, parm) => {
    getProductTypeList().then((response) => {
        if (response.data.state === 200) {
            commit(types.WORK_PRODUCT_CLASS, response.data.data.list)
        } else {
            alert(response.data.msg)
        }
    }, (response) => {
        console.log(response + "商品类型列表");
    })
}
// 获取供应商列表
export const workSupplierList = ({commit}, parm) => {
    seekShowProviderList().then((response) => {
        console.log("获取供应商列表789");
        console.log(response);
        if (response.data.state === 200) {
            commit(types.WORK_SUPPLIER_LIST, response.data.data.supplierList);
        } else {
            alert(response.data.msg)
        }
    }, (response) => {
        console.log(response + "供应商列表出错");
    })
}
// 用户所有部门、店铺权限列表 (计划分销)
export const workApplyDepartement = ({commit}, parm) => {
    var data = {
        "data": {
            "applyId": sessionStorage.getItem("applyId") // 应用id
        },
        "unit": {
            "userId": sessionStorage.getItem("id"),
            "companyId": sessionStorage.getItem("companyId"),
            "channel": "web",
            "os": "string",
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    let url = INTERFACE_URL_9083 + "/v1/apply/getApplyDepShop";
    Vue.http.post(url, data).then((response) => { 
        if (response.data.state === 200) {
            commit(types.WORK_APPLY_DEPARTEMENT, response.data.data.dpList) 
        } else {
            console.log("计划分销商出错");
            console.log(response);
        }
    }, (response) => {
        console.log(response)
    })
}
// 库位列表
export const workRepositoryList = ({commit}, parm) => { // 待删，防止报错先放着
    seekRepositoryList().then((response) => {
        if (response.data.state === 200) {
            commit(types.WORK_REPOSITORY_LIST, response.data.data.repositoryList) 
        } else {
            alert(response.data.msg)
        }
    }, (response) => {
        console.log("库位列表" + response);
    })
}
// 发货多选框（删除）
export const delectSelects = ({commit}, parm) => {
    commit(types.DELECT_SELECTS, parm)
}
// 通过条形码搜索数据
export const seekBarCode = ({commit}, parm) => {
    commit(types.SEEK_BARCODE, parm)
}
// 获取新建带过来的数据（退货）
export const getReturnSelect = ({commit}, parm) => {
    console.log(parm);
    commit(types.GET_RETURN_SELECT, parm)
}
// 获取新建带过来的数据（公共方法）
export const commonality = ({commit}, parm) => {
    console.log(parm);
}

// 商品列表(web)
export const workProductDetail = ({commit}, parm) => {
    seekProductDetail(parm).then((response) => {
        if (response.data.state === 200) {
            commit(types.WORK_PRODUCT_DETAIL, response.data.data);
            if (parm.routerUrl) {
                router.push(parm.routerUrl)
            }
        } else {
            alert(response.data.msg)
        }
    }, (err) => {
        console.log(err);
    })
}
// 商品操作（删）
export const delectProductDetail = ({commit}, parm) => {
      var data = {
          "data": {
              "orderNum": parm.orderNumber, // 单据号
              "productIdList": parm.productIdList, // 商品ID列表
              "type": 1, // 操作类型：1 删除
          },
          "unit": {
              "companyId": sessionStorage.getItem("companyId"),
              "channel": 3,
              "os": "string",
              "ip": "string",
              "userId": sessionStorage.getItem("id"),
              "tokenId": sessionStorage.getItem("tokenId")
          }
      }
      var url = INTERFACE_URL_9083 + "/v1/apply/productOperation";
      Vue.http.post(url, data).then((response) => {
          if (response.data.state === 200) {
              seekProductDetail(parm).then((response) => {
                  console.log("商品列表(web)");
                  console.log(response);
                  console.log("商品列表(web)");
                  if (response.data.state === 200) {
                      commit(types.WORK_PRODUCT_DETAIL, response.data.data);
                  }
              }, (response) => {
                  console.log(response);
              })
          } else {
              console.log(response);
          }
      }, (response) => {
          console.log(response);
      }) 
}
// 删除数据的集合（公共方法）
export const workDelectSelects = ({commit}, parm) => {
    commit(types.WORK_DELECT_SELECTS, parm)
}
// 单据详情
export const workReceiptDetail = ({commit}, parm) => {
    var data = {
        "data": {
            "orderNum": parm.orderNumber || sessionStorage.getItem("orderNumber"), // 单据号
        },
        "unit": {
            "companyId": sessionStorage.getItem("companyId"),
            "channel": 3,
            "os": "string",
            "ip": "string",
            "userId": sessionStorage.getItem("id"),
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    var url = INTERFACE_URL_9083 + "/v1/myWorkApply/receiptDetail";
    Vue.http.post(url, data).then((response) => {
        console.log("单据详情");
        console.log(response);
        console.log("单据详情");
        if (response.data.state === 200) {
        sessionStorage.setItem("DANJUXIANGQING", JSON.stringify(response.data.data))
            commit(types.WORK_RECEIPT_DETAIL, response.data.data);
        } else {
            console.log(response)
        }
    }, (response) => {
        console.log(response);
    })
}
// 单据列表
export const workProductList = ({commit}, parm) => {
    seekDepartmentList(parm).then((response) => {
        console.log("单据列表(web)");
        console.log(response);
        console.log("单据列表(web)");
        // sessionStorage.setItem("错误webweb", JSON.stringify(parm));
        if (response.data.state === 200) {
            commit(types.WORK_PRODUCT_LIST, response.data.data);
            // sessionStorage.setItem("错误", JSON.stringify(response.data.data))
            if (parm.routerUrl) {
                router.push(parm.routerUrl)
            }
        } else {
            let arr = []; // 清空单据列表 
            commit(types.WORK_PRODUCT_LIST, arr)
            alert(response.data.msg)
        }
    }, (err) => {
        console.log(err);
    })
}
// 获取操作部门
export const workDepartmentList = ({commit}, parm) => {
    let url = INTERFACE_URL_9083 + "/v1/apply/getApplyDepShop";
    let data = {
        "data": {
            "applyId": sessionStorage.getItem("applyId")
        },
        "unit": {
            "userId": sessionStorage.getItem("id"),
            "companyId": sessionStorage.getItem("companyId"),
            "channel": 3,
            "os": "string",
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    Vue.http.post(url, data).then((response) => {
        console.log("获取操作部门启动");
        console.log(response);
        console.log("获取操作部门启动");
        if (response.data.state === 200) {
            commit(types.WORK_DEPARTMENT_LIST, response.data.data.dpList);
            let onlyDepartment = [];
            let onlyShop = [];
            response.data.data.dpList.forEach((x,i) =>{
                if (x.type === "1") {
                    onlyDepartment.push(x)
                } else {
                    onlyShop.push(x)
                }
            })
            commit(types.WORK_ONLY_DEPARTMENT, onlyDepartment); // 只有部门
            commit(types.WORK_ONLY_SHOP, onlyShop); // 只有店铺
        } else {
            console.log(response)
        }
    }, (response) => {
        console.log(response)
    })
}
// 店铺列表(分销商) 待删，全部为店铺了
export const workShopList = ({commit}, parm) => {
    var url = INTERFACE_URL_9083 + "/v1/headquarter/showShopList";
    var data = {
        "data": {
            "type": "2",
            "userId": sessionStorage.getItem("id")
        },
        "unit": {
            "companyId": sessionStorage.getItem("companyId"),
            "channel": 3,
            "os": "string",
            "ip": "string",
            "userId": sessionStorage.getItem("id"),
            "tokenId": sessionStorage.getItem("tokenId")
        }
    }
    Vue.http.post(url, data).then((response) => {
      console.log("店铺列表（分销商）");
      console.log(response);
      console.log("店铺列表（分销商）");
      if (response.data.state === 200) {
        commit(types.WORK_SHOP_LIST, response.data.data.shopList)
      }
    }, (response) => {
      console.log(response)
    })
}
export const getShopListByCo = ({commit}, parm) => {
    var options = {
      "page": "1",
      "pageSize": "30"
    }
    seekGetShopListByCo(parm).then((response) => {
        console.log("新店铺接口");
        console.log(response);
        // console.log(response.data.data.shopList);
        if (response.data.state === 200) {
            commit(types.WORK_SHOP_LIST_BY_CO, response.data.data.shopList)
        }
    }, (response) => {

    })
}
// 单据新增/提交审核/保存（销售除外）
export const receiptAddOrCheck = ({commit}, parm) => {
    let productIdList = [];
    for (let i = 0; i < parm.productIdList.length; i++) { // 配置商品id列表
        let productIdObject = {};
        productIdObject.productId = parm.productIdList[i]; // 商品ID
        productIdObject.productStatus = parm.productStatus; // 1新增2删除
        productIdList.push(productIdObject);
    }
    let data = parm;
    data.productIdList = productIdList;
    receiptAddOrChecks(data).then((response) => {
        console.log("单据新增/提交审核/保存（销售除外）");
        console.log(response);
        console.log("单据新增/提交审核/保存（销售除外）");  
        if (response.data.state === 200) {     
            commit(types.WORK_POPUP_SAVE, {
                "saveSuccess": true,
                "saveSuccessData": response.data.data
            })
            let data = {
                "applyType": parm.type, // 1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
                "objId": response.data.data.orderNum,
                "type": "1" // 1 单据号 2 产品类别ID 3 商品ID列表 4 条码模糊查询 5 条码号
            }
            seekProductDetail(data).then((response) => {
                console.log("商品列表(web)");
                console.log(response);
                console.log("商品列表(web)");
                if (response.data.state === 200) {
                    commit(types.WORK_PRODUCT_DETAIL, response.data.data);
                }
            }, (response) => {
                console.log(response);
            })
        } else {
            commit(types.WORK_POPUP_ERROR, response.data.msg);
            // alert(response.data.msg);
            console.log("单据新增/提交审核/保存（销售除外）");
            console.log(response);
            console.log("单据新增/提交审核/保存（销售除外）");     
        }
    }, (response) => {
        console.log(response + "单据新增/提交审核/保存（销售除外）");
    })
}
export const workPopupSave = ({commit}, parm) => { // 保存弹窗
    commit(types.WORK_POPUP_SAVE, parm)
}
export const workCounterList = ({commit}, parm) => { // 柜组列表
    seekShowCounterList(parm).then((response) => {
        console.log("柜组列表");
        console.log(response);
        commit(types.WORK_COUNTTER_LIST, response.data.data.counterList)
    }, (response) => {
        alert(response.data.msg);
    })
}
// 销售
export const sellProductListFun = ({commit}, parm) => { // 商品列表-销售
    commit(types.SELL_PRODUCT_LIST, parm);
}
