import 'whatwg-fetch'
const _computeStyle = (dom) => {
    let width = window.parseInt(getComputedStyle(dom).width)
    let height = window.parseInt(getComputedStyle(dom).height)
    let paddingLeft = window.parseInt(getComputedStyle(dom).paddingLeft)
    let paddingRight = window.parseInt(getComputedStyle(dom).paddingRight)
    let paddingTop = window.parseInt(getComputedStyle(dom).paddingTop)
    let paddingBottom = window.parseInt(getComputedStyle(dom).paddingBottom)
    let borderTopWidth = window.parseInt(getComputedStyle(dom).borderTopWidth)
    let borderLeftWidth = window.parseInt(getComputedStyle(dom).borderLeftWidth)
    let borderRightWidth = window.parseInt(getComputedStyle(dom).borderRightWidth)
    let borderBottomWidth = window.parseInt(getComputedStyle(dom).borderBottomWidth)
    let boxSizing = getComputedStyle(dom).boxSizing
    return {
        width,
        height,
        paddingTop,
        paddingLeft,
        paddingRight,
        paddingBottom,
        borderTopWidth,
        borderLeftWidth,
        borderRightWidth,
        borderBottomWidth,
        boxSizing
    }
}
export const getWidth = (dom) => {
    let {
        width,
        paddingLeft,
        paddingRight,
        borderLeftWidth,
        borderRightWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return width - borderLeftWidth - borderRightWidth - paddingLeft - paddingRight
        default:
            return width
    }
}

export const getInnerWidth = (dom) => {
    let {
        width,
        paddingLeft,
        paddingRight,
        borderLeftWidth,
        borderRightWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return width - borderLeftWidth - borderRightWidth
        default:
            return width + paddingLeft + paddingRight
    }
}

export const getOuterWidth = (dom) => {
    let {
        width,
        paddingLeft,
        paddingRight,
        borderLeftWidth,
        borderRightWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return width
        default:
            return width + paddingLeft + paddingRight + borderLeftWidth + borderRightWidth
    }
}

export const getHeight = (dom) => {
    let {
        height,
        paddingTop,
        paddingBottom,
        borderTopWidth,
        borderBottomWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return height - borderTopWidth - borderBottomWidth - paddingTop - paddingBottom
        default:
            return height
    }
}

export const getInnerHeight = (dom) => {
    let {
        height,
        paddingTop,
        paddingBottom,
        borderTopWidth,
        borderBottomWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return height - borderTopWidth - borderBottomWidth
        default:
            return height + paddingTop + paddingBottom
    }
}

export const getOuterHeight = (dom) => {
    let {
        height,
        paddingTop,
        paddingBottom,
        borderTopWidth,
        borderBottomWidth,
        boxSizing
    } = _computeStyle(dom)
    switch (boxSizing) {
        case 'border-box':
            return height
        default:
            return height + paddingTop + paddingBottom + borderTopWidth + borderBottomWidth
    }
}
export const getComponentBound = (component, container) => {
    let left = component.left
    let top = component.top
    let right = 0
    let bottom = 0
    let width = component.width
    let height = component.height
    let rotateDeg = (container ? container.rotateDeg : 0) + component.rotateDeg
    if (rotateDeg == 90 || rotateDeg == 270) {
        width = component.height
        height = component.width
    }
    right = left + width
    bottom = top + height
    return {
        left,
        top,
        right,
        bottom,
        width,
        height,
    }
}
export const getComponentTranslate = (component) => {
    let translate = ''
    switch (component.rotateDeg) {
        case 90:
            translate = 'translateY(-' + component.height + 'mm)'
            break;
        case 180:
            translate = 'translate(-' + component.width + 'mm, -' + component.height + 'mm)'
            break;
        case 270:
            translate = 'translateX(-' + component.width + 'mm)'
            break;
    }
    return translate
}
export const isInteractWithComponent = (componentA, componentB) => {
    let a = getComponentBound(componentA)
    let b = getComponentBound(componentB)
    return (a.left < b.right) && (a.right > b.left) && (a.top < b.bottom) && (a.bottom > b.top)
}
let ppi = 0
export const getPPI = () => {
    if (!ppi) {
        let dom = document.createElement('div')
        dom.style.width = '1in'
        dom.style.height = '1in'
        dom.style.display = 'none'
        document.body.appendChild(dom)
        ppi = getWidth(dom)
        return ppi
    }
    return ppi
}

