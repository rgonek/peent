import React, { useEffect, useState } from "react";
import { FiCalendar } from "react-icons/fi";
import { Form, InputGroup, Button } from "react-bootstrap";
import Flatpickr from "react-flatpickr";
import { Controller } from "react-hook-form";
import "flatpickr/dist/themes/material_blue.css";
import "./DateTimePicker.css";

function DateTimePicker({
    control,
    name,
    isInvalid,
    value,
    defaultValue,
    className,
    onChange,
    ...restProps
}) {
    const {
        setValue,
        register,
        mode: { isOnSubmit, isOnBlur, isOnChange },
    } = control;

    const [flatpickr, setFlatpickr] = useState(null);

    useEffect(() => {
        register({ name: name });
    }, []);

    const handleChange = (selectedDates, dateStr, instance) => {
        setValue(name, dateStr, true);
        if (onChange) onChange(selectedDates, dateStr, instance);
    };
    const handleCalendarClose = (selectedDates, dateStr, instance) => {
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

    const options = {
        enableTime: false,
        time_24hr: true,
        allowInput: true,
        locale: { firstDayOfWeek: 1 },
        onClose: handleCalendarClose,
    };

    return (
        <Flatpickr
            name={name}
            value={value}
            defaultValue={defaultValue}
            render={inputRender}
            options={options}
            onChange={handleChange}
            ref={setFlatpickrRef}
        />
    );
}

export default DateTimePicker;
