import React, { useEffect, useState } from 'react';
import { Button, ListGroup, Table, Modal } from 'react-bootstrap';

import './style.css';

import iconEmpresa from '../../assets/images/IconEmpresa.png';
import Header from '../../components/header/index';
import Footer from '../../components/footer/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title';
import jwt from '../../services/auth';
import api from '../../services/api';

function MinhaEmpresa() {
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
        Listar()
    }, [])

    const Listar = async () => {
        if (tokenDecoded == null) {
            setAviso('Não foi possível carregar as informações da empresa')
            return showModalAviso();
        }

        const init = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .get(`Empresas/buscar/usuario/${tokenDecoded.jti}`, init)
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
                    <Title img={iconEmpresa} title="Meu Perfil" />
                    <div className="perfil-container">
                        <ListGroup variant="flush" className="infos-perfil">
                            <ListGroup.Item><b>Nome da Empresa:</b> {nomeEmpresa} </ListGroup.Item>

                            <ListGroup.Item className="links"><b>Email:</b> {emailUsuario} <p onClick={event => {
                                event.preventDefault()
                                //função de abrir modal
                            }}>Alterar Email</p></ListGroup.Item>

                            <ListGroup.Item className="links"><b>Senha:</b> ******* <p onClick={event => {
                                event.preventDefault()
                                //função de abrir modal
                            }}>Alterar Senha</p></ListGroup.Item>

                            <ListGroup.Item><b>CNPJ:</b> {maskCnpj(cnpj)} </ListGroup.Item>
                            <ListGroup.Item><b>Tipo da Empresa:</b> {tipoEmpresa} </ListGroup.Item>
                            <ListGroup.Item><b>Endereço:</b> {`${endereco.bairro}, ${endereco.logradouro}, ${endereco.numero !== "" ? endereco.numero : 'S/N'}, ${municipio.descricao}, ${ufSigla.ufEstado} - ${ufSigla.ufSigla}`} </ListGroup.Item>
                            <ListGroup.Item><b>Quadro de sócios e administradores:</b> </ListGroup.Item>

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

export default MinhaEmpresa;