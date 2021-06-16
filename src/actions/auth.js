import { types } from "../types/types"

export const startLoginEmailPassword = (email, password) => {
    return (dispatch) => {
        dispatch(login(email, password))
    }
}

export const login = (uid, displayName) => {
    return {
        type: types.login,
        payload:{
            uid,
            displayName
        }
    }
}

export const logout = ( ) => {
    return {
        type: types.logout                
    }
}