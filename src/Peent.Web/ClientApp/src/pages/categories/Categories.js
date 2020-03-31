import React, { useMemo, useCallback } from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";

function Categories({ categories, loading, pageCount, rowCount, onFetchCategories }) {
    let history = useHistory();
    const columns = useMemo(() => {
        const onEditClick = (e, id) => {
            history.push("categories/" + id);
            e.stopPropagation();
        };
        const onDeleteClick = (e, id) => {
            history.push("categories/" + id + "/delete");
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
        ];
    }, [history]);

    const fetchData = useCallback(
        (pageIndex, pageSize, sortBy, filters) => {
            onFetchCategories(pageIndex, pageSize, sortBy, filters);
        },
        [onFetchCategories]
    );

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
                data={categories}
                fetchData={fetchData}
                loading={loading}
                pageCount={pageCount}
                rowCount={rowCount}
                onRowClick={(data) => {
                    history.push("categories/" + data.id + "/details");
                }}
            />
        </div>
    );
}

Categories.propTypes = {
    categories: PropTypes.array,
    loading: PropTypes.bool,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    onFetchCategories: PropTypes.func,
};

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
