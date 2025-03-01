import './App.css';
import { Animale } from './components/Animale';
import { Stapani } from './components/Stapani';
import { Medici } from './components/Medici';
import { Consultatie } from './components/Consultatie';
import { InregistrareMedicala } from './components/InregistrareMedicala';
import { LogIn } from './components/LogIn';
import { Home } from './components/Home';
import { BrowserRouter, Route, Switch, NavLink } from 'react-router-dom';


function App() {
    return (
        <BrowserRouter>
            <div className="App">
                <nav className="navbar navbar-expand-sm navbar-light" style={{ backgroundColor: 'lightgreen' }}>
                    <img src="/logo.png" alt="Cabinet Logo" className="navbar-logo" />
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/">
                                Log In
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/home">
                                Home
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/animale">
                                Animale
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/stapani">
                                Stăpâni
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/medici">
                                Medici
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/consultatie">
                                Consultații
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="btn btn-outline-dark m-1" to="/inregistrare-medicala">
                                Înregistrări Medicale
                            </NavLink>
                        </li>
                    </ul>
                </nav>

                <Switch>
                    <Route exact path="/" component={LogIn} />
                    <Route path="/home" component={Home} />
                    <Route path="/animale" component={Animale} />
                    <Route path="/stapani" component={Stapani} />
                    <Route path="/medici" component={Medici} />
                    <Route path="/consultatie" component={Consultatie} />
                    <Route path="/inregistrare-medicala" component={InregistrareMedicala} />
                    <Route path="*" component={() => <div>Pagina nu a fost găsită</div>} />
                </Switch>
            </div>
        </BrowserRouter>
    );
}

export default App;
