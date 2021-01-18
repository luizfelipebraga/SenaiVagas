import React, { useEffect, useState } from 'react';
import { Form, FormControl, InputGroup, Modal } from 'react-bootstrap';

import './style.css';
import imgFilter from '../../assets/images/IconFiltro.png';

import Header from '../../components/header';
import Footer from '../../components/footer';
import TopBar from '../../components/TopBar';
import Button from 'react-bootstrap/esm/Button';
import CardCandidatoVerMais from '../../components/cardCandidato/cardCandidatoVerMais/index';
import api from '../../services/api';
import jwt from '../../services/auth';

function HomeCandidato() {
    //dados Alteráveis
    const [palavraChave, setPalavraChave] = useState('')

    // Variavel com matriz de vagas, com o método que atualizará a lista
    const [vagas, updateVagas] = useState([]);

    const [Vagas, setVagas] = useState([]);

    //dados Estáticos
    const [municipios, setMunicipios] = useState([]);
    const [faixasSalariais, setFaixasSalariais] = useState([]);
    const [areasRecomendadas, setAreasRecomendadas] = useState([]);

    const [listMunicipios, setListMunicipios] = useState([]);

    const [idAreaRecomendada, setIdAreaRecomendada] = useState('');
    const [idMunicipio, setIdMunicipio] = useState('');
    const [idFaixaSalarial, setIdFaixaSalarial] = useState('');

    // filtro para municipios
    const [filter, setFilter] = useState('');
    //filtro para vagas
    const [vagafilter, setVagaFilter] = useState('');

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);


    //Modal filtro

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    let tokenDecoded = jwt();

    // Listar as vagas pela área de interesse de usuário e completa campos do filtro
    useEffect(() => {
        ListarVaga();
    }, []);

    // useEffect para o filtro de municípios
    useEffect(() => {
        // Filter na lista de filmes
        let municipiosFilter = municipios.filter((municipio: any) => {
            // Caso o filtro estiver vazio, retorna a lista inteira de filmes
            if (filter.length === 0)
                return municipios;

            // Retorna todos os filmes com nomes semelhantes ao filtro digitado
            return municipio.descricao.toLowerCase().includes(filter.toLowerCase());
        })

        setListMunicipios(municipiosFilter);

        // Caso filtre apenas um munícipio, adiciona o id do municipio
        if (municipiosFilter.length === 1) {
            municipiosFilter.forEach((muni: any) => {
                setIdMunicipio(muni.id);
            })
        }
    }, [filter])

    const listarMunicipios = () => {
        return (
            listMunicipios.map((item: any) => {
                return <option value={item.id}>{item.descricao} / {item.ufSigla.ufSigla}</option>
            })
        );
    }


    const ListarVaga = async () => {

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
            .get(`Vagas/buscar/area-candidato/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                setVagas(resp.data);
                console.log(resp.data)
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })

        await api
            .get('FaixasSalariais/Buscar-faixas-salariais', initListar)
            .then(resp => {
                setFaixasSalariais(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Areas/Buscar-areas', initListar)
            .then(resp => {
                setAreasRecomendadas(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Enderecos/buscar-municipios', initListar)
            .then(resp => {
                setListMunicipios(resp.data);
                setMunicipios(resp.data);
                setIdMunicipio(resp.data[0].id)
            })
            .catch(erro => console.log(erro))
    }

    //modal filtrar vagas
    const modalFiltrarVaga = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível filtrar')
            return showModalAviso();
        }
        //TODO: Fazer depois de entregue o mínimo entregável
    }

    const buscarVagas = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível recuperar as informações do candidato para procurar vagas.')
            return showModalAviso();
        }

        const form = {
            filter: vagafilter
        }

        if (form.filter.length === 0) {
            setAviso('O campo de busca não pode estar vazio.')
            return showModalAviso();
        }


        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`Vagas/buscar/vaga-filtro/usuario/${tokenDecoded.jti}`, form, initListar)
            .then(resp => {
                if (resp.status === 200) {
                    setVagas(resp.data);
                }
            })
            .catch(error => {
                console.log(error.response)
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName={tokenDecoded.unique_name} />
                <div className="content-child">

                    <InputGroup className="mb-3">

                        <div className="box-filtro">
                            <div className="filtro">
                                <img id="imgFiltro" src={imgFilter} alt="imagem de filtro de buscas" onClick={handleShow} />
                                <h4 onClick={handleShow}>Filtrar</h4>
                            </div>
                            <div className="buscar">
                                <FormControl placeholder="Busque vagas" aria-label="Busque vagas" aria-describedby="basic-addon2" className="inputBuscar"
                                    onChange={e => setVagaFilter(e.target.value)} />
                                <InputGroup.Append>
                                    <Button id="btn-filtrar" name="Vagafilter" onClick={event => {
                                        event.preventDefault();
                                        buscarVagas();
                                    }}>Buscar</Button>
                                </InputGroup.Append>
                            </div>
                        </div>

                        <Modal show={show} onHide={handleClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Refine sua Busca</Modal.Title>
                            </Modal.Header>

                            <Modal.Body className="modal-filter-select">

                                <Form.Group controlId="palavras-chave">
                                    <Form.Label>Palavra Chave:</Form.Label>
                                    <Form.Control type="text" />
                                </Form.Group>

                                <Form.Group controlId="municipios">
                                    <Form.Label>Escolha um Municipio ou digite:</Form.Label>
                                    <Form.Control type="text" placeholder="Busque o munícipio" onKeyUp={(e: any) => setFilter(e.currentTarget.value)} />
                                    <Form.Control as="select" className="box-select" onChange={e => {
                                        setIdMunicipio(e.target.value)
                                    }} value={idMunicipio}>
                                        <option value="0" disabled={true} selected>Selecione um município</option>
                                        {
                                            listarMunicipios()
                                        }
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group controlId="faixas-salariais">
                                    <Form.Label>Faixa Salarial:</Form.Label>
                                    <Form.Control as="select" className="box-select" onChange={e => {
                                        setIdFaixaSalarial(e.target.value)
                                    }}>
                                        <option value="0" disabled={true} selected>Filtre pelo Salário</option>
                                        {
                                            faixasSalariais.map((item: any) => {
                                                return <option value={item.id}>{item.descricao}</option>
                                            })
                                        }
                                    </Form.Control>
                                </Form.Group>

                                <Form.Group controlId="areas-interesse">
                                    <Form.Label>Área de Interesse:</Form.Label>
                                    <Form.Control as="select" className="box-select" onChange={e => {
                                        setIdAreaRecomendada(e.target.value)
                                    }} value={idAreaRecomendada}>
                                        <option value="0" disabled={true} selected>Filtre pela Área</option>
                                        {
                                            areasRecomendadas.map((item: any) => {
                                                return <option value={item.id}>{item.descricao}</option>
                                            })
                                        }
                                    </Form.Control>
                                </Form.Group>

                            </Modal.Body>

                            <Modal.Footer>
                                <Button id="btn-filtrar-modal" size="lg" block type="submit">Filtrar</Button>
                            </Modal.Footer>

                        </Modal>
                    </InputGroup>

                    <div className="title">
                        <p>Vagas recomendadas pra você:</p>
                    </div>

                    <div className="pagina">
                        {
                            Vagas.map((item: any) => {
                                return (
                                    <CardCandidatoVerMais
                                        nomeVaga={item.nomeVaga}
                                        descricao={item.descricaoVaga}
                                        vagaId={item.id}
                                        vagaAtiva={item.vagaAtiva}
                                    />
                                )
                            })
                        }
                    </div>

                    {/* MODAL de Aviso */}
                    <Modal
                        show={showAviso}
                        onHide={hideModalAviso}>
                        <Modal.Header closeButton>
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
                <Footer />
            </div>
        </div>
    );
}

export default HomeCandidato;