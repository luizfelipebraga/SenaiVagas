import React, { useState, useEffect } from 'react';
import { Button, Form, InputGroup, FormControl, ListGroup, Modal, Spinner } from 'react-bootstrap';
import './style.css';
import iconPerfil from '../../assets/images/IconUsuario.png';
import Header from '../../components/header/index';
import Footer from '../../components/footer/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title';
import api from '../../services/api';
import jwt from '../../services/auth';

function MeuPerfil() {
    // Dados estáticos
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [curso, setCurso] = useState('');
    const [sexo, setSexo] = useState(true);
    const [termo, setTermo] = useState('');
    const [rma, setRma] = useState('');
    const [dataNascimento, setDataNascimento] = useState('');
    const [dataMatricula, setDataMatricula] = useState('');

    // Dados alteráveis
    const [sobre, setSobre] = useState('');
    const [sobreOld, setSobreOld] = useState('');
    const [linkExterno, setLinkExterno] = useState('');
    const [linkExternoOld, setLinkExternoOld] = useState('');

    // Configuração de Áreas de Interesse
    const [areas, setAreas] = useState([]);
    let areasInteresse: any = [];

    // Loading's
    const [isLoading, setIsLoading] = useState(false);

    // Váriavel booleana para o modal
    const [showConfigInteresse, setShowVagasInteresse] = useState(false);

    // Função para mostrar modal
    const showVagasInteresse = () => setShowVagasInteresse(true);

    // Função para esconder modal
    const hideVagasInteresse = () => setShowVagasInteresse(false);

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    let tokenDecoded = jwt();

    useEffect(() => {
        Listar();
    }, [])

    const initListar = {
        headers: {
            'Content-Type': 'application/json'
        },
    }

    // Requisições GET
    const Listar = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível carregar as informações do candidato')
            return showModalAviso();
        }

        await api
            .get('Areas/Buscar-areas', initListar)
            .then(resp => {
                setAreas(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get(`UsuariosCandidatos/perfil/usuario/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                setNome(resp.data.nomeCompleto);
                setEmail(resp.data.email);
                setCurso(resp.data.tipoCurso.descricao);
                setRma(resp.data.rma);
                setSexo(resp.data.sexo);
                setTermo(resp.data.termoOuEgressoAluno.descricao)
                setSobre(resp.data.perfilCandidato.sobreOCandidato)
                setSobreOld(resp.data.perfilCandidato.sobreOCandidato)
                setLinkExterno(resp.data.perfilCandidato.linkExterno)
                setLinkExternoOld(resp.data.perfilCandidato.linkExterno)

                // Converte data para horário local (sem horas)
                setDataNascimento(new Date(resp.data.dataNascimento).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                setDataMatricula(new Date(resp.data.dataMatricula).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                //console.log(resp.data)
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }

    const salvarPerfil = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível salvar o perfil do usuário')
            return showModalAviso();
        }

        const form = {
            linkExterno: linkExterno,
            sobreOCandidato: sobre
        }

        if (form.linkExterno === linkExternoOld && form.sobreOCandidato === sobreOld) {
            setAviso('Não há nenhuma alteração para salvar :)')
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .put(`UsuariosCandidatos/alterar/descricao/link/${tokenDecoded.jti}`, form, init)
            .then(resp => {
                if (resp.status === 200) {
                    setAviso(resp.data);
                    setLinkExterno(linkExterno)
                    setLinkExternoOld(linkExterno)
                    setSobre(sobre)
                    setSobreOld(sobre)
                    return showModalAviso();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    return showModalAviso();
                }
                // error.response adquire a resposta de erro da API
                // error.response.status (status do retorno)
                // error.response.data (dados retornados da API)
            })
    }

    const BuscarAreasInteresseCandidato = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível adquirir dados do usuário para configurar as áreas de interesse.')
            return showModalAviso();
        }

        await api
            .get(`UsuariosCandidatos/area-interesse/usuario/${tokenDecoded.jti}`, initListar)
            .then(resp => {
                PreencherAreasMarcadas(resp.data);
            })
            .catch(erro => console.log(erro))
    }

    const ConfigAreasInteresse = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível adquirir dados do usuário para configurar as áreas de interesse.')
            setIsLoading(false);
            return showModalAviso();
        }

        PreencherAreasRecomendadas();

        const form = {
            areasInteresse: areasInteresse
        }

        if (form.areasInteresse.length === 0) {
            setAviso('Marque ao menos uma área de interesse.')
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .put(`UsuariosCandidatos/usuario/${tokenDecoded.jti}`, form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setAviso(resp.data)
                    hideVagasInteresse();
                    return showModalAviso();
                }
            })
            .catch(error => {
                setIsLoading(false);
                if (error.response) {
                    setAviso(error.response.data);
                    return showModalAviso();
                }
                // error.response adquire a resposta de erro da API
                // error.response.status (status do retorno)
                // error.response.data (dados retornados da API)
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
                areasInteresse.push(areaForm);
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

    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName={tokenDecoded.unique_name} />

                <div className="content-child">

                    <Title img={iconPerfil} title="Meu Perfil" />

                    <div className="perfil-container">
                        <ListGroup variant="flush" className="infos-perfil">
                            <ListGroup.Item><b>Nome:</b> {nome} </ListGroup.Item>

                            <ListGroup.Item className="links"><b>Email:</b> {email} <p onClick={event => {
                                event.preventDefault()
                                //função de abrir modal
                            }}>Alterar Email</p></ListGroup.Item>

                            <ListGroup.Item className="links"><b>Senha:</b> ******* <p onClick={event => {
                                event.preventDefault()
                                //função de abrir modal
                            }}>Alterar Senha</p></ListGroup.Item>

                            <ListGroup.Item><b>Sexo:</b> {sexo ? 'Masculino' : 'Feminino'} </ListGroup.Item>
                            <ListGroup.Item><b>Data de Nascimento:</b> {dataNascimento} </ListGroup.Item>
                            <ListGroup.Item><b>Curso:</b> {curso} </ListGroup.Item>
                            <ListGroup.Item><b>Termo ou Egresso:</b> {termo} </ListGroup.Item>
                            <ListGroup.Item><b>RMA:</b> {rma} </ListGroup.Item>
                            <ListGroup.Item><b>Data de Matrícula / Renovação:</b> {dataMatricula} </ListGroup.Item>
                            <Button id="btn-vagasInteresse" onClick={event => {
                                event.preventDefault();
                                BuscarAreasInteresseCandidato();
                                showVagasInteresse();
                            }}>
                                Configurar Áreas de Interesse
                            </Button>
                        </ListGroup>


                        <Form className="form-box" onSubmit={event => {
                            event.preventDefault();
                            salvarPerfil();
                        }}>
                            <Form.Group controlId="descricao-candidato">
                                <Form.Label><b>Sobre o Candidato:</b></Form.Label>
                                <Form.Control as="textarea" rows={3} value={sobre} onChange={e => setSobre(e.target.value)} />
                                <Form.Text className="text-muted">
                                    Fale um pouco sobre você para as empresas!
                                </Form.Text>
                            </Form.Group>

                            <Form.Group controlId="link-candidato">
                                <Form.Label><b>Link de Portifólio/Currículo Virtual:</b></Form.Label>
                                <InputGroup className="mb-3">
                                    <FormControl aria-describedby="basic-addon3" value={linkExterno} onChange={e => setLinkExterno(e.target.value)} />
                                </InputGroup>
                                <Form.Text className="text-muted">
                                    Coloque aqui o link do seu portifólio, link de repositório no github, ou um currículo virtual.
                                </Form.Text>
                            </Form.Group>

                            <div className="btn">
                                <Button id="btn-salvar" size="lg" block type="submit">Salvar</Button>{' '}
                            </div>

                        </Form>

                    </div>

                    {/* MODAL Configurar Áreas de Interesse */}
                    <Modal
                        show={showConfigInteresse}
                        onHide={hideVagasInteresse}>
                        <Modal.Header>
                            <Modal.Title>Configurar Áreas de Interesse</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form>
                                <Form.Group controlId="areas-recomendadas">
                                    <Form.Label>Áreas de Interesse</Form.Label>
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
                                        Selecione todas as suas áreas de interesse para receber recomendações de vagas baseados em seu interesse.
                                    </Form.Text>
                                </Form.Group>
                            </Form>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={hideVagasInteresse} id="btn-vagasInteresse-fechar">
                                Cancelar
                            </Button>
                            <Button variant="primary" id="btn-vagasInteresse-salvar" disabled={isLoading} onClick={event => {
                                event.preventDefault();
                                setIsLoading(true);
                                ConfigAreasInteresse();
                            }}>
                                {isLoading ? <Spinner
                                    as="span"
                                    animation="border"
                                    size="sm"
                                    role="status"
                                    aria-hidden="true"
                                /> : 'Salvar'}
                            </Button>
                        </Modal.Footer>
                    </Modal>
                    {/* FIM do Modal Configurar Áreas de Interesse */}

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

export default MeuPerfil;