import React from "react";
import {
    CCreateElement,
    CSidebar,
    CSidebarBrand, CSidebarMinimizer,
    CSidebarNav,
    CSidebarNavDivider,
    CSidebarNavDropdown, CSidebarNavItem, CSidebarNavTitle
} from "@coreui/react";
import CIcon from "@coreui/icons-react";
import navigation from "../../navigation";
import { useDispatch, useSelector } from "react-redux";
import { set } from "../../actions/ui";

const TheSidebar = () => {
    const dispatch = useDispatch()
    const show = useSelector(state => state.ui.sidebarShow)

    return (
        <CSidebar
            show={show}
            onShowChange={(val) => dispatch(set({sidebarShow: val }))}
        >
            <CSidebarBrand className="d-md-down-none" to="/">
                <CIcon
                    className="c-sidebar-brand-full"
                    name="logo-negative"
                    height={35}
                />
            </CSidebarBrand>
            <CSidebarNav>

                <CCreateElement
                    items={navigation}
                    components={{
                        CSidebarNavDivider,
                        CSidebarNavDropdown,
                        CSidebarNavItem,
                        CSidebarNavTitle
                    }}
                />
            </CSidebarNav>
            <CSidebarMinimizer className="c-d-md-down-none"/>
        </CSidebar>
    )
}

export default TheSidebar;
