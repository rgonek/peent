// @flow
import React from 'react';
import ContentHeader from '../../components/ContentHeader';
import { LinkContainer } from 'react-router-bootstrap';
import { ButtonToolbar, Button } from 'react-bootstrap';

function Tags() {
  return (
    <div>
      <ContentHeader>
        <h1 className="h2">Tags</h1>
        <ButtonToolbar>
          <LinkContainer to='/tags/new'>
            <Button variant="outline-secondary" size="sm">Add tag</Button>
          </LinkContainer>
        </ButtonToolbar>
      </ContentHeader>
      data
    </div>
  );
}
  
export default Tags;