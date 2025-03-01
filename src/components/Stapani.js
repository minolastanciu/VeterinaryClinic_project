import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class Stapani extends Component {
    constructor(props) {
        super(props);
        this.state = {
            stapani: [],
            modalTitle: "",
            Nume: "",
            Prenume: "",
            Telefon: "",
            Email: "",
            IDStapan: 0
        };
    }

    refreshList() {
        fetch(variables.API_URL + 'Stapani')  // Endpoint pentru stăpâni
            .then(response => response.json())
            .then(data => {
                this.setState({ stapani: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    addClick() {
        this.setState({
            modalTitle: "Adaugă Stăpân",
            Nume: "",
            Prenume: "",
            Telefon: "",
            Email: "",
            IDStapan: 0
        });
        const modal = new window.bootstrap.Modal(document.getElementById("stapanModal"));
        modal.show();
    }

    editClick(stapan) {
        this.setState({
            modalTitle: "Editează Stăpân",
            IDStapan: stapan.IDStapan,
            Nume: stapan.Nume,
            Prenume: stapan.Prenume,
            Telefon: stapan.Telefon,
            Email: stapan.Email
        });
        const modal = new window.bootstrap.Modal(document.getElementById("stapanModal"));
        modal.show();
    }

    createClick() {
        fetch(variables.API_URL + 'Stapani', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nume: this.state.Nume,
                Prenume: this.state.Prenume,
                Telefon: this.state.Telefon,
                Email: this.state.Email
            })
        }).then(() => {
            this.refreshList();
            window.bootstrap.Modal.getInstance(document.getElementById("stapanModal")).hide();
        });
    }

    updateClick() {
        fetch(variables.API_URL + 'Stapani/' + this.state.IDStapan, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nume: this.state.Nume,
                Prenume: this.state.Prenume,
                Telefon: this.state.Telefon,
                Email: this.state.Email
            })
        }).then(response => {
            if (response.ok) {
                alert('Modificările au fost salvate cu succes!');
                this.refreshList();
                window.bootstrap.Modal.getInstance(document.getElementById("stapanModal")).hide();
            } else {
                alert('Eroare la actualizare!');
            }
        });
    }

    deleteClick(id) {
        if (window.confirm('Ești sigur că vrei să ștergi?')) {
            fetch(variables.API_URL + 'Stapani/' + id, { method: 'DELETE' })
                .then(() => this.refreshList());
        }
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState({ [name]: value });
    }

    render() {
        return (
            <div className="container">
                <button className="btn btn-outline-dark my-3" onClick={() => this.addClick()}>
                    Adaugă Stăpân
                </button>

                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Nume</th>
                            <th>Prenume</th>
                            <th>Telefon</th>
                            <th>Email</th>
                            <th>Acțiuni</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.stapani.map(stapan => (
                            <tr key={stapan.IDStapan}>
                                <td>{stapan.Nume}</td>
                                <td>{stapan.Prenume}</td>
                                <td>{stapan.Telefon}</td>
                                <td>{stapan.Email}</td>
                                <td>
                                    <button className="btn m-1 btn-edit"
                                        onClick={() => this.editClick(stapan)}>
                                        Editează
                                    </button>
                                    <button className="btn btn-delete"
                                        onClick={() => this.deleteClick(stapan.IDStapan)}>
                                        Șterge
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {/* Modal pentru Adăugare/Modificare */}
                <div className="modal fade" id="stapanModal" tabIndex="-1" aria-labelledby="stapanModalLabel" aria-hidden="true">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="stapanModalLabel">{this.state.modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body">
                                <div className="form-group mb-3">
                                    <label>Nume</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="Nume"
                                        value={this.state.Nume}
                                        onChange={this.handleChange}
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Prenume</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="Prenume"
                                        value={this.state.Prenume}
                                        onChange={this.handleChange}
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Telefon</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="Telefon"
                                        value={this.state.Telefon}
                                        onChange={this.handleChange}
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Email</label>
                                    <input
                                        type="email"
                                        className="form-control"
                                        name="Email"
                                        value={this.state.Email}
                                        onChange={this.handleChange}
                                    />
                                </div>
                            </div>
                            <div className="modal-footer">
                                {this.state.IDStapan === 0 ? (
                                    <button type="button" className="btn btn-success" onClick={() => this.createClick()}>
                                        Adaugă
                                    </button>
                                ) : (
                                    <button type="button" className="btn btn-primary" onClick={() => this.updateClick()}>
                                        Salvează modificările
                                    </button>
                                )}
                                <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">
                                    Închide
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
