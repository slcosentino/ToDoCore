import { applyMiddleware, combineReducers, compose, createStore } from "redux"
import thunk from "redux-thunk";
import { authReducer } from "../reducers/authReducer";
import { folderReducer } from "../reducers/folderReducer";
import { todoReducer } from "../reducers/todoReducer";
import { uiReducer } from "../reducers/uiReducer";

const composeEnhancers = (typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__) || compose;

const reducers = combineReducers({
    auth: authReducer,
    folder: folderReducer,
    todo:todoReducer,
    ui: uiReducer   
});

export const store = createStore(
    reducers,
    composeEnhancers(
        applyMiddleware(thunk)
    )
);

 