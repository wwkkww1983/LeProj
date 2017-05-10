<template>
<div class="template-preview-canvas-wrapper" :style="canvasWrapperStyle">
    <div class="template-preview-canvas" :style="canvasStyle">
        <component v-for="component in components" :is="component.type" :isPreview="true" :parent="null" class="component" :data="component.data" :templateData="templateData" @changeComponentData="changeComponentData(component, $event)" :changeComponentData="changeComponentData" :page="page"></component>
    </div>
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
    getPPI,
    transformFileURL
} from '../../utils/print'
import extend from 'lodash/extend'
export default {
        data() {
            return {
                ppi: getPPI(),
                windowWidth: window.innerWidth,
                windowHeight: window.innerHeight
            }
        },
        props: ['canvas', 'templateData', 'page', 'isPrintCanvas'],
        components: {
            TextComponent,
            ImageComponent,
            LineComponent,
            PropertyComponent,
            ItemListComponent,
            ContainerComponent
        },
        computed: {
            components() {
                return this.canvas.components.filter(component => !component.data.itemListId)
            },
            canvasWrapperStyle() {
                if (this.isPrintCanvas) {
                    return {
                        width: this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.height + 'mm' : this.canvas.width + 'mm',
                        height: this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.width + 'mm' : this.canvas.height + 'mm'
                    }
                } else {
                    let w = (this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.height : this.canvas.width) / 10 / 2.54 * this.ppi
                    let h = (this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.width : this.canvas.height) / 10 / 2.54 * this.ppi
                    let maxW = this.windowWidth / 2 - 2 * 20
                    let maxH = this.windowHeight * 0.6 - 55
                    let canvasRate = w / h
                    let dialogRate = maxW / maxH
                    let paddingTop = 0
                    let paddingLeft = 0
                    if (canvasRate > dialogRate) {
                        paddingTop = (maxH - maxW / canvasRate) / 2
                    } else {
                        paddingLeft = (maxW - maxH * canvasRate) / 2
                    }
                    if (canvasRate > dialogRate) {
                        return {
                            width: maxW + 'px',
                            height: maxW / canvasRate + 'px',
                            paddingTop: paddingTop + 'px',
                            paddingBottom: paddingTop + 'px',
                            paddingLeft: paddingLeft + 'px'
                        }
                    } else {
                        return {
                            width: maxH * canvasRate + 'px',
                            height: maxH + 'px',
                            paddingTop: paddingTop + 'px',
                            paddingLeft: paddingLeft + 'px',
                            paddingRight: paddingLeft + 'px'
                        }
                    }
                }
            },
            canvasStyle() {
                let w = (this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.height : this.canvas.width) / 10 / 2.54 * this.ppi
                let h = (this.canvas.rotateDeg == 90 || this.canvas.rotateDeg == 270 ? this.canvas.width : this.canvas.height) / 10 / 2.54 * this.ppi
                let maxW = this.windowWidth / 2 - 2 * 20
                let maxH = this.windowHeight * 0.6 - 55
                let canvasRate = w / h
                let dialogRate = maxW / maxH
                let percentage = 1
                if (canvasRate > dialogRate) {
                    percentage = maxW / w
                } else {
                    percentage = maxH / h
                }
                if (this.isPrintCanvas) {
                    percentage = 1
                }
                let translate = ''
                let rotateDeg = (this.canvas.rotateDeg + 360) % 360
                switch (rotateDeg) {
                    case 90:
                        translate = ' translateY(-' + this.canvas.height * percentage + 'mm)'
                        break;
                    case 180:
                        translate = ' translate(-' + this.canvas.width * percentage + 'mm, -' + this.canvas.height * percentage + 'mm)'
                        break;
                    case 270:
                        translate = ' translateX(-' + this.canvas.width * percentage + 'mm)'
                        break;
                }
                return {
                    width: this.canvas.width + 'mm',
                    height: this.canvas.height + 'mm',
                    backgroundImage: this.canvas.backgroundImage && ('url(' + transformFileURL(this.canvas.backgroundImage) + ')'),
                    backgroundSize: '100% 100%',
                    backgroundRepeat: 'no-repeat',
                    transform: 'rotate(' + rotateDeg + 'deg)' + translate + (this.isPrintCanvas ? '' : ' scale(' + percentage + ')'),
                    transformOrigin: '0 0'
                }
            },
        },
        methods: {
            // 改变组件的数据
            changeComponentData(component, {
                data
            }) {
                console.log('changeComponentData', component, data)
                extend(component.data, data)
            },
            setWindowSize() {
                this.windowWidth = window.innerWidth
                this.windowHeight = window.innerHeight
            }
        },
        mounted() {
            window.addEventListener('resize', this.setWindowSize)
        },
        destroyed() {
            window.removeEventListener('resize', this.setWindowSize)
        }
}
</script>

<style lang="scss">
.template-preview-canvas-wrapper {
    box-sizing: content-box;
    .template-preview-canvas {
        background-color: #fff;
        box-shadow: 0 0 2px #d6d6d6;
        position: relative;
        overflow: hidden;
        .component {
            position: absolute;
            cursor: default;
            user-select: none;
        }
    }
}
</style>