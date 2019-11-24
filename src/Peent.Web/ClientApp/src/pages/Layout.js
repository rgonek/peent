import * as React from 'react';
import { Container, Row } from 'react-bootstrap';
import Header from '../components/Header'
import Navigation from '../components/Navigation'
import "../../node_modules/noty/lib/noty.css";
import "../../node_modules/noty/lib/themes/mint.css";

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
                    <main role="main" className="col-md-9 ml-sm-auto col-lg-10 px-4">
                        {props.children}
                    </main>
                </Row>
            </Container>
        </React.Fragment>
    );
}
  
export default Layout;