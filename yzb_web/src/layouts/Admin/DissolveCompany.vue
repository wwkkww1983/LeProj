<template>
  <div class="dissolveCompany">
    <section>
      <img src="./../../assets/img/set/dissolve.png">
      <h3>全称：{{userInfo.companyName}}</h3>
      <p>简称：周大福</p>
      <el-button class="dissolve-btn" @click="dissolve">解散公司</el-button>
      <h5>解散公司后，我们金掌门软件会免费为您保留2个月的数据，2个月之后会全部清空，请你妥善处理。</h5>
    </section>
  </div>
</template>
<script>
  import {
    mapGetters
  } from 'vuex'
  export default {
    data() {
      return {}
    },
    computed: {
      ...mapGetters({
        userInfo: 'userInfo'
      })
    },
    methods: {
      dissolve() {}
    }
  }
</script>
<style lang="scss">
  .dissolveCompany {
    width: 100%;
    section {
      margin: 100px auto 0;
      width: 340px;
      img {
        position: relative;
        left: 0;
        right: 0;
        margin: auto;
        display: block;
        width: 130px;
        height: 130px;
        border-radius: 50%;
        overflow: hidden;
      }
      h3 {
        text-align: center;
        height: 16px;
        margin: 20px 0 14px 0;
      }
      p {
        text-align: center;
        height: 16px;
        margin-bottom: 16px;
      }
      h5 {
        text-align: center;
        color: #f74c31;
        line-height: 16px;
        margin: 18px 0 0 0;
      }
    }
    .dissolve-btn{display:block;margin: auto}
  }
</style>
