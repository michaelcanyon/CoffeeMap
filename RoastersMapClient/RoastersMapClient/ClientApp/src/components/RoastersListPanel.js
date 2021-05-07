import React, { Component } from 'react';
import './styles/RoastersListPanel.css';
import { Map } from './Map';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import axios from 'axios';
import { Footer } from './Footer';
import listPageBanner from './styles/listPageBanner.jpg';
import * as restConsts from '../Constants.js';
export class RoastersListPanel extends Component {
    constructor(props) {
        super(props);
        this.state = {
            mapButton: false,
            desktop: false,
            data: [],
            hoverRoaster:null,
            buffData: [],
            isLoading: false,
            error: null,
            redirect: false,
            addressFilter: "",
            phoneNumberFilter: "",
            emailFilter: "",
            descriptionFilter: "",
            tagFilter: "",
            webSiteFilter: ""
        };
    }

    componentDidMount() {
        this.addResizeEvent();
        this.getRoasters();
    }

    render() {
        var roasters = this.state.data.filter((roaster) => {
            if (this.state.addressFilter == "" ||
                roaster.address.addressStr.toLowerCase().includes(this.state.addressFilter.toLowerCase()))
                return roaster;
        });

        roasters = roasters.filter((roaster) => {
            if (this.state.phoneNumberFilter == "" ||
                roaster.roaster.contactNumber.includes(this.state.phoneNumberFilter))
                return roaster;
        });

        roasters = roasters.filter((roaster) => {
            if (this.state.emailFilter == "" ||
                roaster.roaster.contactEmail.toLowerCase().includes(this.state.emailFilter.toLowerCase()))
                return roaster;
        });

        roasters = roasters.filter((roaster) => {
            if (this.state.descriptionFilter == "" ||
                roaster.roaster.description.toLowerCase().includes(this.state.descriptionFilter.toLowerCase()))
                return roaster;
        });

        roasters = roasters.filter((roaster) => {
            if (this.state.webSiteFilter == "" ||
                roaster.roaster.webSiteLink.toLowerCase().includes(this.state.webSiteFilter.toLowerCase()))
                return roaster;
        });

        roasters = roasters.filter((roaster) => {
            if (this.state.tagFilter == "")
                return roaster;
            let foundTag = false;
            roaster.tags.forEach((tag) => {
                if (tag.name.toLowerCase().includes(this.state.tagFilter.toLowerCase()))
                    foundTag = true;
            });
            if (foundTag)
                return roaster;
        });

        return (
            <div className="container-fluid px-0">

                <div className="row row-no-padding no-gutters row-no-padding">

                    <div className={this.getStyleForPanel()}>

                        <div>

                            <img className="Logo-image" src={listPageBanner}>
                            </img>

                            <div className="Logo-text">

                                <span className="Logo-text-style">
                                </span>

                            </div>

                        </div>

                        <div className="Left-Bar_Search-form-filter collapse multi-collapse"
                            id="all-filters">

                            <form className="Left-Bar_form">

                                <input className="Left-Bar_Search-field"
                                    value={this.state.addressFilter}
                                    onChange={this.addressFilterUpd.bind(this)}
                                    placeholder="Адрес">
                                </input>

                                <input className="Left-Bar_Search-field"
                                    value={this.state.phoneNumberFilter}
                                    onChange={this.phoneNumberFilterUpd.bind(this)}
                                    placeholder="Номер телефона">
                                </input>

                                <input className="Left-Bar_Search-field"
                                    value={this.state.emailFilter}
                                    onChange={this.emailFilterUpd.bind(this)}
                                    placeholder="Email">
                                </input>

                                <input className="Left-Bar_Search-field"
                                    value={this.state.descriptionFilter}
                                    onChange={this.descriptionFilterUpd.bind(this)}
                                    placeholder="Описание">
                                </input>

                                <input className="Left-Bar_Search-field"
                                    value={this.state.tagFilter}
                                    onChange={this.tagFilterUpd.bind(this)}
                                    placeholder="Тег">
                                </input>

                                <input className="Left-Bar_Search-field"
                                    value={this.state.webSiteFilter}
                                    onChange={this.webSiteFilterUpd.bind(this)}
                                    placeholder="Сайт">
                                </input>

                                <div className="Left-Bar_Search-form_buttons btn-group-vertical">

                                    <button className="filter-buttons"
                                        onClick={this.clearFilters.bind(this)}>
                                        Очистить фильтры
                                    </button>

                                </div>

                            </form>

                        </div>

                        <div className="Left-Bar_Search-form_buttons">

                            <button className="filter-buttons col-12"
                                data-toggle="collapse"
                                data-target="#all-filters"
                                aria-expanded="false"
                                aria-controls="all-filters">
                                Фильтры
                            </button>

                        </div>

                        <div className="Left-Bar_Roasters-List">

                            <div className="Left-Bar_RoastersList">
                                {
                                    roasters.length ? roasters.map(
                                            obj =>
                                            (           
                                                <div key={obj.roaster.id}>
                                                    <a onMouseEnter={() => { this.setState({ hoverRoaster: obj.roaster.id }); }}
                                                        onMouseLeave={() => { this.setState({ hoverRoaster: null }); }}
                                                        href={this.redirectToSingle(obj.roaster.id)}>
                                                            {obj.roaster.name}
                                                        </a>
                                                    </div>
                                            )
                                   ) : ''
                                }
                            </div>

                        </div>

                        <Footer desktop={this.state.desktop} />

                    </div>

                    <div className={this.getStylesForMap()}>

                        <Map roasters={roasters} hovered={this.state.hoverRoaster} />

                    </div>

                    {this.getStylesForMapButton()}

                </div>

            </div>
        );
    }

