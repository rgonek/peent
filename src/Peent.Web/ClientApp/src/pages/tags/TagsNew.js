import React from "react";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect } from "react-router-dom";
import { useForm } from "react-hook-form";

function TagsNew(props) {
    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        date: yup
            .date()
            .nullable()
            .transform((cv, ov) => {
                return ov === "" ? undefined : cv;
            }),
    });
    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
        defaultValues: { name: "", description: "", date: "" },
    });
    const onSubmit = (data) => {
        props.onSubmitTag(data);
    };

    if (props.added) return <Redirect to="/tags" />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Tag</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
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
                        isInvalid={!!errors.description}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.description && errors.description.message}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Date</Form.Label>
                    <Form.Control
                        type="date"
                        name="date"
                        isInvalid={!!errors.date}
                        ref={register}
                    />
                    <Form.Control.Feedback type="invalid">
                        {errors.date && errors.date.message}
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
        loading: state.tag.loading,
        added: state.tag.submitted,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitTag: (tagData) => dispatch(actions.addTag(tagData)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(TagsNew);
