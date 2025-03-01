import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class InregistrareMedicala extends Component {
    constructor(props) {
        super(props);
        this.state = {
            inregistrari: [],
            modalTitle: "",
            IDProgramare: 0,
            Diagnostic: "",
            Tratament: "",
            MedicamentePrescrise: "",
            Data: ""
        };
    }

    refreshList() {
        fetch(variables.API_URL + 'InregistrareMedicala')
            .then(response => response.json())
            .then(data => {
                this.setState({ inregistrari: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    render() {
        return (
            <div>
                <h3>Înregistrări Medicale</h3>
                <table className="table">
                    <thead>
                        <tr>
                            <th>ID Programare</th>
                            <th>Diagnostic</th>
                            <th>Tratament</th>
                            <th>Medicamente Prescrise</th>
                            <th>Data</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.inregistrari.map(i => (
                            <tr key={i.IDInregistrare}>
                                <td>{i.IDProgramare}</td>
                                <td>{i.Diagnostic}</td>
                                <td>{i.Tratament}</td>
                                <td>{i.MedicamentePrescrise}</td>
                                <td>{i.Data}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }
}
