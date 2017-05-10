<template>
  <div class="addShop">
    <div class="mycompany-title">
      <router-link to="/admin/Mycompany/" exact>我的公司</router-link> &gt; 
      <router-link to="/admin/MyCompany/addShop/">我的店铺</router-link>
    </div>
    <div class="contruct-panel">
      <div class="panel-header clearfix">
        <div class="fl">开通店铺付费</div>
      </div>
      <div class="panel-body addShop-body">
        <div class="addShop-section">
          <p class="panel-body-title">选择开通方式</p>
          <el-row :gutter="25" class="clearfix">
            <el-col :span='8'>
              <div class="shopCard" @click = 'type = 1' :class="{'isactive': type === 1}">
                <div class="shopCard-top clearfix">
                  <div class="fl">一次买断</div>
                  <div class="pull-right">￥2988.00元</div>
                </div>
                <div class="shopCard-content">
                  <p>一次购买,永久免费使用</p>
                  <p>30天内无理由退款</p>
                </div>
              </div>
            </el-col>
            <el-col :span="8">
              <div class="shopCard" @click = 'type = 2' :class="{'isactive': type === 2}">
                <div class="shopCard-top clearfix">
                  <div class="fl">按年续费</div>
                  <div class="pull-right">￥1200.00元/年</div>
                </div>
                <div class="shopCard-content">
                  <p>连续购买三年后,可永久免费使用</p>
                  <p>30天内无理由退款</p> 
                </div>             
              </div>
            </el-col>
            <el-col :span="8">
              <div class="shopCard" @click = 'type = 3' :class="{'isactive': type === 3}">
                <div class="shopCard-top clearfix">
                  <div class="fl">免费使用</div>
                  <div class="pull-right">查看名单</div>
                </div>
                <div class="shopCard-content">
                  <p>申请永久免费使用</p>
                  <p>剩余名额：{{freeShopNum}}</p>
                  <p>共300名</p>
                </div>
              </div>
            </el-col>
          </el-row>
        </div>
        <div class="addShop-section">
          <p class="panel-body-title">选择开通的店铺数</p>
          <el-input-number v-model="shopCount" :min="1" :max="99"></el-input-number>
        </div>
        <div class="addShop-section">
          <p class="panel-body-title">选择付费方式</p>
          <div class="payType clearfix">
            <div class="payType-card">
              <i></i>支付宝支付
            </div>
          </div>
        </div>
        <div>
          <el-button class="btn-block pay_btn" @click.native = 'create'>立即开通</el-button>
          <p>点击上面的"立即开通"即表示您同意<a href="#">《服务协议》</a>和<a href="#">《隐私条款》</a></p>
        </div>
      </div>
    </div>    
  </div>
</template>
<script>
  import {getFreeShop, createShopForFree, createShop} from '../../../Api/pay'
  import pingpp from 'pingpp-js'
  export default {
    data () {
      return {
        freeShopNum: 1,
        shopCount: 1,
        type: 1
      }
    },
    methods: {
      create () {
        if (this.type === 3) {
          createShopForFree().then((res) => {
            console.log(res);
          }, (err) => {
            console.log(err);
          })
        } else {
          let data = {
            shopNum: this.shopCount
          }
          createShop(data).then((res) => {
            console.log(res);
            pingpp.createPayment(res.data.data, (res, err) => {
              if (err) console.log(err)
              console.log(res);
            })
          }, (err) => {
            console.log(err);
          })
        }
      }
    },
    created () {
      getFreeShop().then((res) => {
        this.freeShopNum = res.data.data
      }, (err) => {
        console.log(err);
      })
    }
  }
</script>
<style lang="scss"> 
  .addShop{
    .addShop-body{padding:20px;text-align: left;}
    .shopCard{border:solid 1px #c0ccda;height:160px;padding:15px;}
    .isactive{box-shadow: 0 0 8px #09c;}
    .shopCard-selected {}
    .addShop-section {margin-bottom:25px;
      .panel-body-title{margin-bottom:10px;}
    }
    .shopCard-top {font-size:16px;font-weight: bold;color:#475669;margin-bottom:15px;}
    .shopCard-content {font-size:14px;line-height:2}
    .payType{margin-bottom:55px;}
    .payType-card{width:150px;border:solid 1px #c0ccda;padding:8px;float: left;margin-right:25px; border-radius:5px;
      i{border: solid 1px #c0ccda;border-radius: 5px;display:inline-block;height:32px;width:32px;vertical-align: middle;margin-right:10px;}
    }
    .pay_btn{background-color: #c0ccda;width:300px;line-height:36px;font-size:18px;font-weight: bold;margin-bottom: 10px;}
  }
</style>