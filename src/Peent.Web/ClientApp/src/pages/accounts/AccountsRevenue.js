import React, { useMemo, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from '../../store/actions/index';
import Accounts from './Accounts'
import { AccountType } from '../../shared/constants'

function AccountsRevenue(props) {
  const fetchData = useCallback((pageIndex, pageSize, sortBy, filters) => {
    filters.push({id:"type", value: AccountType.revenue});
    props.onFetchAccounts(pageIndex, pageSize, sortBy, filters);
  }, []);

  return (
    <Accounts 
      title='Revenue accounts'
      url='/accounts/revenue/'
      loading={props.loading}
      pageCount={props.pageCount}
      rowCount={props.rowCount}
      accounts={props.accounts}
      fetchData={fetchData} />
  );
}

const mapStateToProps = state => {
  return {
    accounts: state.account.accounts,
    loading: state.account.loading,
    pageCount: state.account.pageCount,
    rowCount: state.account.rowCount
  };
};

const mapDispatchToProps = dispatch => {
  return {
    onFetchAccounts: (pageIndex, pageSize, sortBy, filters) =>
      dispatch(actions.fetchAccounts(pageIndex, pageSize, sortBy, filters))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountsRevenue);