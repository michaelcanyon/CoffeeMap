import React, { Component } from 'react';
import './styles/TagsInput.css';

export class TagsInput extends Component {
    constructor() {
        super();
        this.state = {
            tags: []
        };
        this.tagInput = React.createRef();
    }
    tagEnter = (e) => {
        if (e.keyCode == 32) {
            e.preventDefault();
            var val = this.tagInput.current.value;
            if (this.state.tags.find(tag => tag.toLowerCase() == val.toLowerCase()) || val == "" || val == null)
                return;
            this.setState(
                prevState => ({ tags: [...prevState.tags, val] }),
                () => this.props.tagsList(this.state.tags)
            );
            this.tagInput.current.value = null;
        }
        else if (e.keyCode == 8) {
            if (e.target.value.length > 0)
                return;
            const changedTags = [...this.state.tags];
            changedTags.splice(changedTags.length-1, 1);
            this.setState(
                { tags: changedTags },
                () => this.props.tagsList(this.state.tags)
            );
        }
    }

    removeTag = (i) => {
        const changedTags = [...this.state.tags];
        changedTags.splice(i, 1);
        this.setState(
            { tags: changedTags },
            () => this.props.tagsList(this.state.tags)
        );
    }

    render() {
        return (
            <div className="wrap-input100 input-tag">
                <ul className="input-tag__tags">
                    {this.state.tags.map((tag, i) => (
                        <li key={tag}>  { tag}
                            <button type="button" onClick={() => { this.removeTag(i); }} >+</button>
                        </li>
                    ))}
                </ul>
                <input className="input100 input-tag" type="text" ref={this.tagInput} onKeyDown={(e) => { this.tagEnter(e); }} placeholder="Добавьте тег" ></input>

            </div>
        );
    }

} 