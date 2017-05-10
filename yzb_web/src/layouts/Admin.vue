<template>
  <div class="admin">
    <div class="admin-main">
      <div class="admin_left_side_nav">
        <div v-for="(navItem, index) in navDataList" class="admin_left_side_nav_item" @click="navControl(index)" :class="{active: navShowSwitch===index}">
          <img :src="navItem.iconSrc">
          <span>{{navItem.type}}</span>
        </div>
      </div>
      <div class="admin_right_side_detail">
        <AdminMainHeader :currentTitle="currentTitle"></AdminMainHeader>
        <keep-alive><router-view></router-view></keep-alive>
      </div>
    </div>
  </div>
</template>

<script>
  import AdminMainHeader from './Admin/components/AdminMainHeader'

  export default {
    data () {
      return {
        navDataList: [
          {type: '个人信息', iconSrc: 'static/img/succeed.png'},
          {type: '我的公司', iconSrc: 'static/img/succeed.png'},
          {type: '店铺管理', iconSrc: 'static/img/succeed.png'},
          {type: '密码设置', iconSrc: 'static/img/succeed.png'}
        ],
        navShowSwitch: 0
      }
    },
    computed: {
      currentTitle: function () {
        return this.navDataList[this.navShowSwitch].type;
      }
    },
    components: {
      AdminMainHeader
    },
    methods: {
      navControl (op_value) {
        this.navShowSwitch = op_value;
        switch (op_value) {
          case 0:
            this.$router.push('/admin/personalInfo');
            break;
          case 1:
            this.$router.push('/admin/myCompany');
            break;
          case 2:
            this.$router.push('/admin/shopManage');
            break;
          case 3:
            this.$router.push('/admin/pawdSetting');
            break;
          default:
            this.$router.push('/admin');
            break;
        }
      }
    },
    created () {},
    mounted () {
      //document.getElementsByClassName("admin_left_side_nav_item")[0].setAttribute('class', 'admin_left_side_nav_item active');
      //document.getElementsByClassName("admin_left_side_nav_item")[3].click();
    }
  }
</script>

<style lang="scss" scoped>
  .admin {
    padding-top: 40px;
    width: 100%;
    height: 100%;
    overflow: hidden;
    background-color: #dddddd;
    .admin-main {
      width: 73%;
      height: 82%;
      margin: 0 auto;
      background-color: #ffffff;
      clear: both;
      .admin_left_side_nav {
        width: 20%;
        height: 100%;
        float: left;
        .admin_left_side_nav_item {
          width: 100%;
          height: 110px;
          text-align: center;
          font-size: 14px;
          cursor: pointer;
          color: #666;
          img {
            width:42px;
            height:42px;
            margin-top: 25px;
          }
          span {
            display:block;
          }
        }
        .admin_left_side_nav_item.active {
          background-color: #f0fdfb;
          color: #4fdcca;
          position: relative;
          &::before {
            content: "";
            position: absolute;
            width: 5px;
            height: 110px;
            top: 0;
            left: 0;
            background-color: #4fdcca;
          }
        }
      }
      .admin_right_side_detail {
        width: 80%;
        height: 100%;
        border-left: 1px solid #cccccc;
        float: right;
      }
    }
  }
</style>
