import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class Animale extends Component {
    constructor(props) {
        super(props);
        this.state = {
            animale: [],
            modalTitle: "",
            Nume: "",
            Specie: "",
            DataNasterii: "",
            Rasa: "",
            IDAnimal: 0
        };
    }

    refreshList() {
        fetch(variables.API_URL + 'Animale')
            .then(response => response.json())
            .then(data => {
                this.setState({ animale: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    addClick() {
        this.setState({
            modalTitle: "Adaugă Animal",
            Nume: "",
            Specie: "",
            DataNasterii: "",
            Rasa: "",
            IDAnimal: 0
        });
        // Afișează modalul
        const modal = new window.bootstrap.Modal(document.getElementById("animalModal"));
        modal.show();
    }

    editClick(ani) {
        this.setState({
            modalTitle: "Editează Animal",
            IDAnimal: ani.IDAnimal,
            Nume: ani.Nume,
            Specie: ani.Specie,
            DataNasterii: ani.DataNasterii ? ani.DataNasterii.split('T')[0] : "",  // Extrage doar partea de "YYYY-MM-DD"
            Rasa: ani.Rasa
        });
        // Afișează modalul
        const modal = new window.bootstrap.Modal(document.getElementById("animalModal"));
        modal.show();
    }

    createClick() {
        fetch(variables.API_URL + 'Animale', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nume: this.state.Nume,
                Specie: this.state.Specie,
                DataNasterii: this.state.DataNasterii,
                Rasa: this.state.Rasa
            })
        }).then(() => {
            this.refreshList();
            window.bootstrap.Modal.getInstance(document.getElementById("animalModal")).hide();
        });
    }

    updateClick() {
        fetch(variables.API_URL + 'Animale/' + this.state.IDAnimal, {  // Trimite ID-ul în URL
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                IDAnimal: this.state.IDAnimal,  // Asigură-te că ID-ul este inclus în body
                Nume: this.state.Nume,
                Specie: this.state.Specie,
                DataNasterii: this.state.DataNasterii,
                Rasa: this.state.Rasa
            })
        }).then(response => {
            if (response.ok) {
                alert('Modificările au fost salvate cu succes!');
                this.refreshList();
                window.bootstrap.Modal.getInstance(document.getElementById("animalModal")).hide();
            } else {
                alert('Eroare la actualizare!');
            }
        });
    }
    
    

    deleteClick(id) {
        if (window.confirm('Ești sigur că vrei să ștergi?')) {
            fetch(variables.API_URL + 'Animale/' + id, { method: 'DELETE' })
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
                    Adaugă Animal
                </button>

                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Nume</th>
                            <th>Specie</th>
                            <th>Data Nașterii</th>
                            <th>Rasă</th>
                            <th>Acțiuni</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.animale.map(ani => (
                            <tr key={ani.IDAnimal}>
                                <td>{ani.Nume}</td>
                                <td>{ani.Specie}</td>
                                <td>{ani.DataNasterii}</td>
                                <td>{ani.Rasa}</td>
                                <td>
                                    <button className="btn m-1 btn-edit"
                                        onClick={() => this.editClick(ani)}>
                                        Editează
                                    </button>
                                    <button className="btn btn-delete"
                                        onClick={() => this.deleteClick(ani.IDAnimal)}>
                                        Șterge
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {/* Modal pentru Adăugare/Modificare */}
                <div className="modal fade" id="animalModal" tabIndex="-1" aria-labelledby="animalModalLabel" aria-hidden="true">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="animalModalLabel">{this.state.modalTitle}</h5>
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
                                    <label>Specie</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="Specie"
                                        value={this.state.Specie}
                                        onChange={this.handleChange}
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Data Nașterii</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        name="DataNasterii"
                                        value={this.state.DataNasterii}
                                        onChange={this.handleChange}
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Rasă</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="Rasa"
                                        value={this.state.Rasa}
                                        onChange={this.handleChange}
                                    />
                                </div>
                            </div>
                            <div className="modal-footer">
                                {this.state.IDAnimal === 0 ? (
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
