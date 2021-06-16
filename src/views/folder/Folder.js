import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { set } from '../../actions/ui';
import { Form } from '../../components/Folder/Form';
import { List } from '../../components/Folder/List';
import { startFolderLoading } from '../../actions/folder';

const Folder = () => {

    const { folders } = useSelector(state => state.folder);
    const { formFolderShow } = useSelector(state => state.ui);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(startFolderLoading());
    }, []);    
  
    const handleNewFolder = () => {        
        dispatch(set({formFolderShow: true }))             
    }
    return (
        <div>

            {!formFolderShow &&
                <button className="mt-2 btn btn-primary" onClick={handleNewFolder}>New</button>
            }            

             {formFolderShow &&
                <Form />
             }

             {(!formFolderShow && folders && folders.length > 0) &&
               <List />
             }


        </div>
    )
}

export default Folder;