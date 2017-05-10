<template>
    <div class="work">
        <div class="work-nav"> 
            <ul>
                <router-link tag="li" to="/work/sell">销售</router-link>
                <router-link tag="li" to="/work/no">审批</router-link>
                <router-link tag="li" to="/work/no">公告</router-link>
                <router-link tag="li" to="/work/salesReturn">退货</router-link>
                <router-link tag="li" to="/work/storage">入库</router-link>
                <router-link tag="li" to="/work/storageReturn">退库</router-link>
                <router-link tag="li" to="/work/no">大数据</router-link>
                <router-link tag="li" to="/work/amend">修改</router-link>
                <router-link tag="li" to="/work/sipping">发货</router-link>
            </ul>
        </div>
        <div class="work-view">
            <router-view></router-view>
        </div>
    </div>
</template>
<style lang="scss">
.work{
    width: 100%;
    height: 100%;
    // overflow: hidden;
    background-color: #e0e1e4;
    overflow-x: auto;
    overflow-y: hidden;
    padding-bottom: 70px;
    .work-nav{ // 导航
        background-color: #fff;
        height: 70px;
        line-height: 70px;
        font-size: 0;
        width: 100%;
        ul{
            width: 1570px;
            height: 70px;
            margin: 0 auto;
            background-color: #fff;
            text-align: center;
            li{
                position: relative;
                height: 70px;
                line-height: 70px;
                width: 80px;
                text-align: center;
                margin-right: 70px;
                display: inline-block;
                font-size: 18px;
                overflow: hidden;
                color: #666;
                font-weight: 600;
                cursor: pointer;
                &:hover{
                    border-bottom: 2px solid #46cebc;
                    color: #46c4b4;
                }
            }
            li:first-child{
                margin-left: 0;
            }
            .router-link-active{
                color: #46c4b4;
                border-bottom: 2px solid #46cebc;
            }
        }
    }
    .work-view{
        // min-width: 1570px;
        position: relative;
        height: 100%;
        margin: 0 auto;
        background-color: #e0e1e4;
        overflow: auto;
    }
}
.work-navigation{ // 导航
    font-size: 14px;
    background-color: transparent;
    position: absolute;
    top: 20px;
    z-index: 1000;
    left: 0;
    margin-left: 20px;
    height: 14px;
    line-height: 14px;
    width: 500px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    ul{
        font-size: 0;
        li{
            color: #0abfab;
            font-size: 14px;
            height: 14px;
            line-height: 14px;
            display: inline-block;
            cursor:pointer;
        }
        li:last-child{
            color: red;
            i{
                display: none;
            }
        }
    }
}
</style>