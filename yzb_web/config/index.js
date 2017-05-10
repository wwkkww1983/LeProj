// see http://vuejs-templates.github.io/webpack for documentation.
var path = require('path')

module.exports = {
    build: {
        env: require('./prod.env'),
        index: path.resolve(__dirname, '../dist/index.html'),
        assetsRoot: path.resolve(__dirname, '../dist'),
        assetsSubDirectory: 'static',
        assetsPublicPath: '/',
        productionSourceMap: true,
        // Gzip off by default as many popular static hosts such as
        // Surge or Netlify already gzip all static assets for you.
        // Before setting to `true`, make sure to:
        // npm install --save-dev compression-webpack-plugin
        productionGzip: false,
        productionGzipExtensions: ['js', 'css']
    },
    dev: {
        env: require('./dev.env'),
        port: 80,
        assetsSubDirectory: 'static',
        assetsPublicPath: '/',
        proxyTable: {
            '/yunzhubao': {
                target: 'https://www.jzmsoft.com:8082',
                secure: false,
                changeOrigin: true,
                pathRewrite: {
                    '^/yunzhubao': '/yunzhubao'
                }
            },
            '/file': {
                target: 'https://gz.file.myqcloud.com',
                secure: false,
                changeOrigin: true,
                pathRewrite: {
                    '^/file': '/file'
                }
            },
            '/image': {
                target: 'http://www.example.org',
                changeOrigin: true,
                router: function(req) {
                    var path = req.path
                    if (/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/.test(path)) {
                        var appid = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[1]
                        var bucket = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[2]
                        var filename = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[3]
                        return 'http://' + bucket + '-' + appid + '.cosgz.myqcloud.com'
                    }
                },
                pathRewrite: function(path, request) {
                    if (/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/.test(path)) {
                        var appid = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[1]
                        var bucket = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[2]
                        var filename = path.match(/^\/image\/([^\/]+?)\/([^\/]+?)\/(.+)$/)[3]
                        return '/' + filename
                    }
                }
            }
        },
        // CSS Sourcemaps off by default because relative paths are "buggy"
        // with this option, according to the CSS-Loader README
        // (https://github.com/webpack/css-loader#sourcemaps)
        // In our experience, they generally work as expected,
        // just be aware of this issue when enabling this option.
        cssSourceMap: false
    }
}