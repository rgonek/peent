import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import { LinkContainer } from 'react-router-bootstrap';

function Header() {
      return (
        <Navbar variant="dark" bg="dark" className="fixed-top flex-md-nowrap p-0 shadow">
            <Navbar.Brand className="col-sm-3 col-md-2 mr-0" href="#">Peent</Navbar.Brand>
            <Form.Control className="form-control-dark w-100" type="text" placeholder="Search" />
            <Nav className="px-3" as="ul">
                <Nav.Item as="li" className="text-nowrap">
                    <LinkContainer to='/login'>
                        <Nav.Link>Sign in</Nav.Link>
                    </LinkContainer>
                </Nav.Item>
                <Nav.Item as="li" className="text-nowrap">
                    <LinkContainer to='/register'>
                        <Nav.Link>Sign up</Nav.Link>
                    </LinkContainer>
                </Nav.Item>
            </Nav>
        </Navbar>
      );
}
  
export default Header;