import React, { useState, useEffect } from 'react';
import * as Font from 'expo-font';
import { AppLoading } from 'expo';
import { View, StyleSheet, Image, TouchableOpacity, TextInput, Text } from 'react-native';
//Api
import api from '../services/api';
// Interfaces
import Token from '../interfaces/token';
// Token Decoder
import jwt from '../services/tokenDecoder';
//Page
import CardCandidato from '../../src/components/cardCandidato'


function MinhasInscricoesCandidato() {

    const [Vagas, setVagas] = useState([]);

    //filtro para vagas
    const [vagafilter, setVagaFilter] = useState('');

    const [statusVaga, setStatusVaga] = useState('');
    const [idStatusVaga, setIdStatusVaga] = useState('');
    const [statusVagas, setStatusVagas] = useState([]);


    
    const [tokenUser, setTokenUser] = useState<Token | null>(null);

    const TokenDecoder = (token: string) => {
        // Token Decoder
        let tokenDecoded = jwt(token);
    
        setTokenUser(tokenDecoded);
      }

    

    // Modal
    const [showAviso, setShowAviso] = useState(false);

    // Função para mostrar modal
    const showModalAviso = () => setShowAviso(true);

    // Função para esconder modal
    const hideModalAviso = () => setShowAviso(false);

    // Avisos / Erros
    const [aviso, setAviso] = useState('');



    useEffect(() => {
        Listar();
    }, []);

    const initListar = {
        headers: {
            'Content-Type': 'application/json'
        },
    }

    const Listar = async () => {

        if (tokenUser.jti == null) {
            setAviso('Não foi possível carregar as informações do candidato')
            return showModalAviso();
        }

        await api
            .get(`Inscricoes/usuario/${tokenUser.jti}`, initListar)
            .then(resp => {
                // setVagas(resp.data);
            })
            .catch(erro => console.log(erro))

        await api
            .get('Vagas/buscar-statusvaga', initListar)
            .then(resp => {
                // setStatusVagas(resp.data);
            })
            .catch(erro => console.log(erro))
    }

    const buscarVagas = async () => {
        if (tokenUser == null) {
            setAviso('Não foi possível recuperar as informações do candidato para procurar vagas.')
            return showModalAviso();
        }

        const form = {
            filter: vagafilter
        }

        if (form.filter.length === 0) {
            setAviso('O campo de busca não pode estar vazio.')
            return showModalAviso();
        }


        const initListar = {
            headers: {
                'Content-Type': 'application/json'
            },
        }

        await api
            .post(`Vagas/buscar/vaga-filtro/usuario/${tokenUser.jti}`, form, initListar)
            .then(resp => {
                if (resp.status === 200) {
                    // setVagas(resp.data);
                }
            })
            .catch(error => {
                console.log(error.response)
                if (error.response) {
                    setAviso(error.response.data)
                    return showModalAviso();
                }
            })
    }


    //Funcao para pegar font
    const getFonts = () => Font.loadAsync({
        'sansation-regular': require('../assets/fonts/Sansation_Regular.ttf')
    });

    //funcao para setar a font
    const [fontsLoaded, setFontsLoaded] = useState(false);

    if (fontsLoaded) {

        return (
            <View style={styles.container}>

                <View style={styles.boxTitle}>

                    <Image style={styles.imageInscricao} source={require('../assets/images/IconInscricao.png')} />
                    <Text style={styles.textminhasInscricoes}>Minhas Inscrições</Text>
                </View>

                <View style={styles.boxSearch}>
                    <TextInput style={styles.inputSearch} placeholder="Busque Vagas" />
                    <TouchableOpacity style={styles.botaoSearch} onPress={event => {
                        event.preventDefault();
                        buscarVagas();
                    }}>
                        <Text style={styles.textSearch}>Buscar</Text>
                    </TouchableOpacity>
                </View>

                <View style={styles.modalVaga}>

                    {/* {
                        Vagas.map((item: any) => {
                            return (
                                <CardCandidato
                                    nomeVaga={item.nomeVaga}
                                    descricao={item.descricaoVaga}
                                    vagaId={item.id}
                                    vagaAtiva={item.vagaAtiva}
                                    recebeuConvite={item.candidatoRecebeuConvite}
                                />
                            )
                        })
                    } */}
                </View>

                
            </View>
        );
    } else {
        return (

            <AppLoading
                startAsync={getFonts}
                onFinish={() => setFontsLoaded(true)}
            />
        )
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#FBFBFB',
    },

    boxTitle: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },

    imageInscricao: {
        marginTop: 50,
        height: 70,
        width: 70
    },

    textminhasInscricoes: {
        marginTop: 5,
        fontSize: 18,
        fontFamily: 'sansation-regular',
    },

    boxSearch: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',

        marginTop: 30,
        marginHorizontal: 80,
        backgroundColor: '#FFFFFF',

        borderColor: '#bfbfbf',
        borderStyle: 'solid',
        borderWidth: 1,
        borderRadius: 5,


    },

    inputSearch: {
        fontSize: 15

    },

    botaoSearch: {
        marginLeft: 10,
        backgroundColor: 'red',
        borderRadius: 5,
        padding: 7
    },

    textSearch: {
        color: 'white',
        fontSize: 14,
    },

    modalVaga: {
        display: 'flex',
        justifyContent: 'space-between',
        flexWrap: 'wrap',
        minHeight: 30,
    },

    boxVaga: {
        display: 'flex',
        justifyContent: 'center',
        height: 30,
        width:250
    }
})

export default MinhasInscricoesCandidato;