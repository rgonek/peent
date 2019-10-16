import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/global.css';
import Layout from './components/Layout'
import Dashboard from './components/Dashboard'
import Categories from './components/Categories'
import Tags from './components/Tags'
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";

function App() {
  return (
    <Router>
      <Layout>
        <Switch>
          <Route exact path='/categories' component={Categories} />
          <Route exact path='/tags' component={Tags} />
          <Route exact path='/' component={Dashboard} />
        </Switch>
      </Layout>
    </Router>
  );
}

export default App;
