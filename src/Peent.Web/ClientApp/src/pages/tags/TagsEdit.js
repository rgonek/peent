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

function TagsEdit({ tag, loading, onSubmitTag, onFetchTag }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchTag(id);
    }, [id, onFetchTag]);

    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
    });
    const onSubmit = (data) => {
        onSubmitTag(id, data);
    };

    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
    });

    if (tag == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Edit Tag</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        defaultValue={tag.name}
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
                        defaultValue={tag.description}
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

TagsEdit.propTypes = {
    tag: PropTypes.object,
    loading: PropTypes.bool,
    onSubmitTag: PropTypes.func,
    onFetchTag: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        tag: state.tag.tag,
        loading: state.tag.loading,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitTag: (id, tagData) => dispatch(actions.updateTag(id, tagData)),
        onFetchTag: (id) => dispatch(actions.fetchTag(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(TagsEdit);
