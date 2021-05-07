import React, { Component } from "react";
import './styles/ErrorPage/css/style.css';
import './styles/ErrorPage/img/emoji.png';
import { Redirect } from 'react-router-dom';
export class PostFailed extends Component {
	constructor() {
		super();
		this.state = {
			redirect: false
		}
	}
	componentDidMount() {
		this.id = setTimeout(() => this.setState({ redirect: true }), 7000);
	}
	componentWillUnmount() {
		clearTimeout(this.id);
	}
    render() {

			 return this.state.redirect
				 ? <Redirect to='/' />:
			<div id="notfound">

					 <div className="notfound">

						 <div className="notfound-404">
						 </div>

						 <h1>
							 400
						 </h1>

						 <h2>
							 Ой! В запросе возникла ошибка.
						 </h2>

						 <p>
							 Похоже, сервер на данный момент не может обработать Ваш запрос.
							 Мы уже работаем над данной проблемой.
							 Приносим извинения за доставленные неудобства.
							 Вы будете перенаправлены на домашнюю страницу.
						 </p>

					 </div>

			</div>

    }
}