<template>
    <div class="amend main-wrap">
        <div class="main-box">
            <header class="header-box">
                <div class="header-operation">
                    <ul class="header-operation-btn">
                        <li @click="newStockIn" class="border-no">
                            <button>新建单据</button>
                        </li>
                        <li>打印单据</li>
                        <li>导出</li>
                        <li class="header-delect border-no" @click="crudData.operationCut = false">
                            <button class="btn-w100 border-no" :class="{'forbid-bg': newDatas.userType === '2'}" :disabled="newDatas.userType === '2'">删除</button>
                        </li>
                    </ul>
                    <ul class="select_operation">
                        <li>切换操作所属部门：</li>
                        <li class="select-w130">
                           <el-select ref="handleValue" v-model="newDatas.handleDeptId" placeholder="">
                                <el-option     
                                    v-for="item in applyDepartmentList"
                                    :label="item.depShopName"
                                    :value="item.depShopId">
                                    <span class="wh-100" @click="setUserType(item.type, item.depShopName)">{{item.depShopName}}</span>
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
                                    v-for="item in applyOnlyDepartment"
                                    :label="item.depShopName"
                                    :value="item.depShopId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>商品库位：</span>
                            <div class="select-w100">
                                <el-select filterable v-model="header.warehouseId" placeholder="">
                                    <el-option v-for="item in repositoryList"
                                        :label="item.repositoryName"
                                        :value="item.repositoryId">
                                    </el-option>
                                </el-select>
                            </div>
                        </li>
                        <li>
                            <span>制单人：</span>
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
                        :applyName = '"修改"'
                        :applyType = '"3"'
                        :auditListConfig = 'auditListConfig'
                        :auditThs = "auditThs"
                        :tittleThs="tittleThs"
                        :showStorageList='showStorageList'
                        :operationCut = 'crudData.operationCut'
                        :delectSelectSucces = 'crudData.delectSelectSucces'
                        :routerUrl = '"/work/amend/detailAmend"'
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
                    <a href="javascript: void(0)" class="cancel-btn-lg" @click="cancelDelect">取消</a>
                    <a href="javascript: void(0)" class="confirm-btn-lg" @click="batchDelect">确认</a>
                </div>
            </footer>
            <!-- 弹窗 -->
            <div>
                <!-- 新建修改单 -->
                <div class="dialog-w380-h420">
                    <el-dialog title="新建单据" v-model="popup.newPopup">
                        <section class="new-header radio-20">
                            <div class="new-body">              
                                <el-radio-group v-model="newDatas.switchId">
                                    <div class="rectangle-icon">
                                        <i></i>
                                        <span>选择商品库位</span>
                                    </div>
                                    <ul>
                                        <li v-for="item in repositoryList" @click="getName(item.repositoryName)">
                                            <el-radio :label='item.repositoryId'>{{item.repositoryName}}</el-radio>
                                        </li>
                                    </ul>
                                </el-radio-group>  
                            </div>
                        </section>
                        <span slot="footer">
                            <a href="javascript: void(0)" class="cancel-btn-w120" @click="popup.newPopup=false">取 消</a>
                            <a href="javascript: void(0)" class="confirm-btn-w120 mr0" @click="affirmNew">确 定</a>
                        </span>
                    </el-dialog>
                </div>
                <error></error>
            </div>
        </div>
    </div>
