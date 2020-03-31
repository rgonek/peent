import React, { useEffect } from "react";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import { useForm } from "react-hook-form";
import PropTypes from "prop-types";

function CategoriesEdit({ category, loading, onSubmitCategory, onFetchCategory }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchCategory(id);
    }, [id]);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
    });
    const onSubmit = (data) => {
        onSubmitCategory(id, data);
    };

    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
    });

    if (category == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Edit Category</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        defaultValue={category.name}
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
                        defaultValue={category.description}
                        isInvalid={!!errors.description}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.description && errors.description.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit" variant="primary" disabled={loading}>
                    Submit
                </Button>
            </Form>
        </div>
    );
}

CategoriesEdit.propTypes = {
    category: PropTypes.object,
    loading: PropTypes.bool,
    onSubmitCategory: PropTypes.func,
    onFetchCategory: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        category: state.category.category,
        loading: state.category.loading,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitCategory: (id, categoryData) => dispatch(actions.updateCategory(id, categoryData)),
        onFetchCategory: (id) => dispatch(actions.fetchCategory(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoriesEdit);
