import { useEffect, useState } from 'react';
import './App.css';

import { BrowserRouter,Routes,Route,Link } from 'react-router-dom'
import Register from './components/Register';
import Login from './components/Login';
import Dashboard from './components/Dashboard';
import Logout from './components/Logout';

import Notify from './components/Notify';



function App() {

    return (
        <div>
            
            {/*<div>*/}
            {/*<button type="button" >Login</button>*/}
            {/*<button type="button" >Register</button>*/}
            {/*</div>*/}

            <div>
                <Notify/>
            </div>

            <BrowserRouter>
                <ul id="navlinks">
                    <li>
                    
                        <Link to="/Register">Register</Link>
                    </li>
                    <li>
                        <Link to="/Login">Login</Link>

                    </li>
                    <li>
                        <Link to="/Dashboard">Dashboard</Link>

                    </li>
                    <li>
                        <Link to="/Logout">Logout</Link>

                    </li>
                </ul>
                <Routes>
                    <Route
                        exact
                        path="/Register"
                        element={<Register />}
                    ></Route>
                    <Route
                        exact
                        path="/Login"
                        element={<Login />}
                    ></Route>
                    <Route
                        exact
                        path="/Dashboard"
                        element={<Dashboard />}
                    ></Route>
                    <Route
                        exact
                        path="/Logout"
                        element={<Logout />}
                    ></Route>
                </Routes>
            </BrowserRouter>
        </div>

    )
}

export default App;