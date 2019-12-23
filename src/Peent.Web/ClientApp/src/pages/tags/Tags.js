import React, { useMemo, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from '../../store/actions/index';
import ContentHeader from '../../components/ContentHeader';
import { LinkContainer } from 'react-router-bootstrap';
import { ButtonToolbar, Button } from 'react-bootstrap';
import Table from '../../components/UI/Table/Table'
import { useHistory } from "react-router-dom";
import Card from 'react-bootstrap/Card'

function Tags(props) {
  let history = useHistory();
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
        sortable: true,
        disableFilters: true
      }
    ],
    []
  );

  const fetchData = useCallback((pageIndex, pageSize, sortBy, filters) => {
    props.onFetchTags(pageIndex, pageSize, sortBy, filters);
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
        title='Tags'
        columns={columns}
        data={props.tags}
        fetchData={fetchData}
        loading={props.loading}
        pageCount={props.pageCount}
        rowCount={props.rowCount}
        onRowClick={data => {
          history.push('tags/' + data.id);
        }}
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
    onFetchTags: (pageIndex, pageSize, sortBy, filters) =>
      dispatch(actions.fetchTags(pageIndex, pageSize, sortBy, filters))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(Tags);