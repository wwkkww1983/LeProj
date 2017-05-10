<template>
<div id="app">
  <router-view></router-view>
</div>
</template>
<script>
export default {
  created () {
    Notification.requestPermission() // 请求获取Notification权限
  }
}
</script>
<style lang="scss">
@import "./assets/css/common.scss"
</style>
