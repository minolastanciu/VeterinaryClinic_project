import React, { Component } from 'react';
import { variables } from '../Variables.js';

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            results: [],
            loading: false,
            selectedQuery: "",
            showInputForm: false,
            numeAnimal: "",
            medicament: "",
            diagnosticCount: "",
        };
    }

    handleSearch = (endpoint, params = {}) => {
        this.setState({ loading: true, selectedQuery: endpoint });
        const url = `${variables.API_URL}Home/${endpoint}?${new URLSearchParams(params).toString()}`;
        fetch(url)
            .then(response => response.json())
            .then(data => {
                this.setState({ results: data, loading: false });
            })
            .catch(error => {
                console.error("Eroare:", error);
                this.setState({ loading: false });
            });
    };

    // Funcție pentru a afișa formularul în funcție de interogare
    handleButtonClick = (queryType) => {
        this.setState({
            showInputForm: true,
            selectedQuery: queryType,
            numeAnimal: "",
            medicament: "",
            diagnosticCount: ""
        });
    };

    renderForm = () => {
        const { selectedQuery, numeAnimal, medicament, diagnosticCount } = this.state;

        switch (selectedQuery) {
            case "AnimalMedic":
                return (
                    <div className="mb-3">
                        <input
                            type="text"
                            placeholder="Introduceți numele animalului"
                            className="form-control mb-2"
                            value={numeAnimal}
                            onChange={(e) => this.setState({ numeAnimal: e.target.value })}
                        />
                        <button className="btn btn-primary" onClick={() => this.handleSearch("AnimalMedic", { numeAnimal })}>
                            Caută
                        </button>
                    </div>
                );
            case "AnimaleMedicament":
                return (
                    <div className="mb-3">
                        <input
                            type="text"
                            placeholder="Introduceți numele medicamentului"
                            className="form-control mb-2"
                            value={medicament}
                            onChange={(e) => this.setState({ medicament: e.target.value })}
                        />
                        <button className="btn btn-primary" onClick={() => this.handleSearch("AnimaleMedicament", { medicament })}>
                            Caută
                        </button>
                    </div>
                );
            case "DiagnosticNumarApariții":
                return (
                    <div className="mb-3">
                        <input
                            type="number"
                            placeholder="Introduceți numărul de apariții"
                            className="form-control mb-2"
                            value={diagnosticCount}
                            onChange={(e) => this.setState({ diagnosticCount: e.target.value })}
                        />
                        <button className="btn btn-primary" onClick={() => this.handleSearch("DiagnosticNumarApariții", { diagnosticCount })}>
                            Caută
                        </button>
                    </div>
                );
            default:
                return null;
        }
    };

    render() {
        const { results, loading, selectedQuery, showInputForm } = this.state;

        return (
            <div className="container mt-4">
                <h3 className="text-center mb-4">Cabinet Veterinar Royal</h3>

                {/* Butoane pentru fiecare interogare */}
                <div className="d-flex flex-wrap justify-content-center">
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleButtonClick("AnimalMedic")}>
                        Informații despre animal și medic
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleButtonClick("AnimaleMedicament")}>
                        Animale cu medicamente prescrise
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("MediciMaxAnimale")}>
                        Medici cu cele mai multe animale tratate
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("ServiciiCaini")}>
                        Servicii și diagnostic pentru "Câine"
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("MediciProgramariDesc")}>
                        Medici cu cele mai multe programări
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("MediciTratamentFizioterapie")}>
                        Medici care au prescris "Fizioterapie"
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("MediciFaraConsultatii")}>
                        Medici care nu au consultat animale
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleButtonClick("DiagnosticNumarApariții")}>
                        Diagnostic care apare de X ori
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("AnimaleMultipleDiagnostice")}>
                        Animale cu mai mult de 2 diagnostice
                    </button>
                    <button className="btn btn-outline-dark m-2" onClick={() => this.handleSearch("AnimaleFaraStapan")}>
                        Animale fără stăpân (complex)
                    </button>

                </div>

                {/* Formular de input pentru interogările cu parametri */}
                {showInputForm && (
                    <div className="mt-4">
                        <h5 className="text-primary">Introduceți parametrii pentru interogarea: {selectedQuery}</h5>
                        {this.renderForm()}
                    </div>
                )}

                {/* Rezultatele interogărilor */}
                <div className="mt-4">
                    <h5>Rezultate:</h5>
                    {loading ? (
                        <div className="text-center">Se încarcă datele...</div>
                    ) : (
                        <pre className="bg-light p-3 border" style={{ maxHeight: "300px", overflowY: "auto" }}>
                            {results.length > 0 ? JSON.stringify(results, null, 2) : "Nu există date pentru această interogare."}
                        </pre>
                    )}
                </div>
            </div>
        );
    }
}
