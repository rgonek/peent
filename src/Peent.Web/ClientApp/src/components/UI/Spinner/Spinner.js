import React from 'react';
import Spinner from 'react-bootstrap/Spinner'

const spinner = () => (
    <Spinner animation="border" role="status">
        <span className="sr-only">Loading...</span>
    </Spinner>
);

export default spinner;