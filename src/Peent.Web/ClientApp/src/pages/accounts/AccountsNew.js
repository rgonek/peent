import React from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect } from 'react-router-dom';

function AccountsNew(props) {
    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        date: yup.date()
      });
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onSubmitAccount(values);
        actions.setSubmitting(false);
    };

    if(props.added)
        return (<Redirect to="/accounts" />);
    
    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Account</h1>
            </ContentHeader>
            <Formik
                initialValues={{ name: '', description: '', date: '' }}
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
                        <Form.Label>Date</Form.Label>
                        <Form.Control
                            type="date"
                            name="date"
                            value={values.date ? values.date : ''}
                            onChange={handleChange}
                            isInvalid={!!errors.date} />
                        <Form.Control.Feedback type="invalid">
                            {errors.date}
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
      accounts: state.account.accounts,
      loading: state.account.loading,
      added: state.account.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitAccount: (accountData) =>
      dispatch(actions.addAccount(accountData))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountsNew);