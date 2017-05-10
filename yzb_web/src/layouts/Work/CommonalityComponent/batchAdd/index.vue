<template>
    <div>
        <router-view></router-view>
        <batch-add></batch-add>
    </div>
</template>
<script>
import batchAdd from "./BatchAdd"
export default {
    data () {
        return {
            present: null
        }
    },
    components: {
        batchAdd
    }
}
</script>