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
import AccountsExpense from './pages/accounts/AccountsExpense'
import AccountsExpenseNew from './pages/accounts/AccountsExpenseNew'
import AccountsRevenue from './pages/accounts/AccountsRevenue'
import AccountsRevenueNew from './pages/accounts/AccountsRevenueNew'
import AccountsAsset from './pages/accounts/AccountsAsset'
import AccountsAssetNew from './pages/accounts/AccountsAssetNew'
import AccountsLiabilities from './pages/accounts/AccountsLiabilities'
import AccountsLiabilitiesNew from './pages/accounts/AccountsLiabilitiesNew'
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
          <Route exact path='/accounts/expense/new' component={AccountsExpenseNew} />
          <Route exact path='/accounts/expense' component={AccountsExpense} />
          <Route exact path='/accounts/revenue/new' component={AccountsRevenueNew} />
          <Route exact path='/accounts/revenue' component={AccountsRevenue} />
          <Route exact path='/accounts/asset/new' component={AccountsAssetNew} />
          <Route exact path='/accounts/asset' component={AccountsAsset} />
          <Route exact path='/accounts/liabilities/new' component={AccountsLiabilitiesNew} />
          <Route exact path='/accounts/liabilities' component={AccountsLiabilities} />
          <Route path='/accounts/:id/details' component={AccountDetails} />
          <Route path='/accounts/:id/delete' component={AccountsDelete} />
          <Route path='/accounts/:id' component={AccountsEdit} />
          <Route exact path='/' component={Dashboard} />
        </Switch>
      </Layout>
    </Router>
  );
}

export default App;
