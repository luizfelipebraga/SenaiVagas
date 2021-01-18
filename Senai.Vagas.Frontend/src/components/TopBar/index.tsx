import React, { useState, useRef } from 'react';
import { useHistory } from 'react-router-dom';

// Outras bibliotecas
import { FaUser, FaBell, FaSignOutAlt } from 'react-icons/fa';
import { Overlay, Popover, ListGroup } from 'react-bootstrap';

// Styles
import './style.css';

interface TopBar {
    userName: string;
}

const TopBar: React.FC<TopBar> = ({ userName }) => {
    // Const's para funcionar overlay e popover
    const [showNotification, setShowNotification] = useState(false);
    const [target, setTarget] = useState<any>(null);
    const ref = useRef(null);

    let history = useHistory();

    // Botão logout
    const Logout = () => {
        // Remove o token do localStorage
        localStorage.removeItem('token-usuario');

        // Envia usuário para a página de Login
        history.push('/');
    }

    const MeuPerfil = () => {
        history.push('perfil')
    }

    return (
        <div className="bar">
            <div className="bar-items" onClick={event => {
                event.preventDefault();
                MeuPerfil();
            }}>
                <FaUser />
                <span>{userName}</span>
            </div>
            <div className="bar-items" onClick={event => {
                event.preventDefault();
                setShowNotification(!showNotification);
                setTarget(event.target);
            }}>
                <FaBell />
            </div>
            <div className="bar-items" onClick={event => {
                event.preventDefault();
                Logout();
            }}>
                <FaSignOutAlt />
                <span>Sair</span>
            </div>

            {/* Card de Notificações */}
            <Overlay
                show={showNotification}
                target={target}
                placement="bottom"
                container={ref.current}
                containerPadding={20}
            >
                <Popover id="popover-contained">
                    <Popover.Title as="h3">Notificações</Popover.Title>
                    <ListGroup variant="flush">
                        <ListGroup.Item>
                            <Popover.Content className="item-notification">
                                <strong>Holy guacamole!</strong> Check this info.
                            </Popover.Content>
                        </ListGroup.Item>
                        <ListGroup.Item>
                            <Popover.Content className="item-notification">
                                <strong>Holy guacamole!</strong> Check this info.
                            </Popover.Content>
                        </ListGroup.Item>
                    </ListGroup>
                </Popover>
            </Overlay>
            {/* FIM do Card de Notificações */}

        </div>

    );
}

export default TopBar;