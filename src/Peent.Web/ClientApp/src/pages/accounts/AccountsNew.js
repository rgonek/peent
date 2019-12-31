import React, { useEffect } from 'react';
import { Formik } from 'formik';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import { Redirect } from 'react-router-dom';
import Spinner from '../../components/UI/Spinner/Spinner'

function AccountsNew({
    added,
    url,
    handleSubmit,
    onFetchCurrencies,
    currencies,
    loading
}) {
    
    useEffect(() => {
        onFetchCurrencies();
    }, []);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        currencyId: yup.number().positive().required().integer()
      });

    if(added)
        return <Redirect to={url} />;

    if(currencies == null || loading)
        return <Spinner />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Account</h1>
            </ContentHeader>
            <Formik
                initialValues={{ name: '', description: '', currencyId: 1 }}
                validationSchema={formSchema}
                onSubmit={handleSubmit}>
                {({
                    handleSubmit,
                    handleChange,
                    handleBlur,
                    values,
                    touched,
                    isValid,
                    errors,
                    isSubmitting
                })=> (
                <Form noValidate onSubmit={handleSubmit}>
                    <Form.Group>
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type="text"
                            name="name"
                            value={values.name ? values.name : ''}
                            onChange={handleChange}
                            isInvalid={!!errors.name} />
                        <Form.Control.Feedback type="invalid">
                            {errors.name}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            type="text"
                            name="description"
                            value={values.description ? values.description : ''}
                            onChange={handleChange}
                            isInvalid={!!errors.description} />
                        <Form.Control.Feedback type="invalid">
                            {errors.description}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Currency</Form.Label>
                        <Form.Control
                            as="select"
                            name="currencyId"
                            value={values.currencyId}
                            onChange={handleChange}
                            isInvalid={!!errors.currencyId}>
                            <option />
                            {currencies.map(option => (
                                <option key={option.id} value={option.id}>
                                    {option.name} ({option.symbol})
                                </option>
                            ))}
                        </Form.Control>
                        <Form.Control.Feedback type="invalid">
                            {errors.currencyId}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Button type="submit" variant="primary" disabled={isSubmitting}>Submit</Button>
                </Form>
                )}
            </Formik>
        </div>
    );
}

export default AccountsNew;