Array.prototype.groupBy = function(keyGetter) {
  const map = new Map();
  this.forEach(item => {
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
Map.prototype.toList = function() {
  return Array.from(this);
};

Array.prototype.toOptions = function(labelGetter, valueGetter) {
  return this.map(item => Array.isArray(item) ?
    ({
        label: labelGetter(item[0]),
        options: valueGetter(item[1])
    }) :
    ({
        label: labelGetter(item),
        value: valueGetter(item)
    }));
};

Array.prototype.toFilterModel = function() {
  return this.map(item => ({
      field: item.id,
      values: Array.isArray(item.value) ? 
        item.value.map(val => {
            return val.toString();
        }) : 
        [item.value.toString()]
  }));
};

Array.prototype.toSortModel = function() {
  return this.map((item, index) => {
      return {
          field: item.id,
          direction: item.desc ? 'desc' : 'asc'
      };
  });
};