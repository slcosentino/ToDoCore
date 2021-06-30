import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { set } from '../actions/ui';
import { useAccount } from './useAccount';

export const useUi = () => {  
    const dispatch = useDispatch();
    const { logoutApp } = useAccount();
    const { msgError, msgSuccess } = useSelector(state => state.ui);       

    const handleMsgError = async (response, timeout = 5000) => {        

        const {message, errors} = await response.json() || {};
       
        if (response.status === 401)
            logoutApp();    
        else
        {         
            if(message){
                dispatch(set({msgError : message} ));

                setTimeout(() => {
                    dispatch(set({msgError : null} ));
                    dispatch(set({msgSuccess : null} ));
                }, timeout);

            }                
        }        
    }

    const handleMsgSuccess =  (message, timeout = 5000) => {                        
        dispatch(set({msgSuccess : message} ));   
        
        setTimeout(() => {
            dispatch(set({msgError : null} ));
            dispatch(set({msgSuccess : null} ));
        }, timeout);        
    }  

    return {handleMsgError, handleMsgSuccess, msgError, msgSuccess};
}

