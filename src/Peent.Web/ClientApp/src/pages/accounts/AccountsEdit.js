import React, { useEffect } from "react";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import { useForm } from "react-hook-form";

function AccountsEdit(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchAccount(id);
        props.onFetchCurrencies();
    }, [id]);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        currencyId: yup.number().positive().required().integer(),
    });
    const onSubmit = (data) => {
        props.onSubmitAccount(id, { ...data, currencyId: parseInt(data.currencyId) });
    };

    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
    });

    if (props.account == null || props.currencies == null || props.loading) return <Spinner />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Edit Account</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        defaultValue={props.account.name}
                        isInvalid={!!errors.name}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.name && errors.name.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        type="text"
                        name="description"
                        defaultValue={props.account.description}
                        isInvalid={!!errors.description}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.description && errors.description.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Currency</Form.Label>
                    <Form.Control
                        as="select"
                        name="currencyId"
                        defaultValue={props.account.currencyId}
                        isInvalid={!!errors.currencyId}
                        ref={register}
                    >
                        <option />
                        {props.currencies.map((option) => (
                            <option key={option.id} value={option.id}>
                                {option.name} ({option.symbol})
                            </option>
                        ))}
                    </Form.Control>
                    <Form.Control.Feedback type="invalid">
                        {errors.currencyId && errors.currencyId.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit" variant="primary" disabled={props.loading}>
                    Submit
                </Button>
            </Form>
        </div>
    );
}

const mapStateToProps = (state) => {
    return {
        account: state.account.account,
        loading: state.account.loading || state.currency.loading,
        currencies: state.currency.currencies,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitAccount: (id, accountData) => dispatch(actions.updateAccount(id, accountData)),
        onFetchAccount: (id) => dispatch(actions.fetchAccount(id)),
        onFetchCurrencies: () => dispatch(actions.fetchCurrencies()),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountsEdit);
