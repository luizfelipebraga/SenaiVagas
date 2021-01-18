import React, { useEffect, useState } from 'react';
import LogoAppVermelhoVagasBranco from '../../assets/images/LogoAppVermelho.png';
import { Form, Tab, Tabs, Button, Modal, Spinner } from 'react-bootstrap';
import './style.css';
import { useHistory } from 'react-router-dom';
import jwt from '../../services/auth';
import api from '../../services/api';

const Login = () => {
    // Const para funcionar a troca de Tabs
    const [key, setKey] = useState('login');
    let history = useHistory();

    // Loading's
    const [isLoading, setIsLoading] = useState(false);

    // Váriavel booleana para o modal
    const [show, setShow] = useState(false);
    const [show2, setShow2] = useState(false);
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalCandidato = () => setShow(true);
    const showModalSenha = () => setShow2(true);
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalCandidato = () => setShow(false);
    const hideModalSenha = () => setShow2(false);
    const hideModalAviso = () => setShowAviso(false);

    // Propriedades necessárias
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [emailOuRma, setEmailCandidato] = useState('');

    const [nome, setNome] = useState('');
    const [CNPJ, setCnpj] = useState('');
    const [confirmSenha, setConfirmSenha] = useState('');

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    // Mascará de CNPJ, separa os números em sequencia e acrescenta os caracteres especiais
    const maskCnpj = (cnpj: string) => {
        cnpj = cnpj.replace(/\D/g, "")                           //Remove tudo o que não é dígito
        cnpj = cnpj.replace(/^(\d{2})(\d)/, "$1.$2")             //Coloca ponto entre o segundo e o terceiro dígitos
        cnpj = cnpj.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3") //Coloca ponto entre o quinto e o sexto dígitos
        cnpj = cnpj.replace(/\.(\d{3})(\d)/, ".$1/$2")           //Coloca uma barra entre o oitavo e o nono dígitos
        cnpj = cnpj.replace(/(\d{4})(\d)/, "$1-$2")              //Coloca um hífen depois do bloco de quatro dígitos
        
        setCnpj(cnpj);
    }

    const login = async () => {
        if (email.length === 0 || senha.length === 0) {
            setIsLoading(false);
            setAviso('Preencha os campos de email e senha para logar')
            return showModalAviso();
        }

        const login = {
            email: email,
            senha: senha
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post('Login', login, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.data.token !== undefined) {
                    localStorage.setItem('token-usuario', resp.data.token)

                    // Descriptografa o token
                    let tokenDecoded = jwt();

                    // Caso o usuario for um candidato
                    if (tokenDecoded.role === "1") {
                        history.push('/home-candidato');
                    }
                    // Caso o usuario for uma empresa
                    else if (tokenDecoded.role === "2") {
                        history.push('/home-empresa');
                    }
                    // Caso o usuario for um admin
                    else if (tokenDecoded.role === "3") {
                        history.push('/dashboard');
                    }
                } else {
                    setAviso(resp.data)
                    setIsLoading(false);
                    return showModalAviso();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    setIsLoading(false);
                    return showModalAviso();
                }
                // error.response adquire a resposta de erro da API
                // error.response.status (status do retorno)
                // error.response.data (dados retornados da API)
            })
    }

    const cadastrarEmpresa = async () => {
        const form = {
            nome: nome,
            email: email,
            CNPJ: CNPJ.replace(/\./g, '').replace(/\-/g, '').replace(/\//g, ''),
            senha: senha
        };

        if (form.nome.length === 0 ||
            form.email.length === 0 ||
            form.CNPJ.length === 0 ||
            form.senha.length === 0) {
            setAviso('Todos os campos devem ser preenchidos')
            setIsLoading(false);
            return showModalAviso();
        }

        if (form.senha.length < 8) {
            setAviso('Senha deve conter 8 caracteres ou mais')
            setIsLoading(false);
            return showModalAviso();
        }

        if (form.senha !== confirmSenha) {
            setAviso('Senha e confirmar senha não correspondem')
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post('Usuarios/cadastro/usuario-empresa', form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setAviso(resp.data);
                    setNome('');
                    setCnpj('');
                    setSenha('');
                    setConfirmSenha('');
                    setEmail('');
                    return showModalAviso();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    setIsLoading(false);
                    return showModalAviso();
                }
                // error.response adquire a resposta de erro da API
                // error.response.status (status do retorno)
                // error.response.data (dados retornados da API)
            })
    }

    const cadastrarCandidato = async () => {
        const form = {
            rmaOuEmail: emailOuRma,
        }

        if (form.rmaOuEmail.length === 0) {
            setAviso('Campo de Email/RMA deve ser preenchido')
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post('Usuarios/validacao/candidato', form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    hideModalCandidato();
                    setAviso(resp.data);
                    setEmailCandidato('');
                    return showModalAviso();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    setIsLoading(false);
                    return showModalAviso();
                }
                // error.response adquire a resposta de erro da API
                // error.response.status (status do retorno)
                // error.response.data (dados retornados da API)
            })
    }

    const esqueciSenha = () => {
        //TODO: Fazer qnd terminar MVP
        setEmail('');
    }

    const conteudo = () => {
        return (
            <Tabs
                id="controlled-tab-example"
                activeKey={key}
                onSelect={(k: any) => setKey(k)}
            >
                <Tab eventKey="login" title="Login">
                    <Form onSubmit={event => {
                        event.preventDefault();
                        setIsLoading(true);
                        login();
                    }}>
                        <br></br>
                        <div className="titulo">
                            <h4 id="title-login">
                                <i>Faça seu Login</i>
                            </h4>
                        </div>

                        <Form.Group controlId="Email">
                            <Form.Label>Email:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => setEmail(e.target.value)} />
                        </Form.Group>


                        <Form.Group controlId="Senha">
                            <Form.Label>Senha:</Form.Label>
                            <Form.Control type="password" placeholder="" onChange={e => setSenha(e.target.value)} />
                        </Form.Group>

                        <Button id="btn-login" size="lg" block disabled={isLoading} type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Login'}
                        </Button>
                    </Form>

                    <div className="links-login">
                        <p onClick={event => {
                            event.preventDefault()
                            showModalSenha();
                        }}>Esqueceu sua senha?</p>

                        <p onClick={event => {
                            event.preventDefault()
                            showModalCandidato();
                        }}>Cadastrar como candidato</p>
                    </div>

                    {/* MODAL Cadastro de candidato */}
                    <Modal
                        show={show}
                        onHide={hideModalCandidato}>
                        <Modal.Header>
                            <Modal.Title>Digite seu RM ou Email de matrícula</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form>

                                <Form.Group controlId="email-candidato">
                                    <Form.Control type="text" placeholder="Email ou RM de aluno" value={emailOuRma} onChange={e => setEmailCandidato(e.target.value)} />
                                </Form.Group>
                            </Form>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={hideModalCandidato} id="btn-candidato-fechar">
                                Fechar
                            </Button>
                            <Button variant="primary" id="btn-candidato-enviar" disabled={isLoading} onClick={event => {
                                event.preventDefault();
                                setIsLoading(true);
                                cadastrarCandidato();
                            }}>
                                {isLoading ? <Spinner
                                    as="span"
                                    animation="border"
                                    size="sm"
                                    role="status"
                                    aria-hidden="true"
                                /> : 'Enviar'}
                            </Button>
                        </Modal.Footer>
                    </Modal>
                    {/* FIM do Modal */}

                    {/* MODAL Esqueci Minha Senha */}
                    <Modal
                        show={show2}
                        onHide={hideModalSenha}>
                        <Modal.Header>
                            <Modal.Title>Digite seu Email</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <p>Esqueceu a senha? Não se preocupe! Digite seu email no campo abaixo e siga as instruções para alterá-la e conseguir acesso a sua conta novamente!</p>
                            <Form.Group controlId="email-esqueci-senha">
                                <Form.Control type="email" placeholder="Email" onChange={e => setEmail(e.target.value)} />
                            </Form.Group>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={hideModalSenha} id="btn-candidato-fechar">
                                Fechar
                            </Button>
                            <Button variant="primary" id="btn-candidato-enviar" onClick={e => esqueciSenha()}>
                                Enviar
                            </Button>
                        </Modal.Footer>
                    </Modal>
                    {/* FIM do Modal */}

                </Tab>
                <Tab eventKey="cadastro" title="Cadastrar minha empresa">
                    <Form onSubmit={event => {
                        event.preventDefault();
                        setIsLoading(true);
                        cadastrarEmpresa();
                    }}>
                        <br></br>
                        <div className="titulo">
                            <h4 id="title-cadastro">
                                <i>Cadastre sua empresa</i>
                            </h4>
                        </div>

                        <Form.Group controlId="nome">
                            <Form.Label>Nome</Form.Label>
                            <Form.Control type="text" value={nome} onChange={e => {
                                setNome(e.target.value)
                            }} />
                        </Form.Group>

                        <Form.Group controlId="email">
                            <Form.Label>Email</Form.Label>
                            <Form.Control type="text" value={email} onChange={e => {
                                setEmail(e.target.value)
                            }} />
                        </Form.Group>

                        <Form.Group controlId="cnpj">
                            <Form.Label>CNPJ</Form.Label>
                            <Form.Control type="text" value={CNPJ} maxLength={18} onChange={e => maskCnpj(e.target.value)} />
                        </Form.Group>

                        <Form.Group controlId="senha">
                            <Form.Label>Senha</Form.Label>
                            <Form.Control type="password" value={senha} onChange={e => {
                                setSenha(e.target.value)
                            }} />
                        </Form.Group>

                        <Form.Group controlId="confirmSenha">
                            <Form.Label>Confirmar Senha</Form.Label>
                            <Form.Control type="password" value={confirmSenha} onChange={e => {
                                setConfirmSenha(e.target.value)
                            }} />
                        </Form.Group>

                        <Button id="btn-cadastrar" size="lg" block disabled={isLoading} type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Cadastrar Empresa'}
                        </Button>

                    </Form>
                </Tab>
            </Tabs>
        );
    }

    return (
        <div className='fundo' >
            <div className='vermelho'>
                <div className='caixa'>
                    <img src={LogoAppVermelhoVagasBranco} alt="LogoAppVermelhoVagasBranco" />
                    <div className="content">

                        {conteudo()}

                        {/* MODAL de Aviso */}
                        <Modal
                            show={showAviso}
                            onHide={() => {
                                hideModalAviso();
                                setIsLoading(false);
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
                                }}>
                                    Fechar
                            </Button>
                            </Modal.Footer>
                        </Modal>
                        {/* FIM do modal de Aviso */}

                    </div>
                </div>
            </div>
        </div>
    );
}

export default Login;