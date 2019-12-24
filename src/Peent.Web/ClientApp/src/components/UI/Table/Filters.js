import React from 'react'
import Form from 'react-bootstrap/Form'

export function defaultColumnFilter({
    column: { filterValue, preFilteredRows, setFilter }
  }) {
    return (
        <Form.Control 
            type="text" 
            size="sm"
            placeholder={`Search...`}
            value={filterValue || ''}
            onChange={e => {
                setFilter(e.target.value || undefined) // Set undefined to remove the filter entirely
            }} />
    )
}