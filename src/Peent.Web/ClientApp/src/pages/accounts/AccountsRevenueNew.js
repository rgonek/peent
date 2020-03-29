import React from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import AccountsNew from "./AccountsNew";
import { AccountType } from "../../shared/constants";

function AccountsExpenseNew(props) {
    const onSubmit = (data) => {
        props.onSubmitAccount({
            ...data,
            type: AccountType.revenue,
            currencyId: parseInt(data.currencyId),
        });
    };

    return (
        <AccountsNew
            url="/accounts/revenue"
            added={props.added}
            onSubmit={onSubmit}
            onFetchCurrencies={props.onFetchCurrencies}
            currencies={props.currencies}
            loading={props.loading}
        />
    );
}

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
