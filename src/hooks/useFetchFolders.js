import { useEffect, useState } from "react";
import { fetchWithInToken } from "../helpers/fetch";
import { useUi } from "./useUi";

export const useFetchFolders = () => {
  const [state, setState] = useState({folders:[], loading: true});
  const { handleMsgError } = useUi();

  useEffect(() => {
    getFolders();    
  }, []);

  const getFolders = async () =>{

    try {        
      const response = await fetchWithInToken("folder/GetAllWithOutPagination", {}, 'POST');    
      
      if (response.ok)
      {
        const{folders} =  await response.json(); 
        setState({folders, loading: false});      
      }
      else
        handleMsgError(response);
              
    } catch (error) {                 
        console.log(error);        
    }   
  }

  return state;
}




 