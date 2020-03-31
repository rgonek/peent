import React from "react";
import Form from "react-bootstrap/Form";
import PropTypes from "prop-types";

export function defaultColumnFilter({ column: { filterValue, setFilter } }) {
    return (
        <Form.Control
            type="text"
            size="sm"
            placeholder={`Search...`}
            value={filterValue || ""}
            onChange={(e) => {
                setFilter(e.target.value || undefined); // Set undefined to remove the filter entirely
            }}
        />
    );
}

defaultColumnFilter.propTypes = {
    column: PropTypes.shape({
        filterValue: PropTypes.string,
        setFilter: PropTypes.func,
    }),
};
