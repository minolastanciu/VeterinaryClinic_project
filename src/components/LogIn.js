import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

export class LogIn extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: "",
      password: "",
    };
  }

  handleChange = (event) => {
    const { name, value } = event.target;
    this.setState({ [name]: value });
  };

  handleSubmit = (event) => {
    event.preventDefault();
    console.log("Formular trimis");  // Verifică în consolă
    if (this.state.username === 'admin' && this.state.password === 'cabinet1234') {
      this.props.history.push('/animale');  // Navigare către pagina Animale
    } else {
      alert("Credențiale incorecte!");
    }
  };  

  render() {
    return (
      <div className="d-flex justify-content-center align-items-center vh-100">
        <form
          onSubmit={this.handleSubmit}
          className="p-5 rounded shadow"
          style={{
            backgroundColor: "lightgreen",
            width: "350px",
            border: "2px solid darkgreen",
          }}
        >
          <h3 className="text-center mb-4 text-dark">Autentificare Admin</h3>
          <div className="mb-3">
            <label className="form-label">Username</label>
            <input
              type="text"
              className="form-control"
              name="username"
              value={this.state.username}
              onChange={this.handleChange}
              required
              style={{ border: "2px solid darkgreen" }}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Parolă</label>
            <input
              type="password"
              className="form-control"
              name="password"
              value={this.state.password}
              onChange={this.handleChange}
              required
              style={{ border: "2px solid darkgreen" }}
            />
          </div>
          <button
            type="submit"
            className="btn w-100"
            style={{
              backgroundColor: "white",
              color: "darkgreen",
              border: "2px solid darkgreen",
            }}
          >
            Login
          </button>
        </form>
      </div>
    );
  }
}

export default withRouter(LogIn);
