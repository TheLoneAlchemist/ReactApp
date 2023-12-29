import { useEffect, useState } from 'react';
import './App.css';

import { BrowserRouter,Routes,Route,Link } from 'react-router-dom'
import Register from './components/Register';
import Login from './components/Login';

function App() {

    return (
        <div>
            <p>Home</p>
            <div>
            <button type="button" >Login</button>
            <button type="button" >Register</button>
            </div>

            <BrowserRouter>
                <ul>
                    <li>
                    
                        <Link to="/Register">Register</Link>
                    </li>
                    <li>
                        <Link to="/Login">Login</Link>

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
                </Routes>
            </BrowserRouter>
        </div>

    )
}

export default App;