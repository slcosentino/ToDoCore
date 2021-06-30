import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { folderClearActive } from '../../actions/folder';
import { set } from '../../actions/ui';
import { useFolder } from '../../hooks/useFolder';
import { FormAlerts } from '../CoreUi/FormAlerts';
import { FormButtons } from '../CoreUi/FormButtons';

export const Form = () => {

  const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const dispatch = useDispatch();
  const { active } = useSelector(state => state.folder);
  const { folder: folderUi } = useSelector(state => state.ui);
  const { startFolderAdd, startFolderUpdate } = useFolder();  

  const onSubmit = (folder) => {

    const folderDto = {
      id: (folder.id === undefined) ? 0 : folder.id,
      name: folder.name
    };
    
    if (folder.id > 0)
      dispatch(startFolderUpdate(folderDto));
    else
      dispatch(startFolderAdd(folderDto));

  };

  const handleCancelClick = () => {
    folderUi.formShow = false;
    dispatch(folderClearActive());
    dispatch(set({ folder: folderUi }))
  };

  useEffect(() => {

    if (active) {
      reset({
        id: active.id,
        name: active.name
      });
    }
  }, [active, reset]);  

  return (
    <form className="my-3 p-3 bg-white rounded box-shadow" onSubmit={handleSubmit(onSubmit)}>
      <div className="col-md-6" >
        
        <div className="form-group">
          <label>Name</label>
          <input
            type="text"
            className="form-control"
            {...register("name", { required: { value: true, message: "The field name is required." } })}
          />
          <div className="mt-1" style={{ color: "red" }} role="alert">
            {errors?.name?.message}
          </div>
        </div>
        
        <FormButtons
          firstBtnTitle={active ? "Update": "Save" }
          secondBtnFunction={handleCancelClick}
        />

        <FormAlerts />
        
      </div>
    </form>
  )
}
