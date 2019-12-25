import React from 'react';
import Nav from 'react-bootstrap/Nav';
import { FiHome, FiCreditCard, FiBookmark, FiTag, FiDollarSign } from 'react-icons/fi';
import { LinkContainer } from 'react-router-bootstrap';

function Navigation() {
      return (
        <nav className="col-md-2 d-none d-md-block bg-light sidebar">
            <div className="sidebar-sticky">
              <Nav as="ul" className="nav flex-column">
                <Nav.Item as="li">
                  <LinkContainer to='/'>
                    <Nav.Link>
                      <FiHome className="feather" /> Dashboard
                    </Nav.Link>
                  </LinkContainer>
                </Nav.Item>
                <Nav.Item as="li">
                  <LinkContainer to='/accounts/expense'>
                    <Nav.Link>
                      <FiCreditCard className="feather" /> Accounts Expense
                    </Nav.Link>
                  </LinkContainer>
                </Nav.Item>
                <Nav.Item as="li">
                  <LinkContainer to='/categories'>
                    <Nav.Link>
                      <FiBookmark className="feather" /> Categories
                    </Nav.Link>
                  </LinkContainer>
                </Nav.Item>
                <Nav.Item as="li">
                  <LinkContainer to='/tags'>
                    <Nav.Link>
                      <FiTag className="feather" /> Tags
                    </Nav.Link>
                  </LinkContainer>
                </Nav.Item>
                <Nav.Item as="li">
                  <LinkContainer to='/transactions'>
                    <Nav.Link>
                      <FiDollarSign className="feather" /> Transactions
                    </Nav.Link>
                  </LinkContainer>
                </Nav.Item>
              </Nav>
            </div>
          </nav>
      );
}
  
export default Navigation;