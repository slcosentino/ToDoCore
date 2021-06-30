import {  useState } from "react";
import { useDispatch } from "react-redux";
import { login, logout } from "../actions/auth";
import { set } from "../actions/ui";
import { fetchWithOutToken } from "../helpers/fetch";
import { setToken } from "../helpers/localStorage";

export const useAccount = () => {
    const [auth, setAuth] = useState(false);
    const dispatch = useDispatch();       
   
    const loginApp = async ({userName, password}) =>{                  
       
        dispatch(set({loading : true} ));

        try {        
            const credential = {userName, password};            
            const response = await fetchWithOutToken('account/login', credential, 'POST');           
            const appToken = await response.json();         
            
            if ( response.ok ) {      
                setToken(appToken);         
                dispatch(login(appToken));                 
            }
            else
                dispatch(set({msgError : appToken.message} ));           
        } catch (error) {            
            dispatch(set({msgError : "We have a problem in our system. Ask the administrator."} ));
            console.log(error);
        }        

        dispatch(set({loading : false} ));    
         
    };

    const logoutApp = () =>{                  
        localStorage.clear();    
        dispatch(logout());        
    } ;  

    return { logoutApp, loginApp, auth, setAuth };
}

 

 