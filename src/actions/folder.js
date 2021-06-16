import { fetchSinToken } from "../helpers/fetch";
import { types } from "../types/types"

export const startFolderAdd = ( folder ) => {
    return async( dispatch ) => {

        try {
            const response = await fetchSinToken('folder', folder, 'POST');
            const body = await response.json();         

            if ( response.ok ) {
                folder.id = body.id;                               
                dispatch( folderAdd( folder ) );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const folderAdd = (folder) => {
    return {
        type: types.folderAdd,
        payload: folder      
    }
}

export const startFolderUpdate = ( folder ) => {
    return async( dispatch ) => {

        try {
            const response = await fetchSinToken('folder', folder, 'PUT');
                        
            if ( response.ok ) {                                               
                dispatch( folderUpdate( folder ) );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const folderUpdate = (folder) => {
    return {
        type: types.folderUpdate,
        payload: folder      
    }
}

export const startFolderDelete = ( folder ) => {
    return async( dispatch ) => {

        const action = `folder/${folder.id}`;
        try {
            const response = await fetchSinToken(action, null, 'DELETE');
            
            if ( response.ok ) {                    
                dispatch( folderDelete() );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const folderDelete = () => {
    return {
        type: types.folderDelete            
    }
}

export const startFolderLoading = () => {
    return async(dispatch) => {

        try {

            const pagination = {
                "page": 1,
                "recordsPerPage": 20
              };

            const response = await fetchSinToken("folder/GetAll", pagination, 'POST');
            const folders = await response.json();

            if ( response.ok ) {                                        
                dispatch( folderLoaded( folders ) );
            }

        } catch (error) {
            console.log(error);
        }
    }
}

export const folderLoaded = (folders) => ({
    type: types.folderLoaded,
    payload: folders
})

export const folderSetActive = (folder) => {
    return {
        type: types.folderSetActive,
        payload: folder
    }
}

export const folderClearActive = () => {
    return {
        type: types.folderClearActive        
    }
}



