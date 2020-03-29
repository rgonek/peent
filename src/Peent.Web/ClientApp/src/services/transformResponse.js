import axios from "../axios-peent";

const ISO_8601 = /(\d{4}-\d{2}-\d{2})T(\d{2}:\d{2}:\d{2})/;

const formatDate = (d) => {
    return d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
};

const iterate = (obj, action) => {
    Object.keys(obj).forEach((key) => {
        if (typeof obj[key] === "object" && obj[key]) iterate(obj[key], action);
        else obj[key] = action(obj[key]);
    });
};

const transformResponse = (response) => {
    iterate(response.data, (val) => {
        if (ISO_8601.test(val)) return formatDate(new Date(Date.parse(val)));
        return val;
    });

    return response;
};

axios.interceptors.response.use(transformResponse, (error) => error);
