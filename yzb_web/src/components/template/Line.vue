<template>
<div class="line-component" :style="componentStyle">
    <div class="line" :style="lineStyle"></div>
</div>
</template>

<script>
import {
    getComponentTranslate
} from '../../utils/print'
export default {
        props: ['data', 'parent'],
        computed: {
            componentStyle() {
                let top = this.parent ? this.data.top - this.parent.top : this.data.top
                let left = this.parent ? this.data.left - this.parent.left : this.data.left
                return {
                    top: top + 'mm',
                    left: left + 'mm',
                    transform: 'rotate(' + this.data.rotateDeg + 'deg) ' + getComponentTranslate(this.data),
                    transformOrigin: '0 0',
                    zIndex: this.data.zIndex
                }
            },
            lineStyle() {
                return {
                    width: this.data.width + 'mm',
                    height: this.data.height + 'mm',
                    backgroundColor: this.data.color
                }
            }
        },
}
</script>

<style lang="scss">
.line-component {
    &.active {
        border: 1px dashed #4ec0ff;
    }
}
</style>