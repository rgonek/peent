/*eslint no-extend-native: ["error", { "exceptions": ["Object", "Array", "Map"] }]*/
import { isNullOrUndefined } from "./utility";

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

URLSearchParams.prototype.setOrDelete = function (name, value) {
    if (isNullOrUndefined(value) || value === "") this.delete(name);
    else this.set(name, value);

    return this;
};
