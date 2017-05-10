<template>
  <div class="area-select">
    <el-row :gutter='0'>
      <el-col :span='8'>
        <el-select v-model='p' placeholder="请选择">
          <el-option
            v-for="item in pArea"
            :label="item.name"
            :value="item">
          </el-option>      
        </el-select>
      </el-col>
      <el-col :span='8'>
        <el-select v-model='c' placeholder="请选择">
          <el-option
            v-for="item in cArea"
            :label="item.name"
            :value="item">
          </el-option>      
        </el-select>        
      </el-col>
      <el-col :span='8'>
        <el-select v-model='d' placeholder="请选择">
          <el-option
            v-for="item in dArea"
            :label="item.name"
            :value="item">
          </el-option>      
        </el-select>
      </el-col>
    </el-row>
  </div>
</template>
<script>
  export default {
    data () {
      return {
        pArea: null,
        p: {},
        c: {},
        d: {}
      }
    },
    computed: {
      cArea () {
        if (typeof this.p.city !== 'undefined') return this.p.city
        else {
          return {}
        }
      },
      dArea () {
        if (typeof this.c.district !== 'undefined') return this.c.district
        else {
          return {}
        }
      },
      string () {
        let p = this.p.name || ''
        let c = this.c.name || ''
        let d = this.d.name || ''
        if (c === '区域') c = ''
        return p + c + d
      }
    },
    created () {
      let _self = this
      this.$http.get('/static/address_data.json').then((res) => {
        _self.pArea = res.data.province
      })
    }
  }
</script>
