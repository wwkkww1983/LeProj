<template>
  <div class="appSetPanel">
    <p>已选应用</p>
    <div class="appContainer">
      <Appnode v-for="(app, index) in preApps" :app='app' @click.native='reduceApp(app, index)'></Appnode>
    </div>
    <div class="allApps">
      <p>可用应用</p>
      <div class="container-block">
        <Appnode v-for="(app, index) in notUsedApp" :app='app' @click.native='addApp(app, index)'></Appnode>
      </div>
    </div>
    <!--测试用-->
    <el-row v-if="editPanelV">
      <el-col :span="12">
        <p>添加的应用</p>
        <Appnode v-for="(app, index) in newApplyIds" :app='app'></Appnode>
      </el-col>
      <el-col :span="12">
        <p>删除的应用</p>
        <Appnode v-for="(app, index) in delApplyIds" :app='app'></Appnode>
      </el-col>
    </el-row>
    <el-button @click= "changeApps">确定</el-button>
  </div>
</template>
<script>
  import {depAvailableApp, editDepApp} from '../../../Api/department'
  import Appnode from '../../../components/App-node'
  export default {
    data () {
      return {
        preApps: [],
        notUsedApp: null,
        delApplyIds: [],
        newApplyIds: [],
        editPanelV: true
      }
    },
    props: [
      'id',
      'parentId',
      'type',
      'apps',
      'availableApp',
      'cb'
    ],
    methods: {
      init: function () {
        let _self = this
        function setApp () {
          for (let x of _self.apps) {
            for (let y in _self.notUsedApp) {
              if (x.applyId === _self.notUsedApp[y].applyId) {
                _self.addApp(x, y)
              }
            }
          }
          // 初始化清空newApplyIds
          _self.newApplyIds = []
        }
        let parentId = this.parentId || '0'
        let type = this.type || '1'
  
        if (this.availableApp && type !== 1) {
            _self.notUsedApp = [...this.availableApp]
            setApp()
        } else {
          depAvailableApp({depId: parentId}).then((res) => {
            _self.notUsedApp = res.data.data
            setApp()
          })
        }
      },
      addApp: function (app, index) {
        this.preApps.push(app);
        // 添加的应用是否存在于删除中
        let newIndex = this.delApplyIds.indexOf(app)
        console.log('i', newIndex)
        if (newIndex !== -1) {
          this.delApplyIds.splice(newIndex, 1)
        } else {
          this.newApplyIds.push(app)
        }
        this.notUsedApp.splice(index, 1)
      },
      reduceApp: function (app, index) {
        this.notUsedApp.push(app)
        // 删除的应用是否存在于添加中
        let newIndex = this.newApplyIds.indexOf(app)
        if (newIndex !== -1) {
          this.newApplyIds.splice(newIndex, 1)
        } else {
          this.delApplyIds.push(app)
        }
        this.preApps.splice(index, 1)
      },
      changeApps: function () {
        let _self = this
        let data = {
          degreeType: this.type || '1',
          delApplyIds: this.delApplyIds.map(item => item.applyId).toString(),
          depshopId: this.id,
          newApplyIds: this.newApplyIds.map(item => item.applyId).toString()
        }
        console.log(this.type)
        editDepApp(data).then((res) => {
          _self.$message({
            message: '设置完成',
            type: 'success'
          })
         // 设置成功，清空数据
          _self.delApplyIds = []
          _self.newApplyIds = []
          if (_self.cb) _self.cb()
        })
      }
    },
    components: {
      Appnode
    },
    mounted () {
      let _self = this
      this.init()
      this.$watch('availableApp', function () {
        _self.init()
      })
    }
  }
</script>