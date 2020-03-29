import Noty from "noty";

const toast = {
    error: (message) => {
        new Noty({
            type: "error",
            text: message,
            timeout: 5000,
        }).show();
    },
    success: (message) => {
        new Noty({
            type: "success",
            text: message,
            timeout: 5000,
        }).show();
    },
};

export default toast;
