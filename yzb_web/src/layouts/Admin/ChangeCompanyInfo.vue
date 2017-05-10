<template>
<div class="changeCompanyInfo">
  <el-row>
      <el-col :span="14" :offset="5">
          <el-form :model="companyInfo" label-width="120px" v-if="comInfo">
              <el-form-item label="公司全称">
                  <el-input v-model = 'companyInfo.name' placeholder="不多于15个字"></el-input>
              </el-form-item>
              <el-form-item label="公司简称">
                  <el-input v-model = 'companyInfo.shortName' placeholder="用于打印报表表头"></el-input>
              </el-form-item>
              <el-form-item>
                  <el-button class="create-btn" @click="changeCompanyInfo" :disabled = '!allowChange'>修改</el-button>
              </el-form-item>
          </el-form>
      </el-col>
  </el-row>
</div>
</template>
<script>
  import {
    mapGetters,
    mapActions
  } from 'vuex'
  export default {
    data() {
      return {
        companyInfo: {
          comId: '',
          tokenId: sessionStorage.getItem('tokenId'),
          shortName: '',
          name: ''
        },
        allowChange: false
      }
    },
    computed: {
      ...mapGetters({
        comInfo: 'comInfo'
      })
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ]),
      changeCompanyInfo() {
        let data = encodeURI(JSON.stringify(this.companyInfo))
        let url = INTERFACE_URL + ':9082/v1/company/modify?json=' + data
        this.$http.post(url).then((res) => {
          this.$alert('修改成功！')
        }, (err) => {
          console.log(err);
        })
      }
    },
    created() {
      const _self = this;
      this.getComInfo((res) => {
        _self.companyInfo.comId = res.company.id
        _self.companyInfo.name = res.company.name
        _self.companyInfo.shortName = res.company.shortName
        _self.$watch('companyInfo',
          function(newVal, oldVal) {
            if (!_self.allowChange) _self.allowChange = true
            return
          }, {
            deep: true
          })
      });
    },
    mounted() {
      console.log('mounted')
      console.log(this.comInfo)
    }
  }
</script>
<style>
  .changeCompanyInfo {
    margin-top: 100px;
  }
</style>