import { types } from "../types/types";

const initialState = {
    loading: false,
    msgError: null,
    msgSuccess: null,
    sidebarShow: 'responsive',     
    folder:{
        formShow: false,
        activePage: 1,
        totalPage: 0,
        itemPerPage:20      
    },
    todo:{
        formShow: false,
        activePage: 1,
        totalPage: 0,
        itemPerPage:10    
    }
};

export const uiReducer = (state = initialState, action) => {

    switch (action.type) {
        case types.uiSetError:
            return{ ...state,
                    msgError: action.payload}
            
        case types.uiRemoveError:
            return{ ...state,
                msgError: null}
                
        case types.uiSet:
            return {...state, ...action.payload }

        default:
            return state;
    }

};