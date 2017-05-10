<template>
  <div class="company-construct mycompany-detail" v-if = 'comInfo'>
    <div class="mycompany-title">我的公司</div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl">{{comInfo.company.name}}(共{{comInfo.company.employeeCount}}人)</div>
      </div>
      <div class="panel-body" v-if="comInfo">
        <el-row class="mc-header">
          <el-col :span="4">名称</el-col>
          <el-col :span="4">编号</el-col>
          <el-col :span="4">人数</el-col>
          <el-col :span="4">主管(店长)</el-col>
          <el-col :span="4">管理员1</el-col>
          <el-col :span="4">管理员2</el-col>
        </el-row>
        <el-row class="mc-header">
          <el-col :span="4"><router-link to='admins'>系统管理员</router-link></el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4">{{comInfo.comAdminList.length}}人</el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4"></el-col>
        </el-row>
        <el-row class="mc-header">
          <el-col :span="4"><router-link to='unallocated'>未分配人员</router-link></el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4">{{comInfo.staffList.length}}人</el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4"></el-col>
          <el-col :span="4"></el-col>
        </el-row>
      </div>
    </div>    
    <div class="contruct-panel" v-if = 'comInfo.depList.length > 0'>
      <div class="panel-header clearfix">
        <div class="fl">部门(共{{comInfo.depList.length}}个)</div>
      </div>
      <div class="panel-body">
        <el-row class="mc-header" v-for="item in comInfo.depList" v-if = 'item.manger'>
          <el-col :span="4">
            <router-link :to="'/admin/MyCompany/department/'+item.id">{{item.name}}</router-link>
          </el-col>
          <el-col :span="4">{{item.departmentCode}}</el-col>
          <el-col :span="4">{{item.employeeCount}}人</el-col>
          <el-col :span="4"><span v-if="item.manger[0]">{{item.manger[0].nickName}}</span><span v-else>未设置</span></el-col>
          <el-col :span="4"><span v-if="item.admin.length >= 1">{{item.admin[0].nickName}}</span><span v-else>未设置</span></el-col>
          <el-col :span="4"><span v-if="item.admin.length === 2">{{item.admin[1].nickName}}</span><span v-else>未设置</span></el-col>          
        </el-row>
      </div>      
    </div>
    <div class="contruct-panel" v-if = 'comInfo.shopList.length > 0'>
      <div class="panel-header clearfix">
        <div class="fl">店铺(共{{comInfo.shopList.length}}个)</div>
      </div>
      <div class="panel-body">
        <el-row class="mc-header" v-for="item in comInfo.shopList" v-if = 'item.manger'>
          <el-col :span="4">
            <router-link :to="'/admin/MyCompany/shop/'+item.id">{{item.fullName}}</router-link>
          </el-col>
          <el-col :span="4">{{item.shopCode}}</el-col>
          <el-col :span="4">{{item.shopCount}}人</el-col>
          <el-col :span="4"><span v-if="item.manger[0]">{{item.manger[0].nickName}}</span><span v-else>未设置</span></el-col>
          <el-col :span="4"><span v-if="item.admin.length >= 1">{{item.admin[0].nickName}}</span><span v-else>未设置</span></el-col>
          <el-col :span="4"><span v-if="item.admin.length === 2">{{item.admin[1].nickName}}</span><span v-else>未设置</span></el-col>         
        </el-row>
      </div>      
    </div>      
  </div>
</template>
<script>
  import {mapGetters, mapActions} from 'vuex'

  export default {
    data () {
      return {
      }
    },
    computed: {
      ...mapGetters([
        'comInfo'
      ])
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ])
    },
    created () {
      console.log("哈哈")
      // console.log(this.recentContacts);
    }
  }
</script>
<style lang="scss">
.company-construct{
  line-height: 32px;
  .mycompany-title {
    padding-left: 25px;
    border-bottom: solid 1px #888
  }  
  .mycompany-detail {
    .el-col {height: 38px;}
  }
  .contruct-panel {
    img {width: 50px;height: 50px;display:block;margin: 10px auto;border-radius:50%;}
  }    
  .panel-header {
    padding-left: 25px;
    border-bottom: solid 1px #888;
    background-color: #ccc;
    .tag-text {margin-right:.5em;}
    .el-button {padding: 5px 10px;}
  }
  .panel-body {
    text-align: center;
    .mc-header{
      border-bottom:solid 1px #888;
      line-height: 38px;
    }
  }
}    
</style>