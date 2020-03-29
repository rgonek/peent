import React, { useEffect } from "react";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import { Redirect } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import { useForm } from "react-hook-form";

function AccountsNew({ added, url, onSubmit, onFetchCurrencies, currencies, loading }) {
    useEffect(() => {
        onFetchCurrencies();
    }, []);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        currencyId: yup.number().positive().required().integer(),
    });

    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
        defaultValues: { name: "", description: "", currencyId: 1 },
    });

    if (added) return <Redirect to={url} />;

    if (currencies == null || loading) return <Spinner />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Account</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        isInvalid={!!errors.name}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">{errors.name}</Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        type="text"
                        name="description"
                        isInvalid={!!errors.description}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.description}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Currency</Form.Label>
                    <Form.Control
                        as="select"
                        name="currencyId"
                        isInvalid={!!errors.currencyId}
                        ref={register}
                    >
                        <option />
                        {currencies.map((option) => (
                            <option key={option.id} value={option.id}>
                                {option.name} ({option.symbol})
                            </option>
                        ))}
                    </Form.Control>
                    <Form.Control.Feedback type="invalid">
                        {errors.currencyId}
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit" variant="primary" disabled={loading}>
                    Submit
                </Button>
            </Form>
        </div>
    );
}

export default AccountsNew;
