<template>
  <div class="Containers">
    <div class="Containers-box">
      <div class="block">
        <div class="block-title" v-if="!personalDetails">Excel表格导入</div>
        <div class="item-1" v-if="!personalDetails">
          <div class="info-1">第一步：<a href="http://192.168.5.110:9082/v1/excel/staffExcelExport" class="file">下载模板</a></div>
          <div class="info-2">请根据导入的模板模式整理需要导入的数据</div>
        </div>
        <div class="item-1">
          <div class="info-1">第二步：
            <div class="file">选中导入的文件</div><input @change="addFile($event)" value="" type="file"></div>
          <div class="info-2">导入后将免费向新增加的员工发送邀请短信</div>
        </div>
      </div>
      <div class="but">
        <button class="but-1" @click="addq_bool.addDqs=false">取消</button>
        <button class="but-1" @click="addData">导入数据</button>
      </div>
      <div class="res-panel" v-if="personalDetails">
        <h3>已发送消息</h3>
        <el-table :data="personalDetails.pushInvitedSuccess">
          <el-table-column
            label = '姓名'
            prop = 'nickName'
          ></el-table-column>
          <el-table-column
            label = '电话'
            prop = 'phone'
          ></el-table-column>
          <el-table-column
            label = '岗位'
            prop = 'job'
          ></el-table-column>
        </el-table>
      </div>      
    </div>
  </div>
</template>
<script>
  import {mapGetters} from 'vuex'
  import {inviteStaffs} from '../../../Api/staff'
  export default {
    data() {
      return {
        stallBool: false, // 批量添加返回的显示视图
        fileData: "", // 文件数据
        localStroage: JSON.parse(sessionStorage.getItem("data")), // 获得个人资料数据
        personalDetails: null // 向后台接口发送的个人数据
      }
    },
    prop: [
      'dep',
      'type'
    ],
    methods: {
      ...mapGetters({
        userInfo: 'userInfo'
      }),
      addFile: function(event) {
        console.log('a')
        let obj = event.currentTarget;
        let _self = this
        this.fileData = {
          excelFile: obj.files[0],
          depShopId: _self.dep ? _self.dep.id : '0',
          depShopName: _self.dep ? _self.dep.name : "0",
          msg: '部门邀请',
          type: _self.type
        }
      },
      addData: function() { // 向后台发送文件及数据
        let _self = this
        this.$http.options.emulateJSON = true; // 请求编码为application / json,方便后台读取文件
        if (!this.fileData) {
          alert("请添加Excel文件！！！")
          return false;
        }
        inviteStaffs(this.fileData).then((res) => {
          _self.personalDetails = res.data.data
          console.log(_self.personalDetails)
        })
      }
    }
  }
</script>
<style scoped lang="scss">
  a:hover,
  a:focus {
    text-decoration: none;
  }
  .stallObj {
    .boxItem {
      margin-bottom: 30px;
      .title {
        padding-top: 15px;
        height: 45px;
        font-size: 18px;
        line-height: 1;
      }
      .table {
        border: 1px solid #666666;
      }
      .item {
        height: 35px;
        .info:nth-of-type(1) {
          width: 395px;
        }
        .info:nth-of-type(2) {
          width: 290px;
        }
        .info:nth-of-type(3) {
          width: 290px;
        }
        .info {
          padding-left: 30px;
          float: left;
          height: 35px;
          line-height: 35px;
          text-align: left;
          background: #f5f2e6;
        }
      }
    }
  }
  .Containers-box {
    height: 537px;
  }
  .title {
    height: 55px;
    padding: 24px 0px 0px 30px;
    line-height: 1;
    font-size: 18px;
    font-weight: bold;
  }
  
  .block {
    padding-top: 32px;
    .item-1 {
      height: 110px;
      position: relative;
      .info-1 {
        position: relative;
        width: 250px;
        height: 50px;
        margin: 0px auto;
        line-height: 26px;
        font-size: 14px;
      }
      input[type="file"] {
        position: absolute;
        left: 60px;
        top: 0px;
        opacity: 0;
        width: 124px;
        height: 30px;
        cursor: pointer;
      }
      .file {
        display: inline-block;
        width: 124px;
        height: 30px;
        border: 1px solid #bbbbbb;
        line-height: 30px;
        text-align: center;
        border-radius: 5px;
        color: #000000;
        cursor: pointer;
      }
      .info-2 {
        font-size: 12px;
        line-height: 1;
        width: 250px;
        margin: 0px auto;
      }
    }
  }
  
  .block-title {
    height: 75px;
    line-height: 1;
    text-align: center;
    font-size: 17px;
    font-weight: bold;
  }
  
  .but {
    height: 30px;
    text-align: right;
    padding-right: 17px;
    margin-bottom: 20px;
    .but-1 {
      width: 95px;
      height: 30px;
      font-size: 12px;
      border: 1px solid #bbbbbb;
      border-radius: 5px;
      background: #ffffff;
      text-align: center;
      line-height: 30px;
    }
    .but-1:last-of-type {
      background: #78909c;
      color: #ffffff;
    }
  }
</style>