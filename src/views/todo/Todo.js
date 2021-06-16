 import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { set } from '../../actions/ui';
import { Form } from '../../components/ToDo/Form';
import { List } from '../../components/List';
import { startTodoLoading } from '../../actions/todo';

const Todo = () => {

    const { active,todos } = useSelector(state => state.todo);
    const { formTodoShow } = useSelector(state => state.ui);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(startTodoLoading());
    }, []);    
  
    const handleNewTodo = () => {        
        dispatch(set({formTodoShow: true }))             
    }
    return (
        <div>

            {!formTodoShow &&
                <button className="mt-2 btn btn-primary" onClick={handleNewTodo}  >New</button>
            }            

             {formTodoShow &&
                <Form />
             }

             {(!formTodoShow && todos && todos.length > 0) &&
               <List />
             }


        </div>
    )
}

export default Todo;