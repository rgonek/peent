import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../store/actions/index';
import AccountsNew from './AccountsNew';

function AccountsExpenseNew(props) {
    const handleSubmit = (values, actions) => {
        actions.setSubmitting(true);
        props.onSubmitAccount({...values, type: 3, currencyId: parseInt(values.currencyId)});
        actions.setSubmitting(false);
    };
    
    return (<AccountsNew 
        url="/accounts/revenue"
        added={props.added}
        handleSubmit={handleSubmit}
        onFetchCurrencies={props.onFetchCurrencies}
        currencies={props.currencies}
        loading={props.loading}
    />);
}

const mapStateToProps = state => {
    return {
      added: state.account.submitted,
      loading: state.currency.loading,
      currencies: state.currency.currencies
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitAccount: (accountData) =>
      dispatch(actions.addAccount(accountData)),
    onFetchCurrencies: () => dispatch(actions.fetchCurrencies())
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountsExpenseNew);