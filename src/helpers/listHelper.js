export const getTotalPages = (totalItems = 0, itemPerPage = 0) => {   
    if (itemPerPage <= 0)
        return 1;   
    
    return Math.ceil( totalItems / itemPerPage);
};