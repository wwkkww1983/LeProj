import TemplateIndex from '../../layouts/Work/Template/Index'
import TemplateList from '../../layouts/Work/Template/List'
import TemplateEdit from '../../layouts/Work/Template/Edit'

export default {
    path: 'template',
    name: 'TemplateIndex',
    component: TemplateIndex,
    children: [{
        path: 'list',
        name: 'TemplateList',
        component: TemplateList
    }, {
        path: 'edit',
        name: 'TemplateEdit',
        component: TemplateEdit
    }]
}