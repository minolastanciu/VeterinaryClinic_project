import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class Medici extends Component {
    constructor(props) {
        super(props);
        this.state = {
            medici: [],
            modalTitle: "",
            Nume: "",
            Specializare: "",
            IDMedic: 0
        };
    }

    refreshList() {
        fetch(variables.API_URL + 'Medici')
            .then(response => response.json())
            .then(data => {
                this.setState({ medici: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    addClick() {
        this.setState({
            modalTitle: "Adaugă Medic",
            IDMedic: 0,
            Nume: "",
            Specializare: ""
        });
    }

    editClick(med) {
        this.setState({
            modalTitle: "Editează Medic",
            IDMedic: med.IDMedic,
            Nume: med.Nume,
            Specializare: med.Specializare
        });
    }

    createClick() {
        fetch(variables.API_URL + 'Medici', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nume: this.state.Nume,
                Specializare: this.state.Specializare
            })
        }).then(() => this.refreshList());
    }

    updateClick() {
        fetch(variables.API_URL + 'Medici/' + this.state.IDMedic, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Nume: this.state.Nume,
                Specializare: this.state.Specializare
            })
        }).then(() => this.refreshList());
    }

    deleteClick(id) {
        if (window.confirm('Ești sigur că vrei să ștergi?')) {
            fetch(variables.API_URL + 'Medici/' + id, { method: 'DELETE' })
                .then(() => this.refreshList());
        }
    }

    render() {
        return (
            <div>
                <button className="btn btn-outline-dark m-2" onClick={() => this.addClick()}>
                    Adaugă Medic
                </button>
                <h3>Lista Medicilor</h3>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Nume</th>
                            <th>Specializare</th>
                            <th>Acțiuni</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.medici.map(med => (
                            <tr key={med.IDMedic}>
                                <td>{med.Nume}</td>
                                <td>{med.Specializare}</td>
                                <td>
                                    <button className="btn m-1 btn-edit"
                                        onClick={() => this.editClick(med)}>
                                        Editează
                                    </button>
                                    <button className="btn btn-delete"
                                        onClick={() => this.deleteClick(med.IDMedic)}>
                                        Șterge
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }
}
