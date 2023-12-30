import authService from "../services/auth.service";
import { useEffect } from "react";

import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {

    //if Already Logged in then redirect 
    const navigate = useNavigate();
    const token = localStorage.getItem("accessToken");
    if (token!=null && token !="undefined") {
        useEffect(() => {
            navigate("/Dashboard");
        })
    }

    const [email, onChangeEmail] = useState("");
    const [password, onChangePassword] = useState("");

    function setEmail() {
        let email = document.querySelector("#email").value;
        onChangeEmail(email)
    }

    function setPassword() {
        let password = document.querySelector("#password").value;
        onChangePassword(password)
    }

    function Login() {
        authService.login(email, password)
            .then(res => {
                if (res) {

                    navigate("/Dashboard");

                }

            })
            .catch(error => { console.log(error) })

    }



    return (


        <div className="row">
            Home
            <div className="col">
                <form method="post">

                    <div className="form-group">
                        <label htmlFor="email" >Email</label>
                        <input type="email" className="form-control" name="email" value={email} id="email" onChange={setEmail} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password" >Password</label>
                        <input type="password" className="form-control" name="password" id="password" value={password} onChange={setPassword} />
                    </div>

                    <div className="form-group">
                        <button type="button" id="loginbtn" onClick={Login}> Login </button>
                    </div>

                </form>
            </div>
        </div>
    )


}
export default Login;

