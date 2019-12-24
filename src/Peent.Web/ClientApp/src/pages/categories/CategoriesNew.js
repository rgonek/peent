import React from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect } from 'react-router-dom';

function CategoriesNew(props) {
    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000)
      });
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onSubmitCategory(values);
        actions.setSubmitting(false);
    };

    if(props.added)
        return (<Redirect to="/categories" />);
    
    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Category</h1>
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
                    <Button type="submit" variant="primary" disabled={isSubmitting}>Submit</Button>
                </Form>
                )}
            </Formik>
        </div>
    );
}

const mapStateToProps = state => {
    return {
      categories: state.category.categories,
      loading: state.category.loading,
      added: state.category.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitCategory: (categoryData) =>
      dispatch(actions.addCategory(categoryData))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(CategoriesNew);