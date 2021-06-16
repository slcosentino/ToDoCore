import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { startFolderAdd,  startFolderUpdate, folderClearActive } from '../../actions/folder';
import { set } from '../../actions/ui';

export const Form = () => {

  const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const dispatch = useDispatch();
  const { active } = useSelector(state => state.folder);

  const onSubmit = (folder) => {     
    
    const folderDto = { 
      id: (folder.id === undefined) ? 0 : folder.id,
      name: folder.name     
    };     

    if(folder.id > 0)
    {          
      dispatch(startFolderUpdate(folderDto));    
    }
    else
    {
      dispatch(startFolderAdd(folderDto));    
    }

    dispatch(folderClearActive());   
    dispatch(set({formFolderShow: false }));   
  };    

  const handleCancelClick = () => {
    dispatch(folderClearActive());   
    dispatch(set({formFolderShow: false }))     
  };

  useEffect(() => {

    if (active) {
      reset({
        id: active.id,
        name: active.name        
      });
    }     

  }, [active])
    return (
        <form className="my-3 p-3 bg-white rounded box-shadow" onSubmit={handleSubmit(onSubmit)}>
        <div>
          <label>Name</label>
          <input        
            type="text"
            className="form-control col-md-6"
            {...register("name", { required: { value: true, message: "The field name is required." } })}
          />
          <div className="mt-1" style={{ color: "red" }} role="alert">
            {errors?.name?.message}
          </div>
        </div> 
        
        <button type="submit" className="mt-2 btn btn-primary">Save</button>
        <button className="mt-2 btn btn-primary" onClick={handleCancelClick}>Cancel</button>

      </form>
    )
}
