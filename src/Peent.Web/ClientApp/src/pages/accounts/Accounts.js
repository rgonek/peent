import React, { useMemo, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from '../../store/actions/index';
import ContentHeader from '../../components/ContentHeader';
import { LinkContainer } from 'react-router-bootstrap';
import { ButtonToolbar, Button } from 'react-bootstrap';
import Table from '../../components/UI/Table/Table'
import { useHistory } from "react-router-dom";
import Card from 'react-bootstrap/Card'

function Accounts(props) {
  let history = useHistory();
  const onEditClick = (e, id) => {
    history.push('accounts/' + id);
    e.stopPropagation();
  };
  const onDeleteClick = (e, id) => {
    history.push('accounts/' + id + '/delete');
    e.stopPropagation();
  };
  const columns = useMemo(
    () => [
      {
        Header: '',
        accessor: 'id',
        id: '_actions',
        sortable: false,
        disableFilters: true,
        Cell: ({ cell: { value } }) => {
          return (<>
            <Button variant="primary" size="sm" onClick={e => onEditClick(e, value)}>Edit</Button>{" "}
            <Button variant="danger" size="sm" onClick={e => onDeleteClick(e, value)}>Delete</Button>
          </>)
        }
      },
      {
        Header: 'Name',
        accessor: 'name',
        sortable: true
      },
      {
        Header: 'Description',
        accessor: 'description',
        sortable: true
      },
      {
        Header: 'Date',
        accessor: 'date',
        sortable: true,
        disableFilters: true
      }
    ],
    []
  );

  const fetchData = useCallback((pageIndex, pageSize, sortBy, filters) => {
    props.onFetchAccounts(pageIndex, pageSize, sortBy, filters);
  }, []);

    return (
    <div>
      <ContentHeader>
        <h1 className="h2">Accounts</h1>
        <ButtonToolbar>
          <LinkContainer to='/accounts/new'>
            <Button variant="outline-secondary" size="sm">Add account</Button>
          </LinkContainer>
        </ButtonToolbar>
      </ContentHeader>
      <Table
        title='Accounts'
        columns={columns}
        data={props.accounts}
        fetchData={fetchData}
        loading={props.loading}
        pageCount={props.pageCount}
        rowCount={props.rowCount}
        onRowClick={data => {
          history.push('accounts/' + data.id + '/details');
        }}
      />
    </div>
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
  )(Accounts);