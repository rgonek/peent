import React from 'react';

import Noty from 'noty';
import useHttpErrorHandler from '../../hooks/http-error-handler';

const withErrorHandler = (WrappedComponent, axios) => {
  return props => {
    const [error, ] = useHttpErrorHandler(axios);

    if(error) {
      let message = error.message;
      if (error.response) {
        message = error.response.data;
      }

      new Noty({
          type: 'error',
          text: message,
          timeout: 5000
      }).show();
    }
    return (
      <React.Fragment>
        <WrappedComponent {...props} />
      </React.Fragment>
    );
  };
};

export default withErrorHandler;
