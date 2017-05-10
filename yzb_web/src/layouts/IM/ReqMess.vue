<template>
<div class="req-mess">
  <el-row>
    <el-col :span="8" class="req-mess-content">
      <div class="message-body">
        {{msgContent.userName}} {{msgContent.msgTitle}}
      </div>
      <div class="control-panel">
        <el-button @click ="accept(msgContent)">接受</el-button>
        <el-button>拒绝</el-button>
      </div>
    </el-col>
  </el-row>
</div>
</template>
<script>
export default {
  name: 'ReqMess',
  props: [
    'msg',
    'accept'
  ],
  computed: {
    msgContent: function () {
      return JSON.parse(this.msg.msg)
    }
  }
}
</script>
<style lang="scss">
.req-mess{margin-bottom:15px;
  .message-body{margin-bottom: 10px}
  .req-mess-content{padding:8px;border:solid 1px #888; border-radius:8px;}
}
</style>