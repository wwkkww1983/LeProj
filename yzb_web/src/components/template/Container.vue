<template>
<div class="container-component" :style="componentStyle" :class="{borderRender: data.border}">
    <component v-for="component in data.children" :is="component.type" :isPreview="isPreview" :parent="data" class="component" :class="{active: component.active}" :data="component.data" :templateData="templateData" :page="page" @changeComponentData="changeComponentData(component, $event)" :changeComponentData="changeComponentData">
    </component>
</div>
</template>

<script>
import TextComponent from 'components/template/Text'
import ImageComponent from 'components/template/Image'
import LineComponent from 'components/template/Line'
import PropertyComponent from 'components/template/Property'
import ItemListComponent from 'components/template/ItemList'
import {
    getComponentBound,
    getComponentTranslate
} from '../../utils/print'

export default {
    props: ['isPreview', 'data', 'templateData', 'page', 'changeComponentData'],
    computed: {
        // 渲染容器的样式，根据子组件的边界来确定容器的位置和大小，不一致则调整过来
        componentStyle() {
            let tops = []
            let lefts = []
            let rights = []
            let bottoms = []
            this.data.children.forEach(component => {
                let {
                    left,
                    top,
                    right,
                    bottom
                } = getComponentBound(component.data, this.data)
                tops.push(top)
                lefts.push(left)
                rights.push(right)
                bottoms.push(bottom)
            })
            let top = Math.min.apply(this, tops)
            let left = Math.min.apply(this, lefts)
            let right = Math.max.apply(this, rights)
            let bottom = Math.max.apply(this, bottoms)
            let width = right - left
            let height = bottom - top
            if (top != this.data.top || left != this.data.left || width != this.data.width || height != this.data.height) {
                this.$emit('changeComponentData', {
                    data: {
                        top,
                        left,
                        width,
                        height
                    },
                    shouldUpdate: false
                })
            }
            return {
                top: top + 'mm',
                left: left + 'mm',
                width: width + 'mm',
                height: height + 'mm',
                transform: 'rotate(' + this.data.rotateDeg + 'deg) ' + getComponentTranslate(this.data),
                transformOrigin: '0 0',
                zIndex: this.data.zIndex
            }
        }
    },
    components: {
        TextComponent,
        ImageComponent,
        LineComponent,
        PropertyComponent,
        ItemListComponent
    }
}
</script>

<style lang="scss">
.container-component {
    box-sizing: content-box;
    &.active {
        border: 1px dashed #4ec0ff;
    }
    &.borderRender {
        border: 1px solid #000;
    }
}
</style>