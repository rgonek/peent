import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import PropTypes from "prop-types";

function AccountDetails({ account, loading, onFetchAccount }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchAccount(id);
    }, [id]);

    if (account == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Account details</h1>
            </ContentHeader>
            {account.name}
        </div>
    );
}

AccountDetails.propTypes = {
    account: PropTypes.object,
    loading: PropTypes.bool,
    onFetchAccount: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        account: state.account.account,
        loading: state.account.loading,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchAccount: (id) => dispatch(actions.fetchAccount(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountDetails);
