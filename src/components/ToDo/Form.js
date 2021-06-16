import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { startTodoAdd, startTodoLoading, startTodoUpdate, todoClearActive } from '../../actions/todo';
import { set } from '../../actions/ui';

const folders = [
  { id: 1, name: "Root" },
  { id: 2, name: "Folder1" }  
];


export const Form = () => {

  const { register, handleSubmit, formState: { errors }, reset } = useForm();
  const dispatch = useDispatch();
  const { active } = useSelector(state => state.todo);

  const onSubmit = (todo) => {     
    
    const todoDto = { 
      id: (todo.id === undefined) ? 0 : todo.id,
      name: todo.name,
      folder: folders.find(x => x.id === Number(todo.folder))
    };     

    if(todo.id > 0)
    {          
      dispatch(startTodoUpdate(todoDto));    
    }
    else
    {
      dispatch(startTodoAdd(todoDto));    
    }
    
    dispatch(todoClearActive());  
    dispatch(set({formTodoShow: false }));   
  };    

  const handleCancelClick = () => {
    dispatch(todoClearActive());   
    dispatch(set({formTodoShow: false }))     
  };

  useEffect(() => {

    if (active) {
      reset({
        id: active.id,
        name: active.name,
        folder:active.folder.id
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

      <div className="mt-2">
        <label>Folder</label>
        <select
          name="folder"
          className="form-control col-md-6"
          {...register("folder", { required: { value: true, message: "The field folder is required." } })}
        >
          {
            folders.map(
              folder => (
                <option key={`folder_${folder.id}`} value={folder.id}>{folder.name}</option>
              )
            )
          }

        </select>
        <div className="mt-1" style={{ color: "red" }} role="alert">
          {errors?.folder?.message}
        </div>
      </div>

      <button type="submit" className="mt-2 btn btn-primary">Save</button>
      <button className="mt-2 btn btn-primary" onClick={handleCancelClick}>Cancel</button>

    </form>
  )
}
