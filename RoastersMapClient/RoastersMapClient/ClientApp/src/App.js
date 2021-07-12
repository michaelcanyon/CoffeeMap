import React, { Component } from 'react';
import { BrowserRouter as Router, Redirect, Route, Switch } from 'react-router-dom';
import { Layout } from './components/Layout';
import { RoastersListPanel } from './components/RoastersListPanel';
import { SingleRoasterInfo } from './components/SingleRoasterInfo';
import { AboutUs } from './components/AboutUs';
import { ContactForm } from './components/ContactForm';
import { NotFound } from './components/NotFound';
import './custom.css';
import { PostFailed } from './components/PostFailed';
import { PostSuccess } from './components/PostSuccess';
export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <Router>

                <div>
                    <Switch>


                        <Route exact path='/App' component={RoastersListPanel} />




                        <Route path='/App/SingleRoasterInfo/:id' component={SingleRoasterInfo} />



                        <Route path='/App/AboutUs' component={AboutUs} />



                        <Route path='/App/ContactForm' component={ContactForm} />


                        <Route path='/App/PostFailed' component={PostFailed} />

                        <Route path='/App/PostSuccess' component={PostSuccess} />

                        <Route path="/App/*" component={NotFound} />

                    </Switch>
                </div>

            </Router>
        );
    }
}
