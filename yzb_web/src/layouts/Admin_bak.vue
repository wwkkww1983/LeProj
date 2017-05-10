<template>
  <div class="admin">
    <div class="left-menu">
      <router-link v-for='item in path' :to='item.path' v-if='item.show' tag="div" class='left-menu-item'><a>{{item.name}}</a></router-link>
    </div>
    <div class="right-content">
      <div class="right-top">{{location}}</div>
      <router-view></router-view>
    </div>    
  </div>
</template>
<script>
  import adminPath from '../router/admin'
  export default {
    name: 'Admin',
    computed: {
      path () {
        let show = [0, 1, 2, 3, 4, 5, 6, 7] // 根据权限设置可见内容
        return adminPath.children.map((item, i) => {
          for (let x of show) {
            if (i === x) {
              item.show = true
            }
          }
          return item
        })
      },
      location () {
        for (let item of this.path) {
          if (this.$route.path === item.path) {
            return item.name
          }
        }
      }
    }
  }
</script>
<style lang="scss">
.admin{height:100%;
  .left-menu {height:100%;width:200px;float:left; padding:10px; border-right:solid 1px #ccc;}  
  .left-menu-item {line-height: 48px;font-size: 14px;
    a {display:block;border-bottom: solid 1px #ccc;padding-left:1em;}
  }
  .router-link-active{color:#09c;}
  .right-content {position: absolute;left:200px; right:0;top: 0;bottom: 0;overflow: hidden;}
  .right-top {line-height:50px; text-align: center;font-size:18px;border-bottom:solid 1px #d6d6d6;}
}
</style>