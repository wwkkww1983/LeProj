<template>
    <div class="dialog-w450-h250-hn affirm-delect-main">
        <el-dialog title="选择产品类型" v-model="saveSuccess">
            <ul class="body-wrap">
                <li class="tit">确定收货么？</li>
            </ul>
            <span slot="footer">
                <a href="javascript: void(0)" class="cancel-btn-w120" @click="close">取消</a>
                <a href="javascript: void(0)" class="confirm-btn-w120 mr0" @click="receiptOperation">确定</a>
            </span>
        </el-dialog>
    </div>
</template>
<script>
import {operateReceiptOperation, receiptAddOrChecks, operateTakeOrder} from './../../../../Api/commonality/operate'
export default {
    props: [
        'auditPopup', // 弹窗
        'receiptsOrderNum', // 单号
        'auditsReceiptsForm' // 操作类型
    ],
    data () {
        return {
            "saveSuccess": false,
            "rejectAudit": "" // 驳回备注
        }
    },
    computed: {
        types: function () {
            if (this.auditsReceiptsForm === "3" || this.auditsReceiptsForm === "4" || this.auditsReceiptsForm === "6") {
                return true;
            }
            return false;
        }
    },
    watch: {
        'auditPopup': function () {
            this.saveSuccess = this.auditPopup;
        }
    },
    methods: {
        close () {
            var states = {
                type: "1", // 取消
                name: false
            }
            this.rejectAudit = "";
            this.$emit('delectReceiptsState', states)
        },
        receiptOperation () { // 确定删除
            if (this.auditsReceiptsForm === "1") {
                this.receiptAddOrChecksFun();
            } else {
                this.receiptAddOrChecksOther();
            }
        },
        receiptAddOrChecksOther () { // 其它审核操作
            if (this.auditsReceiptsForm === "9") {
                this.receiptOperateTakeOrder();
            } else {
                this.receiptOperateReceiptOperation();
            }
        },
        receiptOperateReceiptOperation () { // 审核
            var types = this.auditsReceiptsForm;
            if (this.auditsReceiptsForm === "2") {
                types = "5"
            }
            if (this.auditsReceiptsForm === "3") {
                types = "6";
            }
            var options = {
                "handleType": types, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "orderIdList": [{"orderNum": this.receiptsOrderNum}], // 单据ID
                "checkRemark": this.rejectAudit // 审核批注
            }
            operateReceiptOperation(options).then((response) => {
                if (response.data.state === 200) {
                    var states = {
                        type: "2", // 成功
                        name: false
                    }
                    this.$emit('delectReceiptsState', states);
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        },
        receiptOperateTakeOrder () { // 收货
            let options = {
                'orderNum': this.receiptsOrderNum
            }
            operateTakeOrder(options).then((response) => {
                if (response.data.state === 200) {
                    var states = {
                        type: "2", // 成功
                        name: false
                    }
                    this.$emit('delectReceiptsState', states);
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        },
        receiptAddOrChecksFun () { // 提交审核
            console.log("receiptAddOrChecksFun");
            let options = {
                "orderNum": this.receiptsOrderNum
            }
            receiptAddOrChecks(options).then((response) => {
                console.log("提交审核");
                console.log(response);
                sessionStorage.setItem("审核bug", JSON.stringify(options));
                if (response.data.state === 200) {
                    alert("审核操作成功");
                    var states = {
                        type: "2", // 成功
                        name: false
                    }
                    this.$emit('delectReceiptsState', states);
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log(response);
            })
        }
    }
}
</script>
<style lang="scss" scoped>
.affirm-delect-main{
    width: 100%;
    height: 100%;
    .body-wrap{
        // margin-top: 60px;
        li{
            font-size: 14px;
            line-height: 30px;
            color: #333;
            text-align: center;
        }
        .tit {
            padding: 0 30px;
            margin-top: 60px;
        }
        .textarea-input{
            position: relative;
            margin: 30px 40px 0 40px;
            width: 370px;
            height: 120px;
            overflow: hidden;
            border-radius: 4px;
            border: 1px solid red;
            textarea{
                width: 100%;
                font-size: 14px;
                color: #999;
                height: 195px;
                outline: none;
                border: 0px solid #fff;
                border: 2px solid #000;
            }
            span{
                position: absolute;
                bottom: 0;
                right: 0;
                font-size: 20px;
                border: 1px solid red;
            }
        }
    }
}
</style>