import React, { useMemo, useState, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from '../../store/actions/index';
import ContentHeader from '../../components/ContentHeader';
import { LinkContainer } from 'react-router-bootstrap';
import { ButtonToolbar, Button } from 'react-bootstrap';
import withErrorHandler from '../../hoc/withErrorHandler/withErrorHandler';
import Table from '../../components/UI/Table/Table'
import axios from '../../axios-peent';

function Tags(props) {
  const columns = useMemo(
    () => [
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
        sortable: true
      }
    ],
    []
  );

  const fetchData = useCallback((pageIndex, pageSize, sortBy) => {
    props.onFetchTags(pageIndex, pageSize, sortBy);
  }, []);

  return (
    <div>
      <ContentHeader>
        <h1 className="h2">Tags</h1>
        <ButtonToolbar>
          <LinkContainer to='/tags/new'>
            <Button variant="outline-secondary" size="sm">Add tag</Button>
          </LinkContainer>
        </ButtonToolbar>
      </ContentHeader>
      <Table
        columns={columns}
        data={props.tags}
        fetchData={fetchData}
        loading={props.loading}
        pageCount={props.pageCount}
        rowCount={props.rowCount}
      />
    </div>
  );
}

const mapStateToProps = state => {
  return {
    tags: state.tag.tags,
    loading: state.tag.loading,
    pageCount: state.tag.pageCount,
    rowCount: state.tag.rowCount
  };
};

const mapDispatchToProps = dispatch => {
  return {
    onFetchTags: (pageIndex, pageSize, sortBy) =>
      dispatch(actions.fetchTags(pageIndex, pageSize, sortBy))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(withErrorHandler(Tags, axios));