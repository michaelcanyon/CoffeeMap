import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { RoastersListPanel } from './components/RoastersListPanel';
import { SingleRoasterInfo } from './components/SingleRoasterInfo';
import { AboutUs } from './components/AboutUs';
import { ContactForm } from './components/ContactForm';
import './custom.css';
import { PostFailed } from './components/PostFailed';
import { PostSuccess } from './components/PostSuccess';
export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <div>
                <Layout>
                    <Route exact path='/' component={RoastersListPanel} />
                    <Route path='/SingleRoasterInfo/:id' component={SingleRoasterInfo} />
                    <Route path='/AboutUs' component={AboutUs} />
                    <Route path='/ContactForm' component={ContactForm} />
                    <Route path='/PostFailed' component={PostFailed} />
                    <Route path='/PostSuccess' component={PostSuccess} />
                </Layout>
                        
            </div>
        );
    }
}
