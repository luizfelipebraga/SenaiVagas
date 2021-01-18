import React from 'react';
import { Button } from 'react-bootstrap';
import Footer from '../../components/footer/index';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title/index';
import iconCandidatos from '../../assets/images/IconCandidatos.png'

import '../../assets/styles/global.css';
import '../candidatos/style.css';

function Candidatos() {
    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName="{Nome da empresa}" />
                <div className="content-child">

                    <Title img={iconCandidatos} title="Candidatos da vaga" />
                    <div className="centro">
                        <div className="candidatos">
                            <div className="candidato">
                                <p>João Santos</p>
                                <Button id="btnAzul" variant="outline-primary">Ver Perfil</Button>
                                <Button id="btnVermelhoPreenchido" variant="danger">Convidar para entrevista</Button>
                            </div>
                            <div className="candidato">
                                <p>João Santos</p>
                                <Button id="btnAzul" variant="outline-primary">Ver Perfil</Button>
                                <Button id="btnVermelhoPreenchido" variant="danger">Convidar para entrevista</Button>
                            </div>
                            <div className="candidato">
                                <p>João Santos</p>
                                <Button id="btnAzul" variant="outline-primary">Ver Perfil</Button>
                                <Button id="btnVermelhoPreenchido" variant="danger">Convidar para entrevista</Button>
                            </div>
                            <div className="candidato">
                                <p>João Santos</p>
                                <Button id="btnAzul" variant="outline-primary">Ver Perfil</Button>
                                <Button id="btnVermelhoPreenchido" variant="danger">Convidar para entrevista</Button>
                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </div>
        </div>
    )
}

export default Candidatos;