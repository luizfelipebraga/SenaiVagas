import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Spinner, ListGroup } from 'react-bootstrap';
import jwt from '../../services/auth';
import api from '../../services/api';
import './style.css';
import '../../assets/styles/global.css'
interface CardProps {
    nomeVaga: string;
    descricao: string;
    vagaAtiva: Boolean;
    vagaId: number;
}


const CardEmpresa: React.FC<CardProps> = ({ nomeVaga, descricao, vagaAtiva, vagaId }) => {
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
    const [nomeDaVaga, setNomeVaga] = useState('');
    const [dataEncerramento, setDataEncerramento] = useState('');
    const [cargo, setCargo] = useState('');
    const [descricaoVaga, setDescricaoVaga] = useState('');

    // Para funcionar o convite de entrevista
    const [usuarioCandidatoAlunoId, setUsuarioCandidatoId] = useState('');
    const [ruaConvite, setRuaConvite] = useState('');
    const [bairroConvite, setBairroConvite] = useState('');
    const [numeroConvite, setNumeroConvite] = useState('');
    const [infoComplementarConvite, setInfoComplementarConvite] = useState('');
    const [dataHoraEntrevistaConvite, setDataHoraEntrevistaConvite] = useState('');
    const [idMunicipioConvite, setIdMunicipioConvite] = useState('');

    // Para puxar coisas do ver perfil
    const [nomeCompleto, setNomeCompleto] = useState('');
    const [email, setEmail] = useState('');
    const [curso, setCurso] = useState('');
    const [rma, setRma] = useState('');
    const [sobreOCandidato, setSobreOCandidato] = useState('');
    const [linkExterno, setLinkExterno] = useState('');
    const [sexo, setSexo] = useState('');
    const [dataNascimento, setDataNascimento] = useState('');
    const [dataMatricula, setDataMatricula] = useState('');
    const [termo, setTermo] = useState('');


    // Reativar Vaga
    const [novaDataEncerramento, setNovaDataEncerramento] = useState('');

    // filtro para municipios
    const [filter, setFilter] = useState('');

    const [reloading, setReloading] = useState(false);

    // Lista com todas as áreas recomendadas que o usuário marcou (será preenchida quando o formulário for submetido)
    let areasRecomendadas: any = [];

    const [isLoading, setIsLoading] = useState(false);

    const [Inscricoes, setInscricoes] = useState([]);

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    // Outros Modais
    const [showInscritos, setShowInscritos] = useState(false);
    const [showEditar, setEditarShow] = useState(false);
    const [showConvite, setConviteShow] = useState(false);
    const [showReativarVaga, setReativarVagaShow] = useState(false);
    const [showVerPerfil, setVerPerfilShow] = useState(false);

    const inscritosClose = () => setShowInscritos(false);
    const editarClose = () => setEditarShow(false);
    const conviteClose = () => setConviteShow(false);
    const reativarVagaClose = () => setReativarVagaShow(false);
    const verPerfilClose = () => setVerPerfilShow(false);

    const inscritosShow = () => setShowInscritos(true);
    const editarShow = () => setEditarShow(true);
    const conviteShow = () => setConviteShow(true);
    const reativarVagaShow = () => setReativarVagaShow(true);
    const verPerfilShow = () => setVerPerfilShow(true);

    let tokenDecoded = jwt();

    const init = {
        headers: {
            'Content-Type': 'application/json'
        },
    }

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
                setIdMunicipioConvite(muni.id);
            })
        }
    }, [filter])

    // useEffect para listar informações padrões do Editar Vaga
    useEffect(() => {
        Listar();
    }, []);

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
            })
            .catch(erro => console.log(erro))
    }

    const VerInscritos = async (vagaId: number) => {
        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .get(`Inscricoes/vaga/${vagaId}`, init)
            .then(resp => {
                setInscricoes(resp.data);
                //console.log(resp.data)
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })


    }

    const EncerrarVaga = async (vagaId: number) => {
        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .put(`Vagas/encerrar/vaga/${vagaId}/usuario/${tokenDecoded.jti}`, init)
            .then(resp => {
                if (resp.status === 200) {
                    setAviso('Vaga encerrada!');
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso('Houve um erro interno, nao conseguimos encerrar a vaga!')
                    return showModalAviso();
                }
            })
    }

    const ReativarVaga = async (vagaId: number) => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível recuperar dados do usuário para reativar a vaga.');
            setIsLoading(false);
            return showModalAviso();
        }

        const form = {
            usuarioId: tokenDecoded.jti,
            dataEncerramento: novaDataEncerramento,
        }

        if (form.dataEncerramento.length === 0) {
            setAviso('A nova data de encerramento deve ser preenchida.');
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`Vagas/reativar/${vagaId}`, form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setAviso(resp.data);
                    reativarVagaClose();
                    return showModalAviso();
                }
            })
            .catch(error => {
                setIsLoading(false);
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    // Convite de entrevista. obs: precia vir com id de candidato como string
    const ConvidarEntrevista = async (vagaId: number, usuarioCandidatoId: string) => {
        const form = {
            rua: ruaConvite,
            bairro: bairroConvite,
            numero: numeroConvite,
            infosComplementares: infoComplementarConvite,
            municipio: {
                id: parseInt(idMunicipioConvite),
                descricao: '',
                ufSigla: {
                    id: 0,
                    ufEstado: '',
                    ufSigla: ''
                }
            },
            dataHoraEntrevista: dataHoraEntrevistaConvite,
        }

        console.log(form)

        // Validação básica
        if (form.rua.length === 0 ||
            form.bairro.length === 0 ||
            form.numero.length === 0 ||
            form.municipio.id === 0 ||
            form.dataHoraEntrevista.length === 0) {
            setAviso('Todos os campos são obrigatórios e devem ser preenchidos')
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`ConvitesEntrevistas/cadastrar/vaga/${vagaId}/usuario-candidato/${usuarioCandidatoId}`, form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setReloading(true);
                    conviteClose();
                    setAviso(resp.data)
                    return showModalAviso();
                }
            })
            .catch(error => {
                setIsLoading(false);
                if (error.response) {
                    setAviso(error.response.data);
                    return showModalAviso();
                }
            })
    }

    const VerPerfil = async (usuarioCandidatoId: number) => {
        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }
        await api
            .get(`UsuariosCandidatos/perfil/usuarioCandidato/${usuarioCandidatoId}`, init)
            .then(resp => {
                if (resp.status === 200) {
                    setNomeCompleto(resp.data.nomeCompleto);
                    setEmail(resp.data.email);
                    setRma(resp.data.rma);
                    setCurso(resp.data.tipoCurso.descricao);
                    setLinkExterno(resp.data.perfilCandidato.linkExterno);
                    setSobreOCandidato(resp.data.perfilCandidato.sobreOCandidato);
                    setSexo(resp.data.sexo);
                    setDataMatricula(resp.data.dataMatricula);
                    setDataNascimento(resp.data.dataNascimento);
                    setTermo(resp.data.termoOuEgressoAluno.descricao);

                    // Converte data para horário local (sem horas)
                    setDataNascimento(new Date(resp.data.dataNascimento).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                    setDataMatricula(new Date(resp.data.dataMatricula).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso('Houve um erro interno, nao conseguimos ver o perfil!')
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

    const PreencherAreasMarcadas = (areasDbs: any) => {
        // Marca todas as áreas recomendadas da vaga já cadastradas, vinda do banco de dados
        // OBS. manter comparação com apenas 2 iguais ( == )
        document.getElementsByName('areas').forEach((area: any) => {
            areasDbs.forEach((areaDb: any) => {
                // Verifica se o ID da área está no banco para marcar
                if (area.id == areaDb.area.id) {
                    area.checked = true;
                }
            })
        })
    }

    const BuscarVaga = async (vagaId: number) => {
        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .get(`Vagas/buscar/vaga/${vagaId}`, initListar)
            .then(resp => {
                setIdExperiencia(resp.data.tipoExperiencia.id);
                setCargo(resp.data.cargo);
                setNomeVaga(resp.data.nomeVaga)
                setIdFaixaSalarial(resp.data.faixaSalarial.id);
                setIdMunicipio(resp.data.municipio.id);
                setDataEncerramento((resp.data.dataEncerramento).replace('T00:00:00', ''));
                setDescricaoVaga(resp.data.descricaoVaga);
                PreencherAreasMarcadas(resp.data.areaVagaRecomendadas);
                console.log(resp.data)
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    const EditarVaga = async (vagaId: number) => {
        PreencherAreasRecomendadas()
        const form = {
            usuarioId: parseInt(tokenDecoded.jti),
            nomeVaga: nomeDaVaga,
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
            .put(`Vagas/alterar/vaga/${vagaId}`, form, init)
            .then(resp => {
                setAviso(resp.data)
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

    const Botoes = (vagaId: number) => {
        if (vagaAtiva) {
            return (
                <div className="botoes">
                    <Button id="btnAzul" variant="outline-primary" onClick={event => {
                        event.preventDefault();
                        VerInscritos(vagaId);
                        inscritosShow();
                    }}> Ver Inscritos</Button>
                    <Button id="btnVermelhoPreenchido" variant="danger" onClick={event => {
                        event.preventDefault();
                        EncerrarVaga(vagaId);
                    }}>Encerrar Vaga</Button>
                    <Button id="btnAzul" variant="outline-primary" onClick={event => {
                        event.preventDefault();
                        BuscarVaga(vagaId);
                        editarShow();
                    }}>Editar Vaga</Button>
                </div>
            )
        } else {
            return (
                <div className="botoes">
                    <Button id="btnAzul" variant="outline-primary" onClick={event => {
                        event.preventDefault();
                        VerInscritos(vagaId);
                        inscritosShow();
                    }}>Ver Inscritos</Button>
                    <Button id="btnVermelho" variant="danger" onClick={event => {
                        event.preventDefault();
                        reativarVagaShow();
                    }}>Reativar Vaga</Button>
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

            {/* MODAL Ver Inscritos */}
            <Modal size="lg" show={showInscritos} onHide={inscritosClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">Inscritos da vaga</Modal.Title>
                </Modal.Header>

                <Modal.Body className="vercandidatos-container">
                    {
                        Inscricoes.length === 0 ? <b> Esta vaga não tem nenhum inscrito :( </b> :

                            Inscricoes.map((item: any) => {
                                return (

                                    <div className="container">
                                        <div className="row justify-content-md-center">
                                            <div className="col-5">
                                               {item.nomeAluno}
                                            </div>
                                            <div className="col-3">
                                            <Button id="btnAzul" variant="outline-primary" onClick={event => {
                                             event.preventDefault();
                                             VerPerfil(item.usuarioCandidatoAluno.id);
                                             verPerfilShow();
                                         }}>Ver Perfil</Button>
                                            </div>
                                            <div className="col-4">
                                            {item.recebeuConvite === false ? <Button id="btnVermelhoPreenchido" variant="danger" onClick={event => {
                                             event.preventDefault();
                                             setUsuarioCandidatoId(item.usuarioCandidatoAluno.id);
                                             inscritosClose();
                                             conviteShow();
                                         }}>Convidar para entrevista</Button> : <p>Convite ja enviado!</p>}
                                            </div>
                                        </div>
                                    </div>
                                )
                            })
                    }
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="danger" onClick={event => {
                        event.preventDefault();
                        inscritosClose();
                    }}>Fechar</Button>
                </Modal.Footer>
            </Modal>
            {/* FIM Modal Ver Inscritos */}

            {/* MODAL Reativar Vaga */}
            <Modal
                show={showReativarVaga}
                onHide={reativarVagaClose}>
                <Modal.Header>
                    <Modal.Title>Reativar Vaga</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="data-encerramento">
                            <Form.Label>Nova data de Encerramento:</Form.Label>
                            <Form.Control type="date" placeholder="" onChange={e => {
                                setNovaDataEncerramento(e.target.value)
                            }} value={novaDataEncerramento} />
                            <Form.Text>
                                Para reativar uma vaga será necessário que escolha uma nova data de encerramento.
                            </Form.Text>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={reativarVagaClose} id="btn-candidato-fechar">
                        Fechar
                    </Button>
                    <Button variant="primary" id="btn-candidato-enviar" onClick={event => {
                        event.preventDefault();
                        setIsLoading(true);
                        ReativarVaga(vagaId);
                    }}>
                        {isLoading ? <Spinner
                            as="span"
                            animation="border"
                            size="sm"
                            role="status"
                            aria-hidden="true"
                        /> : 'Reativar Vaga'}
                    </Button>
                </Modal.Footer>
            </Modal>
            {/* FIM do Modal Reativar Vaga */}

            {/* MODAL Convite Entrevista */}
            <Modal size="lg" show={showConvite} onHide={conviteClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">Criar um Convite de Entrevista</Modal.Title>
                </Modal.Header>

                <Modal.Body className="vercandidatos-container">
                    <Form className="form-container-editar" onSubmit={event => {
                        event.preventDefault();
                        ConvidarEntrevista(vagaId, usuarioCandidatoAlunoId);
                        setIsLoading(true);
                    }}>

                        <Form.Group controlId="nome-vaga">
                            <Form.Label>Rua:</Form.Label>
                            <Form.Control type="text" placeholder="Nome da rua" onChange={e => {
                                setRuaConvite(e.target.value)
                            }} value={ruaConvite} />
                        </Form.Group>

                        <Form.Group controlId="nome-vaga">
                            <Form.Label>Bairro:</Form.Label>
                            <Form.Control type="text" placeholder="Nome do bairro" onChange={e => {
                                setBairroConvite(e.target.value)
                            }} value={bairroConvite} />
                        </Form.Group>

                        <Form.Group controlId="nome-vaga">
                            <Form.Label>Número:</Form.Label>
                            <Form.Control type="text" placeholder="Número do local" onChange={e => {
                                setNumeroConvite(e.target.value)
                            }} value={numeroConvite} />
                        </Form.Group>

                        <Form.Group controlId="municipios">
                            <Form.Label>Município da entrevista:</Form.Label>
                            <Form.Control type="text" placeholder="Busque o munícipio" onKeyUp={(e: any) => setFilter(e.currentTarget.value)} />
                            <Form.Control as="select" onChange={e => {
                                setIdMunicipioConvite(e.target.value)
                            }} value={idMunicipioConvite}>
                                <option value="0" disabled={true} selected>Selecione um município</option>
                                {
                                    listarMunicipios()
                                }
                            </Form.Control>
                        </Form.Group>

                        <Form.Group controlId="data-encerramento">
                            <Form.Label>Data da entrevista:</Form.Label>
                            <Form.Control type="date" placeholder="" onChange={e => {
                                setDataHoraEntrevistaConvite(e.target.value)
                            }} value={dataHoraEntrevistaConvite} />
                        </Form.Group>

                        <Form.Group controlId="descricao-vaga">
                            <Form.Label>Informações complementares:</Form.Label>
                            <Form.Control as="textarea" rows={3} placeholder="Digite mais informações sobre a entrevista (opcional)" onChange={e => {
                                setInfoComplementarConvite(e.target.value)
                            }} value={infoComplementarConvite} />
                        </Form.Group>

                        <Button id="btn-cadastrar" size="lg" block type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Convidar para Entrevista'}
                        </Button>
                    </Form>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="danger" onClick={event => {
                        event.preventDefault();
                        conviteClose();
                    }}>Fechar</Button>
                </Modal.Footer>
            </Modal>
            {/* FIM Modal Convite Entrevista */}

            {/* MODAL Editar Vaga */}
            <Modal size="lg" show={showEditar} onHide={editarClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">Editar Vaga</Modal.Title>
                </Modal.Header>

                <Modal.Body className="vercandidatos-container">
                    <Form className="form-container-editar" onSubmit={event => {
                        event.preventDefault();
                        EditarVaga(vagaId);
                        setIsLoading(true);
                    }}>

                        <Form.Group controlId="nome-vaga">
                            <Form.Label>Nome da Vaga:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => {
                                setNomeVaga(e.target.value)
                            }} value={nomeDaVaga} />
                            <Form.Text>
                                Uma descrição sobre o campo
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="experiencia">
                            <Form.Label>Experiência:</Form.Label>
                            <Form.Control as="select" onChange={e => {
                                setIdExperiencia(e.target.value)
                            }} value={idExperiencia}>
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
                            }} value={idFaixaSalarial}>
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
                            }} value={descricaoVaga} />
                        </Form.Group>

                        <Button id="btn-cadastrar" size="lg" block type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Editar Vaga'}
                        </Button>
                    </Form>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="danger" onClick={event => {
                        event.preventDefault();
                        editarClose();
                    }}>Fechar</Button>
                </Modal.Footer>
            </Modal>
            {/* FIM Modal Editar Vaga */}

            {/* MODAL de ver perfil */}
            <Modal size="lg" show={showVerPerfil} onHide={verPerfilClose}>
                <Modal.Header>
                    <Modal.Title className="title-modal">Perfil</Modal.Title>
                </Modal.Header>

                <Modal.Body className="vermais-container">
                    <ListGroup variant="flush" className="infos-perfil">
                        <ListGroup.Item><b>Nome:</b> {nomeCompleto} </ListGroup.Item>
                        <ListGroup.Item><b>Email:</b> {email}</ListGroup.Item>
                        <ListGroup.Item><b>Sexo:</b> {sexo ? 'Masculino' : 'Feminino'} </ListGroup.Item>
                        <ListGroup.Item><b>Data de Nascimento:</b> {dataNascimento} </ListGroup.Item>
                        <ListGroup.Item><b>Curso:</b> {curso} </ListGroup.Item>
                        <ListGroup.Item><b>Termo ou Egresso:</b> {termo} </ListGroup.Item>
                        <ListGroup.Item><b>RMA:</b> {rma} </ListGroup.Item>
                        <ListGroup.Item><b>Data de Matrícula</b> {dataMatricula} </ListGroup.Item>

                        <Form.Group controlId="descricao">

                            <div className="descricao">
                                <Form.Label><b>Sobre o Candidato:</b></Form.Label>
                                <Form.Text className="text-muted">
                                    {sobreOCandidato}
                                </Form.Text>
                            </div>

                        </Form.Group>

                        <ListGroup.Item><b>Link Externo:</b> {linkExterno} </ListGroup.Item>
                    </ListGroup>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="danger" onClick={event => {
                        event.preventDefault();
                        verPerfilClose();
                    }}>Fechar</Button>
                </Modal.Footer>
            </Modal>
            {/* FIM modal ver perfil}

            {/* MODAL de Aviso */}
            <Modal
                show={showAviso}
                onHide={() => {
                    hideModalAviso();
                    setAviso('');
                    setIsLoading(false);
                    if (reloading) window.location.reload();
                }}>
                <Modal.Header>
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
    )
}

export default CardEmpresa;