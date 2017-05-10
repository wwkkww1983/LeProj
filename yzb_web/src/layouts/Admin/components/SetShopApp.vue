<template>
  <div class="set-shopApp">
    <div class="container-block">
      <p>已选应用</p>
      <Appnode v-for = '(app, index) in preApps' :app='app' @click.native='reduceApp(app, index)'></Appnode>
    </div>
    <div class="container-block">
      <p>可选应用</p>
      <Appnode v-for = '(app, index) in availableApp' :app='app' @click.native='addApp(app, index)'></Appnode>
    </div>
    <el-button @click= "changeApps">确定</el-button>    
  </div>
</template>
<script>
  import {shopManagerApp} from '../../../Api/shop'
  import {editApp} from '../../../Api/myWorkApply'
  import Appnode from '../../../components/App-node'
  export default {
    data () {
      return {
        preApps: [],
        availableApp: [],
        delApplyIds: [],
        newApplyIds: []
      }
    },
    components: {
      Appnode
    },
    methods: {
      init () {
        this.preApps = []
        this.availableApp = []
        this.delApplyIds = []
        this.newApplyIds = []
        let data = {
          shopId: this.$route.params.id,
          shopType: this.appType ? this.appType : 'SHOPMANGER'
        }
        shopManagerApp(data).then((res) => {
          this.availableApp = res.data.data
          for (let x of this.apps) {
            for (let y in this.availableApp) {
              if (x.applyId === this.availableApp[y].applyId) {
                this.addApp(x, y)
              }
            }
          }
          // 初始化清空newApplyIds
          this.newApplyIds = []
        })
      },
      addApp (app, index) {
        this.preApps.push(app);
        // 添加的应用是否存在于删除中
        let newIndex = this.delApplyIds.indexOf(app)
        if (newIndex !== -1) {
          this.delApplyIds.splice(newIndex, 1)
        } else {
          this.newApplyIds.push(app)
        }
        this.availableApp.splice(index, 1)
      },
      reduceApp (app, index) {
        this.availableApp.push(app)
        // 删除的应用是否存在于添加中
        let newIndex = this.newApplyIds.indexOf(app)
        if (newIndex !== -1) {
          this.newApplyIds.splice(newIndex, 1)
        } else {
          this.delApplyIds.push(app)
        }
        this.preApps.splice(index, 1)
      },
      changeApps () {
        let _self = this
        let data = {
          organizeType: 'SHOP',
          degreeType: this.type || '1',
          delApplyIds: this.delApplyIds.map(item => item.applyId).toString(),
          depshopId: this.$route.params.id,
          newApplyIds: this.newApplyIds.map(item => item.applyId).toString()
        }
        editApp(data).then((res) => {
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
    props: [
      'appType',
      'apps',
      'type',
      'cb'
    ],
    created () {
      this.init()
    },
    watch: {
      'apps': function (val, oldVal) {
        console.log('发现数据变化');
        this.init()
      }
    }
  }
</script>