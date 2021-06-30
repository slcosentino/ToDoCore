import { CButton, CCardBody, CCollapse, CDataTable } from '@coreui/react'
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { folderSetActive  } from '../../actions/folder';
import { set } from '../../actions/ui';
import { ArrowDownCircle, PencilSquare, XCircle } from 'react-bootstrap-icons';
import { ListPagination } from '../CoreUi/ListPagination';
import Swal from 'sweetalert2';
import { useFolder } from '../../hooks/useFolder';

export const List = () => {
    const dispatch = useDispatch();
    const { folders } = useSelector(state => state.folder);
    const { folder: folderUi } = useSelector(state => state.ui);
    const [details, setDetails] = useState([]);
    const [items, setItems] = useState(folders)
    const { activePage: pageActive, itemPerPage: itemPage, totalPage: pageTotal } = folderUi;
    const [activePage, setActivePage] = useState(pageActive);
    const [itemPerPage, setItemPerPage] = useState(itemPage);
    const [totalPage, setTotalPage] = useState(pageTotal);
    const {startFolderLoading, startFolderDelete} = useFolder();

    useEffect(() => {           
        dispatch(startFolderLoading(activePage, itemPerPage));    
    }, []);  // eslint-disable-line react-hooks/exhaustive-deps
    
    useEffect(() => {  
        setActivePage(pageActive);
        setItemPerPage(itemPage);
        setTotalPage(pageTotal);        
        setItems(folders);                      
    }, [folders]); // eslint-disable-line react-hooks/exhaustive-deps

    const toggleDetails = (index) => {
        const position = details.indexOf(index)
        let newDetails = details.slice()
        if (position !== -1) {
            newDetails.splice(position, 1)
        } else {
            newDetails = [...details, index]
        }
        setDetails(newDetails)
    }

    const handleEdit = (item, index) => {
        folderUi.formShow = true;
        dispatch(set({ folder: folderUi }));
        dispatch(folderSetActive(item));
    }

    const handleDelete = (item, index) => {

        Swal.fire({
            title: 'Do you want to delete this folder?',
            showCancelButton: true,
            confirmButtonText: `Delete`,
            denyButtonText: `Don't delete`,
        }).then((result) => {
            if (result.isConfirmed) {
                dispatch(folderSetActive(item));
                dispatch(startFolderDelete(item));
            }
        })
    }

    const handlePageChange = (pageNumber) => {
        dispatch(startFolderLoading(pageNumber, itemPerPage));
    };

    const handleItemsPerPage = (iPerPage) => {
        dispatch(startFolderLoading(activePage, iPerPage));
    };
   
    const fields = [
        { key: 'id', _style: { width: '10%' } },
        { key: 'name', _style: { width: '15%' }, filter: (e, a, c) => { console.log(e, a, c) } },
        {
            key: 'show_details',
            label: 'Actions',
            _style: { width: '10%' },
            sorter: false,
            filter: false
        }
    ];

    return (
        <div className="my-3 p-3 bg-white box-shadow">

            <CDataTable
                items={items}
                fields={fields}
                itemsPerPageSelect
                itemsPerPage={itemPerPage}
                onPaginationChange={handleItemsPerPage}
                hover
                addTableClasses="table2"
                sorter
                scopedSlots={{
                    'show_details':
                        (item, index) => {
                            return (
                                <td className="py-2">

                                    <ArrowDownCircle
                                        className="mr-2"
                                        color="royalblue"
                                        size={22}
                                        onClick={() => { toggleDetails(index) }}
                                        style={{ cursor: 'pointer' }}
                                    />

                                    <PencilSquare
                                        className="mr-2"
                                        color="royalblue"
                                        size={22}
                                        onClick={() => { handleEdit(item, index) }}
                                        style={{ cursor: 'pointer' }}
                                    />

                                    <XCircle
                                        color="red"
                                        size={22}
                                        onClick={() => { handleDelete(item, index) }}
                                        style={{ cursor: 'pointer' }}
                                    />
                                </td>
                            )
                        },
                    'details':
                        (item, index) => {
                            return (
                                <CCollapse show={details.includes(index)}>
                                    <CCardBody>
                                        <h4>
                                            {item.username}
                                        </h4>
                                        <p className="text-muted">User since: {item.registered}</p>
                                        <CButton size="sm" color="info">
                                            User Settings
                                        </CButton>
                                        <CButton size="sm" color="danger" className="ml-1">
                                            Delete
                                        </CButton>
                                    </CCardBody>
                                </CCollapse>
                            )
                        }
                }}
            />
            <ListPagination
                totalPage={totalPage}
                activePage={activePage}
                handleData={handlePageChange}
            />
        </div>
    )
}