export const transformFileURL = (url) => {
    if (/^\/([^/]+?)\/([^/]+?)\/(.+)$/.test(url)) {
        let appid = url.match(/^\/([^/]+?)\/([^/]+?)\/(.+)$/)[1]
        let bucket = url.match(/^\/([^/]+?)\/([^/]+?)\/(.+)$/)[2]
        let filename = url.match(/^\/([^/]+?)\/([^/]+?)\/(.+)$/)[3]
        if (/IE/.test(window.navigator.userAgent)) {
            return '/image' + url
        } else {
            return 'http://' + bucket + '-' + appid + '.cosgz.myqcloud.com' + '/' + filename
        }
    } else {
        return url
    }
}
export const readImageAsDataURL = (file, callback) => {
    if (!window.FileReader && !(window.URL && window.URL.createObjectURL)) {
        callback("抱歉，你的浏览器不支持FileReader和URL.createObjectURL");
    } else {
        if (window.FileReader) {
            let reader = new window.FileReader()
            reader.readAsDataURL(file)
            reader.onload = (e) => {
                let src = reader.result
                let img = new Image()
                img.onload = () => {
                    callback(null, src, {
                        width: img.width,
                        height: img.height
                    })
                }
                img.src = src
            }
        } else {
            let url = URL.createObjectURL(file)
            let image = new Image()
            image.onload = () => {
                let canvas = document.createElement('canvas')
                canvas.width = image.width
                canvas.height = image.height
                let ctx = canvas.getContext('2d')
                ctx.drawImage(image, 0, 0, image.width, image.height)
                let ext = image.src.substring(image.src.lastIndexOf(".") + 1).toLowerCase()
                let dataURL = canvas.toDataURL('image/' + ext)
                callback(null, dataURL, {
                    width: image.width,
                    height: image.height
                })
            }
            image.src = url
        }
    }
}
export const createImageByTemplateCanvas = (canvas, callback) => {
    let ppi = 96
    let sum = 0
    let loaded = 0
    let canvasDom = document.createElement('canvas')
    let canvasW = canvas.width / 25.4 * ppi
    let canvasH = canvas.height / 25.4 * ppi
    canvasDom.width = canvasW
    canvasDom.height = canvasH
    let ctx = canvasDom.getContext('2d')
    ctx.fillStyle = '#fff'
    ctx.fillRect(0, 0, canvasW, canvasH)

    let cbf = () => {
        if (loaded == sum) {
            let dataURL = canvasDom.toDataURL('image/png')
            callback(null, dataURL)
        }
    }
    let draw = () => {
        let items = []
        canvas.components.forEach(component => {
            if (component.type == 'ContainerComponent') {
                if (component.data.border) {
                    let left = component.data.left / 25.4 * ppi
                    let top = component.data.top / 25.4 * ppi
                    let width = component.data.width / 25.4 * ppi
                    let height = component.data.height / 25.4 * ppi
                    ctx.save()
                    ctx.strokeStyle = '#000'
                    ctx.lineWidth = 1
                    ctx.strokeRect(left, top, width, height)
                    ctx.restore()
                }
                items = items.concat(component.data.children)
            } else {
                items.push(component)
            }
        })
        items.forEach(component => {
            let left = component.data.left / 25.4 * ppi
            let top = component.data.top / 25.4 * ppi
            let width = component.data.width / 25.4 * ppi
            let height = component.data.height / 25.4 * ppi
            let rotateDeg = (component.data.rotateDeg + 360) % 360
            let offsetLeft = 0
            let offsetTop = 0
            switch (rotateDeg) {
                case 90:
                    offsetLeft = left + height
                    offsetTop = top
                    break;
                case 180:
                    offsetLeft = left + width
                    offsetTop = top + height
                    break;
                case 270:
                    offsetLeft = left
                    offsetTop = top + width
                    break;
                default:
                    offsetLeft = left
                    offsetTop = top
            }
            ctx.save()
            ctx.translate(offsetLeft, offsetTop)
            ctx.rotate(Math.PI * rotateDeg / 180)
            let fontStyle, fontWeight, fontSize, fontFamily, image
            switch (component.type) {
                case 'TextComponent':
                    fontStyle = component.data.isItalic ? 'italic' : 'normal'
                    fontWeight = component.data.isBold ? 'bold' : 'normal'
                    fontSize = component.data.fontSize + 'px'
                    fontFamily = component.data.fontFamily
                    ctx.textAlign = 'left'
                    ctx.font = [fontStyle, fontWeight, fontSize, fontFamily].join(' ')
                    ctx.fillStyle = component.data.color
                    ctx.fillText(component.data.content, 5, height - 5)
                    if (component.data.border) {
                        ctx.strokeStyle = '#000'
                        ctx.lineWidth = 1
                        ctx.strokeRect(0, 0, width, height)
                    }
                    break
                case 'ImageComponent':
                    image = new Image()
                    image.crossOrigin = 'anonymous'
                    sum++
                    image.onload = () => {
                        ctx.save()
                        ctx.translate(offsetLeft, offsetTop)
                        ctx.rotate(Math.PI * rotateDeg / 180)
                        ctx.drawImage(image, 0, 0, width, height)
                        ctx.translate(-offsetLeft, -offsetTop)
                        ctx.restore()
                        loaded++
                        cbf()
                    }
                    image.src = transformFileURL(component.data.src) || '/static/img/image-sample.png'
                    break
                case 'LineComponent':
                    ctx.fillStyle = component.data.color
                    ctx.fillRect(0, 0, width, height)
                    break
                case 'PropertyComponent':
                    if (component.data.propertyType == 4) {
                        image = new Image()
                        image.src = '/static/img/barcode-sample.png'
                        ctx.drawImage(image, 0, 0, width, height)
                    } else {
                        fontStyle = component.data.valueStyle.isItalic ? 'italic' : 'normal'
                        fontWeight = component.data.valueStyle.isBold ? 'bold' : 'normal'
                        fontSize = component.data.valueStyle.fontSize + 'px'
                        fontFamily = component.data.valueStyle.fontFamily
                        ctx.textAlign = 'left'
                        ctx.font = [fontStyle, fontWeight, fontSize, fontFamily].join(' ')
                        ctx.fillStyle = component.data.valueStyle.color
                        ctx.fillText(component.data.prefix + (component.data.sample || '#{' + component.data.propertyCode + '}') + component.data.suffix, 5, height - 5)
                        if (component.data.border) {
                            ctx.strokeStyle = '#000'
                            ctx.lineWidth = 1
                            ctx.strokeRect(0, 0, width, height)
                        }
                    }
                    break
            }
            ctx.translate(-offsetLeft, -offsetTop)
            ctx.restore()
        })
        cbf()
    }
    if (canvas.backgroundImage) {
        let backgroundImage = new Image()
        backgroundImage.crossOrigin = 'anonymous'
        backgroundImage.onload = () => {
            ctx.drawImage(backgroundImage, 0, 0, canvasW, canvasH)
            draw()
        }
        backgroundImage.src = transformFileURL(canvas.backgroundImage)
    } else {
        draw()
    }
}