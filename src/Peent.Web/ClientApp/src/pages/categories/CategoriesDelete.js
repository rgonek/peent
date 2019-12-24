import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect, useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function CategoriesDelete(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchCategory(id);
    }, [id]);

    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onDeleteCategory(id, values);
        actions.setSubmitting(false);
    };

    if(props.category == null || props.loading) {
        return <Spinner />
    }

    if(props.deleted)
        return (<Redirect to="/categories" />);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Delete Category</h1>
            </ContentHeader>
            <Formik
                initialValues={props.category}
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
      category: state.category.category,
      loading: state.category.loading,
      deleted: state.category.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onDeleteCategory: (id, categoryData) => dispatch(actions.deleteCategory(id, categoryData)),
    onFetchCategory: (id) => dispatch( actions.fetchCategory(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(CategoriesDelete);