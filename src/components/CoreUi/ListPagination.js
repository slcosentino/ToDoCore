import React, { useState } from 'react'
import {CPagination} from "@coreui/react";

export const ListPagination = ({totalPage, activePage = 1, handleData}) => {
    
    const [page, setPage] = useState(activePage); 
    
    const handlePageChange = (pageNumber) => { 

        if(pageNumber > 0)
        {
            setPage(pageNumber);       
            handleData(pageNumber);
       }             
    }

    return (
        <div>
            <CPagination
                activePage={page}
                pages={totalPage}
                onActivePageChange={handlePageChange}
            ></CPagination>
        </div>
    )
}
