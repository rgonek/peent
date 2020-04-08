import React from "react";
import BootstrapTable from "react-bootstrap/Table";
import Form from "react-bootstrap/Form";
import { useTable, usePagination, useSortBy, useFilters } from "react-table";
import Spinner from "../Spinner/Spinner";
import Pagination from "../Pagination/Pagination";
import { DefaultColumnFilter } from "./Filters";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import InputGroup from "react-bootstrap/InputGroup";
import { FaSearch } from "react-icons/fa";
import PropTypes from "prop-types";
import { useRouteMatch, useHistory } from "react-router-dom";
import * as constants from "../../../shared/constants";
import { getQueryState, buildSearchQuery } from "./utility";
import "./Table.css";
import "../../../shared/extensions";

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
            Filter: DefaultColumnFilter,
        }),
        []
    );

    const { url } = useRouteMatch();
    const history = useHistory();
    const query = React.useMemo(() => new URLSearchParams(history.location.search), [history]);
    const {
        pageIndex: initialPageIndex,
        pageSize: InitialPageSize,
        sortBy: initialSortBy,
        filters: initialFilters,
    } = getQueryState(query, columns);

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
            initialState: {
                pageIndex: initialPageIndex,
                pageSize: InitialPageSize,
                sortBy: initialSortBy,
                filters: initialFilters,
            },
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

    const [globalFilterValue, setGlobalFilterValue] = React.useState(
        query.get(constants.QUERY_PARAMETER_GLOBAL_FILTER) ?? ""
    );

    React.useEffect(() => {
        if (pageIndex === 0) {
            gotoPage(1);
        } else {
            const allFilters = filters.addFilter(
                constants.QUERY_PARAMETER_GLOBAL_FILTER,
                globalFilterValue
            );
            const search = buildSearchQuery(
                query,
                columns,
                pageIndex,
                pageSize,
                sortBy,
                allFilters
            );
            history.push(url + search);

            fetchData(pageIndex, pageSize, sortBy, allFilters);
        }
    }, [
        fetchData,
        pageIndex,
        pageSize,
        sortBy,
        filters,
        globalFilterValue,
        gotoPage,
        history,
        url,
        columns,
        query,
    ]);

    const handlePageClick = (pageNumber) => {
        gotoPage(pageNumber);
    };

    const handleSearchChange = (e) => {
        setGlobalFilterValue(e.target.value);
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

    const headers = headerGroups.map((headerGroup) => {
        const { key, role, ...props } = headerGroup.getHeaderGroupProps();
        return [
            <tr key={key} role={role} {...props}>
                {headerGroup.headers.map((column) => renderColumn(column))}
            </tr>,
            renderFilterRow(headerGroup),
        ];
    });

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
                                defaultValue={globalFilterValue}
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
                            let customRowProps = null;
                            if (onRowClick) {
                                customRowProps = {
                                    style: { cursor: "pointer" },
                                    onClick: () => onRowClick(row.original),
                                };
                            }

                            const { rowKey, rowRole, ...rowProps } = row.getRowProps(
                                customRowProps
                            );
                            return (
                                <tr key={rowKey} role={rowRole} {...rowProps}>
                                    {row.cells.map((cell) => {
                                        const {
                                            cellKey,
                                            cellRole,
                                            ...cellProps
                                        } = cell.getCellProps();
                                        return (
                                            <td key={cellKey} role={cellRole} {...cellProps}>
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

Table.propTypes = {
    title: PropTypes.string,
    columns: PropTypes.arrayOf(PropTypes.object),
    data: PropTypes.arrayOf(PropTypes.object),
    fetchData: PropTypes.func,
    loading: PropTypes.bool,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    onRowClick: PropTypes.func,
};
Table.defaultProps = {};

export default Table;
