import React, { useMemo, useCallback } from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";

function Categories(props) {
    let history = useHistory();
    const onEditClick = (e, id) => {
        history.push("categories/" + id);
        e.stopPropagation();
    };
    const onDeleteClick = (e, id) => {
        history.push("categories/" + id + "/delete");
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
        ],
        []
    );

    const fetchData = useCallback((pageIndex, pageSize, sortBy, filters) => {
        props.onFetchCategories(pageIndex, pageSize, sortBy, filters);
    }, []);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Categories</h1>
                <ButtonToolbar>
                    <LinkContainer to="/categories/new">
                        <Button variant="outline-secondary" size="sm">
                            Add category
                        </Button>
                    </LinkContainer>
                </ButtonToolbar>
            </ContentHeader>
            <Table
                title="Categories"
                columns={columns}
                data={props.categories}
                fetchData={fetchData}
                loading={props.loading}
                pageCount={props.pageCount}
                rowCount={props.rowCount}
                onRowClick={(data) => {
                    history.push("categories/" + data.id + "/details");
                }}
            />
        </div>
    );
}

const mapStateToProps = (state) => {
    return {
        categories: state.category.categories,
        loading: state.category.loading,
        pageCount: state.category.pageCount,
        rowCount: state.category.rowCount,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchCategories: (pageIndex, pageSize, sortBy, filters) =>
            dispatch(actions.fetchCategories(pageIndex, pageSize, sortBy, filters)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(Categories);
