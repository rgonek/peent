import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect, useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import { useForm } from "react-hook-form";
import PropTypes from "prop-types";

function CategoriesDelete({ category, loading, deleted, onDeleteCategory, onFetchCategory }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchCategory(id);
    }, [id]);

    const onSubmit = (data) => {
        onDeleteCategory(id, data);
    };

    const { handleSubmit } = useForm();

    if (category == null || loading) {
        return <Spinner />;
    }

    if (deleted) return <Redirect to="/categories" />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Delete Category</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Button type="submit" variant="danger" disabled={loading}>
                    Delete
                </Button>
            </Form>
        </div>
    );
}

CategoriesDelete.propTypes = {
    category: PropTypes.object,
    loading: PropTypes.bool,
    deleted: PropTypes.bool,
    onDeleteCategory: PropTypes.func,
    onFetchCategory: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        category: state.category.category,
        loading: state.category.loading,
        deleted: state.category.submitted,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onDeleteCategory: (id, categoryData) => dispatch(actions.deleteCategory(id, categoryData)),
        onFetchCategory: (id) => dispatch(actions.fetchCategory(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoriesDelete);
