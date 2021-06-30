import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { startTodoAdd, startTodoUpdate, todoClearActive } from '../../actions/todo';
import { set } from '../../actions/ui';
import { useFetchFolders } from '../../hooks/useFetchFolders';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { fas } from '@fortawesome/free-solid-svg-icons';
 

export const Form = () => {

  const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const dispatch = useDispatch();
  const { active } = useSelector(state => state.todo);
  const { todo: todoUi } = useSelector(state => state.ui);
  const {folders, loadingFolders} = useFetchFolders();   

  const onSubmit = (todo) => {     
    
    const todoDto = { 
      id: (todo.id === undefined) ? 0 : todo.id,
      name: todo.name,
      folder: folders.find(x => x.id === Number(todo.folder))
    };     
    todoUi.formShow = false;

    if(todo.id > 0)             
      dispatch(startTodoUpdate(todoDto));        
    else    
      dispatch(startTodoAdd(todoDto));       
    
    dispatch(todoClearActive());     
    dispatch(set({todo: todoUi}));
  };    

  const handleCancelClick = () => {
    todoUi.formShow = false;     
    dispatch(todoClearActive());   
    dispatch(set({todo: todoUi}));        
  };

  useEffect(() => {
    
    if (active) {
      reset({
        id: active.id,
        name: active.name,
        folder:active.folder.id
      });
    }     

  }, [active, reset]);

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

      <div className="mt-2">
        <label>Folder</label>
        <select
          name="folder"
          className="form-control col-md-6"
          {...register("folder", { required: { value: true, message: "The field folder is required." } })}
        >
          {
            !loadingFolders && folders.map(
              folder => (
                <option key={`folder_${folder.id}`} value={folder.id}>{folder.name}</option>
              )
            )
          }

        </select>

        <div className="input-group">
          
            <select type="text" className="form-control">
                <option></option>
                <option>Super option 1</option>
                <option>Super option 2</option>
                <option>Super option 3</option>
            </select>
            <span className="input-group-addon">             
                <i className="fa fa-refresh fa-spin"></i>
                
                <FontAwesomeIcon icon="spinner" spin />
                
            </span>
        </div>
   
    
        
        <div className="mt-1" style={{ color: "red" }} role="alert">
          {errors?.folder?.message}
        </div>
      </div>

      <button type="submit" className="mt-2 btn btn-primary">Save</button>
      <button className="mt-2 btn btn-primary" onClick={handleCancelClick}>Cancel</button>

    </form>
  )
}
