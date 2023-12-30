import axios from 'axios'
import authHeader from './auth.header'
import api from './interceptor';



const API_URL = 'https://localhost:7139/Dashboard/'


class UserService {

    

    //injecting bearer token in each request by authHeader
    async adminDashboard1() {
        return await axios.get(API_URL + 'GetUsers', { headers: authHeader() })
            .then(res => { console.log(res.data[0]); return res; }).
            catch(error => { console.log(error + " axios error : --------------"); throw error });
            
        
    }

    // inject token axios requests
    async adminDashboard() {
       
        return await api.get(API_URL + 'GetUsers')
            .then(res => { console.log(res.data[0]); return res; })
            .catch(error => { console.log(error + " axios error : --------------");throw error });
    }
}


export default new UserService();