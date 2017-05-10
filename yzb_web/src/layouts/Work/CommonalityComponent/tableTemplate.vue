<template>
    <div>
        <div class="table-template">
<el-checkbox-group v-model="crudData.checkList"> 
            <table cellpadding="0" cellspacing="0">
                <thead>
                    <tr class="tr-header">
                        <th v-if="!operationCut">
                            <label class="el-checkbox checkbox-font">
                                <span class="el-checkbox__input is-focus" v-bind:class="{'is-checked': crudData.checked}">
                                    <span class="el-checkbox__inner"></span>
                                    <input type="checkbox" class="el-checkbox__original" v-model="crudData.checked" @click="cutSelect(crudData.checked)"/>
                                </span>
                            </label>
                        </th>
                        <th>序号</th>
                        <th v-if="tittleThs" v-for="tittleTh in tittleThs">{{tittleTh.name}}</th>
                    </tr>
                </thead>
                <tbody>               
                    <tr v-if="showStorageList" v-for="(item, index) in showStorageList">
                        <td v-if="!operationCut"><el-checkbox :label='item.orderId' class="checkbox-font"></el-checkbox></td>
                        <td>{{index+1+((page - 1) * 30)}}</td>
                        <template v-for="key in Object.keys(item)">
                            <template v-if="key !== 'orderId'">
                                <template v-if='key !== "status"'>
                                    <td v-if='key === "receivedStatus"'>{{getReceivedStatus(item[key])}}</td>        
                                    <td v-else @click="toDetail(item)">
                                        {{item[key]}}
                                    </td>
                                </template>
                                <td v-else>
                                    <template v-if="btnState(item)">           
                                        <a href="javascript: void(0)" class="m0 m-lr-10"  @click="submitAudit(item, index)"
                                        :class="[item[key] === '1' ? 'audit-no-btn' : '', item[key] === '2' ? 'audit-no-btn' : '', item[key] === '3' ? 'audit-succeed-btn' : '']"
                                        >{{getStatus(item[key])}}</a>
                                        <a href="javascript: void(0)" class="audit-no-btn" @click="storageDetialTable(item.orderNumber, applyType)" 
                                        >打印单据</a>
                                        <a href="javascript: void(0)" @click="printLabel(item.orderNumber)">打印标签</a>
                                    </template>
                                </td>
                            </template>
                        </template>
                    </tr>
                </tbody>
            </table>
</el-checkbox-group>
            <div class="dialog-w420-h600">
                <audit-template
                :receiptHistoryLists="receiptHistoryLists"
                :applyName="applyName"
                :applyType="applyType"
                :auditThs="auditThs"
                :receiptList="audiuPopupList"
                :newDatas="newDatas"
                ></audit-template>
            </div>
        </div>
    </div>
