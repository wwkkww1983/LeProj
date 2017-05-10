<template>
  <div class="mycompany-admins">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt; 
      <router-link to="/admin/MyCompany/admins/">系统管理员</router-link>
    </div>    
    <div class="contruct-panel" v-if = 'adminList'>
      <div class="panel-header clearfix">
        <div class="fl"><span class="tag-text">系统管理员组</span><span class="tag-text"></span>(共{{adminList.comAdminList.length}}人)</div>
        <div class="pull-right">
            <el-button @click.native = 'addSysAdminV = true'>添加系统管理员</el-button>
        </div>
      </div>
      <div class="panel-body">
        <el-table v-if = "adminList" :data="adminList.comAdminList" class="staff-list" style="width:100%">      
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
              <el-button>取消管理员</el-button>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item @click.native = 'delAdmin(row)'>取消管理员</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown></div></el-table-column>            
        </el-table>
      </div>      
    </div>
    <el-dialog size='auto' v-model="addSysAdminV" title="添加系统管理员">
      <AddSysAdmin></AddSysAdmin>
    </el-dialog>
  </div>
</template>
<script>
import AddSysAdmin from '../components/AddSysAdmin'
import {delSystemAdmin} from '../../../Api/authChange'
import {mapActions} from 'vuex'
export default {
  data () {
    return {
      addSysAdminV: false
    }
  },
  components: {
    AddSysAdmin
  },
  computed: {
    adminList: function () {
      return this.$store.getters.comInfo
    }
  },
  methods: {
    ...mapActions([
      'getComInfo'
    ]),
    delAdmin (row) {
      let data = {
        userId: row.userId
      }
      delSystemAdmin(data).then((res) => {
        console.log(res);
        this.getComInfo()
      }, (err) => {
        console.log(err);
      })
    }
  },
  created () {
    console.log(this.$store.getters.comInfo);
  }
}
</script>