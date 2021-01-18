import React, { useEffect, useState } from 'react';
import Footer from '../../components/footer/index';
import Header from '../../components/header/index';
import TopBar from '../../components/TopBar/index';
import Title from '../../components/title/index';
import CardEmpresa from '../../components/cardEmpresa/index';
import iconVagas from '../../assets/images/IconVagas.png';
import jwt from '../../services/auth';
import api from '../../services/api';

import '../../assets/styles/global.css';
import '../homepageEmpresa/style.css';
import { flattenDiagnosticMessageText } from 'typescript';

function HomePageEmpresa() {
    const [Vagas, setVagas] = useState([]);

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
    })

    const Listar = async () => {
        const init = {
            headers: {
                'Content-Type' : 'application/json'
            },
        }


        await api
            .get(`Vagas/vagas-empresa/buscar/usuario/${tokenDecoded.jti}`, init)
            .then(resp => {
                setVagas(resp.data);
            })
            .catch(error => {
                if (error.response) {
                    setAviso(error.response.data)
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

                    <Title img={iconVagas} title="Suas vagas anunciadas" />
                    <div className="pagina">
                        {
                            Vagas.map((item: any) => {
                                return (

                                    <CardEmpresa nomeVaga={item.nomeVaga} descricao={item.descricaoVaga} vagaId={item.id} vagaAtiva={item.vagaAtiva}/>

                                )
                            })
                        }  
                    </div>

                </div>
                <Footer />
            </div>
        </div>
    )
}

export default HomePageEmpresa;