</template>
<script>
import {receiptHistoryList} from './../../../Api/commonality/seek'
import auditTemplate from './AuditTemplate.vue'
import {receiptStatus, receivedStatus} from './../../../Api/commonality/status'
import {mapGetters, mapActions} from 'vuex'
export default {
    props: [
        'applyName', // 应用的名字
        'applyType', // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
        // 'type', // 应用类型
        'tittleThs', // 大标题
        'auditThs', // 审核弹窗头部
        'auditListConfig', // 弹窗配置的规则
        // 'audiuPopupList', // 弹窗制单显示的数据
        'showStorageList', // 显示的数据
        'operationCut', // 删除按钮的监听
        'delectSelectSucces', // 监听删除成功
        'routerUrl', // 路由
        'page', // 当前页数
        'newDatas' // 带到下一页的数据
    ],
    components: {
        auditTemplate
    },
    data () {
        return {
            crudData: { // 增查更删数据
                checked: false, // 监听全选
                checkList: [] // 选择按钮（选中的数据集合）删除
            },
            receiptList: [], // 当前单据
            receiptHistoryLists: null, // 单据历史
            // popup: {
            //     // audit: false // 弹窗
            // }
            listId: null
        }
    },
    watch: {
        'crudData.checkList': function () { // 监听全选
            this.workDelectSelects(this.crudData.checkList); // vuex收集
            if (this.crudData.checkList.length === this.showStorageList.length) {
                this.crudData.checked = true;
            } else {
                this.crudData.checked = false;
            }
        },
        delectSelectSucces: function () { // 监听删除成功
            if (this.delectSelectSucces === true) {
                console.log(this.crudData.checkList);
                this.crudData.checkList = [];
                console.log(this.crudData.checkList);
            }
        },
        receiptDetail: function () {
            console.log("单据详情得到数据");
            console.log(this.receiptDetail);
            console.log("单据详情得到数据");
        }
    },
    computed: {
        ...mapGetters([
            "receiptDetail" // 单据详情
        ]),
        getCheckedPage: function () { // 监听当页的所有做选择的变量（删除选择）
            var CheckedPage = [];
                for (var i = 0; i < this.showStorageList.length; i++) {
                    CheckedPage.push(this.showStorageList[i].orderId)
                }
            return CheckedPage;
        },
        audiuPopupList: function () { // 配置弹窗数据
            var showData = {};
            if (this.receiptDetail) {
                for (let key in this.auditListConfig) {
                    showData[key] = this.receiptDetail[key] || null;
                }
            }
            console.log("最后得到配置的数据集合");
            console.log(showData);
            return showData;
        }
    },
    methods: {
        ...mapActions([
            "workDelectSelects", // 收集选择的集合
            "workProductDetail", // 商品列表(WEB)
            "workReceiptDetail", // 单据详情
            "workPopupAudit" // 审核弹窗
        ]),
        cutSelect (parm) { // 切换全选
            var _self = this;
            setTimeout(function() {
                _self.cutSelectFun();
            }, 13);
        },
        cutSelectFun () { // 切换全选方法
            if (this.crudData.checked === true) { // 全选
                console.log(this.getCheckedPage);
                for (let i of this.getCheckedPage) {
                    this.crudData.checkList.push(i)
                }
                this.crudData.checkList = [...new Set(this.crudData.checkList)];
                console.log(this.crudData.checkList)
            } else { // 取消
                this.crudData.checkList = [];
            }
        },
        // cutSelectFun () {
        //     if (this.crudData.checked === true) {
        //         this.crudData.checkList = [];
        //         for (var i = 0; i < this.showStorageList.length; i++) {
        //             this.crudData.checkList.push(i);
        //         }
        //         // 如果是保存了的数据，就收集【单据号】
        //     } else {
        //         alert("2")
        //         this.crudData.checkList = [];
        //     }
        // },
        btnState (parm) {
            if (this.applyType === 5) { // 收货
                if (parm.receivedStatus === "2") {
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
        },
        printLabel (parm) { // 打印标签
            console.log(parm);
            alert(parm)
        },
        getReceivedStatus (parm) { // 收货状态
            return receivedStatus(parm);
        },
        getStatus (parm) { // 审核状态
            return receiptStatus(parm);
        },
        submitAudit (parm, index) { // 提交审核
            this.historyListFun(parm.orderId);
            this.workPopupAudit(true);
            // console.log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            // console.log(parm);
            // console.log(this.audiuPopupList[index]);
            // this.receiptList = this.audiuPopupList[index];
            this.workReceiptDetail(
                {"orderNumber": parm.orderNumber}
            );
        },
        historyListFun (parm) { // 获取单据历史
            // console.log("检测输入数据");
            // console.log(parm);
            // console.log("检测输入数据");
            // for (let i of this.workProductListData.dataList) {
            //     // console.log("pppppppppppppppppppppppppppppppppppppppppppppppppp");
            //     // console.log(i);
            //     // console.log(this.receiptList)
            //     if (i.orderNumber === this.receiptList.orderNumber) {
            //         return i.orderId;
            //     }
            // }
            receiptHistoryList(parm).then((response) => { // 单据历史记录
                console.log("单据历史");
                console.log(response);
                console.log("单据历史");
                if (response.data.state === 200) {
                    if (response.data.data.length > 0) {
                        this.receiptHistoryLists = response.data.data;
                    } else { // 没有单据历史
                        this.receiptHistoryLists = null;
                    }
                } else {
                    alert(response.data.msg);
                }
            }, (err) => {
                console.log(err);
            })
        },
        // filtterAudit (parm) {
        //     console.log(parm);
        //     return parm;
        // },
        getProductDetail (parm) {
            console.log(parm);
        },
        auditFun () {
            this.popup.audit = false
        },
        toDetail (parm) { // 去详情页
            console.log("去详情页");
            console.log(parm);
            console.log(console.log(this.$router))
            console.log("去详情页");
            var data = {
                "routerUrl": this.routerUrl,
                "page": this.page,
                "pageSize": "30",
                "objId": parm.orderNumber, // 单据id
                // "sellType": "", // 销售类型1 销售 2 退换  3 截金 4 回收 5 兑换 6 典当
                "type": "1", // 操作类型 1 单据号 2 产品类别ID 3 商品ID列表 4 模糊查询 5 条码号
                // "productIdList": null // 商品ID列表
                "applyType": this.applyType // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
            };
            var newDatas = this.newDatas;
            newDatas.orderNumber = parm.orderNumber; // 方便下一页取数据，防止刷新
            newDatas.productStatus = parm.status; // 审核状态
            // sessionStorage.setItem("orderId", parm.orderId);
            newDatas.orderId = parm.orderId;
            sessionStorage.setItem("newDatas", JSON.stringify(newDatas));
            this.workProductDetail(data);
        },
        storageDetialTable (parm, Type) {
            this.listId = parm
            sessionStorage.setItem("orderNumber", parm);
            switch (Type) { // 当前应用类型：1 调库 2 退库 3 修改 4 入库 5 收货 6 发货 7 退货 8 调柜（销售除外）
                case 1:
                    this.$router.push('/work/TransferStorage/DetailTransferStorageTable')
                    break;
                case 2:
                    this.$router.push('/work/storageReturn/detailStorageReturnTable')
                    break;
                case 4:
                    this.$router.push('/work/storage/detailStorageTable')
                    break;
                case 6:
                    this.$router.push('/work/sipping/detailSippingTable')
                    break;
                case 7:
                    this.$router.push('/work/SalesReturn/detailSalesReturnTable')
                    break;
                case 8:
                    this.$router.push('/work/TransferCabinet/detailTransferCabinetTable')
                    break;
            }
            console.log(parm)
            console.log(Type)
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
.font0{
    font-size: 0;
}
.table-template{
    min-width: 100%;
    height: 100%;
    // background-color: #f1f1f1;
    table{
        width: 100%;
        height: 100%;
        .tr-header{
            background-color: $e8!important;
            &:hover{
                background-color: $e8;
            }
        }
        tr{
            height: 40px;
            width: 100%;
        }
        tbody{
              tr:nth-child(odd){
                  background-color: #fff;
              }
              tr:nth-child(even){
                  background-color: $f6;
              }
              tr{
                  &:hover{
                      background-color: $e6;
                  }
              }
        }
        th,td{
            text-align: center;
            border-right: 1px solid $d6;
            border-bottom: 1px solid $d6;
            font-size: 12px;
            min-width: 70px;
            white-space:nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        td:last-child{
            border-right: 0;
        }
        th{
            color: $c6;
        }
        td{
            color: $c3;
            a{
                line-height: 24px;
            }
        }
    }
}
</style>