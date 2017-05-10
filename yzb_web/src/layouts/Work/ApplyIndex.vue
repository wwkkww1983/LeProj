<template> 
  <div class="no-sort">
    <ul> 
      <li v-for="myWorkApply in myWorkApplys" @click="goApplyContent(myWorkApply.applyName, myWorkApply.applyId)">
          <img :src="myWorkApply.applyLogo3X">
          <p>{{myWorkApply.applyName}}</p>
      </li>
    </ul>
  </div>
</template>
<script>
export default {
    data () {
      return {
        companyData: null, // 待处理
        myWorkApplys: [], // 桌面应用
        isSort: true, // 我的应用排版  待处理
        isIndex: true // 非首页则隐藏桌面应用
      }
    },
    created () {
        this.getIcon();
    },
    methods: {
        getIcon: function () { // 获取个人应用
            let url = INTERFACE_URL_9083 + "/v1/myWorkApply/mySelfWorkApplyList";
            var data = {
                "data": {
                    "companyRole": sessionStorage.getItem("role"), // 角色
                    "type": 2
                },
                "unit": {
                    "channel": 3,
                    "os": "string",
                    "userId": sessionStorage.getItem("id"),
                    "companyId": sessionStorage.getItem("companyId"),
                    "tokenId": sessionStorage.getItem("tokenId")
                }
            }
            sessionStorage.setItem("yinyong", JSON.stringify(data))
            this.$http.post(url, data).then((response) => {
                if (response.data.state === 200) {
                  var applyLists = [];
                  for (let i = 0; i < response.data.data.classifyList.length; i++) {
                      applyLists.push(...response.data.data.classifyList[i].applyList)
                  }
                  this.myWorkApplys = applyLists;
                } else {
                    console.log('获取个人应用列表报错信息: ' + response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        },
        goApplyContent (parm, applyId) {
            var ApplyRouterName = [
                {
                    name: "入库",
                    url: "/work/storage"
                },
                {
                    name: "发货",
                    url: "/work/sipping"
                },
                {
                    name: "修改",
                    url: "/work/amend"
                },
                {
                    name: "退货",
                    url: "/work/salesReturn"
                },
                {
                    name: "调柜",
                    url: "/work/transferCabinet"
                },
                {
                    name: "调库",
                    url: "/work/transferStorage"
                },
                {
                    name: "退库",
                    url: "/work/storageReturn"
                },
                {
                    name: "收货",
                    url: "/work/receiving"
                },
                {
                    name: "公司设置",
                    url: "/work/companySetting"
                },
                {
                    name: "店铺设置",
                    url: "/work/shopSetting"
                },
                {
                    name: "公告",
                    url: "/"
                },
                {
                    name: "销售",
                    url: "/work/sell"
                },
                {
                    name: "点数", // 标签打印
                    url: "/work/template/list"
                }
            ]
            for (let i = 0; i < ApplyRouterName.length; i++) {
                if (ApplyRouterName[i].name === parm) {
                    sessionStorage.setItem("applyId", applyId);
                    this.$router.push(ApplyRouterName[i].url);
                }
            }
        }
    }
}
</script>
<style lang="scss" scoped>
    .work{width: 100%;height:100%;}
    .no-sort{
      width: 100%;
      height: 100%;
      ul{
        position: absolute;
        left: 0;
        right: 0;
        width: 740px;
        margin: auto;
        margin-top: 50px;
        li{
          width: 60px;
          height: 94px;
          float: left;
          margin-right: 76px;
          margin-bottom: 40px;
          cursor: pointer;
          img{
            width: 60px;
            height: 60px;
            display: block;      
          }
          p{
            margin-top: 18px;
            font-size: 16px;
            text-align: center;
          }
        }
        li:nth-child(6n){
          margin-right: 0;
        }
      }
    }
    .sort{
      padding: 20px 70px 0 70px;
      width: 100%;
      height: 100%;
      h3{
        font-size: 16px;
        margin-bottom: 6px;
      }
      ul{
        overflow: hidden;
        width: 100%;
        li{
          width: 379px;
          height: 66px;
          border: 1px solid #fff;
          &:hover { background-color: #e6f8f6; }
          img{
            display: block;
            margin: 8px 10px;
            float: left;
            width: 50px;
            height: 50px;   
          }
          p:nth-child(2){
            margin-top: 12px;
            font-size: 16px;
            color:#ccc;
          }
          p:nth-child(3){
            margin-top: 5px;
            font-size: 12px;
            color: #999;
          }
        }//li
      }
      .sort-left{
        width: 50%;
        float: left;
        overflow: hidden;
      }
      .sort-right{
        width: 50%;
        float: right;
        overflow: hidden;
      }
    }
    .hint {
      position: fixed;
      top: 20px;
      left: 50%;
      margin-left: -200px;
      background-color: yellow;
      width: 400px;
      height: 30px;
      line-height: 30px;
    }
</style>

