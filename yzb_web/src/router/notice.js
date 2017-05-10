import noRead from "./../Notice/noRead"
import hasRead from "./../Notice/hasRead"
import noticeDetial from "./../Notice/noticeDetial"
const message = {
    path: '/',
    component: Message,
    children: [
        {path: "", component: Message},
        {path: "/noRead", component: noRead},
        {path: "/hasRead", component: hasRead},
    ]
}
work.children.push(noRead);
work.children.push(hasRead);
work.children.push(noticeDetial);
export default work;
