<template>
<div class="item-box">
    <ul>
        <li v-for="(item, index) in this.readList" @click="seeNotice(index)" >
            <img v-if="item.noticeLog" :src="item.noticeLog">
            <h4>
                {{item.title}}
                <span>{{item.createTime}}</span>
            </h4>
            <p>{{item.content}}</p>
        </li>        
    </ul>
    <div class="notice-info"  v-show="noticeShow">
        <div class="notice-info-title">
            <h4>{{noticeInfo.title}}</h4>
            <p>{{noticeInfo.publishUserName}}<span>{{noticeInfo.createTime}}</span></p>
            <p>公告范围：{{noticeInfo.range}}</p>
        </div>
        <div class="notice-info-content">
            <p>{{noticeInfo.content}}</p>
        </div>
    </div>
</div>
</template>

<script>
export default {
    data () {
        return {
            readList: null, // 数据列表
            noticeInfo: '',
            noticeShow: false
        }
    },
    created () {
        this.getAjaxNotice()
    },
    methods: {
        getAjaxNotice () {
            var data = {
                "data": {
                    "type": "1", // 显示类型：1 未读 2 已读 3 全部
                    "page": "1", // 当前页
                    "pageSize": "15" // 每页显示数
                },
                "unit": {
                    "companyId": sessionStorage.getItem("companyId"),
                    "channel": 3,
                    "os": "string",
                    "ip": "string",
                    "userId": sessionStorage.getItem("id"),
                    "tokenId": sessionStorage.getItem("tokenId")
                }
            }
            // console.log(this.shopStorage)
            var url = INTERFACE_URL_9083 + "/v1/notice/getNoticeList";
            this.$http.post(url, data).then((response) => {
                console.log("未读公告");
                console.log(response.data.data.noticeList);
                // this.thisShopId = response.data.data.counterList;
                this.readList = response.data.data.noticeList;
            }, (response) => {
                console.log(response);
            })
        },
        seeNotice (index) {
            console.log(index)
            var data = {
                "data": {
                    "noticeId": this.readList[index].noticeId // 公告ID
                },
                "unit": {
                    "companyId": sessionStorage.getItem("companyId"),
                    "channel": 3,
                    "os": "string",
                    "ip": "string",
                    "userId": sessionStorage.getItem("id"),
                    "tokenId": sessionStorage.getItem("tokenId")
                }
            }
            var url = INTERFACE_URL_9083 + "/v1/notice/getNoticeInfo";
            this.$http.post(url, data).then((response) => {
                // console.log("1111111111111111111111111111111");
                // console.log(response.data.data);
                this.noticeInfo = response.data.data;
                this.noticeShow = true;
            }, (response) => {
                console.log(response);
            })
        },
        dateTime () {
        }
    }
}
</script>
<style lang="scss" scoped>
    .item-box{
        li{
            margin-left: 30px;
            padding: 10px 10px 10px 0;
            img{
                width: 40px;
                height: 40px;
                float: left;
                margin-right: 10px;
                border-radius: 50%;
            }
            h4{
                font-style: normal;
                font-size:　16px !important;
                color: #333;
                span{
                    float: right;
                    font-size: 14px;
                    color: #999;
                }
            }
            p{
                font-size: 14px;
                color: #999;
            }
        }
    }
</style>


