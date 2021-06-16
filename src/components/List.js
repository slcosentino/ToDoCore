import CIcon from '@coreui/icons-react';
import { CButton, CCardBody, CCollapse, CDataTable } from '@coreui/react'
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { startTodoDelete, todoSetActive } from '../actions/todo';
import { set } from '../actions/ui';
import { ArrowDownCircle, PencilSquare, XCircle  } from 'react-bootstrap-icons';
 
import {
    cilPencil,
    cilUser
    
} from '@coreui/icons'
import Swal from 'sweetalert2';


export const List = () => {
    const { todos } = useSelector(state => state.todo);
    const dispatch = useDispatch();
    const [details, setDetails] = useState([])
    const [items, setItems] = useState(todos)

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
        dispatch(set({ formTodoShow: true }))
        dispatch(todoSetActive(item));
    }

    const handleDelete = (item, index) => {  
        
        Swal.fire({
            title: 'Do you want to delete this item?',            
            showCancelButton: true,
            confirmButtonText: `Delete`,
            denyButtonText: `Don't delete`,
          }).then((result) => {            
            if (result.isConfirmed) {
                //Swal.fire('Saved!', '', 'success')
                dispatch(todoSetActive(item));
                dispatch(startTodoDelete(item));
            }
          })

        
    }


    const fields = [
        { key: 'id', _style: { width: '10%' } },
        { key: 'name', _style: { width: '15%' }, filter: (e, a, c) => { console.log(e, a, c) } },
        { key: 'folder.name', _style: { width: '15%' } },
       // { key: 'mobileNumber', _style: { width: '15%' } },
       // { key: 'title', _style: { width: '25%' } },
      //  { key: 'developer', _style: { width: '10%' } },
        {
            key: 'show_details',
            label: '',
            _style: { width: '10%' },
            sorter: false,
            filter: false
        }
    ]

    useEffect(() => {
      setItems(todos);     
    }, [todos])

    
     return (
         <div className="my-3 p-3 bg-white box-shadow">
                      
             <CDataTable
                 items={items}
                 fields={fields}                                                             
                 itemsPerPageSelect
                 itemsPerPage={10}
                 hover
                 addTableClasses="table2"
                 sorter
                 //pagination                 
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
         </div>
     ) 
}

