import React, { useState, useEffect } from 'react';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Footer from '../../components/footer/index';
import { Form, Button, Modal, Spinner } from 'react-bootstrap';
import './style.css';
import iconVagas from '../../assets/images/IconVagas.png';
import Title from '../../components/title/index';
import api from '../../services/api';
import jwt from '../../services/auth';

interface formProgresSaved {
    nomeVaga: string,
    cargo: string,
    descricaoVaga: string,
    dataEncerramento: string,
    municipioId: string,
    experienciaId: string,
    faixaSalarialId: string,
    areasRecomendadas: string
}

function CadastrarVaga() {
    // Dados padrões
    const [experiencias, setExperiencias] = useState([]);
    const [municipios, setMunicipios] = useState([]);
    const [faixasSalariais, setFaixasSalariais] = useState([]);
    const [areas, setAreas] = useState([]);

    // usado para funcionar em conjunto com o filtro de municípios
    const [listMunicipios, setListMunicipios] = useState([]);

    // Apenas é necessário o ID para vincular vaga com os dados abaixo
    const [idExperiencia, setIdExperiencia] = useState('');
    const [idMunicipio, setIdMunicipio] = useState('');
    const [idFaixaSalarial, setIdFaixaSalarial] = useState('');

    // Dados restantes
    const [nomeVaga, setNomeVaga] = useState('');
    const [dataEncerramento, setDataEncerramento] = useState('');
    const [cargo, setCargo] = useState('');
    const [descricaoVaga, setDescricaoVaga] = useState('');

    // filtro para municipios
    const [filter, setFilter] = useState('');

    const [reloading, setReloading] = useState(false);

    // Lista com todas as áreas recomendadas que o usuário marcou (será preenchida quando o formulário for submetido)
    let areasRecomendadas: any = [];

    const [isLoading, setIsLoading] = useState(false);

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    const init = {
        headers: {
            'Content-Type': 'application/json'
        },
    }

    // useEffect para o carregamento de todos os inputs E recuperar o "saveProgress"
    useEffect(() => {
        Listar();
        let formSavedString = localStorage.getItem('progress-cadastro-vaga')

        let formSaved: formProgresSaved = formSavedString !== null ? JSON.parse(formSavedString) : null;

        if (formSaved !== null) {
            setNomeVaga(formSaved.nomeVaga);
            setCargo(formSaved.cargo);
            setDataEncerramento(formSaved.dataEncerramento);
            setDescricaoVaga(formSaved.descricaoVaga);
            setIdMunicipio(formSaved.municipioId);
            setIdFaixaSalarial(formSaved.faixaSalarialId);
            setIdExperiencia(formSaved.experienciaId);
        }
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

    // Lista todos os municípios, ou apenas os buscados pelo filtro
    const listarMunicipios = () => {
        return (
            listMunicipios.map((item: any) => {
                return <option value={item.id}>{item.descricao} / {item.ufSigla.ufSigla}</option>
            })
        );
    }

    // Requisições GET
    const Listar = async () => {
        await api
            .get('FaixasSalariais/Buscar-faixas-salariais', init)
            .then(resp => {
                setFaixasSalariais(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Experiencias/Buscar-tipos-experiencia', init)
            .then(resp => {
                setExperiencias(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Areas/Buscar-areas', init)
            .then(resp => {
                setAreas(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Enderecos/buscar-municipios', init)
            .then(resp => {
                setListMunicipios(resp.data);
                setMunicipios(resp.data);
                setIdMunicipio(resp.data[0].id)
            })
            .catch(erro => console.log(erro))
    }

    // Salvamento de progresso em localStorage
    const SaveProgress = () => {
        PreencherAreasRecomendadas()
        const form = {
            nomeVaga: nomeVaga,
            cargo: cargo,
            descricaoVaga: descricaoVaga,
            dataEncerramento: dataEncerramento,
            municipioId: idMunicipio,
            faixaSalarialId: idFaixaSalarial,
            experienciaId: idExperiencia,
        }

        localStorage.setItem('progress-cadastro-vaga', JSON.stringify(form));
    }

    // Cadastra uma vaga
    const cadastrarVaga = async () => {
        let tokenDecoded = jwt();

        if (tokenDecoded == null) {
            setAviso('Não foi possível recuperar dados do usuário para cadastrar a vaga');
            setIsLoading(false);
            return showModalAviso();
        }

        PreencherAreasRecomendadas()
        const form = {
            usuarioId: parseInt(tokenDecoded.jti),
            nomeVaga: nomeVaga,
            cargo: cargo,
            descricaoVaga: descricaoVaga,
            dataEncerramento: dataEncerramento,

            municipio: {
                id: parseInt(idMunicipio),
                descricao: '',
                ufSigla: {
                    id: 0,
                    ufEstado: '',
                    ufSigla: ''
                }
            },

            tipoExperiencia: {
                id: parseInt(idExperiencia),
                descricao: ''
            },

            faixaSalarial: {
                id: parseInt(idFaixaSalarial),
                descricao: ''
            },

            areasRecomendadas: areasRecomendadas
        }

        // Validação básica
        if (form.nomeVaga.length === 0 ||
            form.cargo.length === 0 ||
            form.descricaoVaga.length === 0 ||
            form.dataEncerramento.length === 0 ||
            form.municipio.id === 0 ||
            form.tipoExperiencia.id === 0 ||
            form.faixaSalarial.id === 0 ||
            form.areasRecomendadas.length === 0) {
            setAviso('Todos os campos são obrigatórios e devem ser preenchidos')
            setIsLoading(false);
            return showModalAviso();
        }

        await api
            .post('Vagas/cadastrar', form, init)
            .then(resp => {
                setAviso(resp.data)
                localStorage.removeItem('progress-cadastro-vaga');
                setReloading(true);
                return showModalAviso();
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    setReloading(false);
                    return showModalAviso();
                }
            })
    }

    // Busca todos os checkbox de áreas marcados
    const PreencherAreasRecomendadas = () => {
        // Adquire todos os checkbox's marcados (checked's)
        document.getElementsByName('areas').forEach((area: any) => {
            // Verifica se o componente foi 'checkado'
            if (area.checked) {

                // Monta o objeto
                let areaForm = {
                    id: area.id,
                    descricao: ''
                }

                // Adiciona na lista
                areasRecomendadas.push(areaForm);
            }
        })
    }

    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName="{Nome de usuário}" />
                <div className="content-child">

                    <Title img={iconVagas} title="Cadastrar Vaga" />

                    <Form className="form-container" onSubmit={event => {
                        event.preventDefault();
                        cadastrarVaga();
                        setIsLoading(true);
                    }}>

                        <Form.Group controlId="nome-vaga">
                            <Form.Label>Nome da Vaga:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => {
                                setNomeVaga(e.target.value)
                                SaveProgress();
                            }} value={nomeVaga} />
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="experiencia">
                            <Form.Label>Experiência:</Form.Label>
                            <Form.Control as="select" onChange={e => {
                                setIdExperiencia(e.target.value)
                                SaveProgress();
                            }}>
                                <option value="0" disabled={true} selected>Selecione uma experiência</option>
                                {
                                    experiencias.map((item: any) => {
                                        return <option value={item.id}>{item.descricao}</option>
                                    })
                                }
                            </Form.Control>
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="cargo">
                            <Form.Label>Cargo:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => {
                                setCargo(e.target.value)
                                SaveProgress()
                            }} value={cargo} />
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="municipios">
                            <Form.Label>Local / Município da Vaga:</Form.Label>
                            <Form.Control type="text" placeholder="Busque o munícipio" onKeyUp={(e: any) => setFilter(e.currentTarget.value)} />
                            <Form.Control as="select" onChange={e => {
                                setIdMunicipio(e.target.value)
                                SaveProgress();
                            }} value={idMunicipio}>
                                <option value="0" disabled={true} selected>Selecione um município</option>
                                {
                                    listarMunicipios()
                                }
                            </Form.Control>
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="faixas-salariais">
                            <Form.Label>Faixas Salariais:</Form.Label>
                            <Form.Control as="select" onChange={e => {
                                setIdFaixaSalarial(e.target.value)
                                SaveProgress();
                            }}>
                                <option value="0" disabled={true} selected>Selecione uma experiência</option>
                                {
                                    faixasSalariais.map((item: any) => {
                                        return <option value={item.id}>{item.descricao}</option>
                                    })
                                }
                            </Form.Control>
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="data-encerramento">
                            <Form.Label>Data de Encerramento:</Form.Label>
                            <Form.Control type="date" placeholder="" onChange={e => {
                                setDataEncerramento(e.target.value)
                                SaveProgress();
                            }} value={dataEncerramento} />
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="areas-recomendadas">
                            <Form.Label>Áreas Recomendadas:</Form.Label>
                            {areas.map((area: any) => {
                                return (
                                    <Form.Check
                                        custom
                                        label={area.descricao}
                                        type="checkbox"
                                        name="areas"
                                        id={area.id}
                                        value={area.descricao}
                                    />
                                );
                            })}
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="descricao-vaga">
                            <Form.Label>Descricao da Vaga</Form.Label>
                            <Form.Control as="textarea" rows={3} onChange={e => {
                                setDescricaoVaga(e.target.value)
                                SaveProgress();
                            }} value={descricaoVaga} />
                        </Form.Group>

                        <Button id="btn-cadastrar" size="lg" block type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Cadastrar Vaga'}
                        </Button>
                    </Form>

                    {/* MODAL de Aviso */}
                    <Modal
                        show={showAviso}
                        onHide={() => {
                            hideModalAviso();
                            setAviso('');
                            setIsLoading(false);
                            if (reloading) window.location.reload();
                        }}>
                        <Modal.Header closeButton>
                            <Modal.Title>Aviso</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <p>{aviso}</p>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="primary" id="btn-candidato-enviar" onClick={() => {
                                setIsLoading(false);
                                hideModalAviso();
                                setAviso('');
                                if (reloading) window.location.reload();
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

export default CadastrarVaga;