import axios from '../axios-peent';
import toast from './toast'

const errorResponseHandler = error => {
    if (error.config.hasOwnProperty('errorHandle') && 
        error.config.errorHandle === false) {
        return Promise.reject(error);
    }

    if(error.response){
        if(error.response.data.errors){
            toast.error(error.response.data.title);
            Object.keys(error.response.data.errors)
                .map(key => {
                    error.response.data.errors[key].forEach(element => {
                        toast.error(element);
                    });
                });
        }
        else {
            toast.error(error.response.data);
        }
    }
    else {
        toast.error(error.message);
    }

    return Promise.reject(error);
}

const buildMessage = error => {
    if(error.response){
        if(error.response.data.errors){
            let message = error.response.data.title;
            Object.keys(error.response.data.errors)
                .map(key => {
                    console.log(key)
                    console.log(error.response.data.errors[key])
                    error.response.data.errors[key].forEach(element => {
                        message += ' ' + element;
                    });
                });
            return message;
        }
        return error.response.data;
    }

    return error.message;
}

axios.interceptors.response.use(
    response => response,
    errorResponseHandler
);

export default errorResponseHandler;