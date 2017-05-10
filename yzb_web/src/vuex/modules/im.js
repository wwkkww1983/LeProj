import * as types from './../mutation-types.js'

const store = {
  state: {
    friendList: [], // 好友列表
    groupList: [], // 群组列表
    recentContacts: [], // 最近联系人
    sysMess: [], // 系统消息
    stick: [] // 消息置顶
  },
  mutations: {
    [types.SET_FRIEND_LIST] (state, parm) {
      console.log(JSON.stringify(parm))
      state.friendList = parm
    },
    [types.SET_GROUP_LIST] (state, parm) {
      state.groupList = parm
    },
    [types.SET_RECENT_CONTACTS] (state, parm) {
      state.recentContacts = parm
    },
    [types.SET_SYS_MESS](state, parm) {
      state.sysMess.push(parm)
    },
    [types.SET_IM_STICK] (state, parm) {
      state.stick = parm
    }
  }
}
export default store
