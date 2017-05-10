<template>
  <div class="mycompany-admins">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt; 
      <router-link to="/admin/MyCompany/admins/">未分配人员</router-link>
    </div>    
    <div class="contruct-panel" v-if = "comInfo">
      <div class="panel-header clearfix">
        <div class="fl"><span class="tag-text">未分配人员</span><span class="tag-text"></span>(共{{comInfo.staffList.length}}人)</div>
      </div>
      <div class="panel-body">
        <el-table :data="comInfo.staffList" class="staff-list" style="width:100%">      
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
                <el-dropdown-item @click.native="delStaff(row)">离职员工</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown></div></el-table-column>            
        </el-table>
      </div>      
    </div>
    <el-dialog></el-dialog>
  </div>
</template>
<script>
import {mapGetters, mapActions} from 'vuex'
import {delStaff} from '../../../Api/staff'
// 从公司信息中获取管理员列表
export default {
  data () {
    return {
      staffList: null
    }
  },
  computed: {
    ...mapGetters({
      comInfo: 'comInfo'
    })
  },
  methods: {
    ...mapActions([
      'getComInfo'
    ]),
    delStaff: function (row) {
      let data = {
        userId: row.userId
      }
      delStaff(data).then((res) => {
        console.log(res)
        this.getComInfo()
      })
    }
  },
  mounted () {
    console.log(this.comInfo)
  }
}
</script>