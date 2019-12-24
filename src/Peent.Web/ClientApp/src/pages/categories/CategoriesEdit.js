import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function CategoriesEdit(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchCategory(id);
    }, [id]);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000)
      });
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onSubmitCategory(id, values);
        actions.setSubmitting(false);
    };

    if(props.category == null || props.loading) {
        return <Spinner />
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Edit Category</h1>
            </ContentHeader>
            <Formik
                initialValues={props.category}
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
                    <Button type="submit" variant="primary" disabled={isSubmitting}>Submit</Button>
                </Form>
                )}
            </Formik>
        </div>
    );
}

const mapStateToProps = state => {
    return {
      category: state.category.category,
      loading: state.category.loading
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitCategory: (id, categoryData) => dispatch(actions.updateCategory(id, categoryData)),
    onFetchCategory: (id) => dispatch( actions.fetchCategory(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(CategoriesEdit);