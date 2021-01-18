import React, { useState } from 'react';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Footer from '../../components/footer/index';
import { Form, Button, Modal, Spinner } from 'react-bootstrap';
import './style.css';
import iconAdmin from '../../assets/images/IconAdministrador.png';
import Title from '../../components/title/index';
import api from '../../services/api';
import jwt from '../../services/auth';

function CadastrarVaga() {
    const [nif, setNif] = useState('');
    const [email, setEmail] = useState('');
    const [nome, setNome] = useState('');
    const [senha, setSenha] = useState('');
    const [confirmSenha, setConfirmSenha] = useState('');

    let tokenDecoded = jwt();

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    const [isLoading, setIsLoading] = useState(false);

    const cadastrarAdmin = async () => {
        const form = {
            nif: nif,
            email: email,
            nome: nome,
            senha: senha
        }

        if (form.nif.length === 0 ||
            form.email.length === 0 ||
            form.nome.length === 0 ||
            form.senha.length === 0 ||
            confirmSenha.length === 0) {
            setAviso('Todos os campos são obrigatórios.');
            setIsLoading(false);
            return showModalAviso();
        }

        if (form.senha.length < 8) {
            setAviso('A senha deve ter no mínimo 8 ou mais caracteres.');
            setIsLoading(false);
            return showModalAviso();
        }

        if (form.senha !== confirmSenha) {
            setAviso('Confirmação de senha não corresponde à senha.');
            setIsLoading(false);
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + localStorage.getItem('token-usuario'),
            },
        }

        await api
            .post('Usuarios/cadastro/usuario-administrador', form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setNif('');
                    setEmail('');
                    setNome('');
                    setSenha('');
                    setConfirmSenha('');
                    setAviso(resp.data);
                    return showModalAviso();
                }
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data);
                    setIsLoading(false);
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

                    <Title img={iconAdmin} title="Cadastrar Administrador" />

                    <Form className="form-container" onSubmit={event => {
                        event.preventDefault();
                        cadastrarAdmin();
                        setIsLoading(true);
                    }}>

                        <Form.Group controlId="nif">
                            <Form.Label>NIF:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => {
                                setNif(e.target.value)
                            }} value={nif} />
                            <Form.Text>
                                Número de Identificação de Funcionário
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="email">
                            <Form.Label>Email:</Form.Label>
                            <Form.Control type="text" placeholder="" onChange={e => {
                                setEmail(e.target.value)
                            }} value={email} />
                            <Form.Text>
                                O email do administrador
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="nome">
                            <Form.Label>Nome:</Form.Label>
                            <Form.Control type="text" onChange={e => {
                                setNome(e.target.value)
                            }} value={nome} />
                            <Form.Text>
                                O nome do administrador
                            </Form.Text>
                        </Form.Group>

                        <Form.Group controlId="Senha">
                            <Form.Label>Senha:</Form.Label>
                            <Form.Control type="password" placeholder="" onChange={e => {
                                setSenha(e.target.value)
                            }} value={senha}/>
                            <Form.Text>
                                A senha do usuário, ele poderá mudar mais tarde.
                            </Form.Text>
                        </Form.Group>


                        <Form.Group controlId="confirmSenha">
                            <Form.Label>Confirme Sua Senha:</Form.Label>
                            <Form.Control type="password" placeholder="" onChange={e => {
                                setConfirmSenha(e.target.value)
                            }} value={confirmSenha} />
                            <Form.Text>
                                Confirme a senha
                            </Form.Text>
                        </Form.Group>

                        <Button id="btn-cadastrar" size="lg" block type="submit">
                            {isLoading ? <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            /> : 'Cadastrar'}
                        </Button>
                    </Form>

                    {/* MODAL de Aviso */}
                    <Modal
                        show={showAviso}
                        onHide={() => {
                            hideModalAviso()
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
                <Footer />
            </div>
        </div>
    );
}

export default CadastrarVaga;