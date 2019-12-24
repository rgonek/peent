import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect, useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function AccountsDelete(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchAccount(id);
    }, [id]);

    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onDeleteAccount(id, values);
        actions.setSubmitting(false);
    };

    if(props.account == null || props.loading) {
        return <Spinner />
    }

    if(props.deleted)
        return (<Redirect to="/accounts" />);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Delete Account</h1>
            </ContentHeader>
            <Formik
                initialValues={props.account}
                onSubmit={handleSubmit}>
                {({
                    handleSubmit,
                    isSubmitting
                })=> (
                <Form noValidate onSubmit={handleSubmit}>
                    <Button type="submit" variant="danger" disabled={isSubmitting}>Delete</Button>
                </Form>
                )}
            </Formik>
        </div>
    );
}

const mapStateToProps = state => {
    return {
      account: state.account.account,
      loading: state.account.loading,
      deleted: state.account.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onDeleteAccount: (id, accountData) => dispatch(actions.deleteAccount(id, accountData)),
    onFetchAccount: (id) => dispatch( actions.fetchAccount(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountsDelete);