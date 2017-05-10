<template>
    <div class="affirm-delect-main">
        <div class="dialog-w380">
            <el-dialog title="选择产品类型" v-model="saveSuccess">
                <ul class="body-wrap">
                    <li>确人删除选中单据</li>
                </ul>
                <span slot="footer">
                    <a href="javascript: void(0)" class="cancel-btn-w120" @click="close">取消</a>
                    <a href="javascript: void(0)" class="confirm-btn-w120 mr0" @click="batchDelect">确定</a>
                </span>
            </el-dialog>
        </div>
    </div>   
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import {receiptOperation} from './../../../../Api/commonality/operate'
export default {
    props: [
        'newDatas', // 操作部门
        'applyType' // 应用类型
    ],
    data () {
        return {
            saveSuccess: true
        }
    },
    computed: {
        ...mapGetters([
            "workDelectSelectsData" // 单据删除的数据集合
        ])
    },
    methods: {
        ...mapActions([
            "workProductList"
        ]),
        close () {
            this.$emit('affirmDelectFun', false)
        },
        batchDelect () { // 确定删除
            var orderIdList = [];
            if (this.workDelectSelectsData.length > 0) {
                for (let i = 0; i < this.workDelectSelectsData.length; i++) {
                    var orderIds = {};
                    orderIds.orderId = this.workDelectSelectsData[i];
                    orderIdList.push(orderIds);
                }
            }
            let data = {
                "handleType": 1, // 操作1 删除 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核
                "orderIdList": orderIdList, // 单据ID
                "checkRemark": "审核批注" // 审核批注
            }
            receiptOperation(data).then((response) => {
                console.log("确定删除回调");
                console.log(response);
                if (response.data.state === 200) {
                    this.$emit('affirmDelectFun', false)
                    this.workProductList({ //  获取单据列表
                        "operationUnit": this.newDatas.userType, // 操作部门
                        "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                        "page": 1,
                        "type": this.applyType,
                        "showView": 1
                    })
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("确定删除回调");
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
</style>