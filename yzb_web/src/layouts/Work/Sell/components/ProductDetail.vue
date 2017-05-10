<template>
    <div>
    <input-recovery v-show="show.recovery" :inputRecoveryData='inputRecoveryData'></input-recovery>
    <input-recovery-blind v-show="show.recoveryBlind" :inputRecoveryBlindData='inputRecoveryBlindData'></input-recovery-blind>
    <input-replace v-show="show.replace" :inputReplaceData='inputReplaceData'></input-replace>
    <input-sale v-show="show.sale" :inputSaleData='inputSaleData'></input-sale>
    <input-sale-blind v-show="show.saleBlind" :inputSaleBlindData='inputSaleBlindData'></input-sale-blind>
    </div>
</template>
<script>
import inputRecovery from './../components/InputRecovery'
import inputRecoveryBlind from './../components/InputRecoveryBlind'
import inputReplace from './../components/InputReplace'
import inputSale from './../components/InputSale'
import inputSaleBlind from './../components/InputSaleBlind'
    export default {
        data () {
            return {
                "inputRecoveryData": "", // 素金类回收
                "inputRecoveryBlindData": "", // 宝石类回收
                "inputReplaceData": "", // 素金类替换
                "inputSaleData": "", // 素金类替换
                "inputSaleBlindData": "",
                "show": {
                    "recovery": false,
                    "recoveryBlind": false,
                    "replace": false,
                    "sale": false,
                    "saleBlind": false
                }
            }
        },
        components: {
            inputRecovery,
            inputRecoveryBlind,
            inputReplace,
            inputSale,
            inputSaleBlind
        },
        created () {
            this.running();
        },
        methods: {
            running () {
                this.show.sale = true;
            }
        }
    }
</script>