<template>
    <div>
        <el-dialog title="提交审核" v-model="popup">
            <div class="audit-body">
                <div class="audit-body-header">
                    <i></i>
                    <h3>{{applyName}}审核</h3>
                </div>
                <div class="audit-body-body">
                    <ul>
                        <li v-for="tittleTh in auditThs">
                            {{tittleTh.name}}：
                        </li>
                    </ul>
                    <ul v-if="receiptList">
                        <li v-if="itemKey !== 'editLimit'" v-for="(itemKey, index) in filtterAudit(Object.keys(receiptList))">
                            {{receiptList[itemKey]}}
                        </li>
                    </ul>        
                </div>
                <!-- 单据历史记录 -->
                <div class="parts-color" v-if="receiptHistoryLists"></div>
                <div class="receipts-history" v-if="receiptHistoryLists">
                    <ul>
                        <li v-for="item in receiptHistoryLists">
                            <i class="pass"></i>
                            <img :src="item.userLogo" alt="">
                            <div class="receipts-title">
                                <p><span>{{item.userName}}</span><span class="pass-name">{{getStatus(item.checkStatus)}}</span></p>
                                <p class="time-title">{{item.createTime}}</p>
                                <p class="conter-title">{{item.checkRemark}}</p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="parts-color" v-if="selectName"></div>
                <div class="audit-input-name" v-if="selectName">
                    <span>审核人：</span>
                    <div class="select-w300">
                        <el-select filterable v-model="makeOrderUserId" placeholder="">
                            <el-option v-for="item in applyUserList"
                            :label="item.userName"
                            :value="item.userId">
                            <span @click="getUserName(item.userName)">{{item.userName}}</span>
                            </el-option>
                        </el-select>
                    </div>
                </div>
                <div class="parts-color"></div>
                <div class="audit-input-data">
                    <span>批注：</span>
                    <div class="textarea">
                        <el-input
                        type="textarea"
                        :rows="2"
                        placeholder="请输入内容"
                        v-model="applyRemark"
                        >
                        </el-input>
                    </div>
                </div>
            </div>
           <span slot="footer">
                <ul>
                    <template v-for="item in receiptList.editLimit">
                        <template v-if='item.powerType === "5"'>
                            <li class="confirm-btn-w120 warn-color" @click="auditFun(4)">驳回</li>
                            <li class="confirm-btn-w120 mr0" @click="auditFun(5)">通过</li>
                        </template>     
                        <li class="confirm-btn-lg" v-if='item.powerType === "6"' @click="auditFun(6)">撤销审核</li>
                        <li class="confirm-btn-lg" v-if='item.powerType === "7"' @click="auditFun(7)">提交审核</li>
                    </template>
                </ul>
           </span>
        </el-dialog>
    </div>
