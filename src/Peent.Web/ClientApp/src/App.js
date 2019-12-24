import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles/global.css';
import Layout from './pages/Layout'
import Dashboard from './pages/Dashboard'
import Categories from './pages/categories/Categories'
import CategoriesNew from './pages/categories/CategoriesNew'
import CategoriesEdit from './pages/categories/CategoriesEdit'
import CategoryDetails from './pages/categories/CategoryDetails'
import CategoriesDelete from './pages/categories/CategoriesDelete'
import Tags from './pages/tags/Tags'
import TagsNew from './pages/tags/TagsNew'
import TagsEdit from './pages/tags/TagsEdit'
import TagDetails from './pages/tags/TagDetails'
import TagsDelete from './pages/tags/TagsDelete'
import Accounts from './pages/accounts/Accounts'
import AccountsNew from './pages/accounts/AccountsNew'
import AccountsEdit from './pages/accounts/AccountsEdit'
import AccountDetails from './pages/accounts/AccountDetails'
import AccountsDelete from './pages/accounts/AccountsDelete'
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
          <Route path='/categories/new' component={CategoriesNew} />
          <Route path='/categories/:id/details' component={CategoryDetails} />
          <Route path='/categories/:id/delete' component={CategoriesDelete} />
          <Route path='/categories/:id' component={CategoriesEdit} />
          <Route path='/categories' component={Categories} />
          <Route path='/tags/new' component={TagsNew} />
          <Route path='/tags/:id/details' component={TagDetails} />
          <Route path='/tags/:id/delete' component={TagsDelete} />
          <Route path='/tags/:id' component={TagsEdit} />
          <Route path='/tags' component={Tags} />
          <Route path='/accounts/new' component={AccountsNew} />
          <Route path='/accounts/:id/details' component={AccountDetails} />
          <Route path='/accounts/:id/delete' component={AccountsDelete} />
          <Route path='/accounts/:id' component={AccountsEdit} />
          <Route path='/accounts' component={Accounts} />
          <Route exact path='/' component={Dashboard} />
        </Switch>
      </Layout>
    </Router>
  );
}

export default App;
