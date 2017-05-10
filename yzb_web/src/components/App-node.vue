<template>
<div class="app-node">
  <img :src="app.applyLogo" alt="app.applyName">
  <p>{{app.applyName}}</p>
</div>
</template>
<script>
  export default {
    props: [
      'app'
    ]
  }
</script>
<style lang="scss">
.app-node{display: inline-block;text-align: center;width: 76px;
  img {width:50px;height: 50px;display: block;margin:auto;}
}
</style>