</template>
<script>
import {historyListStatus} from './../../../Api/commonality/status'
import {receiptAddOrChecks, operateReceiptOperation} from './../../../Api/commonality/operate'
// import {receiptHistoryList} from './../../../vuex/Api/Seek'
import {mapActions, mapGetters} from 'vuex'
export default{
    props: [
        'applyName', // 应用的名字
        // 'applyType', // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
        'auditThs', // 审核弹窗头部
        'receiptList', // 当前单据
        'newDatas', // 带到下一页的数据
        'receiptHistoryLists' // 单据历史
    ],
    data () {
        return {
            popup: false,
            makeOrderUserId: null, // 审核人Id
            checkUserName: null, // 审核人名字
            applyRemark: null, // 批注
            affirmAudit: false, // 确认弹窗
            affirmNumber: null, // 操作类型
            popupTop: null, // 确定审核的头部
            popupBody: null, // 确定审核的内容
            // receiptHistoryLists: null, // 单据历史记录
            selectName: false, // 是否出现选择审核人名
            orderId: null // 单据Id
        }
    },
    computed: {
        ...mapGetters([
            "popupAudit", // 审核弹窗
            "applyUserList", // 用户列表
            "receiptDetail", // 单据详情
            "workProductListData" // 单据列表
        ]),
        applyType: function () {
            var type = null;
            switch (this.applyName) {
                case "调库":
                    type = 1;
                    break;
                case "退库":
                    type = 2;
                    break;
                case "修改":
                    type = 3;
                    break;
                case "入库":
                    type = 4;
                    break;
                case "收货":
                    type = 5;
                    break;
                case "发货":
                    type = 6;
                    break;
                case "退货":
                    type = 7;
                    break;
                case "调柜":
                    type = 8;
                    break;
            }
            return type;
        }
    },
    watch: {
        'popupAudit': function () {
            this.popup = this.popupAudit;
        },
        'popup': function () {
            this.workPopupAudit(this.popup);
            // this.historyListFun(this.orderId); // 单据历史记录
        },
        'receiptList': function () { // 当前单据
            this.getOrderId(); // 获取单据Id
            var obj = null;
            // console.log(this.receiptList);
            if (this.receiptList.editLimit) {
                for (let i of this.receiptList.editLimit) {
                    if (i.powerType === "7") {
                        obj = "7";
                    }
                }
                if (obj === "7") {
                    this.selectName = true;
                } else {
                    this.selectName = false;
                }
            }
        }
    },
    methods: {
        ...mapActions([
            "workPopupAudit", // 审核弹窗
            "workProductList" // 单据列表
        ]),
        opens () {
            this.$confirm(this.popupBody, this.popupTop, {
                confirmButtonText: '确定',
                cancelButtonText: '取消'
            }).then(() => {
                this.affirmFun()
            })
        },
        filtterAudit (parm) {
            return parm;
        },
        getUserName (parm) { // 绑定审核人名字
            this.checkUserName = parm;
        },
        getStatus (parm) { // 审核状态
            return historyListStatus(parm);
        },
        getOrderId () { // 获取单据Id
            sessionStorage.setItem("单据列表", JSON.stringify(this.workProductListData));
            for (let i = 0; i < this.workProductListData.dataList.length; i++) {
                if (this.workProductListData.dataList[i].orderNumber === this.receiptList.orderNum) {
                    this.orderId = this.workProductListData.dataList[i].orderId;
                    // this.orderId = this.workProductListData.dataList[i].orderNumber;
                }
            }
        },
        auditFun (parm) {
            switch (parm) {
                case 3:
                    this.popupTop = "取消审核";
                    this.popupBody = "是否确认取消审核";
                    break;
                case 4:
                    this.popupTop = "驳回确认";
                    this.popupBody = "是否确认驳回该审核";
                    break;
                case 5:
                    this.popupTop = "通过确认";
                    this.popupBody = "是否确认通过该审核";
                    break;
                case 6:
                    this.popupTop = "撤销审核确认";
                    this.popupBody = "是否确认撤销该审核";
                    break;
                case 7:
                    this.popupTop = "提交确定";
                    this.popupBody = "是否提交该审核给" + this.checkUserName;
                    break;
            }
            this.affirmNumber = parm;
            this.affirmAudit = true;
            this.opens();
        },
        affirmFun () {
            if (this.affirmNumber === 7) {
                this.receiptAddOrCheck(this.receiptDetail); // 提交审核
            } else {
                this.receiptOperation(this.affirmNumber)
            }
        },
        receiptAddOrCheck (parm) { // 提交审核
            console.log("进了提交审核")
            let data = {
                "orderId": this.orderId, // 单据ID
                "operationUnit": this.newDatas.userType, // 操作单位 1 部门 2 店铺
                "operationUnitId": this.receiptDetail.handleDeptId, // 操作单位ID
                "operationName": this.receiptDetail.handleDept, // 操作单位名称
                "makeOrderUserId": this.receiptDetail.makeOrderUserId, // 制单人ID
                "makeOrderUserName": this.receiptDetail.makeOrderUser, // 制单人
                "orderRemark": this.receiptDetail.orderRemark, // 单据备注
                "applyRemark": this.applyRemark, // 申请备注 /////
                "checkUserId": this.makeOrderUserId, // 审核人ID /////
                "checkUserName": this.checkUserName, // 审核人名称 /////
                "productTpye": this.receiptDetail.productTpye, // 产品类型:1 成  品2 旧料
                "supplierId": this.receiptDetail.supplierId, // 供应商ID
                "supplierName": this.receiptDetail.supplier, // 供应商
                "shopId": this.receiptDetail.shopId, // 分销商ID
                "shopName": this.receiptDetail.shop, // 分销商名称
                "warehouseId": this.receiptDetail.warehouseId, // 当前库位/发货库位/退货库位ID
                "warehouseName": this.receiptDetail.warehouse, // 当前库位/发货库位/退货库位
                "outWarehouseId": this.receiptDetail.outWarehouseId, // 出货库位
                "outWarehouseName": this.receiptDetail.outWarehouse, // 出货库位
                "shopGroupId": this.receiptDetail.shopGroupId, // 当前/调入柜组ID
                "shopGroupName": this.receiptDetail.shopGroup, // 当前/调入柜组名
                "outShopGroupId": this.receiptDetail.outShopGroupId, // 调出柜组ID
                "outShopGroupName": this.receiptDetail.outShopGroup, // 调出柜组名
                "productIdList": [], // 商品ID 列表
                "handleType": 2, // 操作类型 1 保存 2 提交审核 3 修改
                "type": this.applyType // 单据类型:1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜
            }
            receiptAddOrChecks(data).then((response) => { // 提交审核
                console.log("提交审核");
                // console.log(response);
                if (response.data.state === 200) {
                    this.applyRemark = null; // 申请备注
                    this.makeOrderUserId = null; // 审核人ID
                    this.workPopupAudit(false);
                    this.workProductList({ //  获取单据列表
                        "operationUnit": this.newDatas.userType, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
                        "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                        "page": 1,
                        "type": this.applyType,
                        "showView": 1
                    })
                } else {
                    alert(response.data.msg)
                    console.log(response)
                }
            }, (response) => {
                console.log(response + "提交审核")
            })
        },
        receiptOperation (parm) { // 单据操作（删除、取消审核、驳回审核、通过审核、撤销审核）
            var options = {
                "handleType": parm, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "orderIdList": [{"orderId": this.receiptList.orderNum}], // 单据ID
                "checkRemark": this.applyRemark // 审核批注
            }
            operateReceiptOperation(options).then((response) => {
                console.log("提交审核");
                console.log(response);
                if (response.data.state === 200) {
                    this.workPopupAudit(false);
                    this.workProductList({ // 单据列表
                        "operationUnit": this.newDatas.userType, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
                        "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                        "page": 1,
                        "type": this.applyType,
                        "showView": 1
                    })
                } else {
                    alert(response.data.msg);
                    console.log(response)
                }
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
$oa: #0abfab; // 主色
$d6: #d6d6d6; // 分割线颜色
$c3: #333333; // 导航栏文字颜色
$c2: #333742; // 标签栏底色
$es: #e5f5fd; // 标签设置编辑文本 出现旋转框
$f7: #f74c31; // 消息红点颜色
$a8: #a8a8a8; // 搜索页面icon 为选中的底色
$fb: #fb6155; // 删除按钮未选中的底色
$c9: #c9e5e2; // 按钮没选中状态
$f0: #f0f0f0; // 细线颜色
$c9: #999; // 列表内容文字色值
$c6: #666; // 写在标签栏块上的文字图标选中加深颜色
$e6: #e6f8f6; // 选中列表时的状态色
$f6: #f6f6f6; // 没选中列表时的状态色（偶）
$fd: #fd9526; // 按钮图标颜色
$bf: #49bbf3; // 标签设置编辑文本 出现旋转框描边 
$e8: #e8e9eb; // 表格头部的颜色
.warn-color{ // 取消类的颜色
    background-color: #f74c31;
}
.audit-body-header{
    margin: 0 30px;
    font-size: 0;
    height: 60px;
    border-bottom: 1px solid $d6;
    background-color: #fff;
    i{
        display: inline-block;
        background-color: $fd;
        width: 44px;
        height: 44px;
        border-radius: 4px;
        vertical-align: top;
        margin: 8px 10px 0 0;
    }
    h3{
        font-size: 14px;
        font-weight: normal;
        display: inline-block;
        height: 60px;
        line-height: 60px;
        color: #5dbf0a;
    }
}
.audit-body{
    font-size: 0;
    height: 480px;
    overflow: auto;
    background-color: #fff;
    .audit-body-body{
        padding: 0 30px;
        // width: 100%;
        // height: 100%;   
        ul{
            margin-top: 10px;
            vertical-align: top;
            display: inline-block;
            font-size: 0;
            li{
                height: 12px;
                font-size: 12px;
                margin-bottom: 10px;
            }
        }
        ul:last-child{
            color: #333;
        }
        ul:first-child{
            width: 60px;
            text-align: right;
            margin-right: 10px;
            color: #999；
        }
    }
}
.parts-color{ // 暂时用来占位置（待优化）
    height: 10px;
    background-color: #f1f4f8;
}
.audit-input-name{
    height: 50px;
    font-size: 0;
    padding: 10px 20px 10px 30px;
    span{
        display: inline-block;
        height: 50px;
        line-height: 50px;
        font-size: 12px;
        margin: -10px 10px 0 0;
    }
}
.audit-input-data{
    font-size: 0;
    padding: 10px 20px 10px 30px;
    span{
        vertical-align: top;
        display: inline-block;
        width: 50px;
        height: 20px;
        line-height: 20px;
        font-size: 12px;
        margin: 10px 10px 0 0;
    }
    .textarea{
        display: inline-block;
        width: 300px;
    }
}
.affirm-popup{
    position: relative;
    width: 320px;
    height: 60px;
    //
    .body-wrap{
         opacity: 0.9;
        padding: 0 30px;
        margin-top: 60px;
        li{
            font-size: 14px;
            line-height: 30px;
            color: #333;
            text-align: center;
        }
    }
}
.receipts-history{ // 单据历史记录表
    padding: 0 30px;
    ul{
        padding: 20px 0;
        li{
            margin-top: 30px;
            font-size: 0;
            .pass,.noPass{
                vertical-align: top;
                width: 20px;
                height: 20px;
                border-radius: 50%;
                display: inline-block;
                margin-right: 14px;
                margin-top: 10px;
            }
            .pass{
                background: url("./../../../assets/img/work/pass.png") no-repeat center;
            }
            .noPass{
                background-image: url("./../../../assets/img/work/noPass.png") no-repeat;
            }
            img{
                height: 40px;
                width: 40px;
                border-radius: 50%;
                margin-right: 10px;
            }
            .receipts-title{
                vertical-align: top;
                display: inline-block;
                width: 260px;
                .time-title{
                    color: #999;
                }
                .conter-title{
                    color: #333;
                }
                p{
                    font-size: 12px;
                    span{
                        font-size: 14px;
                    }
                    span:first-child{
                        color: #333;
                        margin-right: 16px;
                    }
                    .pass-name{
                        color: #5dbf0a;
                    }
                    .noPass-name{
                        color: #f74c31;
                    }
                }
            }
        }
        li:first-child{
            margin-top: 0;
        }
    }
}
</style>