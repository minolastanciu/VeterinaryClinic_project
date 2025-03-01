import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class Consultatie extends Component {
    constructor(props) {
        super(props);
        this.state = {
            consultatie: [],
            modalTitle: "",
            IDProgramare: 0,
            IDAnimal: 0,
            IDMedic: 0,
            Data: ""
        };
    }

    refreshList() {
        fetch(variables.API_URL + 'Consultatie')
            .then(response => response.json())
            .then(data => {
                this.setState({ consultatie: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    render() {
        return (
            <div>
                <h3>Consultații</h3>
                <table className="table">
                    <thead>
                        <tr>
                            <th>ID Animal</th>
                            <th>ID Medic</th>
                            <th>Data Consultației</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.consultatie.map(c => (
                            <tr key={c.IDProgramare}>
                                <td>{c.IDAnimal}</td>
                                <td>{c.IDMedic}</td>
                                <td>{c.Data}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }
}
