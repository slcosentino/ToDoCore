import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import {        
    BrowserRouter,    
    Switch 
  } from "react-router-dom";
import { login } from '../actions/auth';
import {TheLayout } from '../components/CoreUi/TheLayout';
import { checkValidToken, getToken } from '../helpers/localStorage';
import { useAccount } from '../hooks/useAccount';
import Login from '../views/public/login/Login';
import { PrivateRoute } from './PrivateRoute';
import { PublicRoute } from './PublicRoute';
 
export const AppRouter = () => {

    const { userName } = useSelector(state => state.auth);   
    const { auth, setAuth } = useAccount();
    const dispatch = useDispatch();   
 
    useEffect(() => {    
        
        setAuth(checkValidToken());   
        const token = getToken() || null;
       
        if(token)
            dispatch(login(token));        
       
    }, [userName, dispatch, setAuth]);

    return (
        <BrowserRouter>         
            <div>
                <Switch>     
                    <PublicRoute exact 
                                 path="/login"
                                 component={Login}
                                 isAuthenticated={auth}
                    />
					
				   <PrivateRoute path="/"
                                  component={TheLayout}
                                  isAutenthicated={auth}
                    />                  
                </Switch>
            </div>            
        </BrowserRouter>
    )
}
