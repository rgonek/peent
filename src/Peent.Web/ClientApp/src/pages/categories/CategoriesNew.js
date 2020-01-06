import React from 'react';
import { connect } from 'react-redux';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect } from 'react-router-dom';
import { useForm } from 'react-hook-form'

function CategoriesNew(props) {
    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000)
      });
    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
        defaultValues: { name: '', description: '' }
    });
    const onSubmit = (values, actions) => {
        props.onSubmitCategory(values);
    };

    if(props.added)
        return (<Redirect to="/categories" />);
    
    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Category</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        isInvalid={!!errors.name}
                        ref={register} />
                    <Form.Control.Feedback type="invalid">
                        {errors.name && errors.name.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        type="text"
                        name="description"
                        isInvalid={!!errors.description}
                        ref={register} />
                    <Form.Control.Feedback type="invalid">
                        {errors.description && errors.description.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit" variant="primary" disabled={props.loading}>Submit</Button>
            </Form>
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