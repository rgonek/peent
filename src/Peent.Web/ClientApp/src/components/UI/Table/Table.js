import React from 'react'
import BootstrapTable from 'react-bootstrap/Table'
import Form from 'react-bootstrap/Form'
import { useTable, usePagination, useSortBy, useFilters } from 'react-table'
import Spinner from '../Spinner/Spinner'
import Pagination from '../Pagination/Pagination'
import { defaultColumnFilter } from './Filters'
import './Table.css';

function Table({
    columns,
    data,
    fetchData,
    loading,
    pageCount: controlledPageCount,
    rowCount,
    onRowClick
  }) {
    const defaultColumn = React.useMemo(
      () => ({
        Filter: defaultColumnFilter,
      }),
      []
    );

    const {
      getTableProps,
      getTableBodyProps,
      headerGroups,
      prepareRow,
      page,
      gotoPage,
      setPageSize,
      state: { pageIndex, pageSize, sortBy, filters },
    } = useTable(
      {
        columns,
        data,
        initialState: { pageIndex: 1 },
        manualPagination: true,
        pageCount: controlledPageCount + 1,
        manualSorting: true,
        manualFilters: true,
        defaultColumn
      },
      useFilters,
      useSortBy,
      usePagination
    )
  
    React.useEffect(() => {
      if(pageIndex === 0) {
        gotoPage(1);
      }
      else {
        fetchData(pageIndex, pageSize, sortBy, filters);
      }
    }, [fetchData, pageIndex, pageSize]);

    const handlePageClick = pageNumber => {
      gotoPage(pageNumber);
    };

    const renderColumn = column => {
      const classes = [];
      if (column.canSort) {
        classes.push('sortable');
        if(column.isSorted)
          if(column.isSortedDesc)
            classes.push('desc');
          else
            classes.push('asc');
      }
      return (<th className={classes.join(' ')} {...column.getHeaderProps(column.getSortByToggleProps())}>{column.render('Header')}</th>);
    };
    
    const renderFilterRow = headerGroup => {
      if (headerGroup.headers.some(x => x.canFilter) === false)
        return null;

      return (
        <tr className="row-filter" key={"filter-" + headerGroup.id}>
          {headerGroup.headers.map(column => <th key={column.id + "-filter"}>{column.canFilter ? column.render('Filter') : null}</th>)}
        </tr>)
    }

    const headers = headerGroups.map(headerGroup => [
      (
        <tr {...headerGroup.getHeaderGroupProps()}>
          {headerGroup.headers.map(column => renderColumn(column))}
        </tr>
      ),
      renderFilterRow(headerGroup)
    ]);
  
    return (
      <>
        <BootstrapTable striped bordered hover {...getTableProps()}>
          <thead>
            {headers}
          </thead>
          <tbody {...getTableBodyProps()}>
            {page.map((row, i) => {
              prepareRow(row);
              let rowProps = null;
              if (onRowClick) {
                rowProps = {
                  style: {'cursor':'pointer'},
                  onClick: () => onRowClick(row.original)
                }
              }

              return (
                <tr {...row.getRowProps()} {...rowProps}>
                  {row.cells.map(cell => {
                    return <td {...cell.getCellProps()}>{cell.value ? cell.render('Cell') : null}</td>
                  })}
                </tr>
              )
            })}
          </tbody>
        </BootstrapTable>
        <div className="table-footer form-inline">
          <Pagination
            pageIndex={pageIndex}
            pageCount={controlledPageCount}
            onPageChange={handlePageClick} />
          {loading ? ('') : (
              <div>Showing {page.length} of {rowCount}{' '}
              results</div>
          )}
          <Form.Control as="select"
            value={pageSize}
            onChange={e => {
              setPageSize(Number(e.target.value));
              gotoPage(1);
            }}
          >
            {[1,2,5,10, 20, 30, 40, 50].map(pageSize => (
              <option key={pageSize} value={pageSize}>
                Show {pageSize}
              </option>
            ))}
          </Form.Control>
          {loading ? (<Spinner />) : ('')}
        </div>
      </>
    )
}
  
export default Table;