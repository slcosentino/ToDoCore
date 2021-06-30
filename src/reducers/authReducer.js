import { types } from "../types/types";

const initialState = {
   userName: null
};

export const authReducer = (state = initialState, action) => {

    switch (action.type) {
        
        case types.login:
            return {               
                userName: action.payload.userName                
            };

        case types.logout:
            return {};
    
        default:
            return state;
    }
};