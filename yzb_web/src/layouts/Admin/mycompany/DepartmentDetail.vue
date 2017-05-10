<template>
  <div class="department-detail" v-if="depInfo">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt; 
      <router-link :to="'/admin/MyCompany/department/'+ depInfo.id">{{depInfo.name}}</router-link>
    </div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl" v-if="depInfo"><span class="tag-text">{{depInfo.name}}</span><span class="tag-text">{{depInfo.departmentCode}}</span>(共{{depInfo.employeeCount}}人)</div>
        <div class="pull-right">
          <el-dropdown>
            <el-button>添加员工<i class="el-icon-caret-bottom el-icon--right"></i></el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item @click.native="addStaffV = true">单个添加</el-dropdown-item>
              <el-dropdown-item @click.native="addStaffsV = true">批量添加</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
          <el-button @click='addDepartmentV = true'>添加部门</el-button>
          <el-button><router-link :to = "'/admin/MyCompany/departmentAdmin/'+depInfo.id">部门设置</router-link></el-button>
        </div>
      </div>
      <div class="panel-body">
        <el-table v-if = "depConstruct" :data="staffs" class="staff-list" style="width:100%">      
          <el-table-column
            inline-template
            label="头像"
          ><div><img :src="row.avatarUrl" alt="" class = "avatar"></div></el-table-column>          
          <el-table-column
            prop = "nickName"
            label = "姓名"
          ></el-table-column>
          <el-table-column
            prop = "sex"
            label = "性别"
          ></el-table-column>
          <el-table-column
            prop = "phone"
            label = "手机"
          ></el-table-column>
          <el-table-column
            inline-template
            label = "操作"
          ><div><el-dropdown>
              <el-button>编辑应用</el-button>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item>编辑应用</el-dropdown-item>
                <el-dropdown-item>停职员工</el-dropdown-item>
                <el-dropdown-item @click.native ='delStaff(row)'>离职员工</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown></div></el-table-column>            
        </el-table>
      </div>
    </div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl"><span class="tag-text">子部门</span></div>
      </div>
      <div class="panel-body">
        <el-table v-if = "depConstruct" :data="depConstruct.childDepList" class="staff-list" style="width:100%">
            <el-table-column
              prop="name"
              label="部门名称"
            ></el-table-column>
            <el-table-column
              prop="departmentCode"
              label="编号"
            ></el-table-column>
        </el-table>
      </div>
    </div>
    <el-dialog v-model="addDepartmentV" title="添加部门" size="auto">
      <AddDepartment :dep = "depInfo"></AddDepartment>
    </el-dialog>
    <el-dialog v-model="addStaffV" title="添加员工" size="auto">
      <AddStaff :dep = "depInfo"></AddStaff>
    </el-dialog>
    <el-dialog v-model="addStaffsV" title = "批量添加员工">
      <AddStaffs :dep="depInfo" type="1"></AddStaffs>
    </el-dialog>
  </div>
</template>
<script>
  import {mapActions} from 'vuex'
  import AddDepartment from '../components/AddDepartment' // 添加部门
  import AddStaff from '../components/AddStaff' // 单个添加员工
  import AddStaffs from '../components/AddStaffs' // 批量添加员工
  import {getDepShopInfo} from '../../../Api/company'
  import {delStaff} from '../../../Api/staff'
  export default {
    data () {
      return {
        depInfo: null,
        depConstruct: null,
        staffs: null,
        addDepartmentV: false,
        addStaffV: false,
        addStaffsV: false
      }
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ]),
      delStaff: function (row) {
        let data = {
          type: '1',
          shopDepId: this.depInfo.id,
          groupId: this.depInfo.groupId,
          userId: row.userId
        }
        delStaff(data).then((res) => {
          console.log(res)
          this.updDepInfo()
        })
      },
      updDepInfo () {
        let depId = this.$route.params.id
        this.getComInfo((res) => {
          this.depInfo = res.depList.filter(item => item.id === depId)[0]
        })
        let _self = this
        let data = {
          depShopId: depId
        }
        getDepShopInfo(data).then((res) => {
          _self.depConstruct = res.data.data
          _self.staffs = [..._self.depConstruct.manger, ..._self.depConstruct.admin, ..._self.depConstruct.staffList]
        })
      }
    },
    components: {
      AddDepartment,
      AddStaff,
      AddStaffs
    },
    mounted () {
      this.updDepInfo()
    }
  }
</script>
<style lang="scss">
.staff-list {text-align:left;
  .avatar {display:block;height:42px;width:42px; margin: 5px 0;border-radius:50%;}
}
</style>