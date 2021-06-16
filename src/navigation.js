import CIcon from "@coreui/icons-react";
import React from "react";

const nav = [
        
    {
        _tag: 'CSidebarNavTitle',
        _children: ['Menu'],
    },
    {
        _tag: 'CSidebarNavItem',
        name: 'Folders',
        to: '/folder',
        icon: <CIcon name="cil-speedometer" customClasses="c-sidebar-nav-icon"/>,        
    },
    {
        _tag: 'CSidebarNavItem',
        name: 'ToDos',
        to: '/todo',
        icon: <CIcon name="cil-speedometer" customClasses="c-sidebar-nav-icon"/>,        
    }
    
];

export default nav;