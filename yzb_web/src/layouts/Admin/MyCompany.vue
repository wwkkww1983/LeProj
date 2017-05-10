<template>
  <div class="company_info">
    <div class="cp_main_left">
      <div class="cp_main_left_code">
        <div class="cp_main_left_code_item"><span>公司全称</span><span>周大福珠宝</span></div>
        <div class="cp_main_left_code_item"><span>联系人</span><span>王总</span></div>
        <div class="cp_main_left_code_item"><span>电话</span><span>15112578907</span></div>
        <div class="cp_main_left_code_item"><span>所在地</span><span>广东省深圳市南山区</span></div>
        <div class="cp_main_left_code_item"><span>详细地址</span><span>南山路华润置地大厦E座</span></div>
        <div class="create_btn" @click="createCompany">+ 创建公司</div>
      </div>
    </div>
    <div class="cp_main_right">
      <SelectImagePreview useType="更换Logo" @up="uploadHandler"></SelectImagePreview>
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
      createCompany () {
        alert('Create Company');
      },
      uploadHandler (data) {
        console.log(data);
      }
    }
  }
</script>

<style lang="scss" scoped>
  .company_info {
    width: 100%;
    clear: both;
    .cp_main_left {
      width: 65%;
      float: left;
      .cp_main_left_code {
        background-color: #f6f7f8;
        border-radius: 8px;
        width: 90%;
        height: 100%;
        margin-left: 10%;
        padding: 3% 0 3% 0;
        .cp_main_left_code_item {
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
        .create_btn {
          width: 22%;
          height: 6%;
          line-height: 44px;
          background-color: #FFAA33;
          color: #ffffff;
          font-size: 16px;
          font-weight: bold;
          text-align: center;
          cursor: pointer;
          border-radius: 3px;
          margin: 15px 0 15px 7%;
        }
      }
    }
    .cp_main_right {
      width: 35%;
      float: right;
    }
  }
</style>
