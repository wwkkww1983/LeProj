<template>
  <div class="main_header">{{currentTitle}}</div>
</template>

<script>
  export default {
    data () {
      return {}
    },
    props: ['currentTitle']
  }
</script>

<style lang="scss" scoped>
  .main_header {
    width: 100%;
    height: 120px;
    line-height: 120px;
    padding-left: 70px;
    font-size: 36px;
    color: #666;
  }
</style>
