import React from 'react';
import { Nav, Navbar, Button } from 'react-bootstrap';
import { useHistory } from 'react-router-dom';
import logoNegativo from '../../assets/images/LogoAppVermelhoVagasBranco.png';
import './style.css';
import jwt from 'jwt-decode';

// Interface para obter propriedades do payload do token facilmente
interface token {
    email: string,
    unique_name: string,
    jti: number,
    role: string,
    iss: string,
    aud: string
}

const Header = () => {
    // history para enviar usuário à uma página específica
    let history = useHistory();

    const token = localStorage.getItem('token-usuario');

    // Verifica se o token é nulo para poder decodifica-lo (necessidade do TypeScript)
    var tokenDecoded = token === null ? null : jwt<token>(token);

    // Botão logout
    const Logout = () => {
        // Remove o token do localStorage
        localStorage.removeItem('token-usuario');

        // Envia usuário para a página de Login
        history.push('/');
    }

    const Menu = () => {
        // Caso token for diferente de nulo
        if (token !== null || token !== undefined) {

            // Caso CANDIDATO
            if (tokenDecoded?.role === "1") {
                return (
                    <div>
                        <Nav.Link href="/perfil">Meu Perfil</Nav.Link>
                        <Nav.Link href="/minhas-inscricoes">Minhas Inscrições</Nav.Link>
                        <Nav.Link href="/home-candidato">Procurar Vagas</Nav.Link>
                    </div>
                );
            }
            // Caso EMPRESA
            else if (tokenDecoded?.role === "2") {
                return (
                    <div>
                        <Nav.Link href="/home-empresa">Home - Vagas da Empresa</Nav.Link>
                        <Nav.Link href="/minha-empresa">Minha Empresa</Nav.Link>
                        <Nav.Link href="/cadastrar-vaga">Cadastrar Vaga</Nav.Link>
                    </div>
                );
            }
            // Caso ADMINISTRADOR
            else if ((tokenDecoded?.role === "3")) {
                return (
                    <div>
                        <Nav.Link href="">Gerenciar Estágios</Nav.Link>
                        <Nav.Link href="/cadastrar-admin">Cadastrar Administrador</Nav.Link>
                        <Nav.Link href="">Cadastrar Aluno</Nav.Link>
                        <Nav.Link href="/dashboard">Dashboard</Nav.Link>
                    </div>
                );
            }
        }
    }

    return (
        // OBS. Todas páginas deverão ter className="wrapper"
        <div>
            <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" className="flex-column">
                <Navbar.Brand><img src={logoNegativo} alt="" id="logo" /></Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                <Navbar.Collapse id="responsive-navbar-nav">
                    <Nav className="flex-column navs">
                        {Menu()}
                        <div className="btn-container">
                            <Button variant="danger" id="btn-logout" onClick={event => {
                                event.preventDefault();
                                Logout();
                            }}>
                                Sair da Conta
                        </Button>
                        </div>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        </div>
    );
}

export default Header;