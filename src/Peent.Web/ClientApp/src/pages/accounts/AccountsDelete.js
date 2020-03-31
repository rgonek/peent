import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect, useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import { useForm } from "react-hook-form";
import PropTypes from "prop-types";

function AccountsDelete({ account, loading, deleted, onDeleteAccount, onFetchAccount }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchAccount(id);
    }, [id]);

    const onSubmit = (data) => {
        onDeleteAccount(id, data);
    };

    const { handleSubmit } = useForm();

    if (account == null || loading) {
        return <Spinner />;
    }

    if (deleted) return <Redirect to="/accounts" />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Delete Account</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Button type="submit" variant="danger" disabled={loading}>
                    Delete
                </Button>
            </Form>
        </div>
    );
}

AccountsDelete.propTypes = {
    account: PropTypes.object,
    loading: PropTypes.bool,
    deleted: PropTypes.bool,
    onDeleteAccount: PropTypes.func,
    onFetchAccount: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        account: state.account.account,
        loading: state.account.loading,
        deleted: state.account.submitted,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onDeleteAccount: (id, accountData) => dispatch(actions.deleteAccount(id, accountData)),
        onFetchAccount: (id) => dispatch(actions.fetchAccount(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountsDelete);
