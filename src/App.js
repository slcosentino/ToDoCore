import React from 'react';
import "@coreui/coreui/scss/coreui.scss";
import icons from "./icons";
import { Provider } from 'react-redux';
import { AppRouter } from './routers/AppRouter';
import { store } from './store/store';

React.icons = icons;

function App() { 
    return (

        <Provider store={store}>
          <AppRouter />
        </Provider>  
        
      );
}

export default App;
