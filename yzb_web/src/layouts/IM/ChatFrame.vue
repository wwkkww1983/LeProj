<template>
  <div class="chat-frame">
    <div class="chat-frame-top">{{mess.name}}
      <i :class="mess.type === 'C2C' ? 'personalData' : 'personalData groupImg'" @click="information"></i>
    </div>
    <el-dialog custom-class="friendsProfileItem" v-model="dialogPersonalDataVisible"><!--个人资料-->
        <FriendsProfile :props="dialogPersonaHiden" :userId="mess.id"></FriendsProfile>
    </el-dialog>
    <!--讨论组-->
    <GroupData v-if="privateBle" :props="dialogPersonaHiden" :mess="mess"></GroupData>
    <el-dialog top="8%" custom-class="pulic" v-model="publicBle"><!--公开群-->
        <GroupData :props="dialogPersonaHiden" @removeMsg="removeMsgFun" :mess="mess"></GroupData>
    </el-dialog>
    <div class="chat-frame-main">
      <div class="msg-history" @click="getHistory" style="position:relative; z-index:333">获得消息历史纪录</div>
      <div class="msg-show-panel">
        <MsgNode
        v-for="item in historyListMsg"
        :msg = "item"
        :avatarUrl="item.avatarUrl || item.icon"
        :time = "item.time"
        :type="historyListMsg.type"
        >
        </MsgNode>
        <p v-for="item in historyListMsg">{{item.time}}</p>
        <MsgNode
        v-for="(node, index) in mess.msgs"
        :msg = "node"
        :avatarUrl="mess.avatarUrl || mess.icon"
        :time = "timeFun(node, mess.msgs[index-1])"
        :type="mess.type"
        >
        </MsgNode>
      </div>
      <div class="msg-send-panel">
        <div class="button-panel">
          <span class="chat02_title_btn ctb01" v-clickoutside="hide" @click="onShowEmotionDialog">
              <Emotions v-if="face" :emotionsList="imgList" v-on:increment="incrementTotal"></Emotions>
          </span>
          <span class="chat02_title_btn ctb03" @click="dialogTableVisible = true"></span>
          <el-dialog title="发送图片消息" v-model="dialogTableVisible">
                <sendImg :dialogTableVisibleFun="dialogTableVisibleFun"></sendImg>
          </el-dialog>
        </div>
        <textarea class="editFr" ref="editsf" contenteditable="true" @keyup.enter="onSendMsg" id="editFr" v-model="msgtosend" ></textarea>
      </div>
    </div>
  </div>
</template>
<script>
  // import covertMessage from "../../IM/message/convertMessage"
  import {
    mapGetters,
    mapActions
  } from 'vuex'
  import {
    sendMsg
  } from '../../IM/message/sendMessage'
  import Clickoutside from "element-ui/lib/utils/clickoutside"
  import {getLastC2CHistoryMsgs} from '../../IM/message/getHistoryMsgs.js'
  import MsgNode from './MsgNode' // 对话记录
  import sendImg from './sendImg' // 发送图片
  import Emotions from './Emotions' // 表情框
  import FriendsProfile from './FriendsProfile' // 查看好友资料
  import GroupData from './GroupData' // 群资料
  export default {
    data() {
      return {
        msgtosend: 'aaa',
        lastOne: null,
        hide: false, // 点击其它处隐藏
        face: false, // 表情框显示隐藏
        imgList: [], // 表情列表
        dialogTableVisible: false, // 发送图片消息框
        dialogPersonalDataVisible: false, // 修改个人资料，show/hide
        privateBle: false, // 私有群
        publicBle: false, // 公开群
        historyListMsg: [], // 历史消息
        lastMsgTime: 0, // 保留服务器返回的最近消息时间和消息Key,用于下次向前拉取历史消息
        msgKey: '' // 保留服务器返回的最近消息时间和消息Key,用于下次向前拉取历史消息
      }
    },
    props: [
      'mess'
    ],
    components: {
      MsgNode,
      sendImg,
      Emotions,
      FriendsProfile,
      GroupData
    },
    directives: {
      Clickoutside
    },
    methods: {
      ...mapActions([
        'getRecentContacts',
        'getGroupMemberList'
      ]),
      information: function() {
        if (this.mess.type === 'C2C') {
          this.dialogPersonalDataVisible = true
        } else if (this.mess.type === 'GROUP') {

          if (this.mess.typeEn === 'Private') {
            this.privateBle = !this.privateBle
          } else if (this.mess.typeEn === 'Public') {
            this.publicBle = true
          }
        }
      },
      onSendMsg: function() { // 发送消息
        const _self = this
        if (!this.isFriend(this.mess.id) && this.mess.type === 'C2C') {
          this.$alert("非好友关系,不能发起聊天")
          return
        }
        sendMsg(this.msgtosend, (res) => {
            _self.msgtosend = ''
          console.log('自己发送的消息');
          let sess
          // 获取所有聊天会话
          let sessMap = webim.MsgStore.sessMap();
          let arr = []
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
          _self.getRecentContacts(arr)
        })
      },
      isFriend(str) { // 判断是否为好友
        const friendObj = this.friendList
        if (friendObj) {
          for (let i = 0; i < friendObj.length; i++) {
            if (friendObj[i].id === str) return true
          }
        }
        return false
      },
      dialogTableVisibleFun: function() {
        this.dialogTableVisible = false
      },
      incrementTotal(strData) { // 表情, 添加到发送消息msg
        this.face = false
        this.msgtosend = this.msgtosend + strData
        this.$refs.editsf.focus()
      },
      removeMsgFun() { // 清空历史消息
        this.mess.msgs.splice(0, this.messages.length)
      },
      onShowEmotionDialog() { // 表情
        this.face = !this.face
        this.imgList = []
        for (let index in webim.Emotions) {
          const objdata = {
            id: webim.Emotions[index][0],
            src: webim.Emotions[index][1],
            style: 'cursor:pointer'
          }
          this.imgList.push(objdata)
        }
      },
      dialogPersonaHiden() {
        this.dialogPersonalDataVisible = false
      },
      getHistory() {
         const self = this
         var options = {
            'Peer_Account': selToID, // 好友帐号
            'MaxCnt': 1, // 拉取消息条数
            'LastMsgTime': self.lastMsgTime, // 最近的消息时间，即从这个时间点向前拉取历史消息
            'MsgKey': self.msgKey
         };
         getLastC2CHistoryMsgs(options, (msg) => {

           for (let i = 0; i < msg.length; i++) {
             self.historyListMsg.unshift(msg[i])
           }
           self.lastMsgTime = getPrePageC2CHistroyMsgInfoMap[selToID].LastMsgTime
           self.msgKey = getPrePageC2CHistroyMsgInfoMap[selToID].MsgKey
         }, (data) => {
          console.log(data)
         })
      },
      timeFun(val, valOld) {

       const valTime = webim.Tool.formatTimeStamp(val.time)
        if (!valOld) {
          return valTime
        }
        if (val.time - valOld.time > 300) {
           return valTime
        } else {
           return ''
        }
      }
    },
    computed: {
      ...mapGetters([
        'friendList',
        'groupList',
        'groupMemberList'
      ]),
      messages: function() {
        return this.mess.msgs.map((item) => {
          return item
        })
      }
    },
    created() {
      if (this.mess.type === 'GROUP') {
        this.getGroupMemberList(this.mess.id)
      }
    }
  }
</script>
<style lang="scss">
  .friendsProfileItem {width: 296px;
       .el-dialog__header {border:none; position:absolute; top:0px; right:0px; z-index:3}
      background:#ffffff; border:1px solid rgba(#bbbbbb,.4); z-index:8;
     border-right:none;border-bottom:none;
     .private-dialog {width:436px;}
    .el-dialog__body {padding:0px}
  }

  .chat-frame {
    position: relative;
    height: 100%;
    .chat-frame-top {
      text-align: center;
      font-size: 18px;
      line-height: 42px;
      height: 42px;
      border-bottom: solid 1px #ccc;
    }
    .chat-frame-main {
      position: absolute;
      top: 42px;
      bottom: 0;
      left: 0;
      right: 0;
    }
    .msg-show-panel {
      padding-top: 10px;
      overflow: auto;
      position: absolute;
      bottom: 150px;
      top: 0;
      left: 0;
      right: 0;
    }
    .msg-send-panel {
      height: 150px;
      border-top: solid 1px #ccc;
      position: absolute;
      bottom: 0;
      left: 0;
      right: 0;
    }
    .editFr {
      height: 115px;
      outline: none;
      padding: 5px;
      font-size: 14px;
      border: 0;
      width: 100%;
      resize: none;
    }
    .button-panel {
      height: 30px;
      background-color: #eee;
      .chat02_title_btn {
        position: relative;
        margin: 3px;
        display: inline-block;
        width: 24px;
        height: 24px;
        background-repeat: no-repeat;
        cursor: pointer;
      }
      .ctb01 {
        background: url('../../assets/img/chat/face.png') no-repeat center center;
      }
      .ctb03 {
        background: url('../../assets/img/chat/photo.png') no-repeat center center;
      }
    }
    .personalData {
      position: relative;
      top: 8px;
      right: 10px;
      float: right;
      width: 16px;
      height: 17px;
      background: url('../../assets/img/chat/dataImg.png') no-repeat;
      cursor: pointer;
    }
    .pulic {
      width: 436px;
      height: 630px;
      background: #aaaaaa;
      .el-dialog__header {
        right: 0px;
        z-index: 6
      }
      .el-dialog__body {
        padding: 0;
      }
    }
    .private {
      background: #ffffff;
    }
    .groupImg {
      width: 22px;
      height: 20px;
      background: url('../../assets/img/chat/groupImg.png') no-repeat;
    }
    .pulic,
    .private {
      .el-dialog__header {
        position: absolute;
        top: 1;
        border-bottom: none;
      }
    }
  }
</style>