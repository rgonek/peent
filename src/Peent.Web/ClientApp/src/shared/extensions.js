/*eslint no-extend-native: ["error", { "exceptions": ["Object", "Array", "Map"] }]*/

import * as constants from "./constants";

Array.prototype.groupBy = function (keyGetter) {
    const map = new Map();
    this.forEach((item) => {
        const key = keyGetter(item);
        const collection = map.get(key);
        if (!collection) {
            map.set(key, [item]);
        } else {
            collection.push(item);
        }
    });
    return map;
};

Map.prototype.toList = function () {
    return Array.from(this);
};

Array.prototype.toOptions = function (labelGetter, valueGetter) {
    return this.map((item) =>
        Array.isArray(item)
            ? {
                  label: labelGetter(item[0]),
                  options: valueGetter(item[1]),
              }
            : {
                  label: labelGetter(item),
                  value: valueGetter(item),
              }
    );
};

Array.prototype.toFilterModel = function () {
    return this.map((item) => ({
        field: item.id,
        values: Array.isArray(item.value)
            ? item.value.map((val) => {
                  return val.toString();
              })
            : [item.value.toString()],
    }));
};

Array.prototype.toSortModel = function () {
    return this.map((item) => {
        return {
            field: item.id,
            direction: item.desc
                ? constants.SortDirection.descending
                : constants.SortDirection.ascending,
        };
    });
};

Array.prototype.addFilter = function (columnId, value) {
    if (value) {
        return this.find((item) => item.id === columnId)
            ? this.map((item) => {
                  return item.id === columnId ? buildFilterObject(columnId, value) : item;
              })
            : [...this, buildFilterObject(columnId, value)];
    }
    return this.filter((item) => item.id !== columnId);
};

const buildFilterObject = (columnId, value) => {
    return { id: columnId, value: value };
};
