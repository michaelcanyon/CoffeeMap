import React, { Component } from 'react';
import './styles/Map.css';
import GoogleMapReact from 'google-map-react';
import mapStyle from './styles/MapStyle.json';
import MarkerIco from './styles/mapMarker.png';
import * as API_KEYS from '../API_keys.js';
import * as restConsts from '../Constants.js';

const defaultDesktop = { lat: 60.25, lng: 74.15 };
const defaultMobile = { lat: 55.558741, lng: 37.378847 };
export class Map extends Component {

    static defaultProps = {
        center: {
            lat: 55.558741,
            lng: 37.378847
        },
        zoom: 4
    };

    constructor(props) {

        super(props);

        this.state = {
            bmap: null,
            bmaps: null,
            infoWin: null,
            firstLoad: true,
            markers: []
        }
    }

    renderMarkers = (map, maps, roasters, singleRoaster) => {
        const infoWindow = new maps.InfoWindow({
            disableAutoPan: true
        });
        if (this.state.firstLoad) {
            this.setState({
                bmap: map,
                bmaps: maps,
                firstLoad: false,
                infoWin: infoWindow
            });
        }
        var icon = {
            url: MarkerIco,
            scaledSize: new maps.Size(40, 40)
        };

        var localMarkers = [];
        if (roasters != null)
            roasters.map((roaster) => {
                if (roaster.address == null || roaster.address.latitude == null || roaster.address.longitude == null)
                    return;
                this.renderSingle(map,
                    maps,
                    roaster,
                    localMarkers,
                    this.state.infoWin,
                    icon)
            });
        else if (singleRoaster != null) {
            if (singleRoaster.address == null || singleRoaster.address.latitude == null || singleRoaster.address.longitude == null)
                return;
            this.renderSingle(map,
                maps,
                singleRoaster,
                localMarkers,
                this.state.infoWin,
                icon);
            map.setCenter({ lat: singleRoaster.address.latitude, lng: singleRoaster.address.longitude });
            map.setZoom(14);
        }

        //clear old markers
        this.state.markers.map((marker) => {
            marker.setMap(null);
        });

        this.setState({
            markers: localMarkers
        });
    };

    renderSingle(map, maps, roaster, localMarkers, infoWindow, icon) {
        let marker = new maps.Marker({
            position: { lat: roaster.address.latitude, lng: roaster.address.longitude },
            map,
            title: roaster.roaster.name,
            icon: icon
        });
        marker.metadata = { id: roaster.roaster.id };
        marker.setMap(map);

        marker.addListener("mouseover", () => {
            toggleBounce(marker);
        }
        );

        marker.addListener("click", () => {
            infoWindow.close();
            infoWindow.setContent(this.getInfoWindow(roaster));
            infoWindow.open(marker.getMap(), marker);
            map.setCenter({ lat: marker.getPosition().lat() + 0.0075 , lng: marker.getPosition().lng()});
            map.setZoom(14);
        })

        localMarkers.push(marker);

        function toggleBounce(marker) {
            if (marker.getAnimation() !== null)
                marker.setAnimation(null);
            else {
                marker.setAnimation(maps.Animation.BOUNCE);
                setTimeout(() => { marker.setAnimation(null); }, 700);
            }
        }
    }

    renderInfoWindow = (maps, map) => {
        map.setZoom(this.props.zoom);
        map.getDiv().offsetWidth < 500 ? map.setCenter(defaultMobile) : map.setCenter(defaultDesktop);
        var window = this.state.infoWin;
        this.state.infoWin.close();
        var marker = null;
        var roaster = null;
        if (this.state.markers != null)
            this.state.markers.map(mar => {
                if (mar.metadata.id == this.props.hovered)
                    marker = mar;
            });

        if (this.props.roasters != null)
            this.props.roasters.map(roast => {
                if (roast.roaster.id == this.props.hovered)
                    roaster = roast;
            });
        if (marker == null || roaster == null)
            return;
        if (marker.getAnimation() !== null)
            marker.setAnimation(null);
        else {
            marker.setAnimation(maps.Animation.BOUNCE);
            setTimeout(() => { marker.setAnimation(null); }, 700);
        }
        window.setContent(this.getInfoWindow(roaster));
        window.open(marker.getMap(), marker);
    }

    componentDidUpdate(prevProps) {

        if (this.state.bmap == null || this.state.bmaps == null || this.props.roasters==null || prevProps.roasters==null)
            return;

        if (!(this.props.roasters.length === prevProps.roasters.length) || !(this.props.singleRoaster === prevProps.singleRoaster))
            this.renderMarkers(this.state.bmap, this.state.bmaps, this.props.roasters, this.props.singleRoaster);
        if (!(this.props.hovered === prevProps.hovered))
            this.renderInfoWindow(this.state.bmaps, this.state.bmap);
    }

    render() {

        return (
            <div style={{
                height: '100vh',
                width: '100%',
                position: 'sticky',
                top: 0
            }}>
                <GoogleMapReact
                    bootstrapURLKeys={{ key: API_KEYS.GOOGLE_MAPS_JS_API_KEY }}
                    region="RU"
                    defaultCenter={this.props.center}
                    defaultZoom={this.props.zoom}
                    options={{
                        styles: mapStyle,
                        scrollwheel: true,
                        gestureHandling: 'greedy',
                        streetViewControl:true
                    }}
                    yesIWantToUseGoogleMapApiInternals
                    onGoogleApiLoaded={({ map, maps }) => {
                        map.getDiv().offsetWidth < 500? map.setCenter(defaultMobile) : map.setCenter(defaultDesktop);
                        this.props.hovered != null && this.state.markers.length > 0 ? this.renderInfoWindow(maps, map) :
                            this.renderMarkers(map,
                                maps,
                                this.props.roasters,
                                this.props.singleRoaster,
                                this.props.hovered,
                                this.state.prevHover);
                    }
                    }
                >

                </GoogleMapReact>

            </div>
        )
    }
    getCenter = (map) => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    map.setCenter(pos);
                    map.setZoom(this.props.zoom);
                }
            );
        }
    }
    getInfoWindow=(roaster)=> {
        var link = null;
        link = this.props.roasters == null ? '<div class="marker_name"><span>' + roaster.roaster.name + '</span></div>' :
            '<div><a class="marker_name_hover" href=' + '"' + restConsts.APP_ROUTE_PREFIX + 'SingleRoasterInfo/' + roaster.roaster.id + '">' + roaster.roaster.name + '</a></div>';
        return '<div class="info_window_wraper">' +
            link +
            '<div class="marker_mail">' +
            '<div class="mail_icon"></div>' +
            '<span>' + roaster.roaster.contactEmail +
            '</span>' +
            '</div> ' +

            '<div class=marker_number>' +
            '<div class="phone_icon"></div>' +
            '<span>' + roaster.roaster.contactNumber +
            '</span>' +
            '</div>' +
            this.checkRenderTags(roaster.tags)
            +
            '<div class="address_str_info">' + roaster.address.addressStr + '</div>' +
            '<div class="opening_hours_info">' + roaster.address.openingHours + '</div>' +
            '</div >'
    }

    checkRenderTags(tags) {
        if (tags.length > 0)
            return '<div class="tags_list">' +
                '<ul>' +
                tags.map(tag => (
                    '<li>' +
                    '<div class="icon_image"></div>' +
                    tag.name +
                    '</li>'
                )).join('') +
                '</ul>' +
                '</div>';
        else
            return '';
    }
}
export default Map;