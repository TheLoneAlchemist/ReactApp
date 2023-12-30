import { useState } from "react";
import userService from "../services/user.service"
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";


const Dashboard = () => {

    const navigate = useNavigate();

    let [data, setData] = useState([]);
    let [isLoading, setLoading] = useState(true);
    let [isError, setError] = useState(false)
    try {

        useEffect(() => {
            userService.adminDashboard()
                .then(res => { setData(res.data); setLoading(false) })
                .catch(error => { console.log(error);  setError(true) })
        }, [])

    } catch (e) {
        console.log(e)
    }

    if (isError) {
       
        return <div>Something went wrong! ... </div>
    }

    if (isLoading) {
        return <div> Loading... </div>
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