<template>
<div class="msg-node clearfix" :class="{isSend: msg.isSend}">
    <div class="timeBox">
        {{time}}
    </div>
    <div class="msgSed">
        <div class="avator" :style="{backgroundImage:'url('+urlImg+')'}"></div>
        <div class="msg-right">
            <div class="msg-from-name" v-if="!msg.isSend && type === 'GROUP'">{{msg.fromAccountNick}}</div>
            <pre ref="msgContent" class="msg-node-body"></pre>
        </div>
    </div>
</div>
</template>
<script>
import convertMessage from "../../IM/message/convertMessage"

export default {
  data () {
    return {
      content: null,
      urlImg: ""
    }
  },
  props: [
    'msg',
    'avatarUrl',
    'type',
    'time'
  ],
  watch: {
    'msg': function (oV, nV) {
      const _self = this;
      this.content = convertMessage(this.msg)
      this.urlImg = this.msg.avatarUrl
      _self.$refs.msgContent.innerHTML = this.content
    }
  },
  methods: {
     reader: function () {

     }
  },
  mounted: function () {
    !loginInfo.headurl ? loginInfo.headurl = require('../../assets/img/chat/showChatObjMessage.png') : ''
    this.urlImg = this.msg.isSend ? loginInfo.headurl : this.avatarUrl
    this.content = convertMessage(this.msg)
    this.$refs.msgContent.innerHTML = this.content
  }
}
</script>
<style lang="scss">
.msg-node {position: relative;width:100%;margin-bottom:12px;
  .avator{width:32px;height:32px;overflow: hidden;float:left;margin:0 10px; border-radius:50%; background:#78cdf8 center;background-size:cover;}
  .msg-right {float:left;max-width:80%;
    .msg-node-body{background-color: #78cdf8;padding:8px;border-radius:8px;}
    .msg-from-name{font-size: 12px;line-height: 1;margin-bottom:8px;color:#333;}
    * {max-width:100%;}
  }
  .timeBox {color:#9f978c; text-align:center; line-height:1;}
  &.isSend{
    .avator{float: right;}
    .msg-right {float:right;}
  }
}
</style>