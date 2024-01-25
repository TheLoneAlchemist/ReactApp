import axios from "axios"
import Notify from "../components/Notify";
import api from "./interceptor";


const API = "https://localhost:7139/Account/"

class AuthService {


    /*

    login1(email, password) {
        const res = fetch(API + "Login", {

            method: 'POST',
            headers: { 'Access-Control-Allow-Origin': '*', 'Content-Type': 'application/json;charset=UTF-8' },
            body: JSON.stringify({
                email: email,
                password: password
            }),

        }


        ).then(res => {
            console.log(res);
        }).catch(errors => {
            console.log(errors);
        })
    }
    */
    /*
    login(email, password) {
        return axios.post(API + 'Login', {
            method: 'POST',
            withCredentials: true,
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json;charset=UTF-8',
            },
            data: {
                email: email,
                password: password
            }
        }, (errors) => {console.log(errors) } ).then(res => {



            if (res.data.accessToken) {
                console.log(res.data.accessToken);
                localStorage.setItem("user", JSON.stringify(response.data));
            }
            return res.data;
        });
    }
    */



    async login(email, password) {
        return await axios.post(API + "Login", { email: email, password: password })
            .then(res => {
                //console.log(res);
                if (res.data.status) {

                    if (res.data.accessToken) {
                        // console.log(res.data.accessToken);
                        // console.log(res.data.refreshToken);

                        localStorage.setItem("accessToken", res.data.accessToken);
                        localStorage.setItem("refreshToken", res.data.refreshToken);
                        Notify("success", "Login Successfully.")
                        return res.data.accessToken;
                    }
                }
                else {
                    console.log(res.data.msg);
                    throw new Error(res.data.msg);

                }
            })
            .catch(error => console.log(error))

    }

    async logout() {
        console.log("Revoking RToken");
        return await api.delete(API + "Revoke")
            .then(res => {
                console.log("Revoking message: " + res);
            }).catch(error => { console.log('Revoking Token Error: ' + error); return error; });

    }




    register(firstname, lastname, email, password, confirmpassword) {
        console.log(API + "register")


        return axios.post(API + "Register",
            {

                firstname,
                lastname,
                email,
                password,
                confirmpassword

            }

        ).then(res => {
            console.log(res);
        });



    }
}


export default new AuthService();