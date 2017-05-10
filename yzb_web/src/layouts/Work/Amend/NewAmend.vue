<template>
    <div class="new-storage">
        <div class="main-box">
            <header class="header-box">
                <div class="header-seek mt20">
                    <ul class="header-operation-btn">
                        <li>
                            <span>商品库位：</span>
                            <span class="main-color">{{newDatas.switchName}}</span>
                        </li>
                        <li>
                            <span>备注：</span>
                            <div class="input-w300">     
                                <el-input v-model="orderRemark"></el-input>
                            </div>
                        </li>
                    </ul>
                </div>
            </header>
            <section class="body-box">
                <amend-from :orderRemark='orderRemark'></amend-from>
            </section>
            <!-- 弹窗 -->
            <make-success :routerUrl='"/work/amend/detailAmend"'></make-success>
        </div>
    </div>
</template>
<script>
import makeSuccess from './../CommonalityComponent/popupTemplate/makeSuccess'
import amendFrom from "./../CommonalityComponent/formTemplate/amendFrom"
export default {
    components: {
        amendFrom,
        makeSuccess
    },
    data () {
        return {
            orderRemark: null, // 备注
            newDatas: JSON.parse(sessionStorage.getItem("newDatas")) // 上一步带过来的数据
        }
    }
}
</script>
<style lang="scss" scoped>
$oa: #0abfab; // 主色
$d6: #d6d6d6; // 分割线颜色
$c3: #333333; // 导航栏文字颜色
$c2: #333742; // 标签栏底色
$es: #e5f5fd; // 标签设置编辑文本 出现旋转框
$f7: #f74c31; // 消息红点颜色
$a8: #a8a8a8; // 搜索页面icon 为选中的底色
$fb: #fb6155; // 删除按钮未选中的底色
$c9: #c9e5e2; // 按钮没选中状态
$f0: #f0f0f0; // 细线颜色
$c9: #999; // 列表内容文字色值
$c6: #666; // 写在标签栏块上的文字图标选中加深颜色
$e6: #e6f8f6; // 选中列表时的状态色
$f6: #f6f6f6; // 没选中列表时的状态色（偶）
$fd: #fd9526; // 按钮图标颜色
$bf: #49bbf3; // 标签设置编辑文本 出现旋转框描边 
$e8: #e8e9eb; // 表格头部的颜色
.new-storage{
    width: 100%;
    height: 100%;
    padding: 0 20px;
    .header-seek{
        margin-top: 20px;
        position: relative;
        overflow-y: hidden;
        overflow-x: hidden;
        ul{
          float: left;
          white-space: nowrap;
          overflow: hidden;
          li:first-child{
            border: none;
          }
        }
        .table-btn{
            position: absolute;
            right: 0;
            overflow: hidden;
            li{
                position: relative;
            }
            input{
                position: absolute;
                top: -10px;
                left: 0;
                width: 100%;
                height: 132%;
                font-size: 0;
                border: 0;
                background-color: transparent;
                // display: none;
            }
        }
    }
}
.new-storage{
    .table-template{
        min-width: 100%;
        height: 100%;
        table{
            width: 100%;
            height: 100%;
            background-color: $e8;
            .tr-header{
                background-color: $e8!important;
                &:hover{
                    background-color: $e8;
                }
                .header-state{
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    li:first-child{
                        background-color: $e8;
                    }
                    li{
                        height: 40px;
                        line-height: 40px;
                        border-bottom: 1px solid $d6;
                        background-color: #fff;
                    }
                }
            }
            tr{
                height: 40px;
                width: 100%;
            }
            tr:nth-child(odd){
                background-color: #fff;
            }
            tr:nth-child(even){
                background-color: $f6;
            }
            tr{
                // &:hover{
                //     background-color: $e6;
                // }
            }
            th,td{
                padding: 10px;
                text-align: center;
                border-right: 1px solid $d6;
                border-bottom: 1px solid $d6;
                font-size: 12px;
                min-width: 70px;
                height: 40px;
                overflow: hidden;
                white-space:nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
            }
            td:last-child{
                border-right: 0;
            }
            th{
                color: $c6;
                position: relative;
                span{
                    vertical-align: middle;
                    // margin-right: 10px;
                }
                .open-icon{
                    vertical-align: middle;
                    position: absolute;
                    right: 5px;
                    top: 0;
                    bottom: 0;
                    margin-top: 14px;
                    cursor: pointer;
                }
            }
            td{
                color: $c3;
            }
            .table-pull{
                padding: 0!important;
            }
        }
    }
}
</style>