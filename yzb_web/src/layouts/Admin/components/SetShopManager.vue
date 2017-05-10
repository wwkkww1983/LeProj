<template>
  <div class="setDepManager">
    <p>当前店铺主管：<span v-if="current">{{current.nickName}}</span><span v-else>未设置</span></p>
    <el-input class="search-panel" placeholder="搜索手机号码/好友昵称"></el-input>
    <Qnode v-for='staff in staffs' :class="{isSelected: selected === staff}" @click.native='selecte(staff)' :nodeName="staff.nickName"
      :img="staff.avatarUrl" :nodeMessage="'性别: ' + staff.sex"></Qnode>
    <div class="control-panel">当前选择：<span v-if="selected">{{selected.nickName}}</span><span v-else>未选择</span>
      <el-button class="pull-right" @click='setManager'>设置</el-button>
    </div>
  </div>
</template>
<script>
  import {shopStaff, setShopManager} from '../../../Api/authChange'
  import Qnode from '../../../components/Q-node'
  export default {
    data() {
      return {
        staffs: null,
        selected: null
      }
    },
    props: [
      'current',
      'type'
    ],
    components: {
      Qnode
    },
    methods: {
      selecte: function(item) {
        this.selected = item
      },
      setManager: function() {
        let _self = this
        if (!_self.selected) {
          _self.$message.error('请先选择一个员工')
          return false
        }
        let data = {
          shopId: this.$route.params.id,
          userId: _self.selected.userId,
          preAdminId: _self.current ? _self.current.userId : '0',
          type: _self.type ? _self.type : '5'
        }
        setShopManager(data).then((res) => {
          console.log(res)
        })
      }
    },
    mounted() {
      let _self = this
      let data = {
        shopId: this.$route.params.id
      }
      // 获取店铺员工
      shopStaff(data).then((res) => {
        _self.staffs = res.data.data.map((item) => {
          switch (item.sex) {
            case 1:
              item.sex = '男'
              break;
            case 2:
              item.sex = '女'
              break;
            case 3:
              item.sex = '保密'
              break;
          }
          return item
        })
      })
    }
  }
</script>
<style lang="scss">
  .setDepManager {
    width: 300px;
    .search-panel {
      margin-bottom: 15px;
    }
    .isSelected {
      border: solid 2px;
    }
    .control-panel {
      margin: 15px -20px 0;
      padding: 15px 20px 0;
      border-top: solid 1px #ccc
    }
  }

</style>
