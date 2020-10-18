﻿import React, { Component } from 'react';
import './styles/RoastersListPanel.css';
import { Map } from './Map';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import axios from 'axios';

export class RoastersListPanel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            mapButton: false,
            desktop: false,
            data: [],
            isLoading: false,
            error: null
        };
    }
    componentDidMount() {


    }
    render() {
        return (
            <div className="container-fluid px-0">
                <div className="row row-no-padding no-gutters row-no-padding">
                    <div className={this.getStyleForPanel()}>
                        <div><img className="Logo-image" src="https://sun1-22.userapi.com/1HQBWLokpWfZOKaEM1CBqElI3AIKi1-XbI_hXQ/qTuqb9vRyJQ.jpg"></img>
                            <div className="Logo-text"><span className="Logo-text-style"></span></div>
                        </div>
                        <div className="Left-Bar_Search-form-filter collapse multi-collapse" id="all-filters">
                            <form className="Left-Bar_form">
                                <input className="Left-Bar_Search-field" placeholder="Адрес"></input>
                                <input className="Left-Bar_Search-field" placeholder="Номер телефона"></input>
                                <input className="Left-Bar_Search-field" placeholder="Описание"></input>
                                <input className="Left-Bar_Search-field" placeholder="Тег"></input>
                                <input className="Left-Bar_Search-field" placeholder="Сайт"></input>
                                <div className="Left-Bar_Search-form_buttons">
                                    <button className="filter-buttons">Очистить фильтры</button>
                                </div>
                            </form>
                        </div>
                        <div className="Left-Bar_Search-form_buttons">
                            <button className="filter-buttons" data-toggle="collapse" data-target="#all-filters" aria-expanded="false" aria-controls="all-filters">Фильтры</button>
                        </div>
                        <div className="Left-Bar_Roasters-List">
                            <div className="Left-Bar_RoastersList">
                                <div><a href="#">Обжарщик 1</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Roaster 32</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>
                                <div><a href="#">Обжарщик 2</a></div>

                            </div>
                        </div>
                        <div className="footer-block">
                            <div className="footerText_block">
                                <span className="footer-span">Карта обжарщиков</span><br></br>
                                <span className="copyright">Сверстал:  ©michaelcanyon, 2020</span>
                            </div>
                        </div>
                    </div>
                    <div className={this.getStylesForMap()}>
                        <Map />
                        <img className="mapimage sticky-top" src="https://docs.microsoft.com/ru-ru/azure/azure-maps/media/migrate-google-maps-web-app/google-maps-marker.png"></img>
                    </div>
                </div>
                <div className={this.getStylesForMapButton()} id="mapButtonDiv"><button id="mapButton" className="map-button" type="button" onClick={this.toggleMap}>Карта</button></div>
            </div>
        );
    }
    getRoasters() {
        this.setState({ isLoading: true });
        axios.get
    }
    addResizeEvent() {
        window.addEventListener('resize', this.updateDimensions);
        if (window.innerWidth > 765) {
            this.setState({
                desktop: true
            });
        }
        else if (window.innerWidth < 765) {
            this.setState({
                desktop: false
            });
        }
    }
    getStylesForMapButton() {
        if (this.state.desktop)
            return "fixed-bottom fixed-button";
        else
            return "fixed-bottom fixed-button mobile-visibility";
    }
    getStylesForMap() {
        if (this.state.desktop)
            return "d-none d-md-block col-md-6 col-lg-7 col-xl-8";
        else {
            if (this.state.mapButton)
                return "col-12";
            else
                return "d-none d-xl-block";
        }
    }
    getStyleForPanel() {
        if (this.state.desktop)
            return "col-md-6 col-lg-5 col-xl-4";
        else
            if (this.state.mapButton)
                return "d-none d-xl-block";
            else
                return "col-12";
    }
    updateDimensions = () => {
        if (window.innerWidth > 765) {
            this.setState({
                desktop: true
            });
        }
        else if (window.innerWidth < 765) {
            this.setState({
                desktop: false
            });
        }
    };
    toggleMap = () => {
        this.setState(prevState => ({
            mapButton: !prevState.mapButton
        }));
    };
}