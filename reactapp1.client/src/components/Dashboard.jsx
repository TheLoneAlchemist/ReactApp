import { useState } from "react";
import userService from "../services/user.service"
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import CircleLoader from "react-spinners/CircleLoader";



const Dashboard = () => {

    const custom = {
        display: 'block',
        margin: 'auto',
    }

    const color = '#36d7b7';
    
    const navigate = useNavigate();

    let [data, setData] = useState([]);
    let [loading, setLoading] = useState(true);
    let [isError, setError] = useState(false)
    try {

        useEffect(() => {
            userService.adminDashboard()
                .then(res => { setData(res.data); setLoading(false) })
                .catch(error => { console.log(error); setError(true); navigate("/Login") })
        }, [])

    } catch (e) {
        console.log(e)
    }

    if (isError) {
       
        return <div>Something went wrong! ... </div>
    }

    if (loading) {
        return <div> Loading...
            <CircleLoader color="#36d7b7"
                color={color}
                loading={loading}
                cssOverride={custom}
                size={150}
                aria-label="Loading Spinner"
                data-testid="loader"
            />

        </div>
    }

    if (data.length < 1) {
        return "No Data"
    }

    return (
        <div>

            Dashboard
            <hr />
            <div>

               
                <table style={{ color: "#006ccb", background: "cadetblue" }} >
                    <thead>
                        <tr>
                        <th>FirstName</th>
                        <th>LastName</th>
                        <th>Email</th>
                        </tr>
                    </thead>
                    <tbody >
                    {
                        data.map(u => {
                            return (
                                <tr>
                                    <td>
                                        {u.firstName }
                                    </td>
                                    <td>
                                        {u.lastName}
                                    </td>
                                    <td>
                                        {u.email}
                                    </td>
                                </tr>
                        )
                        })
                    }
                    </tbody>
                </table>

            </div>

        </div>
    );

}

export default Dashboard;