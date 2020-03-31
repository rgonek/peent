import React from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import AccountsNew from "./AccountsNew";
import { AccountType } from "../../shared/constants";
import PropTypes from "prop-types";

function AccountsExpenseNew({ added, loading, currencies, onSubmitAccount, onFetchCurrencies }) {
    const onSubmit = (data) => {
        onSubmitAccount({
            ...data,
            type: AccountType.expense,
            currencyId: parseInt(data.currencyId),
        });
    };

    return (
        <AccountsNew
            url="/accounts/expense"
            added={added}
            onSubmit={onSubmit}
            onFetchCurrencies={onFetchCurrencies}
            currencies={currencies}
            loading={loading}
        />
    );
}

AccountsExpenseNew.propTypes = {
    added: PropTypes.bool,
    loading: PropTypes.bool,
    currencies: PropTypes.array,
    onSubmitAccount: PropTypes.func,
    onFetchCurrencies: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        added: state.account.submitted,
        loading: state.currency.loading,
        currencies: state.currency.currencies,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitAccount: (accountData) => dispatch(actions.addAccount(accountData)),
        onFetchCurrencies: () => dispatch(actions.fetchCurrencies()),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountsExpenseNew);
