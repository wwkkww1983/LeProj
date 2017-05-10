<template>
<div class="property-component barcode" v-show="isShown" :style="componentStyle" v-if="data.propertyType == 4">
    <img ref="barcode" id="barcode" src="/static/img/barcode-sample.png" @dragstart.prevent :style="barcodeStyle">
    <div class="resize" v-if="!isPreview"></div>
</div>
<div class="property-component string" v-show="isShown" :style="componentStyle" :class="{borderRender: data.border}" v-else>
    <span :style="prefixStyle" v-html="rawPrefix"></span><span :style="valueStyle">{{ value }}</span><span :style="suffixStyle" v-html="rawSuffix"></span>
</div>
</template>

<script>
import {
    getOuterWidth,
    getOuterHeight,
    getPPI,
    getComponentTranslate
} from '../../utils/print'
import moment from 'moment'
import JsBarcode from 'jsbarcode'
import find from 'lodash/find'

export default {
        data() {
            return {
                ppi: getPPI(),
                isNull: false
            }
        },
        props: ['isPreview', 'parent', 'data', 'page', 'templateData'],
        computed: {
            isShown() {
                return !this.isPreview || this.data.isNullPrint || !this.isNull
            },
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
            barcodeStyle() {
                return {
                    width: this.data.width + 'mm',
                    height: this.data.height + 'mm'
                }
            },
            prefixStyle() {
                return {
                    fontWeight: this.data.prefixStyle.isBold ? 'bold' : 'normal',
                    fontStyle: this.data.prefixStyle.isItalic ? 'italic' : 'normal',
                    textDecoration: this.data.prefixStyle.isUnderline ? 'underline' : 'none',
                    fontFamily: this.data.prefixStyle.fontFamily,
                    fontSize: this.data.prefixStyle.fontSize + 'px',
                    color: this.data.prefixStyle.color,
                    verticalAlign: this.data.verticalAlign
                }
            },
            rawPrefix() {
                return this.data.prefix.replace(/ /g, '&nbsp;').replace(/\n/g, '<br/>')
            },
            valueStyle() {
                return {
                    fontWeight: this.data.valueStyle.isBold ? 'bold' : 'normal',
                    fontStyle: this.data.valueStyle.isItalic ? 'italic' : 'normal',
                    textDecoration: this.data.valueStyle.isUnderline ? 'underline' : 'none',
                    fontFamily: this.data.valueStyle.fontFamily,
                    fontSize: this.data.valueStyle.fontSize + 'px',
                    color: this.data.valueStyle.color,
                    verticalAlign: this.data.verticalAlign
                }
            },
            suffixStyle() {
                return {
                    fontWeight: this.data.suffixStyle.isBold ? 'bold' : 'normal',
                    fontStyle: this.data.suffixStyle.isItalic ? 'italic' : 'normal',
                    textDecoration: this.data.suffixStyle.isUnderline ? 'underline' : 'none',
                    fontFamily: this.data.suffixStyle.fontFamily,
                    fontSize: this.data.suffixStyle.fontSize + 'px',
                    color: this.data.suffixStyle.color,
                    verticalAlign: this.data.verticalAlign
                }
            },
            rawSuffix() {
                return this.data.suffix.replace(/ /g, '&nbsp;').replace(/\n/g, '<br/>')
            },
            value() {
                let code = this.data.propertyCode
                let product = this.templateData.productList[this.data.productIndex || 0]
                let value = null
                let mapcode = null
                this.isNull = false
                if (product && this.isPreview) {
                    mapcode = find(product.codeList, {
                        key: code
                    })
                    if (!mapcode) {
                        mapcode = find(this.templateData.baseInfoList, {
                            key: code
                        })
                    }
                    if (mapcode) {
                        value = mapcode.value
                        this.isNull = mapcode.isNull != 0
                    }
                } else {
                    if (this.data.sample) {
                        value = this.data.sample
                        this.isNull = false
                    } else {
                        this.isNull = true
                        return '#{' + code + '}'
                    }
                }
                if (this.data.propertyType == 1 || this.data.propertyType == 3) {
                    return Number(value).toFixed(this.data.toFixed)
                } else {
                    if (this.data.propertyType == 2) {
                        return moment(value).format(this.data.dateFormat)
                    } else {
                        return value
                    }
                }
            },
        },
        watch: {
            data: {
                handler(data) {
                    window.setTimeout(() => {
                        this.computeSize()
                    }, this.isPreview ? 500 : 0)
                },
                deep: true
            },
            value() {
                window.setTimeout(() => {
                    this.computeSize()
                }, this.isPreview ? 500 : 0)
            },
            'templateData.productList' () {
                let code = this.data.propertyCode
                let product = this.templateData.productList[this.data.productIndex || 0]
                let value = null
                let mapcode = null
                if (product) {
                    mapcode = find(product.codeList, {
                        key: code
                    })
                    if (!mapcode) {
                        mapcode = find(this.templateData.baseInfoList, {
                            key: code
                        })
                    }
                    if (mapcode) {
                        value = mapcode.value
                        if (value != this.data.sample) {
                            this.$emit('changeComponentData', {
                                data: {
                                    sample: value
                                },
                                shouldUpdate: false
                            })
                        }
                    }
                }
            },
            'data.sample' (sample) {
                if (this.data.propertyType == 4) {
                    if (sample.length == 8) {
                        JsBarcode('#barcode', sample, {
                            displayValue: false
                        })
                    } else {
                        this.$refs.barcode.src = '/static/img/barcode-sample.png'
                    }
                }
            },
            'data.left' () {
                this.computeAlignNumber()
            },
            'data.textAlign' () {
                this.computeAlignNumber()
            },
        },
        methods: {
            computeAlignNumber() {
                if (this.data.propertyType != 4 && !this.isPreview) {
                    let textAlign = this.data.textAlign
                    let alignNumber = 0
                    switch (textAlign) {
                        case 'left':
                            alignNumber = this.data.left
                            break
                        case 'center':
                            alignNumber = this.data.left + 0.5 * this.data.width
                            break;
                        case 'right':
                            alignNumber = this.data.left + this.data.width
                            break;
                    }
                    if (alignNumber != this.data.alignNumber) {
                        this.$emit('changeComponentData', {
                            data: {
                                alignNumber: alignNumber
                            },
                            shouldUpdate: false
                        })
                    }
                }
            },
            computeSize() {
                if (this.data.propertyType != 4 && this.isShown) {
                    let w = Math.round(getOuterWidth(this.$el) / this.ppi * 2.54 * 10)
                    let h = Math.round(getOuterHeight(this.$el) / this.ppi * 2.54 * 10)
                    let l = 0
                    let alignNumber = this.data.alignNumber
                    switch (this.data.textAlign) {
                        case 'left':
                            l = alignNumber
                            break
                        case 'center':
                            l = alignNumber - 0.5 * w
                            break
                        case 'right':
                            l = alignNumber - w
                            break
                    }
                    l = Math.round(l)
                    if (w != this.data.width || h != this.data.height || l != this.data.left) {
                        // console.log(this.$el, this.data.propertyCode, this.value, this.data.width, this.data.height, this.data.left, w, h, l)
                        if (!(isNaN(w) || isNaN(h) || isNaN(l))) {
                            this.$emit('changeComponentData', {
                                data: {
                                    width: w,
                                    height: h,
                                    left: l
                                },
                                shouldUpdate: true
                            })
                        }
                    }
                }
            }
        },
        mounted() {
            if (this.data.propertyType == 4) {
                if (this.data.sample.length == 8) {
                    JsBarcode('#barcode', this.data.sample, {
                        displayValue: false
                    })
                } else {
                    this.$refs.barcode.src = '/static/img/barcode-sample.png'
                }
            } else {
                window.setTimeout(() => {
                    this.computeSize()
                }, this.isPreview ? 500 : 0)
            }
        }
}
</script>
<style lang="scss">
.property-component{
    &.string {
        padding: 5px;
        white-space: nowrap;
        span {
            display: table-cell;
            vertical-align: middle;
            line-height: 1;
        }
        &.active {
            border: 1px dashed #4ec0ff;
            padding: 4px;
            background-color: rgba(78, 192, 255, .15);
        }
        &.borderRender {
            border: 1px solid #000;
            padding: 4px;
        }
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
    &.barcode {
        img {
            display: block;
        }
        &.active {
            opacity: .7;
            border: 1px dashed #4ec0ff;
            .resize {
                display: block;
            }
        }
    }
}
</style>