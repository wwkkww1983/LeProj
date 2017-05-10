<template>
  <div class="create-company">
    <el-row>
      <el-col :span="14" :offset="5">
        <el-form :model="companyInfo" label-width="120px">
          <el-form-item label="公司全称">
            <el-input v-model='companyInfo.comName' placeholder="不多于15个字"></el-input>
          </el-form-item>
          <el-form-item label="公司简称">
            <el-input v-model='companyInfo.shortName' placeholder="用于打印报表表头"></el-input>
          </el-form-item>
          <el-form-item label="联系人姓名">
            <el-input v-model='companyInfo.contactName'></el-input>
          </el-form-item>
          <el-form-item label="联系电话">
            <el-input v-model='companyInfo.contactPhone'></el-input>
          </el-form-item>
          <el-form-item label="公司所在地区">
            <el-row :gutter="5" v-if="areaData">
              <el-col :span="7">
                <el-select placeholder="省" v-model="area.a1">
                  <el-option v-for="item in areaData.sheng" :label="item.name" :value="item"></el-option>
                </el-select>
              </el-col>
              <el-col :span="7">
                <el-select placeholder="市" v-model="area.a2">
                  <el-option v-for="item in areaData.shi" :label="item.name" :value="item"></el-option>
                </el-select>
              </el-col>
              <el-col :span="7">
                <el-select placeholder="县/区" v-model="area.a3">
                  <el-option v-for="item in areaData.xian" :label="item.name" :value="item"></el-option>
                </el-select>
              </el-col>
            </el-row>
          </el-form-item>
          <el-form-item label="详细地址">
            <el-input v-model='companyInfo.detailAddress'></el-input>
          </el-form-item>
          <el-form-item>
            <el-button class="create-btn" @click="createCompany">创建</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>
<script>
  export default {
    data() {
      return {
        companyInfo: {
          comName: '',
          shortName: '',
          contactName: '',
          contactPhone: '',
          area: '',
          detailAddress: '',
          tokenId: sessionStorage.getItem('tokenId')
        },
        areaData: {
          sheng: null,
          shi: null,
          xian: null
        },
        areas: null,
        area: {
          a1: '',
          a2: '',
          a3: ''
        }
      }
    },
    methods: {
      createCompany() {
        let data = this.companyInfo
        let url = INTERFACE_URL + ':9082/v1/company/'
        this.$http.post(url, data).then((res) => {
          this.$alert('创建成功！')
        }, (err) => {
          console.log(err);
        })
      }
    },
    watch: {
      'area': {
        handler: function (n, o) {
          let _self = this
          console.log(n, o)
          console.log(_self.area.a1.name, _self.area.a2.name, _self.area.a3.name)
          this.areaData.shi = _self.areas.filter((item) => {
            return (item.level === 2 && item.sheng === _self.area.a1.sheng)
          })
          this.areaData.xian = _self.areas.filter((item) => {
            return (item.level === 3 && item.sheng === _self.area.a1.sheng && item.di === _self.area.a2.di)
          })
        },
        deep: true
      }
    },
    mounted () {
      let _self = this
      this.$http.get('/static/js/area.json').then((res) => {
        _self.areas = res.data
        _self.areaData.sheng = res.data.filter(item => item.level === 1)
      })
    }
  }
</script>
<style lang="scss">
  .create-company {
    position: relative;
    height: 410px;
    transform: translateY(25%);
    .create-btn {
      display: block;
      width: 100%;
      background-color: #0abfab;
    }
  }

</style>
