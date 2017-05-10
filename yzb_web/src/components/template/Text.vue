<template>
<div class="text-component" :style="componentStyle" :class="{borderRender: data.border}">
    <span v-html="rawContent" :style="textStyle"></span>
</div>
</template>

<script>
import {
    getOuterWidth,
    getOuterHeight,
    getPPI,
    getComponentTranslate
} from '../../utils/print'
export default {
    data() {
            return {
                ppi: getPPI()
            }
        },
        props: ['isPreview', 'parent', 'data'],
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
            textStyle() {
                return {
                    fontWeight: this.data.isBold ? 'bold' : 'normal',
                    fontStyle: this.data.isItalic ? 'italic' : 'normal',
                    textDecoration: this.data.isUnderline ? 'underline' : 'none',
                    fontFamily: this.data.fontFamily,
                    fontSize: this.data.fontSize + 'px',
                    color: this.data.color
                }
            },
            rawContent() {
                return this.data.content.replace(/ /g, '&nbsp;').replace(/\n/g, '<br/>')
            }
        },
        watch: {
            data: {
                handler() {
                    window.setTimeout(() => {
                        this.computeSize()
                    }, this.isPreview ? 500 : 0)
                },
                deep: true
            }
        },
        methods: {
            computeSize() {
                let w = Math.round(getOuterWidth(this.$el) / this.ppi * 2.54 * 10)
                let h = Math.round(getOuterHeight(this.$el) / this.ppi * 2.54 * 10)
                if (w != this.data.width || h != this.data.height) {
                    // console.log(this.$el, this.data.content, this.data.width, this.data.height, w, h)
                    if (!(isNaN(w) || isNaN(h))) {
                        this.$emit('changeComponentData', {
                            data: {
                                width: w,
                                height: h
                            },
                            shouldUpdate: false
                        })
                    }
                }
            }
        },
        mounted() {
            setTimeout(() => {
                this.computeSize()
            }, this.isPreview ? 500 : 0)
        }
}
</script>
<style lang="scss">
.text-component {
    padding: 5px;
    white-space: nowrap;
    display: flex;
    &.active {
        border: 1px dashed #4ec0ff;
        padding: 4px;
        background-color: rgba(78, 192, 255, .15);
    }
    &.borderRender {
        border: 1px solid #000;
        padding: 4px;
    }
    span {
        line-height: 1;
    }
}
</style>