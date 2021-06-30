import { folderAdd, folderDelete, folderLoaded, folderSetActive, folderUpdate } from '../actions/folder';
import { set } from '../actions/ui';
import { fetchWithInToken } from '../helpers/fetch';
import { getTotalPages } from '../helpers/listHelper';
import { useUi } from './useUi';

export const useFolder = () => {

    const { handleMsgError, handleMsgSuccess } = useUi();    

    const startFolderAdd = ( folder ) => {
        return async( dispatch ) => {            
            
            try {
               
                const response = await fetchWithInToken('folder', folder, 'POST');                   
    
                if ( response.ok ) {
                    const body = await response.json();                       
                    folder.id = body.id;                    
                    dispatch( folderAdd( folder ) );
                    dispatch(folderSetActive(folder));                    
                    handleMsgSuccess('Folder added.');
                }
                else
                    handleMsgError(response);

            } catch (error) {
                console.log(error);
            }          
        }
    };

    const startFolderUpdate = ( folder ) => {
        return async( dispatch ) => {
    
            try {
                const response = await fetchWithInToken('folder', folder, 'PUT');
                            
                if ( response.ok ) {                                               
                    dispatch( folderUpdate( folder ) );
                    handleMsgSuccess('Folder updated.');
                }
                else
                    handleMsgError(response);

            } catch (error) {
                console.log(error);
            }
        }
    };
    
    const startFolderDelete = ( folder ) => {
        return async( dispatch ) => {
    
            const action = `folder/${folder.id}`;
            try {
                const response = await fetchWithInToken(action, null, 'DELETE');
                
                if ( response.ok ) {                    
                    dispatch( folderDelete() );
                    handleMsgSuccess('Folder deleted.');
                }
                else
                    handleMsgError(response);

            } catch (error) {
                console.log(error);
            }
        }
    };

    const startFolderLoading = (activePage, recordsPerPage) => {
        return async(dispatch, getState) => {
    
            try {
                const pagination = {
                    "page": activePage,
                    "recordsPerPage": recordsPerPage
                  };
    
                const response = await fetchWithInToken("folder/GetAll", pagination, 'POST');                              
                
                if ( response.ok ) {                     
                    const {folders, totalItems} = await response.json();            
                    const {ui} = getState();
                    const {folder: folderUi} = ui;                               
                    folderUi.totalPage = getTotalPages(totalItems, folderUi.itemPerPage);                                      
                    dispatch( folderLoaded( folders ) );                 
                    dispatch(set({folder: folderUi}));
                }
                else
                    handleMsgError(response);
    
            } catch (error) {
                console.log(error);
            }
        }
    };

    return {startFolderAdd, startFolderUpdate, startFolderDelete, startFolderLoading} ;
    
}
