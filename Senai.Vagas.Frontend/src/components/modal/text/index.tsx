import React, { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';


import './style.css';
import '../../../assets/styles/global.css';

interface ModalTextProps {
    link: string;
    title: string;
    description:string;
    // body:any;
    value:string;

}

const ModalText: React.FC<ModalTextProps> = ({link, title, description,value, ...rest}) => {  

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <div>
            <Button variant="link" onClick={handleShow}>{link}</Button>

            <Modal show={show} onHide={handleClose}>

                <Modal.Header closeButton>
                    <Modal.Title className="title">{title}</Modal.Title>
                </Modal.Header>

            <div className="description">
                <h4>
                    {description}
                </h4>
            </div>

                <Modal.Body></Modal.Body>
     
                <Modal.Footer>
                    <Button variant="danger" onClick={handleClose}>{value}</Button>
                </Modal.Footer>
                
            </Modal>

        </div>
    );
}

export default ModalText;