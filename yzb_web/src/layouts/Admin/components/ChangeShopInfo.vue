<template>
  <div class="change-depInfo">
    <el-form label-width="80px" v-if = "shop">
      <el-form-item label="店铺全称">
        <el-input v-model = "shopInfo.fullName"></el-input>
      </el-form-item>
      <el-form-item label="店铺简称">
        <el-input v-model = "shopInfo.shortName"></el-input>
      </el-form-item>
      <el-form-item label="所在区域">
        <AreaSelect ref ='area' area="广东省深圳市龙岗区"></AreaSelect>
      </el-form-item>
      <el-form-item label="具体地址">
        <el-input v-model = "shopInfo.address"></el-input>
      </el-form-item>
      <el-form-item label="联系人">
        <el-input v-model = "shopInfo.linkman"></el-input>
      </el-form-item>
      <el-form-item label="联系电话">
        <el-input v-model = "shopInfo.phone"></el-input>
      </el-form-item>
        <el-form-item class="button-panel">
          <el-button>取消</el-button>
          <el-button @click.native='changeInfo'>完成</el-button>
        </el-form-item>
    </el-form>
  </div>
</template>
<script>
  import AreaSelect from './area'
  import {changeShopInfo} from '../../../Api/shop'
  export default {
    data () {
      return {
        shopInfo: {}
      }
    },
    props: [
      'shop',
      'cb'
    ],
    methods: {
      showInfo () {
        console.log(this.$refs.a.string)
      },
      changeInfo () {
        this.shopInfo.area = this.$refs.area.string
        changeShopInfo(this.shopInfo).then((res) => {
          if (this.cb instanceof Function) this.cb(res)
        }, (err) => {
          console.log(err)
        })
      }
    },
    mounted () {
      this.shopInfo = {
        shopId: this.shop.id,
        fullName: this.shop.fullName,
        shortName: this.shop.shortName,
        address: this.shop.address,
        linkman: this.shop.linkman,
        phone: this.shop.phone,
        groupId: this.shop.groupId
      }
    },
    components: {
      AreaSelect
    }
  }
</script>
<style>
  .change-depInfo {width:420px;padding:20px;}
</style>