 import { CButton, CCardBody, CCollapse, CDataTable } from '@coreui/react'
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { startTodoDelete, startTodoLoading, todoSetActive } from '../../actions/todo';
import { set } from '../../actions/ui';
import { ArrowDownCircle, PencilSquare, XCircle  } from 'react-bootstrap-icons';
import { ListPagination } from '../CoreUi/ListPagination';  
import Swal from 'sweetalert2';


export const List = () => {
    const dispatch = useDispatch();
    const { todos } = useSelector(state => state.todo);
    const { todo: todoUi } = useSelector(state => state.ui);
    const [details, setDetails] = useState([])
    const [items, setItems] = useState(todos)
    const { activePage: pageActive, itemPerPage: itemPage, totalPage:pageTotal } = todoUi; 
    const [activePage, setActivePage] = useState(pageActive);
    const [itemPerPage, setItemPerPage] = useState(itemPage);
    const [totalPage, setTotalPage] = useState(pageTotal);

    useEffect(() => {           
        dispatch(startTodoLoading(activePage, itemPerPage));    
    }, []);  // eslint-disable-line react-hooks/exhaustive-deps
    
    useEffect(() => {  
        setActivePage(pageActive);
        setItemPerPage(itemPage);
        setTotalPage(pageTotal);        
        setItems(todos);                      
    }, [todos]); // eslint-disable-line react-hooks/exhaustive-deps
    
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

    const handleEdit = (item) => {
        todoUi.formShow = true;
        dispatch(set({todo: todoUi})) ;       
        dispatch(todoSetActive(item));
    }

    const handleDelete = (item) => {  
        
        Swal.fire({
            title: 'Do you want to delete this item?',            
            showCancelButton: true,
            confirmButtonText: `Delete`,
            denyButtonText: `Don't delete`,
          }).then((result) => {            
            if (result.isConfirmed) {             
                dispatch(todoSetActive(item));
                dispatch(startTodoDelete(item));
            }
          })        
    }

    const handlePageChange = (pageNumber) => {                             
        dispatch(startTodoLoading(pageNumber, itemPerPage));             
    };
     
    const handleItemsPerPage = (iPerPage) => {            
        dispatch(startTodoLoading(activePage, iPerPage));                    
    };
   
    const fields = [
        { key: 'id', _style: { width: '10%' } },
        { key: 'name', _style: { width: '15%' } },     
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
                                            style={{cursor:'pointer'}}
                                        />

                                        <PencilSquare 
                                            className="mr-2"
                                            color="royalblue" 
                                            size={22}
                                            onClick={() => { handleEdit(item, index) }}
                                            style={{cursor:'pointer'}}
                                        />  

                                        <XCircle 
                                            color="red" 
                                            size={22}
                                            onClick={() => { handleDelete(item, index) }}
                                            style={{cursor:'pointer'}}
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
                totalPage = {totalPage}
                activePage = {activePage}
                handleData = {handlePageChange}                
            />
         </div>
     ) 
}

