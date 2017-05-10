import * as types from './mutation-types.js'
import { getMyFriends } from '../IM/friend'
import { getMyGroup } from '../IM/group/'
import {getSelfInfo, getUserById} from '../Api/user'
import {getConstruct, companyManagers} from '../Api/company'
/* eslint-disable */

// 获取用户信息
export const getUserInfo = ({ commit }, cb) => {
    let obj = {
        "tokenId": sessionStorage.getItem("tokenId");
    }
    getSelfInfo(obj).then((res) => {
        if (res.data.state === 200) {
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
        console.log("这里");
        console.log(err);
        alert('获取个人数据失败：' + err);
    })
}
// 获取公司信息
export const getComInfo = ({ commit }, cb) => {
  console.log('已触发');
  getConstruct().then((res) => {
    if (res.data.state === 200) {
      companyManagers().then((mRes) => {
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

/* IM部分 */
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
