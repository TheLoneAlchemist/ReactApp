import { Navigate } from 'react-router-dom'


const Logout = () => {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");

        return (
            <Navigate to='/Login' replace={true} />

        );
    }
    else {
        return (

            <Navigate to='/Login' replace={true} />
        )
    }
}

export default Logout;