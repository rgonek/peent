import React, { useEffect, useState } from "react";
import ReactSelect from "react-select";
import PropTypes from "prop-types";
import "./Select.css";

function Select({
    control,
    name,
    isInvalid,
    defaultValue,
    onChange,
    onBlur,
    isMulti,
    className,
    isClearable,
    isSearchable,
    ...props
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
    });

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
            {...props}
            name={name}
            isMulti={isMulti}
            isClearable={isClearable}
            isSearchable={isSearchable}
            onChange={handleChange}
            className={classNames}
            onBlur={handleBlur}
        />
    );
}

Select.propTypes = {
    control: PropTypes.object.isRequired,
    name: PropTypes.string,
    isInvalid: PropTypes.bool,
    defaultValue: PropTypes.any,
    onChange: PropTypes.func,
    onBlur: PropTypes.func,
    isMulti: PropTypes.bool,
    className: PropTypes.string,
    isClearable: PropTypes.bool,
    isSearchable: PropTypes.bool,
};
Select.defaultProps = {
    isMulti: false,
    isClearable: true,
    isSearchable: true,
};

export default Select;
