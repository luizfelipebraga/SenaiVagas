import React, { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';


import './style.css';
import '../../../assets/styles/global.css';

interface ModalInputProps {
    link: string;
    title: string;
    description: string;
    value: string;

}

const ModalInput: React.FC<ModalInputProps> = ({ link, title, description, value, ...rest }) => {

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <div>
            <Button variant="link" onClick={handleShow}>{link}</Button>

            <Modal show={show} onHide={handleClose}>

                <Modal.Header closeButton>
                    <Modal.Title>
                        <div className="title">
                            {title}
                        </div>
                    </Modal.Title>
                </Modal.Header>

                <div className="description">
                    <h4>
                        {description}
                    </h4>
                </div>

                <Modal.Body>
                    <Form>
                        <Form.Group controlId="formBasicEmail" className="box-email">
                            <Form.Label>Email:</Form.Label>
                                <Form.Control type="email" placeholder="email@example.com" />
                        </Form.Group>
                    </Form>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="danger" onClick={handleClose}>{value}</Button>
                </Modal.Footer>

            </Modal>

        </div>
    );
}

export default ModalInput;