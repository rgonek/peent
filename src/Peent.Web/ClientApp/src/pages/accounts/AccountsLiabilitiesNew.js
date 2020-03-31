import React from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import AccountsNew from "./AccountsNew";
import { AccountType } from "../../shared/constants";
import PropTypes from "prop-types";

function AccountsLiabilitiesNew({
    added,
    loading,
    currencies,
    onSubmitAccount,
    onFetchCurrencies,
}) {
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        onSubmitAccount({
            ...values,
            type: AccountType.liabilities,
            currencyId: parseInt(values.currencyId),
        });
        actions.setSubmitting(false);
    };

    return (
        <AccountsNew
            url="/accounts/liabilities"
            added={added}
            handleSubmit={handleSubmit}
            onFetchCurrencies={onFetchCurrencies}
            currencies={currencies}
            loading={loading}
        />
    );
}

AccountsLiabilitiesNew.propTypes = {
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

export default connect(mapStateToProps, mapDispatchToProps)(AccountsLiabilitiesNew);
