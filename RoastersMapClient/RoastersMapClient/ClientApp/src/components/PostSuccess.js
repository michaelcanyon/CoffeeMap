import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import './styles/successPage/style.css';

export class PostSuccess extends Component {

    constructor() {
        super();
        this.state = {
            redirect: false
        }
    }

    componentDidMount() {
        this.id = setTimeout(() => this.setState({ redirect: true }), 10000);
    }
    componentWillUnmount() {
        clearTimeout(this.id);
    }
    render() {

        return this.state.redirect
            ? <Redirect to='/' /> :
            <div className="banner_success">

                <div className="site-header"
                    id="header">

                    <h1 className="site-header__title"
                        data-lead-id="site-header-title">
                        Спасибо, что выбрали нас!
                    </h1>

                </div>

                <div className="main-content">

                    <i className="fa fa-check main-content__checkmark"
                        id="checkmark">
                    </i>

                    <p className="main-content__body"
                        data-lead-id="main-content-body">
                        Ваша заявка успешно оформлена.
                        Для обсуждения деталей участия, оператор свяжется с Вами в ближайшее время.
                    </p>

                </div>

                <div className="site-footer"
                    id="footer">

                    <p className="site-footer__fineprint"
                        id="fineprint">
                    </p>

                </div>

            </div>

    }

}