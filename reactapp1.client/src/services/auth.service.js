import axios from "axios"


const API = "https://localhost:7139/Account/"

class AuthService {


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

    logout() { }


    register(firstname, lastname, email, password, confirmpassword) {
        console.log(API + "register")


        return axios.post(API + "Register",
            {
                withCredentials: true,
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Content-Type': 'application/json',
                },
                data: {
                    firstname,
                    lastname,
                    email,
                    password,
                    confirmpassword
                }
            }

        ).then(res => {
            console.log(res);
        });



    }
}


export default new AuthService();