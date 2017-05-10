<template>
    <div class="amend main-wrap">
        <div class="main-box">
        公告
        </div>   
    </div>
</template>
<script>
export default {
    data () {
        return {
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
            }
        }
    },
    created () {
    },
    components: {
    },
    computed: {
    },
    watch: {
    },
    methods: {
        handleSizeChange (val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange (val) {
            this.crudData.currentPage = val;
            console.log(`当前页: ${val}`);
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