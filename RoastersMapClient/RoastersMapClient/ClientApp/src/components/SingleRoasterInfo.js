import React, { Component } from 'react';
import { Map } from './Map';
import { Footer } from './Footer';
import './styles/SingleRoasterInfo.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import axios from 'axios';
import singleBanner from './styles/SingleDefaultBanner.jpg';
import telegramIco from './styles/telegram.svg';
import webIco from './styles/web.svg';
import vkIco from './styles/vk.svg';
import IgIco from './styles/instagram.svg';
import * as restConsts from '../Constants.js';

export class SingleRoasterInfo extends Component {
    state = {
        mapButton: false,
        desktop: false,
        roasterId: this.props.match.params.id,
        roaster: null,
        isLoading: false,
        error: false,

    };

    componentDidMount() {
        this.addResizeEvent();
        this.getRoasterInfo();
    }

    render() {

        const roast = this.state.roaster;

        if (roast == null)
            return <div className="Roaster_title">Receiving data...</div>

        return (

            <div className="container-fluid px-0">

                <div className="row row-no-padding no-gutters sticky-top">

                    <div className="col-12 backToList_button">

                        <a href="#">
                            К обжарщикам
                        </a>

                    </div>

                </div>

                <div className="row row-no-padding no-gutters row-no-padding">

                    <div className={this.getStyleForPanel()} >

                        <div>

                            <img className="Logo-image"
                                src={this.defineRoasterPicture(roast.roaster.picture)}>
                            </img>

                            <div className="Logo-text">

                                <span className="Logo-text-style">
                                </span>

                            </div>

                        </div>

                        <div>

                            < div className="Left-Bar_RoasterInformation" >

                                <div className="Roaster_title">

                                    <span>
                                        {roast.roaster.name}
                                    </span>

                                </div>

                                <div className="Roaster_description">

                                    <p>
                                        {roast.roaster.description}
                                    </p>

                                </div>

                                {this.renderTag(roast.tags)}

                                <div className="Contacts_container">

                                    <span>
                                        Контакты:
                                    </span>

                                    <ul className="Contact_list">

                                        <li>

                                            <div className="phoneIcon_image">
                                            </div>

                                            <div className="roaster_breakWordContacts"
                                                id="roaster_number">

                                                <span>
                                                    {roast.roaster.contactNumber}
                                                </span>

                                            </div>

                                        </li>

                                        <li>

                                            <div className="mailIcon_image">
                                            </div>

                                            <div className="roaster_breakWordContacts"
                                                id="roaster_mail">

                                                <span>
                                                    {roast.roaster.contactEmail}
                                                </span>

                                            </div>

                                        </li>

                                    </ul>

                                </div>

                                <div className="address_container">

                                    <span className="address_header">
                                        Адрес:
                                    </span>

                                    <br>
                                    </br>

                                    <div className="address_string">

                                        <div className="roasterLocationIcon">
                                        </div>

                                        <div className="Roaster_locationString">

                                            <span>
                                                {roast.address.addressStr}
                                            </span>

                                            <br>
                                            </br>

                                            <span className="openingHours_string">
                                                {roast.address.openingHours}
                                            </span>

                                        </div>

                                    </div>

                                </div>

                                <div className="SocialNetworks_block">

                                    <div className="social_header">
                                        Ссылки:
                                    </div>

                                    <div className="socialNetwork_icons">

                                        <a href={roast.roaster.telegramProfileLink}>

                                            <img src={telegramIco}>
                                            </img>

                                        </a>

                                        <a href={roast.roaster.instagramProfileLink}>

                                            <img src={IgIco}>
                                            </img>

                                        </a>

                                        <a href={roast.roaster.vkProfileLink}>

                                            <img src={vkIco}>
                                            </img>

                                        </a>

                                        <a href={roast.roaster.webSiteLink}>

                                            <img src={webIco}>
                                            </img>

                                        </a>

                                    </div>

                                </div>

                            </div>

                        </div>

                        <Footer desktop={this.state.desktop} />

                    </div>

                    <div className={this.getStylesForMap()}>

                        <Map singleRoaster={this.state.roaster} />

                    </div>

                    <div className={this.getStylesForMapButton()}
                        id="mapButtonDiv">

                        <button id="mapButton"
                            className="map-button"
                            type="button"
                            onClick={this.toggleMap}>
                            Карта
                        </button>

                    </div>

                </div>

            </div>

        );
    }

    renderTag(tags) {
        if (tags.length > 0)
            return < div className="Tags_container" >

                <span>
                    Теги:
                </span>

                <ul className="tag_list">
                    {tags.map(tag =>
                        (
                            <li>
                                <div className="icon_image"></div>
                                {tag.name}
                            </li>
                        ))}
                </ul>

            </div >;
        else
            return '';
    }

    getRoasterInfo() {
        this.setState({ isLoading: true });
        axios.get(restConsts.SERVER_DOMAIN_URL +
            restConsts.SERVER_SINGLE_ROASTER_DATAFETCH_PATH +
            this.state.roasterId)
            .then(result =>
                this.setState({
                    roaster: result.data,
                    isLoading: false
                }))
            .catch(
                error => {
                    this.setState(
                        {
                            error,
                            isLoading: false
                        }
                    );
                    console.log(error);
                });
    }

    defineRoasterPicture=(picture) => picture.length == 0 ?
        singleBanner :
        ("data:image/jpg;base64," + picture);

    getStylesForMapButton = () => this.state.desktop ?
        "fixed-bottom fixed-button" :
        "fixed-bottom fixed-button mobile-visibility";

    getStylesForMap = () => this.state.desktop ?
        "d-none d-md-block col-md-6 col-lg-7 col-xl-8" :
        this.state.mapButton ?
            "col-12" :
            "d-none d-xl-block";

    getStyleForPanel = () => this.state.desktop ?
        "col-md-6 col-lg-5 col-xl-4" :
        this.state.mapButton ?
            "d-none d-xl-block" :
            "col-12";

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

    toggleMap = () => {
        this.setState(prevState => ({
            mapButton: !prevState.mapButton
        }));
    };
}