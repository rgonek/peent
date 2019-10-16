// @flow
import * as React from 'react';

type Props = {
    children?: React.Node
};

function ContentHeader(props: Props) {
    return (
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            {props.children}
        </div>
    );
}
  
export default ContentHeader;