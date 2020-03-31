import React, { useCallback } from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import Accounts from "./Accounts";
import { AccountType } from "../../shared/constants";
import "../../shared/extensions";
import PropTypes from "prop-types";

function AccountsExpense({ accounts, loading, pageCount, rowCount, onFetchAccounts }) {
    const fetchData = useCallback(
        (pageIndex, pageSize, sortBy, filters) => {
            onFetchAccounts(
                pageIndex,
                pageSize,
                sortBy,
                filters.addFilter("type", AccountType.expense)
            );
        },
        [onFetchAccounts]
    );

    return (
        <Accounts
            title="Expense accounts"
            url="/accounts/expense/"
            loading={loading}
            pageCount={pageCount}
            rowCount={rowCount}
            accounts={accounts}
            fetchData={fetchData}
        />
    );
}

AccountsExpense.propTypes = {
    accounts: PropTypes.array,
    loading: PropTypes.bool,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    onFetchAccounts: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        accounts: state.account.accounts,
        loading: state.account.loading,
        pageCount: state.account.pageCount,
        rowCount: state.account.rowCount,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchAccounts: (pageIndex, pageSize, sortBy, filters) =>
            dispatch(actions.fetchAccounts(pageIndex, pageSize, sortBy, filters)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountsExpense);
