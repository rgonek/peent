import React, { useEffect, useState } from "react";
import ReactSelect from "react-select";
import "./Select.css";

function Select({
    control,
    name,
    isInvalid,
    defaultValue,
    onChange,
    onBlur,
    isMulti = false,
    className,
    isClearable = true,
    isSearchable = true,
    ...restProps
}) {
    const {
        setValue,
        register,
        mode: { isOnBlur },
        reValidateMode: { isReValidateOnBlur },
    } = control;

    const [stateValue, setStateValue] = useState(defaultValue);

    useEffect(() => {
        register({ name: name });
    }, []);

    const handleChange = (value, action) => {
        const val = isMulti ? value?.map((v) => v.value) ?? [] : value?.value;
        setValue(name, val, true);
        setStateValue(val);
        if (onChange) onChange(value, action);
    };

    const handleBlur = (e) => {
        const shouldReValidateOnBlur = isOnBlur || isReValidateOnBlur;
        if (shouldReValidateOnBlur) setValue(name, stateValue, true);
        if (onBlur) onBlur(e);
    };

    const classNames = ["react-select", isInvalid ? "is-invalid" : "", className].join(" ");

    return (
        <ReactSelect
            name={name}
            isMulti={isMulti}
            isClearable={isClearable}
            isSearchable={isSearchable}
            onChange={handleChange}
            className={classNames}
            onBlur={handleBlur}
            {...restProps}
        />
    );
}

export default Select;
