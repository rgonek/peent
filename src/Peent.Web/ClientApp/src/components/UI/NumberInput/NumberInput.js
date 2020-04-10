import React, { useState, useEffect } from "react";
import { Form } from "react-bootstrap";
import Math from "math-expression-evaluator";
import PropTypes from "prop-types";

const NumberInput = ({
    control,
    name,
    defaultValue: initialValue,
    allowDotAndCommaAsDecimalSeparator,
    onChange,
    onBlur,
    ...props
}) => {
    const { defaultValuesRef, setValue, register } = control;

    const [changed, setChanged] = useState(false);
    const defaultValue = (initialValue ? initialValue : defaultValuesRef.current[name]) ?? "";
    const [stateValue, setStateValue] = useState(defaultValue);
    const [needUpdate, setNeedUpdate] = useState(true);
    useEffect(() => {
        register({ name: name });
    });

    useEffect(() => {
        if (stateValue !== defaultValue && needUpdate) setStateValue(defaultValue);
    }, [defaultValue, stateValue, needUpdate]);

    const handleChange = (e) => {
        setStateValue(e.target.value);
        setChanged(true);
        setNeedUpdate(false);
        if (onChange) onChange(e);
    };

    const handleBlur = (e) => {
        if (changed) {
            const value = allowDotAndCommaAsDecimalSeparator
                ? e.target.value.replace(",", ".")
                : e.target.value;
            try {
                const result = value ? Math.eval(value) : value;
                if (value.toString() !== result) {
                    setStateValue(result);
                    setValue(name, result, true);
                }
            } catch {
                setValue(name, value, true);
            }
            setChanged(false);
        }
        if (onBlur) onBlur(e);
    };

    return (
        <Form.Control
            {...props}
            type="text"
            name={name}
            value={stateValue}
            onChange={handleChange}
            onBlur={handleBlur}
        />
    );
};

NumberInput.propTypes = {
    control: PropTypes.object.isRequired,
    name: PropTypes.string.isRequired,
    defaultValue: PropTypes.number,
    allowDotAndCommaAsDecimalSeparator: PropTypes.bool,
    onChange: PropTypes.func,
    onBlur: PropTypes.func,
};
NumberInput.defaultProps = {
    allowDotAndCommaAsDecimalSeparator: true,
};

export default NumberInput;
