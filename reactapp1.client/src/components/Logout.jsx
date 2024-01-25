import { Navigate } from 'react-router-dom'
import authService from '../services/auth.service';
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { CSSProperties } from "react";
import ClipLoader from "react-spinners/ClipLoader";
import CircleLoader from "react-spinners/CircleLoader";
import RingLoader from "react-spinners/RingLoader";



//let styleoverride: React.CSSProperties = {
//    display: 'block',
//    margin: 'auto',

//}

const custom = {
    display: 'block',
    margin: 'auto',
}

const color = '#36d7b7';

const Logout = () => {
    const nav = useNavigate();
    let [loading, setLoading] = useState(true);
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {

        const [error, setError] = useState(false);
        const [state, setState] = useState(true);

        useEffect(() => {
            (async () => {
                setState(true);
                try {
                    const res = await authService.logout();
                    console.log("Logout Response: " + res);
                    localStorage.removeItem("accessToken");
                    localStorage.removeItem("refreshToken");

                } catch (e) {
                    console.log(e);
                }
                finally {
                    setState(false);
                    nav("/Login");
                }


            })();

        }, [state])


        if (state) {
            return (

                <div>
                    <div>Logging out... </div>
                    <RingLoader color="#36d7b7"
                        color={color}
                        loading={loading}
                        cssOverride={custom}
                        size={150}
                        aria-label="Loading Spinner"
                        data-testid="loader"
                    />
                </div >

            )
        }



    }


    else {
        return (

            <Navigate to='/Login' replace={true} />
        )
    }
}

export default Logout;