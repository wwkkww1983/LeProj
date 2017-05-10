<template>
    <div class="storage">
        <div class="main-box">
            <header class="header-box">
                <div class="header-operation">
                    <ul class="header-operation-btn">
<!--                         <li @click="newStockIn" class="border-no">
                         <button class="btn-w100 bg-ff main-color border-color-m" :class="{'forbid-bg': newDatas.userType === '2'}" :disabled="newDatas.userType === '2'">收货</button>
                        </li> -->
                        <li class="header-delect border-no" @click="crudData.operationCut = false">
                            <button class="btn-w100 border-no" @click="getReceiving">收货</button>
                        </li>
                        <li>打印单据</li>
                        <li>导出</li>
<!--                         <li class="header-delect border-no" @click="crudData.operationCut = false">
                            <button class="btn-w100 border-no" :class="{'forbid-bg': newDatas.userType === '2'}" :disabled="newDatas.userType === '2'">删除</button>
                        </li> -->
                    </ul>
                    <ul class="select_operation">
                        <li>切换操作所属部门：</li>
                        <li class="select-w130">
                           <el-select ref="handleValue" v-model="newDatas.handleDeptId" placeholder="">
                                <el-option     
                                    v-for="item in shopListByCo"
                                    :label="item.shopName"
                                    :value="item.shopId">
                                </el-option>
                            </el-select>
                        </li>
                    </ul>
                </div>
                <div class="header-seek">
                    <ul>
                        <li>
                            <i class="must-star"></i>
                            <span>日期：</span>
                            <div class="date-w125">
                                <el-date-picker
                                v-model="header.startTime"
                                type="date"
                                placeholder="选择日期时间">
                                </el-date-picker>
                            </div>
                            <span class="middle-title">至</span>
                           <div class="date-w125">
                                <el-date-picker
                                v-model="header.endTime"
                                type="date"
                                placeholder="选择日期时间">
                                </el-date-picker>
                            </div>
                        </li>
                        <li>
                            <span>单号：</span>
                            <div class="input-w100">
                                <el-input v-model="header.orderNumber"></el-input>
                            </div>
                        </li>
                        <li>
                            <span>操作部门：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.depShopId" placeholder="">
                                    <el-option     
                                        v-for="item in shopListByCo"
                                        :label="item.shopName"
                                        :value="item.shopId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>发货库位：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.warehouseId" placeholder="">
                                    <el-option v-for="item in repositoryList"
                                        :label="item.repositoryName"
                                        :value="item.repositoryId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
<!--                         <li>
                            <span>分销商：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.shopId" placeholder="">
                                    <el-option v-for="item in shopLists"
                                        :label="item.shopName"
                                        :value="item.shopId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li> -->
                        <li>
                            <span>收货人：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.makeUserId" placeholder="">
                                    <el-option v-for="item in applyUserList"
                                    :label="item.userName"
                                    :value="item.userId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>审核人：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.auditorId" placeholder="">
                                    <el-option v-for="item in applyUserList"
                                    :label="item.userName"
                                    :value="item.userId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <a href="javascript:void(0)" class="confirm-btn-small" @click="seekFun">查询</a>
                            <a href="javascript:void(0)" class="confirm-btn-small" @click="resetFun">重置</a>
                        </li>
                    </ul>
                </div>
            </header>
            <section class="body-box">
                <div class="body-table">
                    <table-template
                        :newDatas = 'newDatas'
                        :applyName = '"收货"'
                        :applyType = '5'
                        :auditListConfig = "auditListConfig"
                        :auditThs = "auditThs"
                        :tittleThs="tittleThs"
                        :showStorageList='showStorageList'
                        :operationCut = 'crudData.operationCut'
                        :delectSelectSucces = 'crudData.delectSelectSucces'
                        :routerUrl = '"/work/receiving/detailReceiving"'
                        :page = 'crudData.currentPage'
                    ></table-template>
                </div>
                <div class="body-page">
                    <div class="paga-30">
                        <el-pagination
                            @size-change="handleSizeChange"
                            @current-change="handleCurrentChange"
                            :current-page="crudData.currentPage"
                            :page-sizes="[30]"
                            :page-size="30"
                            layout="total, sizes, prev, pager, next, jumper"
                            :total="workProductListData.totalDataNum">
                        </el-pagination>
                    </div>
                </div>
            </section>
            <footer class="footer-box" v-if="!crudData.operationCut">
                <div class="footer-affirm">
                    <a href="javascript: null" class="cancel-btn-lg" @click="cancelDelect">取消</a>
                    <a href="javascript: null" class="confirm-btn-lg" @click="batchDelect">确认</a>
                </div>
            </footer>
            <!-- 弹窗 -->
            <error></error>
            </div>
        </div>
    </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import {shTakeDelivery} from './../../../Api/commonality/operate'
