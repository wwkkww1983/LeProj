<template>
  <div class="department-admin" v-if="depInfo">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt;
      <router-link :to="'/admin/MyCompany/department/'+ depInfo.id">{{oldDepName}}</router-link> &gt;
      <router-link :to="'/admin/MyCompany/departmentAdmin/'+ depInfo.id">部门设置</router-link>
    </div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl">部门设置</div>
      </div>
    </div>
    <el-row>
      <el-col :span="12" :offset="6">
        <div class="department-admin-block">
          <div class="block-title clearfix">
            <div class="fl"><span class="title-name">当前部门信息</span></div>
            <div class="pull-right"><span @click="editDepName" v-if="!depNameState">编辑</span><span @click="changeDepName" v-else>完成</span></div>
          </div>
          <div class="block-content">
            <el-form label-width="80px">
              <el-form-item label="部门名称:" class="row">
                <span v-if="!depNameState">{{depInfo.name}}</span>
                <el-input v-else type="text" v-model="depInfo.name"></el-input>
              </el-form-item>
            </el-form> 
          </div>
        </div>
        <div class="department-admin-block">
          <div class="block-title">部门应用：
            <div class="pull-right"><span class="edit-btn" @click="visible.depSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <Appnode v-for="(app, index) in depManagerApp" :app = 'app' @click.native = 'reduceApp(app, index)'></Appnode>
          </div>
          <div class="block-content">
            部门主管
            <div class="pull-right" @click="visible.setManagerV = true">
              <template v-if="depInfo.manger[0]"><span>{{depInfo.manger[0].nickName}}</span><span>[设置]</span></template>
              <span v-else>未设置</span>
            </div>
          </div>
        </div>
        <div class="department-admin-block">
          <div class="block-title">部门管理应用：
            <div class="pull-right"><span class="edit-btn" @click="visible.adminSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <Appnode v-for="(app, index) in depAdminApp" :app = 'app' @click.native = 'reduceApp(app, index)'></Appnode>
          </div>
          <div class="block-content">
            部门管理员1
            <div class="pull-right" @click="visible.setAdminOneV = true">
              <template v-if="depInfo.admin[0]"><span>{{depInfo.admin[0].nickName}}</span><span>[设置]</span></template>
              <span v-else>未设置</span>
            </div>
          </div>
          <div class="block-content" @click="visible.setAdminTwoV = true">
            部门管理员2
            <div class="pull-right">
              <template v-if="depInfo.admin[1]"><span>{{depInfo.admin[1].nickName}}</span><span>[设置]</span></template>
              <span v-else>未设置</span>
            </div>
          </div>          
        </div>
        <div class="department-admin-block">
          <div class="block-title">员工应用：
            <div class="pull-right"><span class="edit-btn" @click="visible.staffSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <Appnode v-for="(app, index) in depStaffApp" :app = 'app' @click.native = 'reduceApp(app, index)'></Appnode>
          </div>
        </div>             
      </el-col>
    </el-row>
    <el-dialog title="部门应用设置" v-model="visible.depSetV">
      <AppSetPanel :id="depInfo.id" :parentId="depInfo.parentId" :apps = 'depManagerApp' :cb = 'rescb'></AppSetPanel>
    </el-dialog>
    <el-dialog title="部门管理员应用设置" v-model="visible.adminSetV">
      <AppSetPanel :id="depInfo.id" :type ="2" :availableApp = 'depManagerApp' :apps = 'depAdminApp' :cb = 'rescb'></AppSetPanel>
    </el-dialog>
    <el-dialog title="部门员工应用设置" v-model="visible.staffSetV">
      <AppSetPanel :id="depInfo.id" :type ="3" :availableApp = 'depManagerApp' :apps = 'depStaffApp' :cb = 'rescb'></AppSetPanel>
    </el-dialog>  
    <el-dialog title="设置部门主管" v-model="visible.setManagerV" size='auto'>
      <SetDepManager :current="depInfo.manger[0]" type="3"></SetDepManager>
    </el-dialog>
    <el-dialog title="设置部门管理员一" v-model="visible.setAdminOneV" size='auto'>
      <SetDepManager :current="depInfo.admin[0]" type="4"></SetDepManager>
    </el-dialog>
    <el-dialog title="设置部门管理员二" v-model="visible.setAdminTwoV" size='auto'>
      <SetDepManager :current="depInfo.admin[1]" type="4"></SetDepManager>
    </el-dialog>        
  </div>
</template>
<script>
  import {changeDepName, depSetInfo} from '../../../Api/department'
  import {getConstruct, getDepShopInfo} from '../../../Api/company'
  import {mapActions} from 'vuex'
  import Appnode from '../../../components/App-node'
  import AppSetPanel from '../components/appSetPanel'
  import SetDepManager from '../components/setDepManager'
  export default {
    data() {
      return {
        depInfo: null,
        oldDepName: '',
        depNameState: 0,
        depAdminApp: null,
        depManagerApp: null,
        depStaffApp: null,
        visible: {
          depSetV: false,
          adminSetV: false,
          staffSetV: false,
          setManagerV: false,
          setAdminOneV: false,
          setAdminTwoV: false
        }
      }
    },
    components: {
      Appnode,
      AppSetPanel,
      SetDepManager
    },
    methods: {
      ...mapActions(['getComInfo']),
      init: function () { // 初始化
        const _self = this
        this.updateDepInfo()
        getDepShopInfo({
          depShopId: this.$route.params.id
        }).then((res) => {
          console.log('部门详情', res)
        })
        depSetInfo({
          departmentId: this.$route.params.id
        }).then((res) => {
          _self.depAdminApp = res.data.data.depAdminApply
          _self.depManagerApp = res.data.data.depMangerApply
          _self.depStaffApp = res.data.data.depDefStaffApply
          console.log(res)
        })
      },
      editDepName: function () { // 部门名称可编辑状态
        this.depNameState = 1
      },
      rescb: function () { // 设置完成后的回调
        this.init()
      },
      updateDepInfo: function () { // 重新获取comInfo，并得到depInfo数据
        let depId = this.$route.params.id
        this.getComInfo((res) => {
          this.depInfo = res.depList.filter(item => item.id === depId)[0]
          console.log(this.depInfo);
          this.oldDepName = this.depInfo.name
        })
      },
      changeDepName: function () { // 部门名称有改动则提交部门名称修改
        this.depNameState = 0
        let _self = this
        if (this.depInfo.name !== this.oldDepName) {
          let data = {
            departmentId: this.depInfo.id,
            name: this.depInfo.name,
            groupId: this.depInfo.groupId
          }
          changeDepName(data).then((res) => {
              var obj = {
                  "companyId": sessionStorage.getItem('companyId'),
                  "tokenId": sessionStorage.getItem('tokenId'),
                  "type": 'ORGANIZATION' // 组织架构：OGRANIZATION； 公司管理: COMMANAGE
              }
              getConstruct(obj).then((res) => {
                sessionStorage.setItem("construct", JSON.stringify(res.data.data))
                _self.updateDepInfo()
              })
          })
        }
      }
    },
    mounted() {
      this.init()
    }
  }
</script>
<style lang="scss">
.department-admin{
  .department-admin-block{
    .edit-btn{cursor: pointer;}
    .row{margin-bottom:0;}
  }
  .block-content{border:solid 1px #888;padding:10px;margin-bottom:10px;border-radius:5px;}
}
</style>