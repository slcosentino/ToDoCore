import { useEffect, useState } from "react";

export const useForm = (initialState = {}) => {

    const [state, setState] = useState(initialState);
    

    const handleInputChange = ({target}) =>(
        setState({
            ...state,
            [target.name]: target.value
        })
    );    

    const reset = () => (                
        setState(initialState)        
    );

    useEffect(() => {

    }, [state]);   

    return [state, handleInputChange, reset];


}
