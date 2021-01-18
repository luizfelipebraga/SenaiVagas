import React, { useState } from 'react';
import { Button, Form, ListGroup, Modal } from 'react-bootstrap';
import './style.css';
import jwt from '../../../services/auth';
import api from '../../../services/api';

interface CardProps {
    nomeVaga: string;
    descricao: string;
    recebeuConvite: Boolean;
    vagaAtiva: Boolean;
    vagaId: number;
}

const CardCandidatoInscricao: React.FC<CardProps> = ({ nomeVaga, descricao, recebeuConvite, vagaId, vagaAtiva }) => {
    //dados estáticos VerConvite
    const [dataHoraEntrevista, setDataHoraEntrevista] = useState('');
    const [nomeRua, setNomeRua] = useState('');
    const [infosComplementares, setInfosComplementares] = useState('');
    const [nomeBairro, setNomeBairro] = useState('');
    const [numero, setNumero] = useState('');

    // Dados estáticos VerMaisVaga
    const [nomeEmpresa, setNomeEmpresa] = useState('');
    const [experiencia, setExperiencia] = useState('');
    const [cargo, setCargo] = useState('');
    const [faixaSalarial, setFaixaSalarial] = useState('');

    const [nomeMunicipio, setNomeMunicipio] = useState('');
    const [ufSigla, setUfSigla] = useState<any>({});
    const [nomeUfSigla, setNomeUfSigla] = useState('');

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

    let tokenDecoded = jwt();

    const [showConvite, setShowConvite] = useState(false);
    const [showVerMais, setShowVerMais] = useState(false);

    const conviteClose = () => setShowConvite(false);
    const verMaisClose = () => setShowVerMais(false);

    const conviteShow = () => setShowConvite(true);
    const verMaisShow = () => setShowVerMais(true);

    const VerMais = async (vagaId: number) => {
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
                //console.log(resp.data)
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    const VerConvite = async (vagaId: number) => {
        //TODO: Testar se o get está recebendo as informacoes do convite.

        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .get(`ConvitesEntrevistas/convites/vaga/${vagaId}/usuario/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                setNomeEmpresa(resp.data.nomeEmpresa);
                setDataHoraEntrevista(new Date(resp.data.dataHoraEntrevista).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                setNomeRua(resp.data.rua);
                setNomeBairro(resp.data.bairro);
                setNumero(resp.data.numero);
                setNomeMunicipio(resp.data.municipio);
                setNomeUfSigla(resp.data.ufSigla);
                setInfosComplementares(resp.data.infosComplementares);
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })

    }

    const CancelarInscricao = async (vagaId: number) => {
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
        if (recebeuConvite) {
            return (
                <div className="botoes">
                    <Button id="btnAzul" variant="outline-primary" onClick={event => {
                        event.preventDefault();
                        verMaisShow();
                        VerMais(vagaId);
                    }}> Ver Mais</Button>
                    <Button id="btnVermelho" variant="danger" onClick={event => {
                        event.preventDefault();
                        VerConvite(vagaId);
                        conviteShow();
                    }}>Ver Convite</Button>
                </div>
            )
        } else {
            return (
                <div>
                    <div className="botoes">
                        <Button id="btnAzul" variant="outline-primary" onClick={event => {
                            event.preventDefault();
                            VerMais(vagaId);
                            verMaisShow();
                        }}>Ver Mais</Button>
                    </div>
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

            {/* MODAL Ver Mais */}
            <Modal size="lg" show={showVerMais} onHide={verMaisClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">{nomeVaga}</Modal.Title>
                </Modal.Header>

                <Modal.Body className="vermaisinscricao-container">
                    <ListGroup variant="flush" className="infos-perfil">
                        <ListGroup.Item><b>Nome Empresa:</b> {nomeEmpresa} </ListGroup.Item>
                        <ListGroup.Item><b>Experiência:</b> {experiencia} </ListGroup.Item>
                        <ListGroup.Item><b>Cargo:</b> {cargo} </ListGroup.Item>
                        <ListGroup.Item><b>Faixa Salarial:</b> {faixaSalarial} </ListGroup.Item>
                        <ListGroup.Item><b>Município:</b> {`${nomeMunicipio}, ${ufSigla.ufEstado} - ${ufSigla.ufSigla}`} </ListGroup.Item>
                        <ListGroup.Item><b>Data de Encerramento:</b> {dataEncerramento} </ListGroup.Item>

                        <ListGroup.Item><b>Areas recomendadas para a vaga:</b></ListGroup.Item>
                        <ul className="areas-recomendadas-inscricao">
                            {
                                areaVagaRecomendadas.map((area: any) => {
                                    return (
                                        <div className="areas-recomendadas-list">
                                            <li>{area.area.descricao}</li>
                                        </div>
                                    );
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
                    </ListGroup>
                </Modal.Body>

                <Modal.Footer>
                    <Form.Group controlId="btn-fechar" className="btn-fechar">
                        <Button variant="danger" onClick={verMaisClose}>Fechar</Button>
                    </Form.Group>

                    <Form.Group controlId="btn-cancel" className="btn-cancel-insc">
                        <Button variant="danger" onClick={event => {
                            event.preventDefault();
                            CancelarInscricao(vagaId)
                        }}>Cancelar Inscrição</Button>
                        
                    </Form.Group>
                </Modal.Footer>
            </Modal>
            {/* FIM do modal Ver Mais */}

            {/* INICIO DO MODAL VERCONVITE  */}

            <Modal size="lg" show={showConvite} onHide={conviteClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">Você foi convidado para uma entrevista de emprego!</Modal.Title>
                </Modal.Header>

                <Modal.Body className="verconvite-container">
                    
                    <ListGroup variant="flush" className="infos-gerais">
                        <ListGroup.Item><b>Nome Empresa:</b> {nomeEmpresa} </ListGroup.Item>
                        <ListGroup.Item><b>Data e Hora da Entrevista:</b> {dataHoraEntrevista} </ListGroup.Item>
                    </ListGroup>

                    <ListGroup variant="flush" className="infos-entrevista">
                        <ListGroup.Item><b>Rua:</b> {nomeRua} </ListGroup.Item>
                        <ListGroup.Item><b>Bairro:</b> {nomeBairro} </ListGroup.Item>
                        <ListGroup.Item><b>Número:</b> {numero} </ListGroup.Item>
                        <ListGroup.Item><b>Município:</b> {`${nomeMunicipio}, ${nomeUfSigla}`} </ListGroup.Item>

                        <Form.Group controlId="infosComplementares">
                            <div className="infosComplementares">
                                <Form.Label><b>Informações Complementares:</b></Form.Label>
                                <Form.Text className="text-muted">
                                    {infosComplementares}
                                </Form.Text>
                            </div>

                        </Form.Group>
                    </ListGroup>
                </Modal.Body>

                <Modal.Footer>
                    <Form.Group controlId="btn-fechar" className="btn-fechar">
                        <Button variant="danger" onClick={conviteClose}>Fechar</Button>
                    </Form.Group>
                </Modal.Footer>
            </Modal>


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

export default CardCandidatoInscricao;