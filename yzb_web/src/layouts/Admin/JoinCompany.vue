<template>
  <div class="joinCompany">
    <ul class="seek-type clearfix">
      <li :class="{'on-seek' : isSeek}" @click="onSeek()">
        <p>查找公司</p>
      </li>
      <li :class="{'on-seek' : !isSeek}" @click="onSeek()">
        <p>激请码加入</p>
      </li>
    </ul>
    <div class="seek-input">
      <div class="seek-input-inner">
        <div v-show="isSeek">
          <el-row :gutter="20">
            <el-col :span="8" :offset="6">
              <el-input placeholder="输入公司编码" v-model="comCode" @keyup.native.enter="seekCompany"></el-input>
            </el-col>
            <el-col :span="2">
              <el-button>搜索公司</el-button>
            </el-col>
          </el-row>
        </div>
        <div v-show="!isSeek">
          <el-row :gutter="20">
            <el-col :span="8" :offset="6">
              <el-input placeholder="输入邀请码" v-model="comCode" @keyup.native.enter="seekCompany"></el-input>
            </el-col>
            <el-col :span="2">
              <el-button>加入公司</el-button>
            </el-col>
          </el-row>
        </div>
      </div>
    </div>
    <section>
      <ul>
        <li v-for="companyData in companyDatas">
          <div><img src="./../../assets/img/set/xiaohua.png">{{companyData.fullName}}</div>
          <div><span>{{companyData.comType}}</span><a href="javascript:void(0)" @click="addCompany(companyData)">申请加入</a></div>
        </li>
      </ul>
    </section>
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
        isSeek: true, // 搜索类型
        comCode: null, // 搜索的输入
        companyDatas: {} // 搜索到的公司信息
      }
    },
    computed: {
      ...mapGetters({
        userInfo: 'userInfo',
        comInfo: 'comInfo'
      })
    },
    methods: {
      ...mapActions([
        'getComInfo'
      ]),
      onSeek() {
        this.isSeek = !this.isSeek;
        this.companyDatas = {};
      },
      seekCompany() {
        this.companyDatas = null;
        this.$http.get('http://119.29.138.104:9082/v1/company/findCompany/' + this.comCode + '/' + sessionStorage.getItem("tokenId")).then((response) => {
          this.companyDatas = response.data.data;
        }, (response) => {
          alert('搜索公司失败!!!');
        })
      },
      addCompany() {
        let url = urlId + ':9082/v1/company/agreeJoin' // eslint-disable-line
        let data = { // eslint-disable-line
          comId: '',
          comShopDepName: '',
          shopDepId: 0,
          tokenId: sessionStorage.getItem(tokenId),
          type: '3',
          userId: sessionStorage.getItem(id),
          userName: this.userInfo.nickname,
          validationMessage: ''
        }
      }
    },
    mounted() {
      this.getComInfo((res) => {
        console.log(res);
      })
    }
  }
</script>
<style lang="scss">
  .joinCompany {
    .seek-type {
      line-height: 40px;
      li {
        float: left;
        width: 50%;
        border-bottom: solid 1px #666;
        text-align: center;
        p {
          border-bottom: solid 2px transparent
        }
      }
      li:first-child {
        border-right: solid 1px #666;
      }
      .on-seek {
        border-bottom: solid 1px #0abfab;
        p {
          border-bottom: solid 2px #0abfab
        }
      }
    }
    .seek-input-inner {
      padding: 150px 15px 0;
    }
  }

</style>
