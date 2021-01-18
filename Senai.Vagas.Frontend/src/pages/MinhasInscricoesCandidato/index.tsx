import React, { useEffect, useState } from 'react';

import './style.css';
import imgInscricao from '../../assets/images/IconInscricao.png';

import Header from '../../components/header';
import Footer from '../../components/footer';
import TopBar from '../../components/TopBar';
import Title from '../../components/title';
import CardCandidatoInscricao from '../../components/cardCandidato/cardCandidatoInscricao';

import { Form } from 'react-bootstrap';
import api from '../../services/api';
import jwt from '../../services/auth';

function MinhasInscricoesCandidato() {

    // const [nome, setNome] = useState('');
    // const [descricao, setDescricao] = useState('');

    const [Vagas, setVagas] = useState([]);
    

    const [statusVaga, setStatusVaga] = useState('');

    const [idStatusVaga, setIdStatusVaga] = useState('');

    const [statusVagas, setStatusVagas] = useState([]);

    let tokenDecoded = jwt();

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');



    useEffect(() => {
        Listar();
    }, []);

    const initListar = {
        headers: {
            'Content-Type': 'application/json'
        },
    }

    const Listar = async () => {

        if (tokenDecoded == null) {
            setAviso('Não foi possível carregar as informações do candidato')
            return showModalAviso();
        }

        await api
            .get(`Inscricoes/usuario/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                setVagas(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Vagas/buscar-statusvaga', initListar)
            .then(resp => {
                setStatusVagas(resp.data);
            })
            .catch(erro => console.log(erro))
    }

    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName={tokenDecoded.unique_name} />
                <div className="content-child">
                    
                    <div className="title-box">

                    <Title img={imgInscricao} title="Minhas Inscrições" />

                    <div className="buscarDiv">

                        <Form.Group controlId="status-vaga">
                            <Form.Control as="select" className="box-select" onChange={e => {
                                setIdStatusVaga(e.target.value)
                            }} value={idStatusVaga}>
                                <option value="0" disabled={true} selected>Filtre pelo Status da Vaga</option>
                                {
                                    statusVagas.map((item: any) => {
                                        return <option value={item.id}>{item.descricao}</option>
                                    })
                                }
                            </Form.Control>
                        </Form.Group>

                    </div>
                </div>


                    <div className="pagina-card">
                        {
                            Vagas.map((item: any) => {
                                return (
                                    <CardCandidatoInscricao
                                        nomeVaga={item.nomeVaga}
                                        descricao={item.descricaoVaga}
                                        vagaId={item.id}
                                        vagaAtiva={item.vagaAtiva}
                                        recebeuConvite={item.candidatoRecebeuConvite}
                                    />
                                )
                            })
                        }
                    </div>

                </div>
                <Footer />
            </div>
        </div>
    );
}

export default MinhasInscricoesCandidato;