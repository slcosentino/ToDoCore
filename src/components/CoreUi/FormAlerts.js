import React from 'react'
import { CAlert, CProgress, CProgressBar  } from '@coreui/react'
import { useUi } from '../../hooks/useUi'

export const FormAlerts = () => {
    const { msgError, msgSuccess } = useUi();    

    return (
        <div className="mt-4">
            <CAlert color="danger"
                style={{ display: msgError ? "" : "none" }}
            >   {msgError}                
            </CAlert>
            
            <CAlert color="success" 
                style={{ display: msgSuccess ? "" : "none" }}> 
                {msgSuccess}
            </CAlert>
        </div>
    )
}
