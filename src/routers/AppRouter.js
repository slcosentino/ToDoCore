import React from 'react'
import { useSelector } from 'react-redux';
import {        
    BrowserRouter,    
    Switch 
  } from "react-router-dom";
import {TheLayout } from '../components/CoreUi/TheLayout';
import Login from '../views/public/login/Login';
import { PrivateRoute } from './PrivateRoute';
import { PublicRoute } from './PublicRoute';

 

 
export const AppRouter = () => {

    const {uid} = useSelector(state => state.auth)
    const auth = true; //(uid ) ? true: false;

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
