import 'whatwg-fetch'

export default class PrintAPI {
    static serverHost = process.env.NODE_ENV === 'development' ? '/yunzhubao' : 'https://www.jzmsoft.com:8082/yunzhubao'

    static version = 'v1'

    static namespace = 'print'

    static getRequestUnit() {
        return {
            companyId: sessionStorage && sessionStorage.getItem('companyId'),
            channel: 3,
            os: "",
            ip: "",
            userId: sessionStorage && sessionStorage.getItem('id'),
            tokenId: sessionStorage && sessionStorage.getItem('tokenId')
        }
    }

    static getTemplateList(filter) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getTemplateList', {
            method: 'POST',
            credentials: 'include',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                data: filter,
                unit: this.getRequestUnit()
            })
        }).then(res => res.json());
    }

    static getPrintShopData(type) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getPrintShopData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: {
                    type
                }
            })
        }).then(res => res.json());
    }

    static createTemplate(templateData) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/createTemplate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: templateData
            })
        }).then(res => res.json());
    }

    static updateTemplate(templateData) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/updateTemplate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: templateData
            })
        }).then(res => res.json());
    }

    static setDefaultTemplate(templateId) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/setDefaultTemplate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: {
                    templateId
                }
            })
        }).then(res => res.json());
    }

    static previewTemplate(templateId) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/previewTemplate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: {
                    templateId
                }
            })
        }).then(res => res.json());
    }

    static deleteTemplate(templateId) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/delTemplate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: {
                    templateId
                }
            })
        }).then(res => res.json());
    }

    static getPrintMenuData(type) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getPrintMenuData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: {
                    type
                }
            })
        }).then(res => res.json());
    }

    static getPrintQualityData(filter) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getPrintQualityData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: filter
            })
        }).then(res => res.json());
    }

    static getPrintLabelData(filter) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getPrintLabelData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: filter
            })
        }).then(res => res.json());
    }

    static getPrintLabelByOrder(filter) {
        return fetch(this.serverHost + '/' + this.version + '/' + this.namespace + '/getPrintLabelByOrder', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: filter
            })
        }).then(res => res.json());
    }

    static getAppSign(params, callback) {
        return fetch(this.serverHost + '/' + this.version + '/apply/getCOSSign', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                unit: this.getRequestUnit(),
                data: params
            })
        }).then(res => res.json()).then(res => {
            if (res.state != 200) {
                callback(res.msg)
            } else {
                callback(null, {
                    sign: res.data.signStr,
                    dir_name: res.data.url
                })
            }
        })
    }
}