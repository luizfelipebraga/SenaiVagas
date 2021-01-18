import LogoAppVermelhoVagasBranco from '../../assets/images/LogoAppVermelho.png';
import React, { useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import api from '../../services/api';

import { Form, Button, Modal, Spinner } from 'react-bootstrap'
import './style.css';

const Nova_Senha = () => {
    let history = useHistory();

    // Obtêm paramêtros da URL utilizando params.nomeDaVariavelURL
    let params: any = useParams();
    //console.log(`EXEMPLO: ${params.token + params.path}`) Exibe os dois parametros da url, "token" e "path"
    // 3 tipos virão do backend: "validacao", "email" ou "senha"

    // Propriedades necessárias
    const [senha, setSenha] = useState('');
    const [confirmSenha, setConfirmSenha] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    // Modais
    const [showAviso, setShowAviso] = useState(false);

    // Função para esconder e mostrar modal
    const hideModalAviso = () => setShowAviso(false);
    const showModalAviso = () => setShowAviso(true);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');

    const [senhaRegistrada, setSenhaRegistrada] = useState(false);

    const registrarSenha = async () => {
        const form = {
            senha: senha,
        }

        if (form.senha.length === 0 || confirmSenha.length === 0) {
            setIsLoading(false);
            setAviso('Preencha corretamente os campos de senha e confirmar senha');
            return showModalAviso();
        }

        if (form.senha.length < 8) {
            setIsLoading(false);
            setAviso('A senha deve ter no mínimo 8 ou mais caracteres');
            return showModalAviso();
        }

        if (form.senha !== confirmSenha) {
            setIsLoading(false);
            setAviso('Confirmação de senha não corresponde à senha');
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`Usuarios/cadastro/usuario-candidato/token/${params.token}`, form, init)
            .then(resp => {
                setIsLoading(false);
                if (resp.status === 200) {
                    setAviso(resp.data);
                    setSenhaRegistrada(true);
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

    const conteudo = () => {
        if (params.path === "senha" || params.path === "validacao") {
            return (
                <Form onSubmit={event => {
                    event.preventDefault();
                    setIsLoading(true);
                    registrarSenha();
                }}>
                    <h4 className="title">
                        <i>Digite sua nova senha</i>
                    </h4>

                    <Form.Group controlId="Senha">
                        <Form.Label>Senha:</Form.Label>
                        <Form.Control type="password" placeholder="" onChange={e => setSenha(e.target.value)} />
                    </Form.Group>


                    <Form.Group controlId="confirmSenha">
                        <Form.Label>Confirme Sua Senha:</Form.Label>
                        <Form.Control type="password" placeholder="" onChange={e => setConfirmSenha(e.target.value)} />
                    </Form.Group>

                    <Button id="btn-cadastrar" size="lg" block type="submit">
                        {isLoading ? <Spinner
                            as="span"
                            animation="border"
                            size="sm"
                            role="status"
                            aria-hidden="true"
                        /> : 'Registrar senha'}
                    </Button>
                </Form>
            );
        }
        else if (params.path === "email") {
            return (
                <p>{aviso}</p>
            );
        }

        return (
            <p>Paramêtros de URL inválida</p>
        );
    }

    return (
        <div className='fundoNV' >
            <div className='vermelhoNV'>
                <div className='caixaNV'>
                    <img src={LogoAppVermelhoVagasBranco} alt="LogoAppVermelhoVagasBranco" />
                    <div className="content">

                        {conteudo()}

                        {/* MODAL de Aviso */}
                        <Modal
                            show={showAviso}
                            onHide={() => {
                                hideModalAviso();
                                if (senhaRegistrada) {
                                    history.push('/')
                                }
                            }}>
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
                                    if (senhaRegistrada) {
                                        history.push('/')
                                    }
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

export default Nova_Senha;