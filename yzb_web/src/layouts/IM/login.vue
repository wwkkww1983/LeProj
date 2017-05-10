<template>
<div class="hidden-login"></div>
</template>
<script>
import { login } from '../../IM'
import {mapActions, mapGetters} from 'vuex'
export default {
  methods: {
    ...mapActions([
      'getRecentContacts',
      'getFriendList',
      'getGroupList'
    ]),
    xiaohua () {
      console.log("小华新消息来了");
    }
  },
  computed: {
    ...mapGetters([
      'groupList'
    ])
  },
  created () {
    const _self = this
    function onMsgNotify (newMsgList) {
      sessionStorage.setItem("HHHHH", newMsgList);
      sessionStorage.setItem("新消息来了", JSON.stringify(newMsgList.Msg));
      console.log("新消息来了");
      console.log(newMsgList);
      let sess, newMsg
      // 获取所有聊天会话
      let sessMap = webim.MsgStore.sessMap();
      console.log(webim.MsgStore);
      for (let j in newMsgList) { // 遍历新消息
        newMsg = newMsgList[j]
        if (newMsg.getSession().id() === selToID) { // 设置当前聊天对象
          selSess = newMsg.getSession();
        }
      }
      // 消息已读标记
      // webim.setAutoRead(selSess, true, true) // 标记为已读
      let arr = []
      // console.log(_self.groupList)
      for (let i in sessMap) {
        sess = sessMap[i]._impl
        if (sess.type === 'C2C') {
          for (let j of _self.$store.getters.friendList) {
            if (sess.id === j.id) {
              sess.name = j.nickname
              j.avatarUrl ? sess.avatarUrl = j.avatarUrl : sess.avatarUrl = ''
            }
          }
        }
        if (sess.type === 'GROUP') {
          for (let j of _self.groupList) {
            if (sess.id === j.GroupId) {
              sess.typeEn = j.TypeEn
            }
          }
        }
        arr.unshift(sess)
      }

      // 将未读消息做上标志
      for (let i = 0; i < newMsgList.length; i++) {
           let obj = newMsgList[i].sess._impl
          for (let j = 0; j < arr.length; j++) {
              if (obj.id === arr[j].id && selToID !== arr[j].id) {
                 arr[j].unread = true
              }
          }
      }
      _self.getRecentContacts(arr)
    }
    login(onMsgNotify, (res) => {
      if (res.ActionStatus === 'OK') {
        console.info('IM连接成功，' + res.identifierNick)
        // 好友列表
        _self.getFriendList(() => {
          webim.syncMsgs(onMsgNotify)
        })
        // 群组列表
        _self.getGroupList()
      }
    })
  }
}
</script>