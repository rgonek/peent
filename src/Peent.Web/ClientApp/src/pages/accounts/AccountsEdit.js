import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function AccountsEdit(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchAccount(id);
        props.onFetchCurrencies();
    }, [id]);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        currencyId: yup.number().positive().required().integer()
      });
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onSubmitAccount(id, {...values, currencyId: parseInt(values.currencyId)});
        actions.setSubmitting(false);
    };

    if(props.account == null || props.currencies == null || props.loading)
        return <Spinner />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Edit Account</h1>
            </ContentHeader>
            <Formik
                initialValues={props.account}
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
                            {props.currencies.map(option => (
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

const mapStateToProps = state => {
    return {
      account: state.account.account,
      loading: state.account.loading || state.currency.loading,
      currencies: state.currency.currencies
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitAccount: (id, accountData) => dispatch(actions.updateAccount(id, accountData)),
    onFetchAccount: (id) => dispatch( actions.fetchAccount(id) ),
    onFetchCurrencies: () => dispatch(actions.fetchCurrencies())
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountsEdit);