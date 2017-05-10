<template>
<div class="image-component" :style="componentStyle">
    <img :src="src" @dragstart.prevent :style="imageStyle">
    <div class="resize"></div>
</div>
</template>

<script>
import {
    getComponentTranslate,
    transformFileURL
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
            imageStyle() {
                return {
                    width: this.data.width + 'mm',
                    height: this.data.height + 'mm'
                }
            },
            src() {
                if (this.data.src) {
                    return transformFileURL(this.data.src)
                } else {
                    return '/static/img/image-sample.png'
                }
            }
        }
}
</script>

<style lang="scss">
.image-component {
    position: relative;
    img {
        display: block;
    }
    .resize {
        display: none;
        position: absolute;
        opacity: 0;
        filter: alpha(opacity=0);
        width: 20px;
        height: 20px;
        top: 100%;
        left: 100%;
        margin-left: -12px;
        margin-top: -12px;
        cursor: nwse-resize;
    }
    &.active {
        opacity: .7;
        border: 1px dashed #4ec0ff;
        .resize {
            display: block;
        }
    }
}
</style>