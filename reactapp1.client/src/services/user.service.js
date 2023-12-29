import axios from 'axios'
import authHeader from './auth.header'


const API_URL = https://localhost:7139/Admin/
class UserService {

    adminDashboard() {
        return axios.get(API_URL + 'Dashboard');
    }

}


export default new UserService();