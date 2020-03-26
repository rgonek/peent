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
