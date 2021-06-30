import { types } from "../types/types"

export const folderAdd = (folder) => {
    return {
        type: types.folderAdd,
        payload: folder      
    }
}

export const folderUpdate = (folder) => {
    return {
        type: types.folderUpdate,
        payload: folder      
    }
}

export const folderDelete = () => {
    return {
        type: types.folderDelete            
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