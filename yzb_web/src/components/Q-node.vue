<template>
<div :class="selectBlo ? 'Q-node selectCss' : 'Q-node'" @click="chanAction">
  <i class="node-icon" v-if="unread"></i>
  <div class="selectCss-action" :class="blo ? 'actionCss' : ''"></div>
  <div class="img-fr" :style="{backgroundImage: 'url('+ avatarUrl +')'}">
    <span v-if="!avatarUrl">{{ nodeName }}</span>
  </div>
  <p class="Q-node-name" :class="{largeName: !nodeMessage}">{{ nodeName }} <i class="Q-node-icon" v-if="role === 'Owner'"></i></p>
  <p class="Q-node-message">{{nodeMessage}}</p>
</div>
</template>
<script>
export default {
  data () {
    return {
      msgStr: '',
      firend: false,
      blo: false
    }
  },
  props: [
    'avatarUrl', // 头像
    'nodeName', // 名字
    'nodeMessage', // 最新一条消息
    'unread', // 未读
    'role', // 判断是否为管理
    'selectBlo', // 选框样式
    "id"
  ],
  methods: {
    chanAction: function() { // 选中状态
      this.blo = !this.blo
      this.$emit('addAction', this.blo, this.id)
    }
  },
  created () {

  }
}
</script>
<style lang="scss">
.Q-node{padding:5px 0 5px 60px;line-height:24px;font-size:12px;position:relative;height:60px;border-bottom:solid 1px #aaa;cursor:pointer;
   .selectCss-action {display:none}
  .largeName{line-height:48px;font-size: 16px;}
  .img-fr{background: #0abfab;position:absolute;left:10px;top:10px;width: 40px;height: 40px; border-radius:50%;overflow:hidden;background-size:cover;background-position:center;
    span{font-size:24px;color:#fff;line-height:40px;display:block;text-align: center;}
  }
  .Q-node-message{padding-right:1em;overflow:hidden;text-overflow:ellipsis;white-space: nowrap}
  &:hover{box-shadow:0 0 8px #09c;}
  .node-icon { position:absolute; right:20px; top:20px; width:15px; height:15px; background:rgba(red,.6); border-radius:50%; }
  .Q-node-icon {margin-left:5px; width:16px; height:17px; background:url(../assets/img/chat/dataImg.png) no-repeat; display:inline-block;}
}
.selectCss {
  .node-icon {display:none;}
  .img-fr {left:52px}
  .Q-node-name {text-align:right}
  .selectCss-action {display:block; position:absolute; left:10px; top:18px; width:20px; height:20px; border-radius:50%; border:1px solid #cecece;}
  .actionCss {background:red}
}

</style>