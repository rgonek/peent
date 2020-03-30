import React from "react";
import BootstrapTable from "react-bootstrap/Table";
import Form from "react-bootstrap/Form";
import { useTable, usePagination, useSortBy, useFilters } from "react-table";
import Spinner from "../Spinner/Spinner";
import Pagination from "../Pagination/Pagination";
import { defaultColumnFilter } from "./Filters";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import InputGroup from "react-bootstrap/InputGroup";
import { FaSearch } from "react-icons/fa";
import "./Table.css";
import "../../../shared/extensions";
import * as constants from "../../../shared/constants";

function Table({
    title,
    columns,
    data,
    fetchData,
    loading,
    pageCount: controlledPageCount,
    rowCount,
    onRowClick,
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
            manualSortBy: true,
            manualFilters: true,
            defaultColumn,
        },
        useFilters,
        useSortBy,
        usePagination
    );

    const [globalFilterValue, setGlobalFilterValue] = React.useState("");

    React.useEffect(() => {
        if (pageIndex === 0) {
            gotoPage(1);
        } else {
            fetchData(
                pageIndex,
                pageSize,
                sortBy,
                filters.addFilter(constants.GLOBAL_FILTER, globalFilterValue)
            );
        }
    }, [fetchData, pageIndex, pageSize, sortBy, filters]);

    const handlePageClick = (pageNumber) => {
        gotoPage(pageNumber);
    };

    const handleSearchChange = (e) => {
        setGlobalFilterValue(e.target.value);
        fetchData(
            pageIndex,
            pageSize,
            sortBy,
            filters.addFilter(constants.GLOBAL_FILTER, e.target.value)
        );
    };

    const renderColumn = (column) => {
        const classes = [];
        if (column.canSort) {
            classes.push("sortable");
            if (column.isSorted)
                if (column.isSortedDesc) classes.push("desc");
                else classes.push("asc");
        }
        return (
            <th
                className={classes.join(" ")}
                {...column.getHeaderProps(column.getSortByToggleProps())}
            >
                {column.render("Header")}
            </th>
        );
    };

    const renderFilterRow = (headerGroup) => {
        if (headerGroup.headers.some((x) => x.canFilter) === false) return null;

        return (
            <tr className="row-filter" key={"filter-" + headerGroup.id}>
                {headerGroup.headers.map((column) => (
                    <th key={column.id + "-filter"}>
                        {column.canFilter ? column.render("Filter") : null}
                    </th>
                ))}
            </tr>
        );
    };

    const headers = headerGroups.map((headerGroup) => [
        <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => renderColumn(column))}
        </tr>,
        renderFilterRow(headerGroup),
    ]);

    return (
        <Card className="card-table">
            <Card.Header>
                <Row>
                    <Col xs={10}>{title}</Col>
                    <Col>
                        <InputGroup size="sm">
                            <InputGroup.Prepend>
                                <InputGroup.Text id="inputGroupPrepend">
                                    <FaSearch />
                                </InputGroup.Text>
                            </InputGroup.Prepend>
                            <Form.Control
                                type="text"
                                placeholder="Search..."
                                aria-describedby="inputGroupPrepend"
                                onChange={handleSearchChange}
                            />
                        </InputGroup>
                    </Col>
                </Row>
            </Card.Header>
            <Card.Body>
                <BootstrapTable striped bordered hover {...getTableProps()}>
                    <thead>{headers}</thead>
                    <tbody {...getTableBodyProps()}>
                        {page.map((row) => {
                            prepareRow(row);
                            let rowProps = null;
                            if (onRowClick) {
                                rowProps = {
                                    style: { cursor: "pointer" },
                                    onClick: () => onRowClick(row.original),
                                };
                            }

                            return (
                                <tr {...row.getRowProps()} {...rowProps}>
                                    {row.cells.map((cell) => {
                                        return (
                                            <td {...cell.getCellProps()}>
                                                {cell.value ? cell.render("Cell") : null}
                                            </td>
                                        );
                                    })}
                                </tr>
                            );
                        })}
                    </tbody>
                </BootstrapTable>
            </Card.Body>
            <Card.Footer>
                <Row>
                    <Col md="auto">
                        <Form.Control
                            as="select"
                            value={pageSize}
                            onChange={(e) => {
                                setPageSize(Number(e.target.value));
                                gotoPage(1);
                            }}
                        >
                            {[1, 2, 5, 10, 20, 30, 40, 50].map((pageSize) => (
                                <option key={pageSize} value={pageSize}>
                                    Show {pageSize}
                                </option>
                            ))}
                        </Form.Control>
                    </Col>
                    <Col>
                        {loading ? (
                            <Spinner />
                        ) : (
                            <div>
                                Showing {page.length} of {rowCount} results
                            </div>
                        )}
                    </Col>
                    <Col>
                        <Pagination
                            pageIndex={pageIndex}
                            pageCount={controlledPageCount}
                            onPageChange={handlePageClick}
                        />
                    </Col>
                </Row>
            </Card.Footer>
        </Card>
    );
}

export default Table;
