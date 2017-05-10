<template>
  <div class="mycompany-detail" v-if = 'comInfo'>
    <div class="mycompany-title">我的公司</div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl">{{comInfo.company.name}}(共{{comInfo.company.employeeCount}}人)</div>
        <div class="pull-right">
          <el-dropdown>
            <el-button>添加员工<i class="el-icon-caret-bottom el-icon--right"></i></el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item @click.native="addStaffV = true">单个添加</el-dropdown-item>
              <el-dropdown-item @click.native="addStaffsV = true">批量添加</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
          <el-button @click='addDepartmentV = true'>添加部门</el-button>
          <el-button><router-link to="addShop">添加店铺</router-link></el-button>
        </div>
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
    <el-dialog v-model="addDepartmentV" title="添加部门" size="auto">
      <AddDepartment :ok ='addOk'></AddDepartment>
    </el-dialog>
    <el-dialog v-model="addStaffV" title="添加员工" size="auto">
      <AddStaff :company="comInfo"></AddStaff>
    </el-dialog>
    <el-dialog v-model="addStaffsV" title="批量添加员工">
      <AddStaffs></AddStaffs>
    </el-dialog>    
  </div>
</template>
<script>
  import {mapGetters, mapActions} from 'vuex'
  import AddStaff from '../components/AddStaff' // 单个添加员工
  import AddStaffs from '../components/AddStaffs' // 批量添加员工
  import AddDepartment from '../components/AddDepartment' // 添加部门

  export default {
    data () {
      return {
        addDepartmentV: false,
        addStaffV: false,
        addStaffsV: false
      }
    },
    components: {
      AddStaff,
      AddStaffs,
      AddDepartment
    },
    computed: {
      ...mapGetters([
        'comInfo'
      ])
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ]),
      addOk: function() {
        let _self = this
        this.$alert('部门添加成功', {
          callback: () => {
            _self.addDepartmentV = false
            _self.addStaffV = false
            _self.addStaffsV = false
          }
        })
        this.getComInfo()
      }
    }
  }
</script>
<style lang="scss">
  .mycompany-detail {
    .el-col {height: 38px;}
  }
</style>