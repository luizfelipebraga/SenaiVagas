import React from 'react';
import './style.css';

interface Title {
    img: any,
    title: string
}

const Title: React.FC<Title> = ({ img, title }) => {
    return (
        <div className="title-container">
            <img src={img} alt={title} />
            <h2>{title}</h2>
        </div>
    );
}

export default Title;