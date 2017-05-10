<template>
<div class="plain-receipts-detail">
    <div class="header">
        <span>备注：</span>
        <a href="javascript:void(0)" v-show="remarkContent" @click="reviseRemark">修改备注</a>
        <a href="javascript:void(0)" v-show="!remarkContent" @click="submitRemark">提交备注</a>      
    </div>
    <ul class="content">
        <li v-if="receiptRemark" v-for="(item, i) in receiptRemark">
            <div v-if="item.orderRemarkManName!==null">
                <p class="content-title">{{item.orderRemarkManName}}:<span>{{item.createTime}}</span>
                </p>
                <p class="content-info" v-if="remarkContent">{{item.content}}</p>
                <p class="add-remark" v-if="!remarkContent"><textarea :value="item.content" @keyup.enter="reviseRemark"></textarea></p>
            </div>
        </li>
    </ul>
</div>
</template>
<script>
import { operateHandleXGReceipt } from './../../Api/commonality/operate'
export default {
    data () {
        return {
            "remarkContent": true
        }
    },
    props: [
        "receiptRemark"
    ],
    created () {
        // this.reviseRemark()
    },
    methods: {
        reviseRemark () { // 修改备注
            this.remarkContent = false;
        },
        submitRemark () { // 提交备注
            this.remarkContent = true;
            let options = {
                "modifyList": [{
                    "dataType": "5",
                    "objectData": this.receiptRemark.orderRemarkList.content
                }],
                "orderNum": JSON.parse(sessionStorage.getItem("receiptDes")).orderNum,
                "modelType": "6"
            };
            operateHandleXGReceipt(options).then((response) => {
                console.log(response);
            }, (response) => {
                console.log(response)
            })
        }
    }
}
</script>
<style lang="scss" scoped>
 .plain-receipts-detail{
    color: #333;
    position: relative;
    max-height: 460px;
    background-color: #fff;
    margin-top: 20px;
    .header{
        height: 50px;
        line-height: 50px;
        margin: 0 10px;
        border-bottom: 1px solid #d6d6d6;
        span{
            font-size: 16px;
        }
        a{
            float: right;
            font-size: 12px;
        }
    }
    .content{
        padding: 10px 0;
        height: 320px; 
        li{
            padding: 10px;
            &:nth-child(odd){
                background: #fff;
            }
            &:nth-child(even){
                background: #fafbfc;
            }
            .content-title{
                font-size: 14px;
                color: #333;
                line-height: 26px;
            }
            .content-info{
                font-size: 12px;
                color: #999;
            }            
        }
    }
    .add-remark{
        margin: 0 10px;
        height: 90px;
        padding: 10px 0;        
        textarea{
            resize: none;
            border: none;
            width: 100%;
            height:100%;
        }       
    }
}
</style>