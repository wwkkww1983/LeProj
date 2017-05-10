/* eslint-disable */
import apiCall from './apiCall'

// 群组成员
export const groupMember = function (opt) {
  return apiCall({
    type: 'post',
    url: "/v1/changeGroup/getGroupMemberInfo",
    init: {
      tokenId: sessionStorage.getItem('tokenId')
    }
  }, opt)
}
