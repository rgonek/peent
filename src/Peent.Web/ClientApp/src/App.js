import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/global.css';
import Layout from './pages/Layout'
import Dashboard from './pages/Dashboard'
import Categories from './pages/Categories'
import Tags from './pages/tags/Tags'
import TagsNew from './pages/tags/TagsNew'
import TagsEdit from './pages/tags/TagsEdit'
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import './services/errorHandler'
import './services/transformResponse'

function App() {
  return (
    <Router>
      <Layout>
        <Switch>
          <Route path='/categories' component={Categories} />
          <Route path='/tags/new' component={TagsNew} />
          <Route path='/tags/:id' component={TagsEdit} />
          <Route path='/tags' component={Tags} />
          <Route exact path='/' component={Dashboard} />
        </Switch>
      </Layout>
    </Router>
  );
}

export default App;
