import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { RoastersListPanel } from './components/RoastersListPanel';
import { SingleRoasterInfo } from './components/SingleRoasterInfo';
import { AboutUs } from './components/AboutUs';

import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={RoastersListPanel} />
        <Route path='/SingleRoasterInfo' component={SingleRoasterInfo} />
        <Route path='/AboutUs' component={AboutUs} />
      </Layout>
    );
  }
}
