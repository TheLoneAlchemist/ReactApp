import { useState,useEffect } from "react"
import  AuthService  from "../services/auth.service";
import { useNavigate } from "react-router-dom";





const Register = () => {

    const navigate = useNavigate();
    const token = localStorage.getItem("accessToken");
    if (token != null && token != "undefined") {
        useEffect(() => {
            navigate("/Dashboard");
        })
    }


    const [email, onChangeEmail] = useState("");
    const [password, onChangePassword] = useState("");
    const [firstname, onChangefirstname] = useState("");
    const [lastname, onChangelastname] = useState("");
    const [cpassword, onChangecpassword] = useState("");

   function setEmail()
   {
       let email = document.querySelector("#email").value;
        onChangeEmail(email)
    }
    function setfirstname() {
        let firstname = document.querySelector("#firstname").value;
        onChangefirstname(firstname);
    }

    function setlastname() {
        let lastname = document.querySelector("#lastname").value;
        onChangelastname(lastname);
    }
    function setPassword() {
        let password = document.querySelector("#password").value;
        onChangePassword(password)
    }
    function setcpassword() {
        let cpassword = document.querySelector("#cpassword").value;
        onChangecpassword(cpassword)
    }
    function Register() {


        //fix cross origin 
        AuthService.register(firstname,lastname,email,password,cpassword) 
            .then(response => {
                console.log(response)
            }, error => {
                console.log(error);
            })
            
    }
   

    return (
       
        <div className="row">
        Home
            <div className="col">
                <form method="post">
                    <div className="form-group">
                        <label htmlFor="firstname" >firstname</label>
                        <input type="text" className="form-control" name="firstname" value={firstname} id="firstname" onChange={setfirstname} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="lastname" >lastname</label>
                        <input type="text" className="form-control" name="lastname" value={lastname} id="lastname" onChange={setlastname} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="email" >Email</label>
                        <input type="email" className="form-control" name="email" value={email} id="email" onChange={setEmail} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password" >Password</label>
                        <input type="password" className="form-control" name="password" id="password" value={password} onChange={setPassword } />
                    </div>
                    <div className="form-group">
                        <label htmlFor="cpassword" >cpassword</label>
                        <input type="cpassword" className="form-control" name="confirmpassword" id="cpassword" value={cpassword} onChange={setcpassword} />
                    </div>
                    <div className="form-group">
                        <button type="button" id="registerbtn" onClick={Register }> Register </button>
                    </div>

                </form>

            </div>
        </div>
    )
}

export default Register;