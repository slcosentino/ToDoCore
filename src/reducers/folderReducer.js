import { types } from "../types/types";

const initialState = {
    active: null,
    folders: []  
};

export const folderReducer = (state = initialState, action) => {

    switch (action.type) {    

        case types.folderLoaded:
            return {
                ...state,
                folders: [...action.payload]
            }

        case types.folderAdd:
            return {
                ...state,
                folders: [...state.folders, action.payload]
            }

        case types.folderDelete:
            return {
                ...state,
                folders: state.folders.filter(folder =>
                    folder.id !== state.active.id  
                ),
                active: null
            }
        case types.folderSetActive:
            return {
                ...state,
                active: action.payload 
            }
        case types.folderClearActive:
            return {
                ...state,
                active: null
            }
        case types.folderUpdate:
            return {
                ...state,
                folders: state.folders.map(folder =>
                    folder.id === action.payload.id ? action.payload : folder
                )
            }                   

        default:
            return state;
    }

};