<template>
  <div class="department-admin" v-if="depInfo">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt;
      <router-link :to="'/admin/MyCompany/shop/'+ depInfo.id">{{depInfo.shortName}}</router-link> &gt;
      <router-link :to="'/admin/MyCompany/shopAdmin/'+ depInfo.id">店铺设置</router-link>
    </div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl">店铺设置</div>
      </div>
    </div>
    <el-row>
      <el-col :span="12" :offset="6">
        <div class="department-admin-block">
          <div class="block-title clearfix">
            <div class="fl"><span class="title-name">当前店铺信息</span></div>
            <div class="pull-right"><span @click="visible.depInfoSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <el-form label-width="80px">
              <el-form-item label="店铺全称:" class="row">
                <span>{{depInfo.fullName}}</span>
              </el-form-item>
              <el-form-item label="店铺简称:" class="row">
                <span>{{depInfo.shortName}}</span>
              </el-form-item>
              <el-form-item label="所在地区:" class="row">{{depInfo.area}}</el-form-item>
              <el-form-item label="具体地址:" class="row">{{depInfo.address}}</el-form-item>
              <el-form-item label="联系人:" class="row">{{depInfo.linkman}}</el-form-item>
              <el-form-item label="联系电话:" class="row">{{depInfo.phone}}</el-form-item>
            </el-form> 
          </div>
        </div>
        <div class="department-admin-block">
          <div class="block-title">店铺应用：
            <div class="pull-right"><span class="edit-btn" @click="visible.depSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <Appnode v-for="(app, index) in managerApp" :app = 'app'></Appnode>
          </div>
          <div class="block-content">
            店铺主管
            <div class="pull-right" @click="visible.setManagerV = true">
              <template v-if="depInfo.manger[0]"><span>{{depInfo.manger[0].nickName}}</span><span>[设置]</span></template>
              <span v-else>未设置</span>
            </div>
          </div>
        </div>
        <div class="department-admin-block">
          <div class="block-title">店铺管理应用：
            <div class="pull-right"><span class="edit-btn" @click="visible.adminSetV = true">编辑</span></div>
          </div>
          <div class="block-content">
            <Appnode v-for="(app, index) in adminApp" :app = 'app'></Appnode>
          </div>
          <div class="block-content">
            店铺管理员1
            <div class="pull-right" @click="visible.setAdminOneV = true">
              <template v-if="depInfo.admin[0]"><span>{{depInfo.admin[0].nickName}}</span><span>[设置]</span></template>
              <span v-else>未设置</span>
            </div>
          </div>
          <div class="block-content" @click="visible.setAdminTwoV = true">
            店铺管理员2
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
            <Appnode v-for="(app, index) in defStaffApp" :app = 'app'></Appnode>
          </div>
        </div>             
      </el-col>
    </el-row>
    <el-dialog title="店铺信息设置" v-model="visible.depInfoSetV" size='auto'>
        <ChangeShopInfo :shop="depInfo" :cb = 'rescb'></ChangeShopInfo>
    </el-dialog>
    <el-dialog title="店铺应用设置" v-model="visible.depSetV">
      <SetShopApp :id="depInfo.id" :parentId="depInfo.parentId" :apps = 'managerApp' :cb = 'rescb'></SetShopApp>
    </el-dialog>
    <el-dialog title="店铺管理员应用设置" v-model="visible.adminSetV">
      <SetShopApp :id="depInfo.id" :type ="2" appType='SHOPADMIN' :apps = 'adminApp' :cb = 'rescb'></SetShopApp>
    </el-dialog>
    <el-dialog title="店铺员工应用设置" v-model="visible.staffSetV">
      <SetShopApp :id="depInfo.id" :type ="3" appType='SHOPDEFSTAFF' :apps = 'defStaffApp' :cb = 'rescb'></SetShopApp>
    </el-dialog>  
    <el-dialog title="设置店铺主管" v-model="visible.setManagerV" size='auto'>
      <SetDepManager :current="depInfo.manger[0]" type="5"></SetDepManager>
    </el-dialog>
    <el-dialog title="设置店铺管理员一" v-model="visible.setAdminOneV" size='auto'>
      <SetDepManager :current="depInfo.admin[0]" type="6"></SetDepManager>
    </el-dialog>
    <el-dialog title="设置店铺管理员二" v-model="visible.setAdminTwoV" size='auto'>
      <SetDepManager :current="depInfo.admin[1]" type="6"></SetDepManager>
    </el-dialog>
  </div>
</template>
<script>
  import {shopSetInfo} from '../../../Api/shop'
  import {mapActions} from 'vuex'
  import Appnode from '../../../components/App-node'
  import SetDepManager from '../components/setShopManager'
  import ChangeShopInfo from '../components/ChangeShopInfo'
  import SetShopApp from '../components/SetShopApp'
  export default {
    data() {
      return {
        depInfo: null,
        managerApp: null,
        adminApp: null,
        defStaffApp: null,
        visible: {
          depInfoSetV: false,
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
      SetDepManager,
      ChangeShopInfo,
      SetShopApp
    },
    methods: {
      ...mapActions(['getComInfo']),
      updDepInfo () {
        console.log('aaa');
        let depId = this.$route.params.id
        this.getComInfo((res) => {
          this.depInfo = res.shopList.filter(item => item.id === depId)[0]
        })
        let data = {
          depShopId: depId
        }
        // 相关应用列表
        shopSetInfo(data).then((res) => {
          this.adminApp = res.data.data.shopAdminApply
          this.managerApp = res.data.data.shopMangerApply
          this.defStaffApp = res.data.data.shopDefStaffApply
        })
      },
      rescb (res) {
        this.updDepInfo()
      }
    },
    created () {
      this.updDepInfo()
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