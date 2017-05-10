<template>
  <div class="logout">
    <el-button @click.native = 'logout'>登出</el-button>
  </div>
</template>
<script>
  export default {
    data () {
      return {}
    },
    methods: {
      logout () {
        sessionStorage.clear();
        this.$router.push('/member')
      }
    }
  }
</script>