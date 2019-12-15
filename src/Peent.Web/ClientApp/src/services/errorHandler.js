import axios from '../axios-peent';
import toast from './toast'

const errorResponseHandler = error => {
    if (error.config.hasOwnProperty('errorHandle') && 
        error.config.errorHandle === false) {
        return Promise.reject(error);
    }

    const message = error.response ? 
        error.response.data : 
        error.message;
    toast.error(message);

    return Promise.reject(error);
}

axios.interceptors.response.use(
    response => response,
    errorResponseHandler
);

export default errorResponseHandler;