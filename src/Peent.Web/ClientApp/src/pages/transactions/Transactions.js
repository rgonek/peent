import React, { useMemo, useCallback } from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";

function Transactions({ transactions, loading, pageCount, rowCount, onFetchTransactions }) {
    let history = useHistory();
    const columns = useMemo(() => {
        const onEditClick = (e, id) => {
            history.push("transactions/" + id);
            e.stopPropagation();
        };
        const onDeleteClick = (e, id) => {
            history.push("transactions/" + id + "/delete");
            e.stopPropagation();
        };
        const actionsCell = ({ cell: { value } }) => {
            return (
                <>
                    <Button variant="primary" size="sm" onClick={(e) => onEditClick(e, value)}>
                        Edit
                    </Button>{" "}
                    <Button variant="danger" size="sm" onClick={(e) => onDeleteClick(e, value)}>
                        Delete
                    </Button>
                </>
            );
        };
        actionsCell.propTypes = {
            cell: PropTypes.shape({
                value: PropTypes.any,
            }),
        };
        return [
            {
                Header: "",
                accessor: "id",
                id: "_actions",
                disableSortBy: true,
                disableFilters: true,
                Cell: actionsCell,
            },
            {
                Header: "Name",
                accessor: "name",
            },
            {
                Header: "Description",
                accessor: "description",
            },
            {
                Header: "Date",
                accessor: "date",
                disableFilters: true,
            },
        ];
    }, [history]);

    const fetchData = useCallback(
        (pageIndex, pageSize, sortBy, filters) => {
            onFetchTransactions(pageIndex, pageSize, sortBy, filters);
        },
        [onFetchTransactions]
    );

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Transactions</h1>
                <ButtonToolbar>
                    <LinkContainer to="/transactions/new">
                        <Button variant="outline-secondary" size="sm">
                            Add transaction
                        </Button>
                    </LinkContainer>
                </ButtonToolbar>
            </ContentHeader>
            <Table
                title="Transactions"
                columns={columns}
                data={transactions}
                fetchData={fetchData}
                loading={loading}
                pageCount={pageCount}
                rowCount={rowCount}
                onRowClick={(data) => {
                    history.push("transactions/" + data.id + "/details");
                }}
            />
        </div>
    );
}

Transactions.propTypes = {
    transactions: PropTypes.array,
    loading: PropTypes.bool,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    onFetchTransactions: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        transactions: state.transaction.transactions,
        loading: state.transaction.loading,
        pageCount: state.transaction.pageCount,
        rowCount: state.transaction.rowCount,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchTransactions: (pageIndex, pageSize, sortBy, filters) =>
            dispatch(actions.fetchTransactions(pageIndex, pageSize, sortBy, filters)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(Transactions);