    setHoveredRoaster = (event) => this.setState({ hoverRoaster: true });

    addressFilterUpd = (event) => this.setState({ addressFilter: event.target.value });

    phoneNumberFilterUpd = (event) => this.setState({ phoneNumberFilter: event.target.value });

    emailFilterUpd = (event) => this.setState({ emailFilter: event.target.value });

    descriptionFilterUpd = (event) => this.setState({ descriptionFilter: event.target.value });

    tagFilterUpd = (event) => this.setState({ tagFilter: event.target.value });

    webSiteFilterUpd = (event) => this.setState({ webSiteFilter: event.target.value });

    clearFilters = (event) => {
        this.setState({
            addressFilter: "",
            phoneNumberFilter: "",
            emailFilter: "",
            descriptionFilter: "",
            tagFilter: "",
            webSiteFilter: ""
        });
        event.preventDefault();
    }

    getRoasters() {
        this.setState({ isLoading: true });
        axios.get(restConsts.SERVER_DOMAIN_URL + restConsts.SERVER_ALL_ROASTERS)
            .then(result => this.setState(
                {
                    data: result.data,
                    isLoading: false
                }
            ))
            .catch(error => {
                this.setState(
                    {
                        error,
                        isLoading: false

                    });
                console.log(error);
            });
    }

    redirectToSingle(id) {
        return restConsts.CLIENT_CURRENT_DOMAIN +
            restConsts.CLIENT_SINGLE_ROASTER_REDIRECT_PATH +
            id;
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

    getStylesForMapButton = () => this.state.desktop ? "" :
        <div className="col-12 fixed-bottom" id="mapButtonDiv"><button id="mapButton" className="map-button" type="button" onClick={this.toggleMap}>Карта</button></div>;

    getStylesForMap = () => this.state.desktop ?
        "d-none d-md-block col-md-6 col-lg-7 col-xl-8" :
        this.state.mapButton ? "col-12" : "d-none d-xl-block";

    getStyleForPanel = () => this.state.desktop ?
        "col-md-6 col-lg-5 col-xl-4" : this.state.mapButton ?
            "d-none d-xl-block" : "col-12";

    getFooterPadding = () => this.state.desktop ?
        "footer-block" :
        "footer-block footer-padding";

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

    toggleMap = () => {
        this.setState(prevState => ({
            mapButton: !prevState.mapButton
        }));
    };
}