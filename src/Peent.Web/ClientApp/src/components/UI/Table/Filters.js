import React from "react";
import Form from "react-bootstrap/Form";
import PropTypes from "prop-types";
// import { useQuery } from "../../../shared/utility";

export function DefaultColumnFilter({ column: { filterValue, setFilter } }) {
    // const query = useQuery();
    const defaultValue = filterValue || "";
    return (
        <Form.Control
            type="text"
            size="sm"
            placeholder={`Search...`}
            defaultValue={defaultValue}
            onChange={(e) => {
                setFilter(e.target.value || undefined); // Set undefined to remove the filter entirely
            }}
        />
    );
}

DefaultColumnFilter.propTypes = {
    column: PropTypes.shape({
        id: PropTypes.string,
        filterValue: PropTypes.string,
        setFilter: PropTypes.func,
    }),
};
