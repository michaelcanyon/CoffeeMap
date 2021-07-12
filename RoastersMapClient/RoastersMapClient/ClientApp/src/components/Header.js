import React, { Component } from 'react';
import './styles/Header.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import beanpic from './styles/main_bean.svg';
import * as restConsts from '../Constants.js';

export class Header extends Component {

    constructor(props) {
        super(props);
        this.state = {
            desktop: false
        };
    }
    componentDidMount() {
        this.addResizeEvent();
    }
    render() {
        let paddings = this.getTogglerPaddings();
        return (
            < nav className="navbar navbar-expand-lg navbar-light py-1 navbar_style" >

                <div className="container-fluid">

                    <img className="d-none d-sm-block col-sm-2 col-md-2 col-lg-1 col-xl-1"
                        src={beanpic}>
                    </img>

                    <div className="navbar-header header-text">
                        RoastersMap
                     </div>

                    <button className="navbar-toggler"
                        type="button"
                        data-toggle="collapse"
                        data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation">

                        <span className="navbar-toggler-icon">
                        </span>

                    </button>

                    <div className={this.getTogglerPadding()}
                        id="navbarSupportedContent">

                        <ul className="navbar-nav flex-fill">

                            <li className={paddings}>

                                <a className="nav-link"
                                    href={restConsts.APP_ROUTE_PREFIX}>
                                    обжарщики
                                    
                                    <span className="sr-only">
                                        (current)
                                    </span>

                                </a>

                            </li>

                            <li className={paddings}>

                                <a className="nav-link"
                                    href={restConsts.APP_ROUTE_PREFIX +"AboutUs"}>
                                    о проекте
                                </a>

                            </li>

                        </ul>

                    </div>

                </div>

            </nav>
        );
    }

    addResizeEvent() {
        window.addEventListener('resize', this.updateDimensions);
        if (window.innerWidth < 765) {
            this.setState({
                desktop: false
            });
            return;
        }
        else if (window.innerWidth > 765) {
            this.setState({
                desktop: true
            });
        }
    };

    updateDimensions = () => {
        if (window.innerWidth < 765) {
            this.setState({
                desktop: false
            });
            return;
        }
        else if (window.innerWidth > 765) {
            this.setState({
                desktop: true
            });
        }
    };

    getTogglerPadding = () => this.state.desktop ?
        "collapse navbar-collapse flex-column flex-row-reverse pl-5 " :
        "collapse navbar-collapse flex-column flex-row-reverse";

    getTogglerPaddings = () => this.state.desktop ?
        "text-center flex-fill navigation_buttons_margin nav_butto" :
        "text-center flex-fill nav_butto";
}