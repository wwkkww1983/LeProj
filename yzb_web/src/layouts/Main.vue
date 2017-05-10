<template>
  <div class="jzm-main">
   <!--  <Login></Login> -->
    <header class="app-header" v-if = 'userInfo'>
      <span class="change-company" @click = 'showCompanyList'></span>
      <ul class="company-list" v-show = 'cutData.isShow' v-clickoutside = "hideCutData">
        <li v-for="Company in cutData.CompanyLists" :class="{'now-company': Company.isShow}" @click="changeCompany(Company)">{{Company.comName}}</li>
      </ul>
      <div class="company-name-now">{{userInfo.companyName}}</div>
    </header>
    <aside class="app-left">
        <div class="user-info" v-if = 'userInfo'>
            <img :src="userInfo.avatarUrl" alt="userInfo.nickname">
            <span>{{userInfo.nickname}}</span>
        </div>
        <div class="route-list">
            <ul>
                <router-link tag="li" to="/" class="rl-chat" exact><a>消息</a></router-link>
                <router-link tag="li" to="/IM" class="rl-contacts"><a>通讯录</a></router-link>
                <router-link tag="li" to="/work" class="rl-work"><a>工作</a></router-link>
                <router-link tag="li" to="/admin" class="rl-admin"><a>我的</a></router-link>
                <!-- <router-link tag="li" to="/xiaohua" class="rl-chat"><a>小华测试</a></router-link> -->
            </ul>       
        </div>
    </aside>
    <div class="app-main">
      <router-view></router-view>
    </div>    
  </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import Login from './IM/login'
import clickoutside from 'element-ui/lib/utils/clickoutside'
import {companyList, updUserInCompany} from '../Api/company'
import Yunba from '../utils/notify'
export default {
    name: 'Main',
    components: {Login},
    data () {
        return {
            cutData: {
                CompanyLists: [],
                isShow: false
            }
        }
    },
    directives: {clickoutside},
    created () {
        this.getComInfo(); // 拿初始数据
    },
    methods: {
      ...mapActions([
        'getUserInfo',
        'getComInfo',
        'getSysMess'
      ]),
      hideCutData () {
        this.cutData.isShow = false
      },
      showCompanyList () {
        companyList().then((res) => {
          this.cutData.CompanyLists = res.data.data
          this.cutData.isShow = true // 显示公司列表
        }, (err) => {
          console.log(err);
        })
        // let _self = this
      },
      changeCompany (node) {
        this.cutData.isShow = false
        let data = {
          comId: node.comId,
          comName: node.comName,
          tokenId: ssionStorage.getItem('tokenId')
        }
        updUserInCompany(data).then((res) => {
          sessionStorage.setItem('companyId', data.comId)
          this.getComInfo()
        }, (err) => {
          console.log(err);
        })
      },
      yubanxiaohua (parm) { // 小华测试云吧
          console.log("小华测试云吧")
      }
    },
    computed: {
      ...mapGetters({
          userInfo: 'userInfo'
      })
    },
    beforeRouteEnter (to, from, next) {
      const tokenId = sessionStorage.getItem('id')
      if (!tokenId) {
        next({path: '/member'})
      } else {
        next(Main => {
          Main.getUserInfo((res) => {
            if (res.companyNum > 0) {
              Main.getComInfo()
            }
          })
        })
      }
    },
    mounted () {
      Yunba((res) => {
        this.yubanxiaohua(res);
        // this.getSysMess(res)
        // console.log(res);
      })
    }
}
</script>
<style lang="scss">
.jzm-main {
    .app-header {
        position: relative;
        padding-left: 60px;
        color: #fff;
        height: 64px;
        line-height: 64px;
        // background-color: rgb(10, 191, 171);
        background-color: #4fdccb;
        .company-name-now {
            font-size: 16px;
        }
        .change-company {
            position: absolute;
            width: 20px;
            height: 20px;
            background: url('../assets/img/maincommon/changeCompany.png') no-repeat;
            left: 33px;
            top: 22px;
            cursor: pointer;
        }
        .company-list {
            position: absolute;
            z-index: 999;
            color: #000;
            left: 26px;
            top: 64px;
            background-color: #fff;
            box-shadow: 0 0 8px #888;
            line-height: 48px;
            width: 260px;
            li {
                padding-left: 34px;
                font-size: 14px;
                cursor: pointer;
            }
            li:hover {
                background-color: #e6f8f6;
            }
        }
    }
    .app-left {
        position: absolute;
        width: 126px;
        top: 64px;
        bottom: 0;
        background-color: #333742;
        border-right: #d7d7d7 1px solid;
        .user-info {
            padding: 48px 33px 45px 33px;
            img {
                width: 60px;
                height: 60px;
                border-radius: 60px;
                margin-bottom: 5px;
            }
            span{
                color: #fff;
            }
        }
        .route-list {
            li {
                font-size: 14px;
                color: #777;
                width: 80px;
                margin-left: 27px;
                text-indent: 18px;
                line-height: 24px;
                margin-bottom: 26px;
                text-align: center;
                background-repeat: no-repeat !important;
            }
            a {
                display: block;
                color: #fff;
            }
            .router-link-active {
                a {
                    color: rgb(10, 191, 171);
                }
            }
            .rl-chat {
                background-image: url('../assets/img/maincommon/messageNotSelected.png');
            }
            .rl-contacts {
                background-image: url('../assets/img/maincommon/contactsNotSelected.png')
            }
            .rl-work {
                background-image: url('../assets/img/maincommon/workNotselected.png')
            }
            .rl-admin {
                background-image: url('../assets/img/maincommon/settingsNotSelected.png')
            }
            .rl-chat.router-link-active {
                background-image: url('../assets/img/maincommon/messageSelected.png');
            }
            .rl-contacts.router-link-active {
                background-image: url('../assets/img/maincommon/contactsSelected.png');
            }
            .rl-work.router-link-active {
                background-image: url('../assets/img/maincommon/workSelected.png');
            }
            .rl-admin.router-link-active {
                background-image: url('../assets/img/maincommon/settingsSelected.png');
            }
        }
    }
    .app-main {
        position: absolute;
        top: 64px;
        bottom: 0;
        left: 126px;
        right: 0;
    }
}
</style>