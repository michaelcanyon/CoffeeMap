import React, { Component } from 'react';
import './styles/Header.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
export class Header extends Component {
    render() {
        return (
            < nav className="navbar navbar-expand-lg navbar-light bg-light" >
                <div className="container-fluid">
                    <img className="d-none d-sm-block col-sm-2 col-md-2 col-lg-1 col-xl-1" src="https://image.flaticon.com/icons/svg/31/31082.svg"></img>
                    <div className="navbar-header header-text">
                        RoasteRsBase
                     </div>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>

                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav ml-auto">
                            <li className="nav-item active">
                                <a className="nav-link" href="#">Обжарщики <span className="sr-only">(current)</span></a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="#">О проекте</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="#">Контакты</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        );
    }
}