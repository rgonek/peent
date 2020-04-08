import * as constants from "./constants";

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

export const buildQueryParams = (pageIndex, pageSize, sortBy, filters) => {
    const query = { ...buildFiltersQueryParams(filters) };
    if (pageIndex !== constants.DEFAULT_PAGE) query.page = pageIndex;
    if (pageSize !== constants.DEFAULT_PAGE_SIZE) query.page_size = pageSize;
    if (sortBy.length > 0) query.sort = buildSortByQueryParams(sortBy);

    return query;
};

const buildFiltersQueryParams = (obj) => {
    const filters = {};
    obj.forEach(({ id, value }) => {
        filters[id] =
            isUndefined(value) === false && isArray(value)
                ? value
                      .map((val) => {
                          return val.toString();
                      })
                      .join(constants.FILTER_VALUES_SEPARATOR)
                : (value ?? "").toString();
    });

    return filters;
};

const buildSortByQueryParams = (obj) => {
    return obj
        .map(
            (item) =>
                (item.desc ? constants.SORT_DESCENDING_PREFIX : constants.SORT_ASCENDING_PREFIX) +
                item.id
        )
        .join(constants.SORT_FIELDS_SEPARATOR);
};

export const isUndefined = (value) => value === undefined;
export const isNullOrUndefined = (value) => value === null || isUndefined(value);
export const isArray = (value) => Array.isArray(value);
export const isObjectType = (value) => typeof value === "object";
export const isObject = (value) =>
    !isNullOrUndefined(value) && !isArray(value) && isObjectType(value);
export const isPrimitive = (value) => isNullOrUndefined(value) || !isObjectType(value);
