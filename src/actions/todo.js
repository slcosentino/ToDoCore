import { fetchSinToken } from "../helpers/fetch"
import { types } from "../types/types"
 
export const startTodoAdd = ( todo ) => {
    return async( dispatch ) => {

        try {
            const response = await fetchSinToken('todo', todo, 'POST');
            const body = await response.json();         

            if ( response.ok ) {
                todo.id = body.id;                               
                dispatch( todoAdd( todo ) );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const todoAdd = (todo) => {
    return {
        type: types.todoAdd,
        payload: todo      
    }
}

export const startTodoUpdate = ( todo ) => {
    return async( dispatch ) => {

        try {
            const response = await fetchSinToken('todo', todo, 'PUT');
                        
            if ( response.ok ) {                                     
                dispatch( todoUpdate( todo ) );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const todoUpdate = (todo) => {
    return {
        type: types.todoUpdate,
        payload: todo      
    }
}

export const startTodoDelete = ( todo ) => {
    return async( dispatch ) => {

        const action = `todo/${todo.id}`;
        try {
            const response = await fetchSinToken(action, null, 'DELETE');
            
            if ( response.ok ) {                    
                dispatch( todoDelete() );
            }
        } catch (error) {
            console.log(error);
        }
    }
}

export const todoDelete = () => {
    return {
        type: types.todoDelete            
    }
}


export const todoSetActive = (todo) => {
    return {
        type: types.todoSetActive,
        payload: todo
    }
}

export const todoClearActive = () => {

    return {
        type: types.todoClearActive        
    }
}

export const startTodoLoading = () => {
    return async(dispatch) => {

        try {

            const pagination = {
                "page": 1,
                "recordsPerPage": 20
              };

            const response = await fetchSinToken("todo/GetAll", pagination, 'POST');
            const todos = await response.json();
            
            if ( response.ok ) {                                        
                dispatch( todoLoaded( todos ) );
            }

        } catch (error) {
            console.log(error);
        }
    }
}

export const todoLoaded = (todos) => ({
    type: types.todoLoaded,
    payload: todos
})




