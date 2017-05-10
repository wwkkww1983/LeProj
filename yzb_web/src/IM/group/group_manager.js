// 获取我的群组
export const getMyGroup = function (cbOk) {
  let options = {
    'Member_Account': loginInfo.identifier,
    'Limit': totalCount,
    'Offset': 0,
    // 'GroupType':'',
    'GroupBaseInfoFilter': [
      'Type',
      'Name',
      'Introduction',
      'Notification',
      'FaceUrl',
      'CreateTime',
      'Owner_Account',
      'LastInfoTime',
      'LastMsgTime',
      'NextMsgSeq',
      'MemberNum',
      'MaxMemberNum',
      'ApplyJoinOption'
    ],
    'SelfInfoFilter': [
      'Role',
      'JoinTime',
      'MsgFlag',
      'UnreadMsgNum'
    ]
  };
  webim.getJoinedGroupListHigh(
    options,
    function (resp) {
      if (!resp.GroupIdList || resp.GroupIdList.length === 0) {
        alert('你目前还没有加入任何群组');
        return;
      }
      let data = [];
      for (let i = 0; i < resp.GroupIdList.length; i++) {
        let group_id = resp.GroupIdList[i].GroupId;
        let name = webim.Tool.formatText2Html(resp.GroupIdList[i].Name);
        let type_en = resp.GroupIdList[i].Type;
        let type = webim.Tool.groupTypeEn2Ch(resp.GroupIdList[i].Type);
        let role_en = resp.GroupIdList[i].SelfInfo.Role;
        let role = webim.Tool.groupRoleEn2Ch(resp.GroupIdList[i].SelfInfo.Role);
        let msg_flag = webim.Tool.groupMsgFlagEn2Ch(resp.GroupIdList[i].SelfInfo.MsgFlag);
        let msg_flag_en = resp.GroupIdList[i].SelfInfo.MsgFlag;
        let join_time = webim.Tool.formatTimeStamp(resp.GroupIdList[i].SelfInfo.JoinTime);
        let member_num = resp.GroupIdList[i].MemberNum;
        let notification = webim.Tool.formatText2Html(resp.GroupIdList[i].Notification);
        let introduction = webim.Tool.formatText2Html(resp.GroupIdList[i].Introduction);
        data.push({
          'GroupId': group_id,
          'Name': name,
          'TypeEn': type_en,
          'Type': type,
          'RoleEn': role_en,
          'Role': role,
          'MsgFlagEn': msg_flag_en,
          'MsgFlag': msg_flag,
          'MemberNum': member_num,
          'Notification': notification,
          'Introduction': introduction,
          'JoinTime': join_time
        });
      }
      cbOk(data);
    },
    function (err) {
      alert(err.ErrorInfo);
    }
  );
};
// 解散群组
export const dissolveGroup = function (group_id, cbOk) {
  let options = null
  if (group_id) {
    options = {
      'GroupId': group_id
    }
  }
  if (options === null) {
    alert('解散群失败，请选择群组');
    return;
  }
  webim.destroyGroup(
    options,
    function (res) {
      if (cbOk instanceof Function) cbOk(res);
    },
    function (err) {
      alert(err.ErrorInfo)
    }
  )
}
