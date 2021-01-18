import React from 'react';
import { BrowserRouter, Route, Redirect, Switch, useHistory } from 'react-router-dom';
import jwt from './services/auth';
import Login from './pages/login/index';
import Nova_Senha from './pages/nova_senha';
import CadastrarVaga from './pages/CadastrarVaga/index';
import HomeEmpresa from './pages/homepageEmpresa/index';
import CandidatosAdmin from './pages/candidatosAdmin/index';
import DashboardAdmin from './pages/DashboardAdmin/index';
import NotFound from './pages/NotFound/index';
import MeuPerfil from './pages/MeuPerfil/index';
import HomeCandidato from './pages/HomeCandidato';
import MinhasInscricoesCandidato from './pages/MinhasInscricoesCandidato';
import MinhaEmpresa from './pages/minhaEmpresa/index';
import CadastrarAdmin from './pages/CadastrarAdmin/index';

function Routes() {

  // Rota para que usuários não consigam entrar na página de login estando já logados
  // Também serve para auto redirecionar o usuário caso ele saia do site ainda logado e volte
  const RotaLogin = ({ Component, ...rest }: any) => {
    return(
    <Route
      {...rest}
      render={props =>
        // Se sim, renderiza de acordo com a rota solicitada e permitida
        // Se não, redireciona para a página anterior
        jwt() == null ? (<Component {...props} />) : (
          jwt().role === '1' ? <Redirect to={{pathname: '/home-candidato'}}/> :
          jwt().role === '2' ? <Redirect to={{pathname: '/home-empresa'}}/> : 
          jwt().role === '3' ? <Redirect to={{pathname: '/dashboard'}} /> : (<Component {...props} />)
        )
      }
    />
  )};

  // Rota privada para impedir usuários de entrarem em páginas quando não logados
  const RotaPrivadaCandidato = ({ Component, ...rest }: any) => {
    let history = useHistory();
    return(
    <Route
      {...rest}
      render={props =>
        // Se sim, renderiza de acordo com a rota solicitada e permitida
        // Se não, redireciona para a página anterior
        jwt() !== null && jwt().role === '1' ? (<Component {...props} />) : history.goBack()
      }
    />
  )};

  const RotaPrivadaEmpresa = ({ Component, ...rest }: any) => {
    let history = useHistory();
    return (
      <Route
        {...rest}
        render={props =>
          // Se sim, renderiza de acordo com a rota solicitada e permitida
          // Se não, redireciona para a página anterior
          jwt() !== null && jwt().role === '2' ? (<Component {...props} />) : history.goBack()
        }
      />
    )
  };

  const RotaPrivadaAdm = ({ Component, ...rest }: any) => {
    let history = useHistory();
    return(
    <Route
      {...rest}
      render={props =>
        // Se sim, renderiza de acordo com a rota solicitada e permitida
        // Se não, redireciona para a página de login
        jwt() !== null && jwt().role === '3' ? (<Component {...props} />) : history.goBack()
      }
    />
  )};

  return (
    <BrowserRouter>
      <Switch>
        <RotaLogin path="/" exact Component={Login} />
        <Route path="/novas-credenciais/path/:path/token/:token" component={Nova_Senha} />

        <RotaPrivadaCandidato path="/perfil" Component={MeuPerfil} />
        <RotaPrivadaCandidato path="/home-candidato" Component={HomeCandidato}/>
        <RotaPrivadaCandidato path="/minhas-inscricoes" Component={MinhasInscricoesCandidato}/>

        <RotaPrivadaEmpresa path="/minha-empresa" Component={MinhaEmpresa} />
        <RotaPrivadaEmpresa path="/cadastrar-vaga" Component={CadastrarVaga} />
        <RotaPrivadaEmpresa path="/home-empresa" Component={HomeEmpresa}/>
        <RotaPrivadaEmpresa path="/minha-empresa" Component={MinhaEmpresa}/>
        
        <RotaPrivadaAdm path="/dashboard" Component={DashboardAdmin} />
        <RotaPrivadaAdm path="/cadastrar-admin" Component={CadastrarAdmin} />
        <RotaPrivadaAdm path="/candidatos" Component={CandidatosAdmin} />

        {/* 
          Not Found deve ficar no final
          Caso não encontre nenhuma página pela url, redireciona para /404 (página não encontrada)
        */}
        <Route path="/404" component={NotFound} />
        <Redirect to="/404" />
      </Switch>
    </BrowserRouter>
  );
}

export default Routes;