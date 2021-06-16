import { types } from "../types/types";

const initialState = {
    active: null,
    todos: []  
};

export const todoReducer = (state = initialState, action) => {

    switch (action.type) {
        
        case types.todoLoaded:
            return {
                ...state,
                todos: [...action.payload]
            }
        
        case types.todoAdd:
            return {
                ...state,
                todos: [...state.todos, action.payload]
            }

        case types.todoDelete:
            return {
                ...state,
                todos: state.todos.filter(todo =>
                    todo.id !== state.active.id  
                ),
                active: null
            }
        case types.todoSetActive:
            return {
                ...state,
                active: action.payload 
            }
        case types.todoClearActive:
            return {
                ...state,
                active: null
            }
        case types.todoUpdate:
            return {
                ...state,
                todos: state.todos.map(todo =>
                    todo.id === action.payload.id ? action.payload : todo
                )
            }                   

        default:
            return state;
    }

}