import React, { useMemo } from "react";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";

function Accounts({ title, url, accounts, pageCount, rowCount, loading, fetchData }) {
    let history = useHistory();
    const onEditClick = (e, id) => {
        history.push("/accounts/" + id);
        e.stopPropagation();
    };
    const onDeleteClick = (e, id) => {
        history.push("/accounts/" + id + "/delete");
        e.stopPropagation();
    };
    const columns = useMemo(
        () => [
            {
                Header: "",
                accessor: "id",
                id: "_actions",
                disableSortBy: true,
                disableFilters: true,
                Cell: ({ cell: { value } }) => {
                    return (
                        <>
                            <Button
                                variant="primary"
                                size="sm"
                                onClick={(e) => onEditClick(e, value)}
                            >
                                Edit
                            </Button>{" "}
                            <Button
                                variant="danger"
                                size="sm"
                                onClick={(e) => onDeleteClick(e, value)}
                            >
                                Delete
                            </Button>
                        </>
                    );
                },
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
        ],
        []
    );

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

export default Accounts;
