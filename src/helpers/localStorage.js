export const setToken = (appToken) => {    
    localStorage.removeItem("appToken");
    localStorage.setItem("appToken", JSON.stringify(appToken));    
};

export const getToken = () =>{
    return JSON.parse(localStorage.getItem("appToken"));
};

export const checkValidToken = () =>{    
    let validToken = true;    
    const appToken = getToken();
    const {expiration} =  appToken || {};   
     
    if(!expiration  || Date.parse(expiration) < Date.now())
    {
        validToken = false;        
    }        
    return validToken;
};