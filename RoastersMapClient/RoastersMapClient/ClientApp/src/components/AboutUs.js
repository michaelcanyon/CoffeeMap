import React, { Component } from 'react';
import './styles/AboutUs.css';
import 'bootstrap/dist/css/bootstrap.min.css';
export class AboutUs extends Component {

    render() {
        return (<div className="page">
            <div className="container-fluid px-0">
                <div className="row row-no-padding no-gutters">
                    <div className="col-sm-12 col-md-12 col-lg-12 col-xl-12 aboutUs_container">
                        <div className="aboutUs_banner">
                            <div className="aboutUs_banner-bg_image"></div>
                            <div className="aboutUs_banner-benner_text">
                                <h4>Подробнее о RoasteRsBase</h4>
                            </div>
                        </div>
                        <div className="aboutUs_information-container">
                            <h4>Здесь какой-нибудь слоган</h4>
                            <p className="aboutUs_information-paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                            sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation
                            ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
                            cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt
                                    mollit anim id est laborum.</p>
                        </div>
                        <div className="box-1">
                            <div className="btn btn-one">
                                <a href="#">Хочу на карту!</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row row-no-padding no-gutters">
                    <div className="col-sm-12 colmd-12 col-lg-12 col-xl-12">
                        <div className="footer-block">
                            <div className="footerText_block">
                                <span className="footer-span">Карта обжарщиков</span><br></br>
                                <span className="copyright">Сверстал:  ©michaelcanyon, 2020</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        )
    }
}