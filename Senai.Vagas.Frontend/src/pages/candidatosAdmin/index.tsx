import React from 'react';
import { Button, Table } from 'react-bootstrap';
import Footer from '../../components/footer/index';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title/index';
import iconCandidatos from '../../assets/images/IconCandidatos.png'

import '../../assets/styles/global.css';
import '../candidatosAdmin/style.css';

function CandidatosAdmin() {
    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName="{Nome de usuário}" />
                <div className="content-child">

                    <Title img={iconCandidatos} title="Candidatos da vaga" />
                    <div className="table">
                        <div className="barraSuperior">
                            <div>
                                <h3>Lista de Candidatos</h3>
                            </div>
                        </div>
                        <Table responsive striped bordered hover>
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Nome</th>
                                    <th>Email</th>
                                    <th>Convite de entrevista</th>
                                    <th>Ações</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>Mark Zukenberg</td>
                                    <td>TioMark@gmail.com</td>
                                    <td>Sim</td>
                                    <td id="linhaBotoes">
                                        <Button id="btnPerfil" variant="outline-primary">Ver Perfil</Button>
                                        <Button id="btnRemover" variant="danger">Remover Inscrição</Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>Mark Zukenberg</td>
                                    <td>TioMark@gmail.com</td>
                                    <td>Sim</td>
                                    <td id="linhaBotoes">
                                        <Button id="btnPerfil" variant="outline-primary">Ver Perfil</Button>
                                        <Button id="btnRemover" variant="danger">Remover Inscrição</Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>Mark Zukenberg</td>
                                    <td>TioMark@gmail.com</td>
                                    <td>Nao</td>
                                    <td id="linhaBotoes">
                                        <Button id="btnPerfil" variant="outline-primary">Ver Perfil</Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>Mark Zukenberg</td>
                                    <td>TioMark@gmail.com</td>
                                    <td>Sim</td>
                                    <td id="linhaBotoes">
                                        <Button id="btnPerfil" variant="outline-primary">Ver Perfil</Button>
                                        <Button id="btnRemover" variant="danger">Remover Inscrição</Button>
                                    </td>
                                </tr>
                            </tbody>
                        </Table>
                    </div>
                </div>
                <Footer />
            </div>
        </div>
    )
}

export default CandidatosAdmin;