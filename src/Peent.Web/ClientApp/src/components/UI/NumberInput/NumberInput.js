import React, { useState, useEffect } from "react";
import { Form } from "react-bootstrap";
import Math from "math-expression-evaluator";

const NumberInput = ({
    control,
    name,
    defaultValue,
    allowDotAndCommaAsDecimalSeparator = true,
    onChange,
    onBlur,
    ...restProps
}) => {
    const { defaultValuesRef, setValue, register } = control;

    const [changed, setChanged] = useState(false);
    const [stateValue, setStateValue] = useState(
        defaultValue ? defaultValue : defaultValuesRef.current[name]
    );

    useEffect(() => {
        register({ name: name });
    }, []);

    const handleChange = (e) => {
        setStateValue(e.target.value);
        setChanged(true);
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
            type="text"
            name={name}
            value={stateValue}
            onChange={handleChange}
            onBlur={handleBlur}
            {...restProps}
        />
    );
};

export default NumberInput;
