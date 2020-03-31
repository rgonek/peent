import React, { useEffect, useState } from "react";
import { FiCalendar } from "react-icons/fi";
import { Form, InputGroup, Button } from "react-bootstrap";
import Flatpickr from "react-flatpickr";
import PropTypes from "prop-types";
import "flatpickr/dist/themes/material_blue.css";
import "./DateTimePicker.css";

function DateTimePicker({ control, name, isInvalid, defaultValue, className, onChange, ...props }) {
    const { defaultValuesRef, setValue, register } = control;

    const value = defaultValue ? defaultValue : defaultValuesRef.current[name];
    const [flatpickr, setFlatpickr] = useState(null);

    useEffect(() => {
        register({ name: name });
    }, []);

    const handleChange = (selectedDates, dateStr, instance) => {
        setValue(name, dateStr, true);
        if (onChange) onChange(selectedDates, dateStr, instance);
    };
    const handleCalendarClose = (selectedDates, dateStr) => {
        flatpickr.setDate(dateStr, true);
    };

    const handleInputButtonClick = () => {
        flatpickr.open();
    };
    const setFlatpickrRef = (ref) => {
        if (ref) setFlatpickr(ref.flatpickr);
    };

    const isInvalidClassName = isInvalid ? "is-invalid" : "";
    const groupClassNames = ["date-time-picker", isInvalidClassName, className].join(" ");
    const inputClassNames = [isInvalidClassName].join(" ");

    const inputRender = ({ defaultValue, value, ...props }, ref) => {
        return (
            <InputGroup className={groupClassNames}>
                <Form.Control
                    {...props}
                    name={name}
                    value={value}
                    defaultValue={defaultValue}
                    className={inputClassNames}
                    ref={ref}
                />
                <InputGroup.Append>
                    <Button variant="outline-primary" onClick={handleInputButtonClick}>
                        <FiCalendar className="feather" />
                    </Button>
                </InputGroup.Append>
            </InputGroup>
        );
    };
    inputRender.propTypes = {
        defaultValue: PropTypes.string,
        value: PropTypes.string,
    };

    const options = {
        enableTime: false,
        time_24hr: true,
        allowInput: true,
        locale: { firstDayOfWeek: 1 },
        onClose: handleCalendarClose,
    };

    return (
        <Flatpickr
            {...props}
            name={name}
            defaultValue={value}
            render={inputRender}
            options={options}
            onChange={handleChange}
            ref={setFlatpickrRef}
        />
    );
}

DateTimePicker.propTypes = {
    control: PropTypes.object.isRequired,
    name: PropTypes.string.isRequired,
    isInvalid: PropTypes.bool.isRequired,
    defaultValue: PropTypes.string,
    className: PropTypes.string,
    onChange: PropTypes.func,
};

export default DateTimePicker;
