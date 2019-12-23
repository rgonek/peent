import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Formik } from 'formik';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect, useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function TagsDelete(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchTag(id);
    }, [id]);

    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onDeleteTag(id, values);
        actions.setSubmitting(false);
    };

    if(props.tag == null || props.loading) {
        return <Spinner />
    }

    if(props.deleted)
        return (<Redirect to="/tags" />);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Delete Tag</h1>
            </ContentHeader>
            <Formik
                initialValues={props.tag}
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
      tag: state.tag.tag,
      loading: state.tag.loading,
      deleted: state.tag.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onDeleteTag: (id, tagData) => dispatch(actions.deleteTag(id, tagData)),
    onFetchTag: (id) => dispatch( actions.fetchTag(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(TagsDelete);