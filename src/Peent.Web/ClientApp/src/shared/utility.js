export const updateObject = (oldObject, updatedProperties) => {
    return {
        ...oldObject,
        ...updatedProperties
    };
};

export const convertEmptyStringsToNulls = (obj) => {
    const replacer = (key, value) => String(value) === '' ? null : value;
  
    return JSON.parse( JSON.stringify(obj, replacer));
};

export const convertToSortModel = sortBy => {
    return sortBy.map((item, index) => {
        return {
            field: item.id,
            direction: item.desc ? 'desc' : 'asc'
        };
    });
}

export const convertToFilterModel = filters => {
    return Object.keys(filters)
        .map(key => {
            return {
                field: key,
                values: Array.isArray(filters[key]) ? filters[key].map(item => item.toString()) : [filters[key].toString()]
            }
        });
}