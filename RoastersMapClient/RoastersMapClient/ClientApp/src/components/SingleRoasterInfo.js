import React, { Component } from 'react';
import { Map } from './Map';
import './styles/SingleRoasterInfo.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import { Animated } from 'react-animated-css';
import axios from 'axios';

export class SingleRoasterInfo extends Component {
    state = {
        mapButton: false,
        desktop: false,
        roasterId: this.props.match.params.id,
        roaster: [],
        isLoading: false,
        error: null

    };
    render() {
        return (
            <div className="container-fluid px-0">
                <div className="row row-no-padding no-gutters sticky-top">
                    <div className="col-12 backToList_button"><a href="#">К обжарщикам</a>
                    </div>
                </div>
                <div className="row row-no-padding no-gutters row-no-padding">
                    <div className={this.getStyleForPanel()} >
                        <div><img className="Logo-image" src="https://cdn.the-village.ru/the-village.ru/post_image-image/0PSvW6yyK9DQKKWAxLggiA.jpg"></img>
                            <div className="Logo-text"><span className="Logo-text-style"></span></div>
                        </div>
                        {this.state.roaster.map((fields) => (
                            <div className="Left-Bar_RoasterInformation">
                                <div className="Roaster_title">
                                    <span>{fields.roaster.name}</span>
                                </div>
                                <div className="Roaster_description">
                                    <p>{fields.roaster.description}</p></div>
                                < div className="Tags_container">
                                    <span>Теги:</span>
                                    <ul className="tag_list">
                                        {fields.tags.map((tag) =>
                                            (
                                                <li>
                                                    <div className="icon_image"></div>
                                                    {tag.tagTitle}
                                                </li>
                                            ))}
                                    </ul>
                                </div>
                                <div className="Contacts_container">
                                    <span>Контакты: </span>
                                    <ul className="Contact_list">
                                        <li><div className="phoneIcon_image"></div><div className="roaster_breakWordContacts" id="roaster_number"><span>{fields.roaster.contactNumber}</span></div></li>
                                        <li><div className="mailIcon_image"></div><div className="roaster_breakWordContacts" id="roaster_mail"><span>{fields.roaster.contactEmail}</span></div></li>
                                    </ul>
                                </div>
                                <div className="address_container">
                                    <span className="address_header">Адрес: </span><br></br>
                                    <div className="address_string"><div className="roasterLocationIcon"></div><div className="Roaster_locationString"><span>{fields.address.addressStr}</span><br></br>
                                        <span className="openingHours_string">{fields.address.openingHours}</span></div>
                                    </div>
                                </div>
                                <div className="SocialNetworks_block">
                                    <div className="social_header">Ссылки: </div>
                                    <div className="socialNetwork_icons">
                                        <a href={fields.roaster.telegramProfileLink}><img src="https://www.flaticon.com/svg/static/icons/svg/123/123726.svg"></img></a>
                                        <a href={fields.roaster.instagramProfileLink}><img src="https://www.flaticon.com/svg/static/icons/svg/123/123739.svg"></img></a>
                                        <a href={fields.roaster.vkProfileLink}><img src="https://www.flaticon.com/svg/static/icons/svg/121/121507.svg"></img></a>
                                        <a href={fields.roaster.webSiteLink}><img src="https://www.flaticon.com/svg/static/icons/svg/288/288888.svg"></img></a>
                                    </div>
                                </div>
                            </div>
                        ))}
                        <div className="footer-block">
                            <div class="footerText_block">
                                <span className="footer-span">Карта обжарщиков</span><br></br>
                                <span className="copyright">Сверстал:  ©michaelcanyon, 2020</span>
                            </div>
                        </div>
                    </div>
                    <div className={this.getStylesForMap()}>
                        <Map />
                        <img className="mapimage sticky-top" src="https://docs.microsoft.com/ru-ru/azure/azure-maps/media/migrate-google-maps-web-app/google-maps-marker.png"></img>
                    </div>
                    <div className={this.getStylesForMapButton()} id="mapButtonDiv"><button id="mapButton" className="map-button" type="button" onClick={this.toggleMap}>Карта</button></div>
                </div>
            </div>
        );
    }
    getRoasterInfo() {
        this.setState({ isLoading: true });
        axios.get("https://localhost:5001/Roasters/Single?id=" + this.state.roasterId)
            .then(result = this.setState({
                roaster: result.data,
                isLoading: false
            })).catch(
                error => this.setState(
                    {
                        error,
                        isLoading: false
                    }
                ));
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
    componentDidMount() {
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
    toggleMap = () => {
        this.setState(prevState => ({
            mapButton: !prevState.mapButton
        }));
    };
}