import * as React from 'react';
import { Container, Row } from 'react-bootstrap';
import Header from './Header'
import Navigation from './Navigation'

type Props = {
    children?: React.Node
};

function Layout(props: Props) {
    return (
        <React.Fragment>
            <Header />
            <Container fluid="true">
                <Row>
                    <Navigation />
                    <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-4">
                        {props.children}
                    </main>
                </Row>
            </Container>
        </React.Fragment>
    );
}
  
export default Layout;