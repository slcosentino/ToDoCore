import { getToken } from "./localStorage";

const baseUrl = process.env.REACT_APP_API_URL;

const fetchWithOutToken = ( endpoint, data, method = 'GET' ) => {

    const url = `${ baseUrl }/${ endpoint }`;

    if ( method === 'GET' ) {
        return fetch( url );
    } else {
        return fetch( url, {
            method,
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify( data )
        });
    }
}

const fetchWithInToken = ( endpoint, data, method = 'GET' ) => { 

    const url = `${ baseUrl }/${ endpoint }`;
    const {token} = getToken() || '';

    if ( method === 'GET' ) {
        return fetch( url, {
            method,
            headers: {
                'Authorization': `bearer ${token}`
            }
        });
    } else {
                
        return fetch( url, {
            method,
            headers: {
                'Content-type': 'application/json',
                'Authorization': `bearer ${token}`
            },
            body: JSON.stringify( data )
        });
    }
}

export {
    fetchWithInToken,
    fetchWithOutToken
}