<template>
    <div class="dialog-w320-success">
        <el-dialog v-model="saveSuccesss">
            <section class="success-body">
                <img src="./../../../../assets/img/commonality/succeed.png">
                <p>制单成功</p>
<!--                 <p>{{successData}}</p> -->
                <ul v-if="successData">
                    <li>单<i class="w24"></i>号：</li>
                    <li>{{successData.orderNumber || successData.orderNum}}</li>
                    <li>制<i class="w6"></i>单<i class="w6"></i>人：</li>
                    <li>{{userInfo.nickname}}</li>
                    <li>制单时间：</li>
                    <li>{{successData.operateTime || successData.createTime}}</li>
                </ul>
            </section>
            <span slot="footer">
                <a href="javascript: null" class="confirm-btn-w120 m0" @click="goDetail">确 定</a>
            </span>
        </el-dialog>
    </div>
</template>
<script>
import {mapGetters} from 'vuex'
export default {
    props: [
        'routerUrl',
        'successData'
    ],
    data () {
        return {
            "saveSuccesss": true,
            "newDatas": JSON.parse(sessionStorage.getItem("newDatas"))
        }
    },
    computed: {
        ...mapGetters([
            "userInfo"
        ])
    },
    methods: {
        goDetail () { // 去到详情页
            // let newDatas = this.newDatas;
            // 由于后台返回的单据号的key不一致，就直接在制单成功后直接存
            // newDatas.orderNumber = this.successData.orderNumber;
            // sessionStorage.setItem("orderId", this.successData.orderId)
            // sessionStorage.setItem("newDatas", JSON.stringify(newDatas))
            console.log("去到的地址");
            console.log(this.routerUrl);
            this.$router.push(this.routerUrl);
            // setTimeout(function () {
            //     if (this.routerUrl) {
            //         this.$router.push(this.routerUrl);
            //     }
            // }, 100)
        }
    }
}
</script>