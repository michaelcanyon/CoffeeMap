import React, { Component } from "react";
import './styles/ErrorPage/css/style.css';
import './styles/ErrorPage/img/emoji.png';
import { Redirect } from 'react-router-dom';
import * as restConsts from '../Constants.js';
export class NotFound extends Component {
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
			? <Redirect to={restConsts.APP_ROUTE_PREFIX} /> :
			<div id="notfound">

				<div className="notfound">

					<div className="notfound-404">
					</div>

					<h1>
						404
						 </h1>

					<h2>
						Страница не найдена.
						 </h2>

					<p>
						Страница, которую Вы ищете, не существует!
						Проверьте корректность введенного запроса.
						 </p>

				</div>

			</div>

	}
}