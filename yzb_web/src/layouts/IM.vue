<template>
  <div class="im">
    <div class="im-main">
      <div class="left_side">
        <div class="left_side_search">
          <input type="text" class="search" placeholder="通过条件查找" />
        </div>
        <div class="left_side_nav">
          <div v-for="(item, idx) in leftSideNavData">
            <div class="left_side_nav_item" :class="{active: activeName === item.name}" @click="clickHandler(item, idx)">
              <img :src="item.imgSrc">
              <span>{{item.name}}</span>
              <span style="position:absolute;right:60px;top:50%;margin-top:-12px;">{{item.numbers}}</span>
              <img style="position:absolute;right:22px;top:50%;margin-top:-5px;" v-if="item.children" src="../assets/img/Contact/icon-2.png" @click.stop="clickSmHandler(item, idx)" class="arrow" :class="{active: childrenShowStatus[idx].isOpenShow}">
            </div>
            <div v-if="childrenShowStatus[idx].hasChildren && childrenShowStatus[idx].isOpenShow" v-for="(cdnItem, indx) in item.children" class="left_side_nav_item_cdn" @click.stop="clickCdnHandler(cdnItem)" :class="{active: cdnActiveName === cdnItem.name}">
              <img :src="cdnItem.imgSrc" width="35px" height="35px">
              <span>{{cdnItem.name}}</span>
              <span style="float:right;margin-right:25px;">{{cdnItem.numbers}}</span>
            </div>
          </div>
        </div>
      </div>
      <div class="right_side">
        <div class="right_side_title">{{activeName?activeName:cdnActiveName}}<span @click="addFriend">+ 新增好友</span></div>
        <div class="right_side_concat_list">
          <div v-for="(list, index) in currentNavItemData" class="concat_list_item">
            <span style="width:10%;"><img :src="list.headImgSrc"></span><span style="width:20%;">{{list.name}}</span><span style="width:10%;">{{list.sex}}</span><span style="width:25%;">{{list.phone}}</span><span style="width:15%;">{{list.role}}</span><span style="width:10%;" @click="opMore"><img src="../assets/img/set/default/setAdmin.png"></span><span style="font-weight:bold;cursor:pointer;width:10%;text-align:left;" @click="opMore">. . .</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        activeName: '',
        cdnActiveName: '',
        leftSideNavData: [
          {name: '新的好友', numbers: 20, imgSrc: 'static/img/staffDefaultImg.png'},
          {name: '我的好友', numbers: 32, imgSrc: 'static/img/staffDefaultImg.png'},
          {name: '周大福珠宝', numbers: 27, imgSrc: 'static/img/staffDefaultImg.png'},
          {
            name: '店铺',
            numbers: 3,
            imgSrc: 'static/img/staffDefaultImg.png',
            children: [
              {name: '水贝店A', numbers: 12, imgSrc: 'static/img/staffDefaultImg.png'},
              {name: '老街店A', numbers: 13, imgSrc: 'static/img/staffDefaultImg.png'},
              {name: '坂田店A', numbers: 12, imgSrc: 'static/img/staffDefaultImg.png'}
            ]
          }
        ],
        childrenShowStatus: [],
        navItemDataList: [
          [
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '周大福Boss', sex: '男', phone: '13112345678', role: '超级管理员', isFriend: true},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '周大福小艾', sex: '男', phone: '13112345679', role: '管理员', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '周大福掌柜的', sex: '女', phone: '13112345670', role: '店长', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '周大福小希', sex: '女', phone: '13112345672', role: '店员', isFriend: true}
          ],
          [
            {headImgSrc: 'static/img/staffDefaultImg.png', name: 'Boss', sex: '男', phone: '13188886784', role: '超级管理员', isFriend: true},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '小艾', sex: '男', phone: '13119999679', role: '管理员', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '掌柜的', sex: '女', phone: '13122225670', role: '店长', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: '小希', sex: '女', phone: '13116666792', role: '店员', isFriend: true}
          ],
          [
            {headImgSrc: 'static/img/staffDefaultImg.png', name: 'BigBoss', sex: '男', phone: '15112578321', role: '超级管理员', isFriend: true},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: 'Sm小艾', sex: '男', phone: '13123499679', role: '管理员', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: 'Borid掌柜的', sex: '女', phone: '13136635670', role: '店长', isFriend: false},
            {headImgSrc: 'static/img/staffDefaultImg.png', name: 'Alice小希', sex: '女', phone: '13177788002', role: '店员', isFriend: true}
          ]
        ],
        currentNavItemData: []
      }
    },
    created () {
      for (let i = 0; i < this.leftSideNavData.length; i++) {
        let d = {};
        if (this.leftSideNavData[i].children) {
          d.hasChildren = true;
          d.isOpenShow = false;
        } else {
          d.hasChildren = false;
          d.isOpenShow = false;
        }
        this.childrenShowStatus.push(d);
      }
    },
    methods: {
      addFriend () {
          alert('Add Friend..');
      },
      clickHandler (item, index) {
        this.activeName = item.name;
        this.cdnActiveName = '';

        this.currentNavItemData = this.navItemDataList[index];
      },
      clickCdnHandler (cdnItem) {
        this.cdnActiveName = cdnItem.name;
        this.activeName = '';
      },
      clickSmHandler (item, index) {
        this.activeName = item.name;
        this.childrenShowStatus[index].isOpenShow = !this.childrenShowStatus[index].isOpenShow;

        this.activeName = '';
        this.cdnActiveName = '';
      },
      opMore () {
        alert('更多操作');
      }
    },
    mounted () {
      document.getElementsByClassName("left_side_nav_item")[0].click();
    }
  }
