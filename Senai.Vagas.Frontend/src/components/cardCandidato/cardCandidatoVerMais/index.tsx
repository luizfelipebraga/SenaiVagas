import React, { useState } from 'react';
import { Button, Form, ListGroup, Modal } from 'react-bootstrap';
import './style.css';
import jwt from '../../../services/auth';
import api from '../../../services/api';
import ModalText from '../../modal/text';

interface CardProps {
    nomeVaga: string;
    descricao: string;
    vagaAtiva: Boolean;
    vagaId: number;
}

const CardCandidatoVerMais: React.FC<CardProps> = ({ nomeVaga, descricao, vagaId, vagaAtiva }) => {

    //token do usuario
    let tokenDecoded = jwt();

    // Dados estáticos
    const [nomeEmpresa, setNomeEmpresa] = useState('');
    const [tipoExperiencia, setExperiencia] = useState('');
    const [cargo, setCargo] = useState('');
    const [faixaSalarial, setFaixaSalarial] = useState('');

    const [nomeMunicipio, setNomeMunicipio] = useState('');
    const [ufSigla, setUfSigla] = useState<any>({});

    const [dataEncerramento, setDataEncerramento] = useState('');
    const [areaVagaRecomendadas, setAreaVagaRecomendadas] = useState([]);

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const VerMais = async (vagaId: number) => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível carregar as informações do candidato')
            return showModalAviso();
        }

        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .get(`Vagas/buscar/vaga/${vagaId}`, initListar)
            .then(resp => {
                setNomeEmpresa(resp.data.nomeEmpresa);
                setExperiencia(resp.data.tipoExperiencia.descricao);
                setCargo(resp.data.cargo);
                setFaixaSalarial(resp.data.faixaSalarial.descricao);
                setNomeMunicipio(resp.data.municipio.descricao);
                setUfSigla(resp.data.municipio.ufSigla);
                setDataEncerramento(new Date(resp.data.dataEncerramento).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                setAreaVagaRecomendadas(resp.data.areaVagaRecomendadas);
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    const FazerInscricao = async (vagaId: number) => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível carregar as informações do candidato')
            return showModalAviso();
        }

        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`Inscricoes/vaga/${vagaId}/usuario/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                if (resp.status === 200) {
                    window.location.reload();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    return showModalAviso();
                }
            })
    }

    const Botoes = (vagaId: number) => {
        if (vagaAtiva) {
            return (
                <div>
                    <div className="botoes">
                        <Button id="btnAzul" variant="outline-primary" onClick={event => {
                            event.preventDefault();
                            VerMais(vagaId);
                            handleShow();
                        }}>Ver Mais</Button>
                    </div>

                    <Modal size="lg" show={show} onHide={handleClose}>
                        <Modal.Header>
                            <Modal.Title className="title-modal">{nomeVaga}</Modal.Title>
                        </Modal.Header>

                        <Modal.Body className="vermaisvaga-container">
                            <ListGroup.Item><b>Nome Empresa:</b> {nomeEmpresa} </ListGroup.Item>
                            <ListGroup.Item><b>Experiência:</b> {tipoExperiencia} </ListGroup.Item>
                            <ListGroup.Item><b>Cargo:</b> {cargo} </ListGroup.Item>
                            <ListGroup.Item><b>Faixa Salarial:</b> {faixaSalarial} </ListGroup.Item>
                            <ListGroup.Item><b>Município:</b> {`${nomeMunicipio}, ${ufSigla.ufEstado} - ${ufSigla.ufSigla}`} </ListGroup.Item>
                            <ListGroup.Item><b>Data de Encerramento:</b> {dataEncerramento} </ListGroup.Item>
                            <ListGroup.Item><b>Áreas recomendadas para a vaga:</b> </ListGroup.Item>
                            <ul className="areaVagaRecomendadas">
                                {
                                    areaVagaRecomendadas.map((area: any) => {
                                        return (
                                            <div className="list-areaRecomendada">
                                                <li>{area.area.descricao}</li>
                                            </div>
                                        )
                                    })
                                }
                            </ul>

                            <Form.Group controlId="descricao">
                                <div className="descricao">
                                    <Form.Label><b>Descrição da Vaga:</b></Form.Label>
                                    <Form.Text className="text-muted">
                                        {descricao}
                                    </Form.Text>
                                </div>
                            </Form.Group>
                        </Modal.Body>

                        <Modal.Footer>
                            <Form.Group controlId="btn-fechar" className="btn-fechar">
                                <Button variant="danger" onClick={handleClose}>Fechar</Button>
                            </Form.Group>

                            <Form.Group controlId="btn-insc" className="btn-inscrever">
                                <Button variant="danger" onClick={event => {
                                    event.preventDefault();
                                    FazerInscricao(vagaId);
                                }}>Fazer Inscrição</Button>
                            </Form.Group>
                        </Modal.Footer>
                    </Modal>
                </div>
            )
        }
    }

    return (
        <div className="cardteste">
            <div className="titulo">
                <h4>{nomeVaga}</h4>

                <ul>
                    {
                        vagaAtiva ? <li id="vagaAtiva">Vaga ativa</li> : <li id="vagaEncerrada">Vaga Encerrada</li>
                    }
                </ul>
            </div>
            <div className="textos">
                <p>{descricao.length > 300 ? descricao.substring(0, 300) + " [...]" : descricao}</p>
            </div>
            {
                Botoes(vagaId)
            }

            {/* MODAL de Aviso */}
            <Modal
                show={showAviso}
                onHide={hideModalAviso}>
                <Modal.Header>
                    <Modal.Title>Aviso</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>{aviso}</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="primary" id="btn-candidato-enviar" onClick={() => {
                        hideModalAviso();
                        setAviso('');
                    }}>
                        Fechar
                            </Button>
                </Modal.Footer>
            </Modal>
            {/* FIM do modal de Aviso */}
        </div>
    )
}

export default CardCandidatoVerMais;