import React from 'react'
import BootstrapTable from 'react-bootstrap/Table'
import Form from 'react-bootstrap/Form'
import ReactPaginate from 'react-paginate';
import { useTable, usePagination, useSortBy } from 'react-table'
import Spinner from '../Spinner/Spinner'
import './Table.css';

function Table({
    columns,
    data,
    fetchData,
    loading,
    pageCount: controlledPageCount,
    rowCount
  }) {
    const {
      getTableProps,
      getTableBodyProps,
      headerGroups,
      prepareRow,
      page,
      canPreviousPage,
      canNextPage,
      pageOptions,
      pageCount,
      gotoPage,
      nextPage,
      previousPage,
      setPageSize,
      rows,
      state: { pageIndex, pageSize, sortBy },
    } = useTable(
      {
        columns,
        data,
        initialState: { pageIndex: 0 },
        manualPagination: true,
        pageCount: controlledPageCount,
        manualSorting: true
      },
      useSortBy,
      usePagination
    )
  
    React.useEffect(() => {
      console.log('fetch data');
      console.log(pageIndex + 1);
      console.log(sortBy);
      fetchData(pageIndex + 1, pageSize)
    }, [fetchData, pageIndex, pageSize, sortBy]);

    const handlePageClick = data => {
      gotoPage(data.selected);
    };

    const renderColumn = column => {
      const classes = ['sortable'];
      if(column.isSorted)
        if(column.isSortedDesc)
          classes.push('desc');
        else
          classes.push('asc');
      return (<th className={classes.join(' ')} {...column.getHeaderProps(column.getSortByToggleProps())}>{column.render('Header')}</th>);
    };

    const headers = headerGroups.map(headerGroup => (
      <tr {...headerGroup.getHeaderGroupProps()}>
        {headerGroup.headers.map(column => renderColumn(column))}
      </tr>
    ));
  
    return (
      <>
        <BootstrapTable striped bordered hover {...getTableProps()}>
          <thead>
            {headers}
          </thead>
          <tbody {...getTableBodyProps()}>
            {page.map((row, i) => {
              prepareRow(row)
              return (
                <tr {...row.getRowProps()}>
                  {row.cells.map(cell => {
                    return <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                  })}
                </tr>
              )
            })}
          </tbody>
        </BootstrapTable>
        <div className="table-footer form-inline">
          <ReactPaginate
            previousLabel={'previous'}
            nextLabel={'next'}
            breakLabel={'...'}
            pageCount={controlledPageCount}
            marginPagesDisplayed={2}
            pageRangeDisplayed={5}
            onPageChange={handlePageClick}
            containerClassName={'pagination'}
            pageClassName={'page-item'}
            nextClassName={'page-item'}
            previousClassName={'page-item'}
            breakClassName={'page-item'}
            pageLinkClassName={'page-link'}
            nextLinkClassName={'page-link'}
            previousLinkClassName={'page-link'}
            breakLinkClassName={'page-link'}
            activeClassName={'active'}
          />
          {loading ? ('') : (
              <div>Showing {page.length} of {rowCount}{' '}
              results</div>
          )}
          <Form.Control as="select"
            value={pageSize}
            onChange={e => {
              setPageSize(Number(e.target.value))
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