</script>

<style lang="scss" scoped>
  .im {
    width: 100%;height:100%;background-color: #dddddd;padding-top:40px;overflow:hidden;-moz-user-select:none;-webkit-user-select:none;-ms-user-select:none;user-select:none;
    .im-main {margin: 0 auto;width: 80%;height: 100%;background-color: #ffffff;position:relative;clear:both;
      .left_side {width:25%;height:100%;float:left;
        .left_side_search {position:relative;height:68px;line-height:68px;background-color:#eeeeee;text-align:center;
          input.search {width:90%;height:36px;line-height:36px;font-size:14px;border:2px solid #4fdcca;border-radius:5px;padding-left:12px;}
        }
        .left_side_nav {
          font-size: 15px;
          padding-top: 20px;
          .left_side_nav_item {
            position: relative;
            padding-top: 15px;
            height: 70px;
            cursor: pointer;
            .arrow {
              -moz-transform:rotate(0deg);
              -webkit-transform:rotate(0deg);
              -o-transform:rotate(0deg);
              -ms-transform:rotate(0deg);
              transform:rotate(0deg);
            }
            .arrow.active {
              -moz-transform:rotate(180deg);
              -webkit-transform:rotate(180deg);
              -o-transform:rotate(180deg);
              -ms-transform:rotate(180deg);
              transform:rotate(180deg);
            }
            img {
              margin: 0 5px 0 20px;
              vertical-align: middle;
            }
          }
          .left_side_nav_item.active {
            background-color: #f0fdfb;
            position: relative;
            &::before {
              content: "";
              position: absolute;
              width: 4px;
              height: 70px;
              top: 0;
              left: 0;
              background-color: #4fdcca;
            }
          }
          .left_side_nav_item_cdn {
            font-size: 13px;
            height: 70px;
            line-height: 70px;
            cursor: pointer;
            img {
              margin: 0 5px 0 35px;
              vertical-align: middle;
            }
          }
          .left_side_nav_item_cdn.active {
            background-color: #f0fdfb;
            position: relative;
            &::before {
              content: "";
              position: absolute;
              width: 4px;
              height: 70px;
              top: 0;
              left: 0;
              background-color: #4fdcca;
            }
          }
        }
      }
      .right_side {width:75%;height:100%;float:right;border-left:1px solid #cccccc;
        .right_side_title {height:68px;line-height:68px;background-color:#eeeeee;padding-left:28px;font-weight:bold;position:relative;
          span {position:absolute;right:40px;top:50%;width:120px;height:34px;line-height:34px;margin-top:-17px;background-color:#83bbf7;text-align:center;color:#eeeeee;cursor:pointer;border-radius:5px;}
        }
        .right_side_concat_list {
          .concat_list_item {
            font-size: 14px;
            width: 100%;
            height: 70px;
            line-height: 70px;
            border-bottom: 1px solid #cccccc;
            span {
              display: inline-block;
              text-align: center;
              img {
                margin: 0 5px 0 20px;
                vertical-align: middle;
              }
            }
          }
        }
      }
    }
  }
</style>