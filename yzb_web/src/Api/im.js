import apiCall from './apiCall'

// 获取群成员列表
export const getGroupMembers = function (opt) {
    return apiCall({
        url: "/v1/changeGroup/getGroupMemberInfo",
        init: {
            tokenId: sessionStorage.getItem('tokenId')
        }
    }, opt)
}
