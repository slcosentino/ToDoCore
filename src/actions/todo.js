import { fetchWithInToken } from "../helpers/fetch"
import { getTotalPages } from "../helpers/listHelper";
import { types } from "../types/types"
import { set } from "./ui";
 
export const startTodoAdd = ( todo ) => {
    return async( dispatch ) => {

        try {
            const response = await fetchWithInToken('todo', todo, 'POST');
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
            const response = await fetchWithInToken('todo', todo, 'PUT');
                        
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
            const response = await fetchWithInToken(action, null, 'DELETE');
            
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

export const startTodoLoading = (activePage, recordsPerPage) => {
    return async(dispatch, getState) => {
       
        try {

            const pagination = {
                "page": activePage,
                "recordsPerPage": recordsPerPage
              };

            const response = await fetchWithInToken("todo/GetAll", pagination, 'POST');
            const {todos, totalItems} = await response.json();      
           
            if ( response.ok ) {                     
                const {ui} = getState();
                const {todo: todoUi} = ui;                   
                todoUi.itemPerPage = recordsPerPage;
                todoUi.activePage = activePage;           
                todoUi.totalPage = getTotalPages(totalItems, todoUi.itemPerPage);                                      
                dispatch( todoLoaded( todos ) );                
                dispatch(set({todo: todoUi}));
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




