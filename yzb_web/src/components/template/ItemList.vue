<template>
<div class="item-list-component" :style="componentStyle" v-if="!isPreview">
    <div class="resize"></div>
</div>
<div class="item-list-component" v-else>
    <component v-for="child in items" :is="child.type" :isPreview="true" :parent="null" class="component" :data="child.data" :templateData="templateData" @changeComponentData="changeComponentData(child, $event)" :changeComponentData="changeComponentData"></component>
</div>
</template>

<script>
import TextComponent from './Text'
import ImageComponent from './Image'
import LineComponent from './Line'
import PropertyComponent from './Property'
import ItemListComponent from './ItemList'
import ContainerComponent from './Container'
import {
    getComponentTranslate
} from '../../utils/print'
export default {
    props: ['isPreview', 'parent', 'data', 'templateData', 'page', 'changeComponentData'],
    components: {
        TextComponent,
        ImageComponent,
        LineComponent,
        PropertyComponent,
        ItemListComponent,
        ContainerComponent
    },
    data() {
        return {
            children: []
        }
    },
    computed: {
        componentStyle() {
            let top = this.parent ? this.data.top - this.parent.top : this.data.top
            let left = this.parent ? this.data.left - this.parent.left : this.data.left
            return {
                top: top + 'mm',
                left: left + 'mm',
                width: this.data.width + 'mm',
                height: this.data.height + 'mm',
                transform: 'rotate(' + this.data.rotateDeg + 'deg) ' + getComponentTranslate(this.data),
                transformOrigin: '0 0',
                zIndex: this.data.zIndex
            }
        },
        items() {
            //如果预览时，渲染商品列表，返回属性列表
            this.children = []
            let top = this.data.top
            let h = this.data.height
            let number = this.data.number
            //分页商品列表
            let productList = this.templateData.productList.slice((this.page - 1) * number, this.page * number)
            //渲染的行数
            number = Math.min(number, productList.length)
            if (number) {
                //数据之间的间距
                let gap = h / (number + 1)
                    //每条数据克隆每个属性，定位属性的位置，传递productIndex来定位属性所属的product
                productList.forEach((item, i) => {
                    let items = JSON.parse(JSON.stringify(this.data.children))
                    items.forEach(child => {
                        child.data.productIndex = (this.page - 1) * this.data.number + i
                        let itemTop = top + (i + 1) * gap - 0.5 * child.data.height
                        if (child.type === 'ContainerComponent') {
                            let dy = itemTop - child.data.top
                            child.data.children.forEach(tmp => {
                                tmp.data.productIndex = (this.page - 1) * this.data.number + i
                                tmp.data.top += dy
                            })
                        } else {
                            child.data.top = itemTop
                        }
                    })
                    this.children = this.children.concat(items)
                })
            } else {
                //还没加载数据就渲染属性的样本值，只有一行
                this.data.children.forEach(child => {
                    let childClone = JSON.parse(JSON.stringify(child))
                    childClone.data.productIndex = 0
                    this.children.push(childClone)
                })
            }
            return this.children
        }
    },
}
</script>

<style lang="scss">
.item-list-component {
    background-color: rgba(10, 191, 171, .2);
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
        border: 1px dashed #4ec0ff;
        margin-left: -1px;
        margin-top: -1px;
        display: block;
        .resize {
            display: block;
        }
    }
}
</style>