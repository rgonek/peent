import React, { useEffect, useState, useCallback, useMemo } from "react";
import ReactSelect from "react-select";
import PropTypes from "prop-types";
import { isPrimitive, isNullOrUndefined } from "../../../shared/utility";
import "./Select.css";

function Select({
    control,
    name,
    options,
    isInvalid,
    defaultValue: initialValue,
    onChange,
    onBlur,
    isMulti,
    className,
    isClearable,
    isSearchable,
    ...props
}) {
    const {
        defaultValuesRef,
        setValue,
        register,
        mode: { isOnBlur },
        reValidateMode: { isReValidateOnBlur },
    } = control;

    const hasGroups = useMemo(() => {
        if (isNullOrUndefined(options) || options.length === 0) return false;

        return isNullOrUndefined(options[0].options) === false;
    }, [options]);

    const getOptionItem = (value) => {
        if (isPrimitive(value)) return getOptionItemBasedOnPrimitiveValue(value);
        return getOptionItemBasedOnPrimitiveValue(value.id);
    };

    const getOptionItemBasedOnPrimitiveValue = (value) => {
        if (hasGroups) {
            for (const option of options) {
                const result = option.options.find((x) => x.value === value);
                if (isNullOrUndefined(result) === false) return result;
            }
            return value;
        } else {
            return options.find((x) => x.value === value);
        }
    };

    const isEqual = useCallback(
        (left, right) => {
            if (isMulti) {
                if (isNullOrUndefined(left) && isNullOrUndefined(right)) return true;
                if (
                    isNullOrUndefined(left) ||
                    isNullOrUndefined(right) ||
                    left.length !== right.length
                )
                    return false;

                for (let i = 0; i < left.length; i++)
                    if (left[i]?.value !== right[i]?.value) return false;

                return true;
            }

            return left?.value === right?.value;
        },
        [isMulti]
    );

    let defaultValue = initialValue ? initialValue : defaultValuesRef.current[name];
    if (defaultValue)
        defaultValue = isMulti ? defaultValue.map(getOptionItem) : getOptionItem(defaultValue);

    const [stateValue, setStateValue] = useState(defaultValue);
    const [needUpdate, setNeedUpdate] = useState(true);

    useEffect(() => {
        if (isEqual(stateValue, defaultValue) === false && needUpdate) {
            setStateValue(defaultValue);
        }
    }, [defaultValue, stateValue, needUpdate, isEqual]);

    useEffect(() => {
        register({ name: name });
    });

    const getPrimitiveValue = (value) =>
        isMulti ? value?.map((v) => v.value) ?? [] : value?.value;

    const handleChange = (value, action) => {
        const primitiveValue = getPrimitiveValue(value);
        setValue(name, primitiveValue, true);
        setNeedUpdate(false);
        setStateValue(value);
        if (onChange) onChange(value, action);
    };

    const handleBlur = (e) => {
        const shouldReValidateOnBlur = isOnBlur || isReValidateOnBlur;
        if (shouldReValidateOnBlur) setValue(name, getPrimitiveValue(stateValue), true);
        if (onBlur) onBlur(e);
    };

    const classNames = ["react-select", isInvalid ? "is-invalid" : "", className].join(" ");

    return (
        <ReactSelect
            {...props}
            name={name}
            options={options}
            value={stateValue}
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
    options: PropTypes.array,
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
