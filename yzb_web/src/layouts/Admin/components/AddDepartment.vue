<template>
<div class="addDepartment">
  <p>部门名称</p>
  <el-input v-model="depName"></el-input>
  <p>部门应用</p>
  <div class="appContainer">
    <Appnode v-for="(app, index) in apps" :app = 'app' @click.native = 'reduceApp(app, index)'></Appnode>
  </div>
  <div class="allApps">
    <p>可用应用</p>
    <div class="container-block">
      <Appnode v-for="(app, index) in notUsedApp" :app = 'app' @click.native = 'addApp(app, index)' ></Appnode>
    </div>
  </div>
  <div class="addDepartment-control-panel">
    <el-row :gutter="20"> 
      <el-col :span="8" :offset="4">
        <el-button class="btn-block" size="large">取消</el-button>
      </el-col>
      <el-col :span="8">
        <el-button class="btn-block" size="large" @click = 'createDepartment'>确定</el-button>
      </el-col>
    </el-row>    
  </div>
</div>
</template>
<script>
import Appnode from '../../../components/App-node'
import {createDep} from '../../../Api/department.js'

export default {
  data () {
    return {
      depName: '',
      apps: [],
      notUsedApp: []
    }
  },
  components: {
    Appnode
  },
  methods: {
    addApp (app, index) {
      this.apps.push(app);
      this.notUsedApp.splice(index, 1)
    },
    reduceApp (app, index) {
      this.notUsedApp.push(app);
      this.apps.splice(index, 1)
    },
    createDepartment () {
      let data = {
        name: this.depName,
        parentId: this.dep ? this.dep.id : '0',
        applyId: this.apps.map(item => item.applyId).toString()
      }
      createDep(data).then((res) => {
        console.log(res);
        if (this.ok) {
          if (this.ok instanceof Function) {
            this.ok(res)
          }
        }
      })
    },
    getApps () {
      let _self = this
      let depId = this.dep ? this.dep.id : '0'
      let tokenId = sessionStorage.getItem('tokenId')
      let allAppUrl = INTERFACE_URL + `:9082/v1/department/showCreateDepartmentApply/${tokenId}/${depId}`
      let defAppUrl = INTERFACE_URL + `:9082/v1/department/defDepartmentApply/${tokenId}/${depId}`
      this.$http.get(allAppUrl).then((res) => {
        _self.notUsedApp = res.data.data
        _self.$http.get(defAppUrl).then((res2) => {
          for (let x of res2.data.data) {
            for (let y in _self.notUsedApp) {
              if (x.applyId === _self.notUsedApp[y].applyId) {
                _self.addApp(x, y)
              }
            }
          }
        })
      })
    }
  },
  props: [
    'dep',
    'ok'
  ],
  watch: {
    'dep': function () {
      console.log('b')
      getApps()
    }
  },
  mounted () {
    console.log('a')
    this.getApps();
  }
}
</script>
<style lang="scss">
.addDepartment {
  width: 500px;
  .appContainer {
    padding:10px;
    border: 1px solid #C0CCDA;
    border-radius: 4px;
  }
  .allApps{
    .container-block {
      padding:10px;
      border: 1px solid #C0CCDA;
      border-radius: 4px;
    }
  }
  .addDepartment-control-panel{
    margin-top: 15px;
    text-align: center;
  }
}
</style>