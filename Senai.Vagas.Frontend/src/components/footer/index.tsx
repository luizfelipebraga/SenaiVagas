import React from 'react';


import './style.css';
import senaiWhite from '../../assets/images/LOGO SENAI-SP/PNG/Logo_SENAI_BRANCO_NEGATIVO.png';

function Footer() {
    return (
        <div className="main">
            
            <div className="footer">

                <div className="inner-footer">
                    <div className="logo-container">
                        <img className="logonegativo" src={senaiWhite} alt="logo negativo"></img>
                    </div>
                </div>

                <div className="verticalLine"></div>

                <div className="footer-third">
                    <p>INSTITUTO SENAI DE TECNOLOGIA </p>
                    <p>DA INFORMAÇÃO E COMUNICAÇÃO</p>
                    <p>Alameda Barão de Limeira, 539</p>
                    <p>Santa Cecília, São Paulo - SP</p>
                    <p>(11) 3273-5000</p>
                </div>
                
            </div>

        </div>
    );
}

export default Footer;