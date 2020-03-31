import React, { useMemo, useCallback } from "react";
import { connect } from "react-redux";
import * as actions from "../../store/actions/index";
import ContentHeader from "../../components/ContentHeader";
import { LinkContainer } from "react-router-bootstrap";
import { ButtonToolbar, Button } from "react-bootstrap";
import Table from "../../components/UI/Table/Table";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";

function Tags({ tags, loading, pageCount, rowCount, onFetchTags }) {
    let history = useHistory();
    const onEditClick = (e, id) => {
        history.push("tags/" + id);
        e.stopPropagation();
    };
    const onDeleteClick = (e, id) => {
        history.push("tags/" + id + "/delete");
        e.stopPropagation();
    };
    const actionsCell = useMemo(({ cell: { value } }) => {
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
    });
    const columns = useMemo(
        () => [
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
        ],
        []
    );

    const fetchData = useCallback((pageIndex, pageSize, sortBy, filters) => {
        onFetchTags(pageIndex, pageSize, sortBy, filters);
    }, []);

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Tags</h1>
                <ButtonToolbar>
                    <LinkContainer to="/tags/new">
                        <Button variant="outline-secondary" size="sm">
                            Add tag
                        </Button>
                    </LinkContainer>
                </ButtonToolbar>
            </ContentHeader>
            <Table
                title="Tags"
                columns={columns}
                data={tags}
                fetchData={fetchData}
                loading={loading}
                pageCount={pageCount}
                rowCount={rowCount}
                onRowClick={(data) => {
                    history.push("tags/" + data.id + "/details");
                }}
            />
        </div>
    );
}

Tags.propTypes = {
    tags: PropTypes.array,
    loading: PropTypes.bool,
    pageCount: PropTypes.number,
    rowCount: PropTypes.number,
    onFetchTags: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        tags: state.tag.tags,
        loading: state.tag.loading,
        pageCount: state.tag.pageCount,
        rowCount: state.tag.rowCount,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchTags: (pageIndex, pageSize, sortBy, filters) =>
            dispatch(actions.fetchTags(pageIndex, pageSize, sortBy, filters)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(Tags);
