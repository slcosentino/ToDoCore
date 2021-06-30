import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { set } from '../../actions/ui';
import { Form } from '../../components/Folder/Form';
import { List } from '../../components/Folder/List';

const Folder = () => {

    const dispatch = useDispatch();
    const { folder: folderUi } = useSelector(state => state.ui);    
    const {formShow: formFolderShow} = folderUi;
  
    const handleNewFolder = () => { 
        folderUi.formShow = true;
        dispatch(set({folder:folderUi}))          
    }
    return (
        <div>

            {!formFolderShow &&
                <button className="mt-2 btn btn-primary" onClick={handleNewFolder}>New</button>
            }            

             {formFolderShow &&
                <Form />
             }

             {!formFolderShow &&
               <List />
             }


        </div>
    )
}

export default Folder;