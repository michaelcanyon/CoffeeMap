import React, { Component } from 'react';

export class NotFoundRoute extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        
        return (
            <div>abcd - {window.location.pathname}</div>
            )
    }

}