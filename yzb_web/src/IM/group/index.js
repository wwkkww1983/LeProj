// 获取群列表
export const getMyGroup = function (cbOk) {

    var options = {
        'Member_Account': loginInfo.identifier,
        'Limit': totalCount,
        'Offset': 0,
        'GroupType': '',
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
                var data = [];
                for (var i = 0; i < resp.GroupIdList.length; i++) {
                    var group_id = resp.GroupIdList[i].GroupId;
                    if (i === 1) {
                       getGroupMemberInfo(group_id)
                    }
                    var name = webim.Tool.formatText2Html(resp.GroupIdList[i].Name);
                    var type_en = resp.GroupIdList[i].Type;
                    var type = webim.Tool.groupTypeEn2Ch(resp.GroupIdList[i].Type);
                    var role_en = resp.GroupIdList[i].SelfInfo.Role;
                    var role = webim.Tool.groupRoleEn2Ch(resp.GroupIdList[i].SelfInfo.Role);
                    var msg_flag = webim.Tool.groupMsgFlagEn2Ch(
                    resp.GroupIdList[i].SelfInfo.MsgFlag);
                    var msg_flag_en = resp.GroupIdList[i].SelfInfo.MsgFlag;
                    var join_time = webim.Tool.formatTimeStamp(
                    resp.GroupIdList[i].SelfInfo.JoinTime);
                    var member_num = resp.GroupIdList[i].MemberNum;
                    var notification = webim.Tool.formatText2Html(
                    resp.GroupIdList[i].Notification);
                    var introduction = webim.Tool.formatText2Html(
                    resp.GroupIdList[i].Introduction);
                    var faceUrl = resp.GroupIdList[i].FaceUrl

                    data.push({
                        'GroupId': group_id,
                        'Name': name,
                        'TypeEn': type_en, // Private/Public
                        'Type': type,
                        'RoleEn': role_en,
                        'Role': role,
                        'MsgFlagEn': msg_flag_en,
                        'MsgFlag': msg_flag,
                        'MemberNum': member_num,
                        'Notification': notification,
                        'Introduction': introduction,
                        'JoinTime': join_time,
                        'FaceUrl': faceUrl,
                        'group': '发起会话'
                    })
                }
                cbOk(data)

            },
            function (err) {
                alert(err.ErrorInfo);
            }
    )
}

// 读取群组基本资料-高级接口
export const getGroupInfo = function (group_id, cbOK) {
    var options = {
        'GroupIdList': [
            group_id
        ],
        'GroupBaseInfoFilter': [
            'Type', // Private/Public
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
        'MemberInfoFilter': [
            'Account',
            'Role',
            'JoinTime',
            'LastSendMsgTime',
            'ShutUpUntil'
        ]
    };
    webim.getGroupInfo(
            options,
            function (resp) {
                if (cbOK) {
                    cbOK(resp);
                }
            },
            function (err) {
                alert(err.ErrorInfo);
            }
    );
}

// 读取群组成员
export const getGroupMemberInfo = function (group_id, cbOk) {
    var options = {
        'GroupId': group_id,
        'Offset': 0, // 必须从0开始
        'Limit': totalCount,
        'MemberInfoFilter': [
            'Account',
            'Role',
            'JoinTime',
            'LastSendMsgTime',
            'ShutUpUntil',
            'avatarUrl',
            'nickname'
        ]
    };
    webim.getGroupMemberInfo(
            options,
            function (resp) {
                if (resp.MemberNum <= 0) {
                    alert('该群组目前没有成员');
                    return;
                }
                var data = [];
                for (var i in resp.MemberList) {
                    var account = resp.MemberList[i].Member_Account;
                    var role = webim.Tool.groupRoleEn2Ch(resp.MemberList[i].Role);
                    var join_time = webim.Tool.formatTimeStamp(
                    resp.MemberList[i].JoinTime);
                    var shut_up_until = webim.Tool.formatTimeStamp(
                    resp.MemberList[i].ShutUpUntil);
                    if (shut_up_until === 0) {
                        shut_up_until = '-';
                    }
                    data.push({
                        GroupId: group_id,
                        Member_Account: account,
                        Role: role,
                        JoinTime: join_time,
                        ShutUpUntil: shut_up_until
                    });
                }
                if (cbOk instanceof Function) cbOk(data)
            },
            function (err) {
                alert(err.ErrorInfo);
            }
    );
};

// 删除群组成员
export const deleteGroupMember = function (options, check) {
    // var options = {
    //     'GroupId': $('#dgm_group_id').val(),
    //     // 'Silence': $('input[name="dgm_silence_radio"]:checked').val(),//只有ROOT用户采用权限设置该字段（是否静默移除）
    //     'MemberToDel_Account': [$('#dgm_account').val()]
    // };
    webim.deleteGroupMember(
            options,
            check,
            function (err) {
                console.log(err.ErrorInfo)
                alert('你不能进行删除操作')
            }
    );
};
// 邀请好友加群
export const addGroupMember = function (options, check) {
    // var options = {
    //     'GroupId': $('#agm_group_id').val(),
    //     'MemberList': [
    //         {
    //             'Member_Account': $('#agm_account').val()
    //         }

    //     ]
    // };
    webim.addGroupMember(
            options,
            check,
            function (err) {
                alert(err.ErrorInfo);
            }
    );
};
