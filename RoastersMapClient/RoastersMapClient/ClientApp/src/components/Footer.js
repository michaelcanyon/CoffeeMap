import React, { Component } from 'react';
import './styles/Footer.css';

export class Footer extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        return (
            < div className={this.getFooterPadding()} >
                <div className="footerText_block">
                    <span className="footer-span">Карта обжарщиков</span><br></br>
                    <span className="copyright">Сверстал:  ©michaelcanyon, 2020</span>
                </div>
            </div >
        );
    }
    getFooterPadding = () => this.props.desktop || this.props.aboutus ? "footer-block" : "footer-block footer-padding";
}