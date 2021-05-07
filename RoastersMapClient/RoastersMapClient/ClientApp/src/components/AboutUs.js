import React, { Component } from 'react';
import { Footer } from './Footer'
import './styles/AboutUs.css';
import 'bootstrap/dist/css/bootstrap.min.css';

export class AboutUs extends Component {

    constructor(props) {
        super(props);
        this.state = {
            desktop: false
        };
    }

    componentDidMount = () => this.addResizeEvent();

    render() {

        return (
            <div className="page">

                <div className="container-fluid px-0">

                    <div className="row row-no-padding no-gutters">

                        <div className="col-sm-12 col-md-12 col-lg-12 col-xl-12 aboutUs_container">

                            <div className="aboutUs_banner">

                                <div className="aboutUs_banner-bg_image">
                                </div>

                                <div className="aboutUs_banner-benner_text">

                                    <span>
                                        Подробнее о RoasteRsBase
                                    </span>

                                </div>

                            </div>

                            <div className="aboutUs_information-container">

                                    <span>
                                        Здесь какой-нибудь слоган
                                    </span>

                                    <p className="aboutUs_information-paragraph">
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                                        sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation
                                        ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
                                        cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
                                        mollit anim id est laborum.
                                    </p>

                            </div>

                            <div className="box-1">

                                <div className="btn from-top">

                                    <a href="/ContactForm">
                                        Стать участником
                                    </a>

                                </div>

                            </div>

                        </div>

                    </div>

                    <div className="row row-no-padding no-gutters">

                        <div className="col-12">

                            <Footer desktop={this.state.desktop}
                                aboutus={true} />

                        </div>

                    </div>

                </div>

            </div>

        )
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
    }

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

}