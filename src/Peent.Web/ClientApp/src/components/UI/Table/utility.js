import * as constants from "../../../shared/constants";

export const getQueryState = (query, columns) => {
    const sortBy = query.has(constants.QUERY_PARAMETER_SORT)
        ? query
              .get(constants.QUERY_PARAMETER_SORT)
              .split(constants.SORT_FIELDS_SEPARATOR)
              .map((item) => {
                  const desc = item[0] === constants.SORT_DESCENDING_PREFIX;
                  const id = desc ? item.substring(1) : item;
                  return { id, desc };
              })
        : [];
    const pageIndex = query.has(constants.QUERY_PARAMETER_PAGE)
        ? Number(query.get(constants.QUERY_PARAMETER_PAGE))
        : constants.DEFAULT_PAGE;
    const pageSize = query.has(constants.QUERY_PARAMETER_PAGE_SIZE)
        ? Number(query.get(constants.QUERY_PARAMETER_PAGE_SIZE))
        : constants.DEFAULT_PAGE_SIZE;
    const filters = [];
    for (let column of columns) {
        const id = column.id ?? column.accessor;
        if (query.has(id)) {
            const values = query.get(id).split(constants.FILTER_VALUES_SEPARATOR);
            let value = values;
            if (values.length === 1) value = values[0];
            filters.push({
                id,
                value,
            });
        }
    }

    return { pageIndex, pageSize, sortBy, filters };
};

export const buildSearchQuery = (query, columns, pageIndex, pageSize, sortBy, filters) => {
    query.delete(constants.QUERY_PARAMETER_GLOBAL_FILTER);
    columns.forEach(({ id, accessor }) => {
        query.delete(accessor || id);
    });
    query.setOrDelete(
        constants.QUERY_PARAMETER_PAGE,
        pageIndex === constants.DEFAULT_PAGE ? null : pageIndex
    );
    query.setOrDelete(
        constants.QUERY_PARAMETER_PAGE_SIZE,
        pageSize === constants.DEFAULT_PAGE_SIZE ? null : pageSize
    );

    filters.forEach(({ id, value }) => query.setOrDelete(id, value));
    const sortQueryParamValue = sortBy.map(({ id, desc }) => (desc ? "-" : "") + id).join(",");
    query.setOrDelete(constants.QUERY_PARAMETER_SORT, sortQueryParamValue);
    query.sort();

    return "?" + query.toString().replace("%2C", ",");
};
