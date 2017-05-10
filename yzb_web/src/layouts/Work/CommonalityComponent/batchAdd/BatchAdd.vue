<template>
    <div class="batch-add"> 
        <div class="dialog-w420-h520">
            <el-dialog title="批量修改" v-model="susses">
                <section class="p-lr-30">
                    <!-- 日期 -->
                    <div class="date">
                        <ul>
                            <li>
                                <span>日期：</span>
                                <div class="date-select">   
                                    <div class="date-w130">
                                        <el-date-picker
                                        v-model="date.startTime"
                                        type="date"
                                        placeholder="">
                                        </el-date-picker>
                                    </div>
                                    <span class="m-lr-10">至</span>
                                    <div class="date-w130">
                                        <el-date-picker
                                        v-model="date.endTime"
                                        type="date"
                                        placeholder="">
                                        </el-date-picker>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <a href="javascript: void(0)" @click="newData">本日</a>
                                <a href="javascript: void(0)">本周</a>
                                <a href="javascript: void(0)">本月</a>
                            </li>
                        </ul>
                    </div>
                    <!-- 单据 -->
                    <div class="receipt">
                        <ul>
                            <li>
                                <span>单据类型：</span>
                                <div class="select-w280 fr">
                                    <el-select v-model="receiptClass" placeholder="">
                                        <el-option v-for="item in receiptClassList"
                                        :label="item.name"
                                        :value="item.userType">
                                        </el-option>
                                    </el-select>
                                </div>
                            </li>
                            <li>
                                <span>单号：</span>
                                <div class="fr">  
                                    <div class="input-w130">   
                                        <el-input></el-input>
                                    </div>   
                                    <span class="m-lr-10">至</span>
                                    <div class="input-w130">   
                                        <el-input></el-input>
                                    </div>  
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="footer">
                        <!-- 产品类别 -->
                        <div class="footer-style productClass">
                            <div @click="">
                                <span>产品类别</span>
                                <div class="footer-right">    
                                    <span>未设置</span>
                                    <i></i>
                                </div>
                            </div>
     <!--                        <ul>
                                <li></li>
                            </ul> -->
                        </div>
                        <!-- 成色名称 -->
                        <div class="footer-style colorName">
                            <div>
                                <span>成色名称</span>
                                <div class="footer-right">    
                                    <span>未设置</span>
                                    <i></i>
                                </div>
                            </div>
<!--                             <ul>
                                <li></li>
                            </ul> -->
                        </div>
                        <!-- 宝石名称 -->
                        <div class="footer-style colorName">
                            <div>
                                <span>宝石名称</span>
                                <div class="footer-right">    
                                    <span>未设置</span>
                                    <i></i>
                                </div>
                            </div>
<!--                             <ul>
                                <li></li>
                            </ul> -->
                        </div>
                        <!-- 宝石名称 -->
                        <div class="footer-style colorName">
                            <div>
                                <span>手饰类别</span>
                                <div class="footer-right">    
                                    <span>未设置</span>
                                    <i></i>
                                </div>
                            </div>
<!--                             <ul>
                                <li></li>
                            </ul> -->
                        </div>
                    </div>
                </section>
                <span slot="footer">
                    <a href="javascript: void(0)" class="btn-w100">取 消</a>
                    <a href="javascript: void(0)" class="btn-w100">重 置</a>
                    <a href="javascript: void(0)" class="btn-w100 m0">下一步</a>
                </span>
            </el-dialog>
        </div>
    </div>
</template>
<script>
    export default {
        data () {
            return {
                date: {
                    "startTime": null, // 开始时间
                    "endTime": null // 结束时间
                },
                receiptClass: null, // 单据类型
                susses: true,
                receiptClassList: [ // 单据列表
                    {
                        name: "调库",
                        useType: 1
                    },
                    {
                        name: "退库",
                        useType: 2
                    },
                    {
                        name: "修改",
                        useType: 3
                    },
                    {
                        name: "入库",
                        useType: 4
                    },
                    {
                        name: "收货",
                        useType: 5
                    },
                    {
                        name: "发货",
                        useType: 6
                    },
                    {
                        name: "退货",
                        useType: 7
                    },
                    {
                        name: "调柜",
                        useType: 8
                    },
                    {
                        name: "销售/回购",
                        useType: 9
                    }
                ]
            }
        },
        methods: {
            newData () { // 本日
                let date = new Date();
                this.date.startTime = date;
                this.date.endTime = date;
            }
        }
    }
</script>
<style lang="scss" scoped>
.batch-add{
    section{
        height: 406px;
        overflow: auto;
        ul{
            li{
                font-size: 0;
                height: 30px;
                span{
                    display: inline-block;
                    font-size: 12px;
                    height: 30px;
                    line-height: 30px;
                }
            }
        }
        .date{
            margin-top: 30px;
            height: 76px;
            ul:first-child{
                li:first-child{
                    position: relative;
                    .date-select{
                        position: absolute;
                        right: 0;
                        vertical-align: top;
                        display: inline-block;
                    }
                }
                li:nth-child(2){
                    margin-top: 16px;
                    float: right;
                    a{
                        display: inline-block;
                        font-size: 12px;
                        width: 90px;
                        height: 30px;
                        line-height: 30px;
                        text-align: center;
                        border: 1px solid #0abfab;
                        border-radius: 4px;
                        margin-right: 16px;
                        color: #0abfab;
                    }
                    a:last-child{
                        margin-right: 0;
                    }
                }
            }
        }
        .receipt{
            margin-top: 20px;
            ul{
                li:nth-child(2){
                    margin-top: 20px;
                }
            }
        }
        .footer{
            margin-top: 20px;
            .footer-style{
                position: relative;
                font-size: 0;
                height: 44px;
                line-height: 44px;
                border-bottom: 1px solid #d6d6d6;
                span{
                    font-size: 12px;
                    color: #666;
                    height: 44px;
                    line-height: 44px;
                    overflow: hidden;
                    display: inline-block;
                    vertical-align: center;
                }
                .footer-right{
                    display: inline-block;
                    position: absolute;
                    width: 80px;
                    height: 44px;
                    right: 0px;
                    color: red;
                    span{
                        vertical-align: top;
                        margin-right: 10px;
                    }
                    i{
                        display: inline-block;
                        width: 10px;
                        height: 18px;
                        margin-top: 11px;
                        margin-left: 5px;
                        background: url('./../../../../assets/img/commonality/arrowhead.png') no-repeat;
                        
                    }
                }
            }
        }
    }
}
</style>