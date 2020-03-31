import React, { useMemo } from "react";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";

function Accounts({ title, url, accounts, pageCount, rowCount, loading, fetchData }) {
    let history = useHistory();
    const columns = useMemo(() => {
        const onEditClick = (e, id) => {
            history.push("/accounts/" + id);
            e.stopPropagation();
        };
        const onDeleteClick = (e, id) => {
            history.push("/accounts/" + id + "/delete");
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
                Header: "Currency",
                accessor: "currency.name",
            },
        ];
    }, [history]);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">{title}</h1>
                <ButtonToolbar>
                    <LinkContainer to={url + "new"}>
                        <Button variant="outline-secondary" size="sm">
                            Add account
                        </Button>
                    </LinkContainer>
                </ButtonToolbar>
            </ContentHeader>
            <Table
                title={title}
                columns={columns}
                data={accounts}
                fetchData={fetchData}
                loading={loading}
                pageCount={pageCount}
                rowCount={rowCount}
                onRowClick={(data) => {
                    history.push("/accounts/" + data.id + "/details");
                }}
            />
        </div>
    );
}

Accounts.propTypes = {
    title: PropTypes.string,
    url: PropTypes.string,
    accounts: PropTypes.array,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    loading: PropTypes.bool,
    fetchData: PropTypes.func,
};

export default Accounts;
