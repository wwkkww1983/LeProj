// IM
import * as types from "./../mutation-types.js"
const store = {
    state: {
        imOnMsgNotify: false, // 新消息
        imCutFriendGroup: '' // 切换好友和群的当前数据
    },
    mutations: {
        [types.IM_ON_MSG_NOTIFY] (state, parm) { // 新消息
            state.imOnMsgNotify = parm
        },
        [types.IM_CUT_FRIEND_GROUP] (state, parm) { // 切换好友和群的当前数据
            state.imCutFriendGroup = parm
        }
    }
}
export default store;
