import React, { useState } from 'react';
import Nav from 'react-bootstrap/Nav';
import { FiHome, FiCreditCard, FiBookmark, FiTag, FiDollarSign, FiChevronLeft, FiLogIn, FiLogOut } from 'react-icons/fi';
import { LinkContainer } from 'react-router-bootstrap';
import Collapse from 'react-bootstrap/Collapse'
import './Navigation.css'

function Navigation() {
  const [open, setOpen] = useState(false);

  return (
    <nav className="col-md-2 d-none d-md-block bg-light sidebar">
        <div className="sidebar-sticky">
          <Nav as="ul" className="flex-column">
            <Nav.Item as="li">
              <LinkContainer to='/'>
                <Nav.Link>
                  <FiHome className="feather" /> Dashboard
                </Nav.Link>
              </LinkContainer>
            </Nav.Item>
            <Nav.Item as="li">
              <Nav.Link
                onClick={() => setOpen(!open)}
                aria-controls="example-collapse-text"
                aria-expanded={open}>
                <FiCreditCard className="feather" /> Accounts
                <FiChevronLeft className="feather toggle-arrow" />
              </Nav.Link>
              <Collapse in={open}>
                <div>
                  <Nav as="ul" className="flex-column">
                    <Nav.Item as="li">
                      <LinkContainer to='/accounts/revenue'>
                        <Nav.Link>
                          <FiLogIn className="feather" /> Revenue accounts 
                        </Nav.Link>
                      </LinkContainer>
                    </Nav.Item>
                    <Nav.Item as="li">
                      <LinkContainer to='/accounts/expense'>
                        <Nav.Link>
                          <FiLogOut className="feather" /> Expense accounts
                        </Nav.Link>
                      </LinkContainer>
                    </Nav.Item>
                  </Nav>
                </div>
              </Collapse>
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