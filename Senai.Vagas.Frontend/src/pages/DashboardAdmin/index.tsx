import React, { useEffect, useState } from 'react';
import { Tab, Tabs, Table, Form, Button, Modal, ListGroup } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import Footer from '../../components/footer/index';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title/index';
import iconVagas from '../../assets/images/IconVagas.png';
import iconEmpresa from '../../assets/images/IconEmpresa.png';
import iconCandidato from '../../assets/images/IconCandidatos.png';
import iconAdmin from '../../assets/images/IconAdministrador.png';
import jwt from '../../services/auth';
import api from '../../services/api';
import './style.css';

function DashboardAdmin() {
    // Const para funcionar a troca de Tabs
    const [key, setKey] = useState('empresas');

    // Propriedades duplicadas para funcionar o filtro em cada lista
    const [admins, setAdmins] = useState([]);
    const [adminList, setAdminList] = useState([]);

    const [empresas, setEmpresas] = useState([]);
    const [empresaList, setEmpresaList] = useState([]);

    const [candidatos, setCandidatos] = useState([]);
    const [candidatoList, setCandidatoList] = useState([]);

    const [vagas, setVagas] = useState([]);
    const [vagaList, setVagaList] = useState([]);

    // Dados da empresa
    const [nomeEmpresa, setNomeEmpresa] = useState('');
    const [cnpj, setCnpj] = useState('');
    const [emailUsuario, setEmailUsuario] = useState('');
    const [tipoEmpresa, setTipoEmpresa] = useState('');
    const [atvPrincipalCode, setAtvPrincipalCode] = useState<any>({});

    const [endereco, setEndereco] = useState<any>({});
    const [municipio, setMunicipio] = useState<any>({});
    const [ufSigla, setUfSigla] = useState<any>({});
    const [qsas, setQsas] = useState([]);
    const [atvSecundarias, setAtvSecundarias] = useState([]);

    // Dados do candidato
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [curso, setCurso] = useState('');
    const [sexo, setSexo] = useState(true);
    const [termo, setTermo] = useState('');
    const [rma, setRma] = useState('');
    const [dataNascimento, setDataNascimento] = useState('');
    const [dataMatricula, setDataMatricula] = useState('');
    const [sobre, setSobre] = useState('');
    const [linkExterno, setLinkExterno] = useState('');

    // Dados da vaga
    const [nomeEmpresaVaga, setNomeEmpresaVaga] = useState('');
    const [nomeVaga, setNomeVaga] = useState('');
    const [experiencia, setExperiencia] = useState('');
    const [cargo, setCargo] = useState('');
    const [faixaSalarial, setFaixaSalarial] = useState('');
    const [descricaoVaga, setDescricaoVaga] = useState('');

    const [nomeMunicipioVaga, setNomeMunicipioVaga] = useState('');
    const [ufSiglaVaga, setUfSiglaVaga] = useState<any>({});

    const [dataEncerramento, setDataEncerramento] = useState('');
    const [areaVagaRecomendadas, setAreaVagaRecomendadas] = useState([]);

    // Modais VER MAIS
    const [showEmpresa, setShowEmpresa] = useState(false);
    const [showCandidato, setShowCandidato] = useState(false);
    const [showVaga, setShowVaga] = useState(false);
    const [showAdmin, setShowAdmin] = useState(false);

    const empClose = () => setShowEmpresa(false);
    const empShow = () => setShowEmpresa(true);

    const vagaClose = () => setShowVaga(false);
    const vagaShow = () => setShowVaga(true);

    const candClose = () => setShowCandidato(false);
    const candShow = () => setShowCandidato(true);

    // Modal de AVISO
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    // Token
    let tokenDecoded = jwt();

    // Listagem para completar todas as tabelas
    useEffect(() => {
        Listar();
    }, [])

    const ListarVagas = () => {
        //TODO: fazer url para mostrar inscritos de vagas
        let url = '';
        const VerDetalhesVaga = async (vagaId: number) => {
            const initListar = {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.getItem('token-usuario'),
                },
            }

            await api
                .get(`Vagas/buscar/vaga/${vagaId}`, initListar)
                .then(resp => {
                    setNomeEmpresaVaga(resp.data.nomeEmpresa);
                    setExperiencia(resp.data.tipoExperiencia.descricao);
                    setCargo(resp.data.cargo);
                    setFaixaSalarial(resp.data.faixaSalarial.descricao);
                    setNomeMunicipioVaga(resp.data.municipio.descricao);
                    setUfSiglaVaga(resp.data.municipio.ufSigla);
                    setDataEncerramento(new Date(resp.data.dataEncerramento).toLocaleDateString('pt-BR', { timeZone: 'UTC' }));
                    setAreaVagaRecomendadas(resp.data.areaVagaRecomendadas);
                    setDescricaoVaga(resp.data.descricaoVaga)
                    setNomeVaga(resp.data.nomeVaga);
                    //console.log(resp.data)
                })
                .catch(error => {
                    if (error.response) {
                        setAviso(error.response.data)
                        return showModalAviso();
                    }
                })
        }

        return (
            <tbody>
                {
                    vagaList.map((vaga: any) => {
                        return (
                            <tr key={vaga.id}>
                                <td>{vaga.id}</td>
                                <td>{vaga.nomeEmpresa}</td>
                                <td>{vaga.nomeVaga}</td>
                                <td className="btns-tables">
                                    <Button variant="primary" id="btn-verDetalhes" onClick={event => {
                                        event.preventDefault();
                                        VerDetalhesVaga(vaga.id);
                                        vagaShow();
                                    }}> Ver Detalhes </Button>

                                    <Button variant="primary" id="btn-verInscritos" onClick={event => {
                                        event.preventDefault();
                                    }}> Ver Inscritos </Button>
                                </td>
                            </tr>
                        );
                    })
                }
            </tbody>
        );
    }

    const ListarEmpresas = () => {
        const VerMaisEmpresa = async (empresaId: number) => {
            const init = {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.getItem('token-usuario'),
                },
            }

            await api
                .get(`Empresas/buscar/empresa/${empresaId}`, init)
                .then(resp => {
                    setNomeEmpresa(resp.data.nome);
                    setCnpj(resp.data.cnpj);
                    setEmailUsuario(resp.data.emailUsuario);
                    setTipoEmpresa(resp.data.tipoEmpresa.descricao);
                    setAtvPrincipalCode(resp.data.atividadePrincipal.tipoCnae);
                    setEndereco(resp.data.endereco);
                    setMunicipio(resp.data.endereco.municipio);
                    setUfSigla(resp.data.endereco.municipio.ufSigla);
                    setAtvSecundarias(resp.data.atividadesSecundarias);
                    setQsas(resp.data.qsAs)
                    //console.log(resp.data)
                })
                .catch(error => {
                    if (error.response) {
                        setAviso(error.response.data)
                        return showModalAviso();
                    }
                })
        }

        return (
            <tbody>
                {
                    empresaList.map((emp: any) => {
                        return (
                            <tr key={emp.id}>
                                <td>{emp.id}</td>
                                <td>{emp.nome}</td>
                                <td>{emp.emailUsuario}</td>
                                <td>{maskCnpj(emp.cnpj)}</td>
                                <td className="btns-tables">
                                    <Button variant="primary" id="btn-verMaisCand" onClick={event => {
                                        event.preventDefault();
                                        VerMaisEmpresa(emp.id);
                                        empShow();
                                    }}> Ver Mais </Button>
                                </td>
                            </tr>
                        );
                    })
                }
            </tbody>
        );
    }

    const ListarCandidatos = () => {
        const VerMaisCandidato = async (alunoId: number) => {
            const initListar = {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.getItem('token-usuario'),
                },
            }

            await api
                .get(`UsuariosCandidatos/perfil/aluno/${alunoId}`, initListar)
                .then(resp => {
                    setNome(resp.data.nomeCompleto);
                    setEmail(resp.data.email);
                    setCurso(resp.data.tipoCurso.descricao);
                    setRma(resp.data.rma);
                    setSexo(resp.data.sexo);
                    setTermo(resp.data.termoOuEgressoAluno.descricao)
                    setSobre(resp.data.perfilCandidato.sobreOCandidato)
                    setLinkExterno(resp.data.perfilCandidato.linkExterno)

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

        return (
            <tbody>
                {
                    candidatoList.map((cand: any) => {
                        return (
                            <tr key={cand.id}>
                                <td>{cand.id}</td>
                                <td>{cand.nomeCompleto}</td>
                                <td>{cand.email}</td>
                                <td>{cand.rma}</td>
                                <td className="btns-tables">
                                    <Button variant="primary" id="btn-verMaisCand" onClick={event => {
                                        event.preventDefault();
                                        VerMaisCandidato(cand.id);
                                        candShow();
                                    }}> Ver Mais </Button>
                                </td>
                            </tr>
                        );
                    })
                }
            </tbody>
        );
    }

    const ListarAdmins = () => {
        const DesativarAdmin = (vagaId: number) => {

        }

        return (
            <tbody>
                {
                    adminList.map((adm: any) => {
                        return (
                            <tr key={adm.id}>
                                <td>{adm.id}</td>
                                <td>{adm.nome}</td>
                                <td>{adm.nif}</td>
                                <td>{adm.email}</td>
                                <td className="btns-tables">
                                    <Button variant="primary" id="btn-desativarAdm" onClick={event => {
                                        event.preventDefault();
                                        DesativarAdmin(adm.id);
                                    }}> Desativar Admin </Button>
                                </td>
                            </tr>
                        );
                    })
                }
            </tbody>
        );
    }

    const Listar = async () => {
        const initListar = {
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + localStorage.getItem('token-usuario'),
            },
        }

        await api
            .get('Administrador/buscar-todos', initListar)
            .then(resp => {
                setAdminList(resp.data);
                setAdmins(resp.data);
                //console.log(resp.data)
            })
            .catch(error => console.log(error))

        await api
            .get('Empresas', initListar)
            .then(resp => {
                setEmpresaList(resp.data);
                setEmpresas(resp.data);
                //console.log(resp.data)
            })
            .catch(error => console.log(error))

        await api
            .get('UsuariosCandidatos/buscar-todos', initListar)
            .then(resp => {
                setCandidatoList(resp.data);
                setCandidatos(resp.data);
                //console.log(resp.data)
            })
            .catch(error => console.log(error))

        await api
            .get('Vagas/buscar-todas', initListar)
            .then(resp => {
                setVagaList(resp.data);
                setVagas(resp.data);
                //console.log(resp.data)
            })
            .catch(error => console.log(error))
    }

    // Mascará de CNPJ, separa os números em sequencia e acrescenta os caracteres especiais
    const maskCnpj = (cnpj: string) => {
        return cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
    }

    return (
        <div className="wrapper">
            <Header />
            <div className="content-main">
                <TopBar userName={tokenDecoded.unique_name} />
                <div className="content-child">
                    <div className="tabs">

                        <Tabs
                            id="controlled-tab-example"
                            activeKey={key}
                            onSelect={(k: any) => setKey(k)}
                        >
                            <Tab eventKey="empresas" title="Lista de Empresas">
                                <Title img={iconEmpresa} title="Empresas Cadastradas" />

                                <div className="filter-container">
                                    <div>
                                        <h3>Lista de Empresas</h3>
                                    </div>
                                    <div className="filter">
                                        <Form.Group controlId="filterEmpresa">
                                            <Form.Control type="text" placeholder="Filtre por nome, email ou CNPJ de empresas" />
                                        </Form.Group>
                                    </div>
                                </div>
                                <Table responsive striped bordered hover size="sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Nome</th>
                                            <th>Email</th>
                                            <th>CNPJ</th>
                                            <th>Ações</th>
                                        </tr>
                                    </thead>
                                    {ListarEmpresas()}
                                </Table>

                                <Modal size="lg" show={showEmpresa} onHide={empClose}>
                                    <Modal.Header>
                                        <Modal.Title className="title-modal">Informações da Empresa</Modal.Title>
                                    </Modal.Header>

                                    <Modal.Body className="vermais-container">
                                        <ListGroup variant="flush" className="infos-perfil">
                                            <ListGroup.Item><b>Nome da Empresa:</b> {nomeEmpresa} </ListGroup.Item>

                                            <ListGroup.Item><b>Email:</b> {emailUsuario}</ListGroup.Item>

                                            <ListGroup.Item><b>CNPJ:</b> {maskCnpj(cnpj)} </ListGroup.Item>
                                            <ListGroup.Item><b>Tipo da Empresa:</b> {tipoEmpresa} </ListGroup.Item>
                                            <ListGroup.Item><b>Endereço:</b> {`${endereco.bairro}, ${endereco.logradouro}, ${endereco.numero !== "" ? endereco.numero : 'S/N'}, ${municipio.descricao}, ${ufSigla.ufEstado} - ${ufSigla.ufSigla}`} </ListGroup.Item>
                                            <ListGroup.Item><b>Quadro de sócios e administradores:</b>  </ListGroup.Item>

                                            <div className="table">
                                                <div className="barraSuperior">
                                                    <div>
                                                        <h3>QSA</h3>
                                                    </div>
                                                </div>
                                                <Table responsive striped bordered hover>
                                                    <thead>
                                                        <tr>
                                                            <th>#Id</th>
                                                            <th>Nome</th>
                                                            <th>Qualificação</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        {
                                                            qsas.map((qsa: any) => {
                                                                return (
                                                                    <tr key={qsa.id}>
                                                                        <td>{qsa.id}</td>
                                                                        <td>{qsa.nome}</td>
                                                                        <td>{qsa.qualificacao}</td>
                                                                    </tr>
                                                                );
                                                            })
                                                        }
                                                    </tbody>
                                                </Table>
                                            </div>
                                            <ListGroup.Item><b>Atividade principal - CNAE:</b>  </ListGroup.Item>
                                            <ul className="cnae">
                                                <li>Texto: {atvPrincipalCode.descricao} </li>
                                                <li>Código: {atvPrincipalCode.codigo} </li>
                                            </ul>
                                            <ListGroup.Item><b>Atividade secundária - CNAE's:</b>  </ListGroup.Item>
                                            <ul className="cnae">
                                                {
                                                    atvSecundarias.map((atv: any) => {
                                                        return (
                                                            <div className="cnae-list">
                                                                <li>Texto: {atv.tipoCnae.descricao}</li>
                                                                <li>Código: {atv.tipoCnae.codigo}</li>
                                                            </div>
                                                        );
                                                    })
                                                }
                                            </ul>
                                        </ListGroup>
                                    </Modal.Body>

                                    <Modal.Footer>
                                        <Button variant="danger" onClick={event => {
                                            event.preventDefault();
                                            empClose();
                                        }}>Fechar</Button>
                                    </Modal.Footer>
                                </Modal>
                            </Tab>
                            <Tab eventKey="candidatos" title="Lista de Candidatos">
                                <Title img={iconCandidato} title="Candidatos Cadastrados" />

                                <div className="filter-container">
                                    <div>
                                        <h3>Lista de Candidatos</h3>
                                    </div>
                                    <div className="filter">
                                        <Form.Group controlId="filterCandidato">
                                            <Form.Control type="text" placeholder="Filtre por nome, email ou RM de candidato" />
                                        </Form.Group>
                                    </div>
                                </div>
                                <Table responsive striped bordered hover size="sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Nome</th>
                                            <th>Email</th>
                                            <th>RMA</th>
                                            <th>Ações</th>
                                        </tr>
                                    </thead>
                                    {ListarCandidatos()}
                                </Table>
                                <Modal size="lg" show={showCandidato} onHide={candClose}>
                                    <Modal.Header>
                                        <Modal.Title className="title-modal">Informações do Candidato</Modal.Title>
                                    </Modal.Header>

                                    <Modal.Body className="vermais-container">
                                        <ListGroup variant="flush" className="infos-perfil">
                                            <ListGroup.Item><b>Nome:</b> {nome} </ListGroup.Item>
                                            <ListGroup.Item><b>Email:</b> {email}</ListGroup.Item>
                                            <ListGroup.Item><b>Sexo:</b> {sexo ? 'Masculino' : 'Feminino'} </ListGroup.Item>
                                            <ListGroup.Item><b>Data de Nascimento:</b> {dataNascimento} </ListGroup.Item>
                                            <ListGroup.Item><b>Curso:</b> {curso} </ListGroup.Item>
                                            <ListGroup.Item><b>Termo ou Egresso:</b> {termo} </ListGroup.Item>
                                            <ListGroup.Item><b>RMA:</b> {rma} </ListGroup.Item>
                                            <ListGroup.Item><b>Data de Matrícula / Renovação:</b> {dataMatricula} </ListGroup.Item>

                                            <Form.Group controlId="descricao">

                                                <div className="descricao">
                                                    <Form.Label><b>Sobre o Candidato:</b></Form.Label>
                                                    <Form.Text className="text-muted">
                                                        {sobre}
                                                    </Form.Text>
                                                </div>

                                            </Form.Group>

                                            <ListGroup.Item><b>Link Externo:</b> {linkExterno} </ListGroup.Item>
                                        </ListGroup>
                                    </Modal.Body>

                                    <Modal.Footer>
                                        <Button variant="danger" onClick={event => {
                                            event.preventDefault();
                                            candClose();
                                        }}>Fechar</Button>
                                    </Modal.Footer>
                                </Modal>
                            </Tab>
                            <Tab eventKey="vagas" title="Vagas Cadastradas">
                                <Title img={iconVagas} title="Vagas Cadastradas" />

                                <div className="filter-container">
                                    <div>
                                        <h3>Vagas Cadastradas</h3>
                                    </div>
                                    <div className="filter">
                                        <Form.Group controlId="filterVagas">
                                            <Form.Control type="text" placeholder="Filtre por nome de empresa ou vaga" />
                                        </Form.Group>
                                    </div>
                                </div>
                                <Table responsive striped bordered hover size="sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Empresa</th>
                                            <th>Nome da Vaga</th>
                                            <th>Ações</th>
                                        </tr>
                                    </thead>
                                    {ListarVagas()}
                                </Table>
                                <Modal size="lg" show={showVaga} onHide={vagaClose}>
                                    <Modal.Header>
                                        <Modal.Title className="title-modal">{nomeVaga}</Modal.Title>
                                    </Modal.Header>

                                    <Modal.Body className="vermais-container">
                                        <ListGroup variant="flush" className="infos-perfil">
                                            <ListGroup.Item><b>Nome Empresa:</b> {nomeEmpresaVaga} </ListGroup.Item>
                                            <ListGroup.Item><b>Experiência:</b> {experiencia} </ListGroup.Item>
                                            <ListGroup.Item><b>Cargo:</b> {cargo} </ListGroup.Item>
                                            <ListGroup.Item><b>Faixa Salarial:</b> {faixaSalarial} </ListGroup.Item>
                                            <ListGroup.Item><b>Município:</b> {`${nomeMunicipioVaga}, ${ufSiglaVaga.ufEstado} - ${ufSiglaVaga.ufSigla}`} </ListGroup.Item>
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
                                                        {descricaoVaga}
                                                    </Form.Text>
                                                </div>

                                            </Form.Group>
                                        </ListGroup>
                                    </Modal.Body>

                                    <Modal.Footer>
                                        <Button variant="danger" onClick={event => {
                                            event.preventDefault();
                                            vagaClose();
                                        }}>Fechar</Button>
                                    </Modal.Footer>
                                </Modal>
                            </Tab>
                            <Tab eventKey="admins" title="Lista de Administradores">
                                <Title img={iconAdmin} title="Administradores Cadastrados" />

                                <div className="filter-container">
                                    <div>
                                        <h3>Lista de Administradores</h3>
                                    </div>
                                    <div className="filter">
                                        <Form.Group controlId="filterAdmin">
                                            <Form.Control type="text" placeholder="Filtre por nome ou NIF" />
                                        </Form.Group>
                                    </div>
                                </div>
                                <Table responsive striped bordered hover size="sm">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Nome</th>
                                            <th>NIF</th>
                                            <th>Email</th>
                                            <th>Ações</th>
                                        </tr>
                                    </thead>
                                    {ListarAdmins()}
                                </Table>

                            </Tab>
                        </Tabs>
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
    )
}

export default DashboardAdmin;