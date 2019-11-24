import React from 'react';
import { Form } from 'react-bootstrap';

const input = ( props ) => {
    let inputElement = null;
    const inputClasses = [classes.InputElement];

    if (props.invalid && props.shouldValidate && props.touched) {
        inputClasses.push(classes.Invalid);
    }

    switch ( props.elementType ) {
        case ( 'textarea' ):
            inputElement = <Form.Control
                as="textarea"
                className={inputClasses.join(' ')}
                {...props.elementConfig}
                value={props.value}
                onChange={props.changed} />;
            break;
        case ( 'select' ):
            inputElement = (
                <Form.Control
                    as="select"
                    className={inputClasses.join(' ')}
                    value={props.value}
                    onChange={props.changed}>
                    {props.elementConfig.options.map(option => (
                        <option key={option.value} value={option.value}>
                            {option.displayValue}
                        </option>
                    ))}
                </Form.Control>
            );
            break;
        case ( 'input' ):
        default:
            inputElement = <Form.Control
                className={inputClasses.join(' ')}
                {...props.elementConfig}
                value={props.value}
                onChange={props.changed} />;
    }

    return (
        <Form.Group controlId={props.key} className={classes.Input}>
            <Form.Label className={classes.Label}>{props.label}</Form.Label>
            {inputElement}
        </Form.Group>
    );

};

export default input;