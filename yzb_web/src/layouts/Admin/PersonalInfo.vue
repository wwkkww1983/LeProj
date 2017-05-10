<template>
  <div class="user_info">
    <div class="uf_main_left">
      <div class="uf_main_left_code">
        <div class="uf_main_left_code_item"><span>姓名</span><span>王微微</span></div>
        <div class="uf_main_left_code_item"><span>性别</span><span>女</span></div>
        <div class="uf_main_left_code_item"><span>电话</span><span>15112578907</span></div>
        <div class="uf_main_left_code_item"><span>邮箱</span><span>2854656456@qq.com</span></div>
        <div class="uf_main_left_code_item"><span>所在地</span><span>广东省深圳市南山区</span></div>
        <div class="uf_main_left_code_item"><span>详细地址</span><span>南山路华润置地大厦E座</span></div>
      </div>
    </div>
    <div class="uf_main_right">
      <SelectImagePreview useType="更换头像" @up="uploadHandler"></SelectImagePreview>
    </div>
  </div>
</template>

<script>
  import SelectImagePreview from './components/SelectImagePreview'

  export default {
    data () {
      return {}
    },
    components: {
      SelectImagePreview
    },
    methods: {
      uploadHandler (data) {
        console.log(data);
      }
    }
  }
</script>

<style lang="scss" scoped>
  .user_info {
    width: 100%;
    clear: both;
    .uf_main_left {
      width: 65%;
      float: left;
      .uf_main_left_code {
        background-color: #f6f7f8;
        border-radius: 8px;
        width: 90%;
        height: 100%;
        margin-left: 10%;
        padding: 3% 0 3% 0;
        .uf_main_left_code_item {
          height: 50px;
          line-height: 50px;
          color: #999;
          font-size: 14px;
          span:first-child {
            width: 22%;
            display: inline-block;
            padding-left: 7%;
          }
          span:last-child {
            width: 78%;
            display: inline-block;
            cursor: pointer;
            padding-left: 5%;
          }
        }
      }
    }
    .uf_main_right {
      width: 35%;
      float: right;
    }
  }
</style>
