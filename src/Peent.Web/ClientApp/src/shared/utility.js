export const updateObject = (oldObject, updatedProperties) => {
    return {
        ...oldObject,
        ...updatedProperties,
    };
};

export const convertEmptyStringsToNulls = (obj) => {
    const replacer = (key, value) => (String(value) === "" ? null : value);

    return JSON.parse(JSON.stringify(obj, replacer));
};
