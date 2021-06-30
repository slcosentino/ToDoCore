 import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { set } from '../../actions/ui';
import { Form } from '../../components/ToDo/Form';
import { List } from '../../components/ToDo/List';

const Todo = () => {
    const dispatch = useDispatch();    
    const { todo: todoUi } = useSelector(state => state.ui);
    const {formShow: formTodoShow} = todoUi;
  
    const handleNewTodo = () => {     
        todoUi.formShow = true;         
        dispatch(set({todo: todoUi})) ;    
    }
    return (
        <div>

            {!formTodoShow &&
                <button className="mt-2 btn btn-primary" onClick={handleNewTodo}  >New</button>
            }            

             {formTodoShow &&
                <Form />
             }

             {!formTodoShow &&
               <List />
             }


        </div>
    )
}

export default Todo;