import tableTemplate from "./../CommonalityComponent/tableTemplate"
import error from "./../CommonalityComponent/popupTemplate/error"
export default {
    data () {
        return {
            auditThs: [ // 审核弹窗
                {name: '单号'},
                {name: '操作部门'},
                {name: '发库库位'},
                {name: '分销商'},
                {name: '总件数'},
                {name: '总重量'},
                {name: '总金额'},
                {name: '制单人'},
                {name: '制单时间'},
                {name: '单据备注'}
            ],
            auditListConfig: { // 审核弹窗
                "editLimit": null, // 权限列表1 删除 2 保存 3 取消审核 4 驳回审核 5 通过审核 6 撤销审核 7 提交审核
                "orderNum": null, // 单号
                "handleDept": null, // 操作部门
                "warehouse": null, // 发库库位
                "shop": null, // 分销商
                "totalNum": null, // 总件数
                "totalWeight": null, // 总重量
                "totalMoney": null, // 总金额
                "makeOrderUser": null, // 制单人
                "createTime": null, // 制单时间
                "orderRemark": null // 单据备注
            },
            tittleThs: [
                // {"name": '序号'},
                {"name": '单号'},
                {"name": '操作部门'},
                {"name": '发库库位'},
                {"name": '分销商'},
                {"name": '总件数'},
                {"name": '总重量'},
                {"name": '总金额'},
                {"name": '制单人'},
                {"name": '制单时间'},
                {"name": '收货情况'},
                {"name": '审核人'},
                {"name": '审核时间'},
                {"name": '单据备注'},
                {"name": '状态'}
            ],
            configData: {
                "orderId": null, // 单据id,用来查明细用的
                "orderNumber": null, // 单号
                "handleDept": null, // 操作部门
                "warehouse": null, // 发库库位
                "shop": null, // 分销商
                "totalNum": null, // 总件数
                "totalWeight": null, // 总重量
                "totalMoney": null, // 总金额
                "makeOrderUser": null, // 制单人
                "createTime": null, // 制单时间
                "receivedStatus": null, // 收货情况
                "checkUser": null, // 审核人
                "checkTime": null, // 审核时间
                "orderRemark": null, // 单据备注
                "status": null // 状态
            },
            header: {
                startTime: '',
                endTime: '',
                // supplierId: null, // 供应商
                orderNumber: null, // 开始单号
                warehouseId: null, // 计划分销Id
                depShopId: null, // 操作部门（seek）
                // classesId: null, // 产品类别
                shopId: null, // 店铺列表（计划分销）Id
                makeUserId: null, // 收货人
                auditorId: null // 审核人
            },
            newDatas: { // 新增数据(其它页面也用到的)
                handleDeptId: null, // 操作部门Id
                userType: 2, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
                // classesId: null, // 产品类别Id
                // classesName: null, // 产品类别名
                sippingLocationId: null, // 发货库位Id
                sippingLocation: null, // 发货库位
                orderNumber: null // 单据号
            },
            crudData: { // 增查更删数据
                checked: false, // 监听全选
                checkList: [], // 选择按钮（选中的数据集合）删除
                delectSelectSucces: false, // 监听删除成功
                operationCut: true, // 操作按钮切换
                currentPage: 1 // 当前页
            },
            popup: { // 弹窗
                productClass: false // 产品类型
            }
        }
    },
    created () {
        this.workApplyUser({ // 获取公司所有有这个应用的人
            "type": "2",
            "objId": sessionStorage.getItem("companyId")
        }); // 应用用户列表
        this.workRepositoryList(); // 库位列表
        this.workShopList(); // 店铺列表（分销商）
        this.workSupplierList(); // 供应商列表
        this.workProductClass(); // 产品类别
        this.getShopListByCo(); // 店铺列表
    },
    components: {
        tableTemplate, error
    },
    computed: {
        ...mapGetters([
            "workProductListData", // 单据列表
            "applyDepartmentList", // 操作部门（含店铺）
            "shopLists", // 店铺列表(分销商)
            "workStorageList", // 商品列表（详细）
            "workDelectSelectsData", // 单据删除的数据集合
            "repositoryList", // 库位列表
            "productClass", // 产品类别
            "supplierListData", // 供应商列表
            // "userType", // 监听编辑权限
            "applyUserList", // 应用用户列表
            "shopListByCo" // 店铺列表
        ]),
        // audiuPopupList: function () {
        //     var showData = [];
        //     if (this.workProductListData.dataList) {
        //         for (let i = 0; i < this.workProductListData.dataList.length; i++) {
        //             let newAddDatas = {};
        //             for (let key in this.auditListConfig) {
        //                 newAddDatas[key] = this.workProductListData.dataList[i][key] || null;
        //             }
        //             showData.push(newAddDatas);
        //         }
        //     }
        //     return showData;
        // },
        showStorageList: function () {
            var showData = [];
            if (this.workProductListData.dataList) {
                for (let i = 0; i < this.workProductListData.dataList.length; i++) {
                    let newAddDatas = {};
                    for (let key in this.configData) {
                        newAddDatas[key] = this.workProductListData.dataList[i][key] || null;
                    }
                    showData.push(newAddDatas);
                }
            }
            return showData;
        },
        productClassPull: function () {
            var productClass = [];
            for (var i = 0; i < this.productClass.length; i++) {
                productClass.push(...this.productClass[i].typeList)
            }
            return productClass;
        }
    },
    watch: {
        'shopListByCo': function () {
            this.$refs.handleValue.selectedLabel = this.shopListByCo[0].shopName; // 默认操作部门
            this.newDatas.handleDeptId = this.shopListByCo[0].shopId; // 默认操作部门Id
        },
        'newDatas.handleDeptId': function () {
            this.workProductList({ //  获取单据列表
                "operationUnit": this.newDatas.userType, // 操作部门
                "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                "page": 1,
                "type": "5",
                "showView": 1
            })
        }
    },
    methods: {
        ...mapActions([
            // "workUserType", // 监听编辑权限
            "workProductList", // 单据列表
            "workShopList", // 店铺列表（计划分销）
            "workProductClass", // 产品类别
            "workRepositoryList", // 库位列表
            "workSupplierList", // 供应商列表
            "workApplyUser", // 应用用户列表
            "getShopListByCo" // 店铺列表
        ]),
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
        },
        resetFun () { // 重置
            this.header.startTime = ''; // 开始时间
            this.header.endTime = ''; // 结束时间
            this.header.orderNumber = null; // 单号
            // this.header.supplierId = null; // 供应商
            // this.header.shopId = null; // 店铺列表（计划分销）
            this.header.depShopId = null; // 操作部门
            this.header.warehouseId = null; // 入库库位
            this.header.shopId = null; // 店铺列表（分销商）
            // this.header.classesId = null; // 产品类别
            this.header.makeUserId = null; // 收货人
            this.header.auditorId = null; // 审核人
        },
        seekFun () { // 查询
            this.workProductList({
                "operationUnit": this.$refs.handleValue.selectedLabel, // 操作部门
                "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                "page": this.crudData.currentPage,
                "type": "5", // 发库
                "showView": 4, // 显示列表：1 显示列表 2 编辑列表 3 查询结果 4 可收货列表
                "startTime": this.header.startTime, // 开始时间
                "endTime": this.header.endTime, // 结束时间
                "orderNumber": this.header.orderNumber, // 单号
                // "supplierId": this.header.supplierId, // 供应商
                "shopId": this.header.shopId, // 店铺列表（分销商）
                "warehouseId": this.header.warehouseId // 入库库位
                // "classesId": this.header.classesId // 产品类别
            })
            alert("查询")
        },
        getReceiving () { // 获取可收获数据
            this.workProductList({ //  获取单据列表
                "operationUnit": this.newDatas.userType, // 操作部门
                "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                "page": "1",
                "type": "5", // 收货
                "showView": "4" // 可收货
            })
        },
        cancelDelect () { // 取消删除
            this.crudData.delectSelectSucces = true;
            this.crudData.operationCut = true;
        },
        // getReceivingOrderId () { // 获取所有收获Id
            // console.log("999")
        // },
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
                "orderId": orderIdList[0].orderId // 单据Id,
                // "checkRemark": "审核批注"// 审核批注
            }
            shTakeDelivery(data).then((response) => {
                console.log("收获回调");
                console.log(response);
                if (response.data.state === 200) {
                    this.workProductList({ //  获取单据列表
                        "operationUnit": this.newDatas.userType, // 操作部门
                        "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                        "page": 1,
                        "type": "5",
                        "showView": 1
                    })
                } else {
                    alert(response.data.msg);
                }
            }, (response) => {
                console.log("确定删除回调");
                console.log(response);
            })
            this.crudData.delectSelectSucces = true;
            this.crudData.operationCut = true;
        },
        newStockIn () { // 新建发货
            this.popup.productClass = true;
        },
        getsippingLocation (parm) { // 绑定库位名
            this.newDatas.sippingLocation = parm;
            console.log(parm)
        },
        affirmNew () { // 确定新建
            if (!this.newDatas.handleDeptId) {
                alert("请选择操作部门")
                return;
            } else if (!this.newDatas.sippingLocation) {
                alert("请选择库位")
                return;
            } else {
                this.popup.productClass = false;
                // 存本地可以防止刷新
                // this.newDatas.userType = this.userType; // 监听编辑权限（部门）
                sessionStorage.setItem("newStockData", JSON.stringify(this.newDatas));
                this.$router.push('/work/sipping/newSipping');
            }
            this.popup.productClass = false;
        },
        submitAudit (parm) { // 提交审核
            console.log("提交审核");
            console.log(parm);
        }
    }
}
</script>
<style lang="scss" scoped>
.storage{
    width: 100%;
    height: 100%;
    padding: 0 20px;
}
.new-data{ // 新建发货弹窗
    font-size: 0;
    margin-top: 30px;
    height: 280px;
    overflow: hidden;
    background-color: #fff;
    .audit-body-body{
        padding: 0 30px;
        ul{
            margin-top: 10px;
            vertical-align: top;
            font-size: 0;
            height: 250px;
            overflow: auto;
            li{
                height: 50px;
                line-height: 50px;
                font-size: 12px;
                margin-bottom: 10px;
                border-bottom: 1px solid #d6d6d6;
                span{
                    padding-left: 0;
                }
            }
        }
    }
}
</style>