</template>
<script>
import {mapActions, mapGetters} from 'vuex'
import {receiptOperation} from './../../../Api/commonality/operate'
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
                shopId: null, // 店铺列表（计划分销）Id
                makeUserId: null, // 制单人
                auditorId: null // 审核人
            },
            newDatas: { // 新增数据(其它页面也用到的)
                switchId: null, // 库位Id（开关）
                switchName: null, // 库位名（开关）
                handleDeptId: null, // 操作部门Id
                handleDept: null, // 操作部门
                userType: null, // 1.部门，2.店铺（部门可以编辑，店铺不可以编辑）监听编辑权限
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
                newPopup: false // 产品类型
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
        this.workDepartmentList(); // 获取操作部门
    },
    components: {
        tableTemplate, error
    },
    computed: {
        ...mapGetters([
            "workProductListData", // 单据列表
            "applyDepartmentList", // 操作部门（含店铺）
            "shopLists", // 店铺列表(分销商)
            "applyOnlyDepartment", // 只有操作部门
            "workStorageList", // 商品列表（详细）
            "workDelectSelectsData", // 单据删除的数据集合
            "repositoryList", // 库位列表
            "productClass", // 产品类别
            "supplierListData", // 供应商列表
            // "userType", // 监听编辑权限
            "applyUserList" // 应用用户列表
        ]),
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
        'applyDepartmentList': function () { // 操作部门（含店铺）初始化数据
            console.log(this.applyDepartmentList);
            this.$refs.handleValue.selectedLabel = this.applyDepartmentList[0].depShopName; // 默认操作部门
            this.newDatas.handleDept = this.applyDepartmentList[0].depShopName // 默认操作部门
            this.newDatas.handleDeptId = this.applyDepartmentList[0].depShopId; // 默认操作部门Id
            this.newDatas.userType = this.applyDepartmentList[0].type;
        },
        'newDatas.handleDeptId': function () {
            this.workProductList({ //  获取单据列表
                "operationUnit": this.newDatas.userType, // 操作部门
                "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                "page": 1,
                "type": "3", // 修改
                "showView": 1
            })
        }
    },
    methods: {
        ...mapActions([
            // "workUserType", // 监听编辑权限
            "workProductList", // 单据列表
            "workShopList", // 店铺列表（计划分销）
            "workDepartmentList", // 获取操作部门
            "workProductClass", // 产品类别
            "workRepositoryList", // 库位列表
            "workSupplierList", // 供应商列表
            "workApplyUser" // 应用用户列表
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
            // this.header.shopId = null; // 店铺列表（分销商）
            // this.header.classesId = null; // 产品类别
            this.header.makeUserId = null; // 制单人
            this.header.auditorId = null; // 审核人
        },
        seekFun () { // 查询
            this.workProductList({
                "operationUnit": this.$refs.handleValue.selectedLabel, // 操作部门
                "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                "page": this.crudData.currentPage,
                "type": 3, // 修改
                "showView": 1, // 显示列表：1 显示列表 2 编辑列表 3 查询结果 4 可收货列表
                "startTime": this.header.startTime, // 开始时间
                "endTime": this.header.endTime, // 结束时间
                "orderNumber": this.header.orderNumber, // 单号
                // "supplierId": this.header.supplierId, // 供应商
                // "shopId": this.header.shopId, // 店铺列表（分销商）
                "warehouseId": this.header.warehouseId // 入库库位
                // "classesId": this.header.classesId // 产品类别
            })
        },
        setUserType (parm, parm2) { // 监听编辑权限
            this.newDatas.userType = parm;
            this.newDatas.handleDept = parm2; // 操作部门名
        },
        cancelDelect () { // 取消删除
            this.crudData.delectSelectSucces = true;
            this.crudData.operationCut = true;
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
                    this.workProductList({ //  获取单据列表
                        "operationUnit": this.newDatas.userType, // 操作部门
                        "operationUnitId": this.newDatas.handleDeptId, // 操作部门Id
                        "page": 1,
                        "type": "3", // 修改
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
            this.popup.newPopup = true;
        },
        getName (parm) { // 绑定库位名
            this.newDatas.switchName = parm;
        },
        affirmNew () { // 确定新建
            if (!this.newDatas.handleDeptId) {
                alert("请选择操作部门")
                return;
            } else if (!this.newDatas.switchId) {
                alert("请选择库位")
                return;
            } else {
                this.popup.newPopup = false;
                // 存本地可以防止刷新
                // this.newDatas.userType = this.userType; // 监听编辑权限（部门）
                sessionStorage.setItem("newDatas", JSON.stringify(this.newDatas));
                this.$router.push('/work/amend/newAmend');
            }
            this.popup.newPopup = false;
        }
    }
}
</script>
<style lang="scss" scoped>
.main-wrap{
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