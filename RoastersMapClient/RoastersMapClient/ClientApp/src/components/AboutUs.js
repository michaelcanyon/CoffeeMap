import React, { Component } from 'react';
import { Footer } from './Footer'
import './styles/AboutUs.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Header } from './Header';
import * as restConsts from '../Constants.js';

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
            <div>
                <Header />

                <div className="page">

                    <div className="container-fluid px-0">

                        <div className="row row-no-padding no-gutters">

                            <div className="col-sm-12 col-md-12 col-lg-12 col-xl-12 aboutUs_container">

                                <div className="aboutUs_banner">

                                    <div className="aboutUs_banner-bg_image">
                                    </div>

                                    <div className="aboutUs_banner-benner_text">

                                        <span>
                                            Подробнее о RoastersMap
                                    </span>

                                    </div>

                                </div>

                                <div className="aboutUs_information-container">

                                    <span>
                                        RoastersMap — это карта кофейных обжарщиков России.
                                    </span>

                                    <p className="aboutUs_information-paragraph">
                                        &nbsp;&nbsp;&nbsp;&nbsp;В последние годы кофе, обжаривающийся в нашей стране, высоко оценивается мировым спешелти сообществом.
                                        Российские профессионалы нередко занимают первые места на мировых чемпионатах, разрабатывают новые стили обжарки и собирают линейки уникальных лотов.
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;С главными игроками на рынке Вы, совершенно точно, знакомы, а вот маленькие региональные проекты могли пройти мимо Вас.
                                    Помочь потребителю и обжарщику найти друг друга — вот причина, по которой мы сделали RoastersMap.
                                    Команда кофейных энтузиастов собрала на карте всех обжарщиков России, чтобы обеспечить:
                                    <ul className="description_list">

                                            <li>
                                                - Быструю связь специалистов из вашего города или региона с Вами;
                                        </li>

                                            <li>
                                                - Возможность изучить их ассортимент и выбрать тот кофе, который подходит именно Вам!
                                        </li>

                                        </ul>
                                    &nbsp;&nbsp;&nbsp;&nbsp;Карта будет постоянно обновляться, так что, информация всегда будет актуальна.
                                    Делитесь картой с друзьями, чтобы как можно больше людей знали своих героев в лицо и пили вкусный свежеобжаренный кофе по всей стране!

                                    </p>

                                </div>

                                <div className="box-1">

                                    <div className="btn from-top">

                                        <a href={restConsts.APP_ROUTE_PREFIX+"ContactForm"}>
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