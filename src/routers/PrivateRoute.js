import React from 'react';
import { Redirect, Route } from "react-router-dom"
import PropTypes from 'prop-types'

export const PrivateRoute = ({
    isAutenthicated,
    component: Component,
    ...rest

}) => {

    return (
        <Route  {...rest}
                component={ (props) => (
                    (isAutenthicated) 
                        ? <Component {...props} />
                        : <Redirect to="/login" />
                )}
        />
    )
}

PrivateRoute.propTypes = {
    isAutenthicated: PropTypes.bool.isRequired,
    component: PropTypes.func.isRequired
}