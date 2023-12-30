
import axios from 'axios'
import { useNavigate } from "react-router-dom";


const baseURL = "https://localhost:7139/";

const api = axios.create({ baseURL: baseURL });

api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('accessToken');

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

export default api;



// Add a response interceptor
api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;

        // If the error status is 401 and there is no originalRequest._retry flag,
        // it means the token has expired and we need to refresh it
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            try {
                const refreshTkn = localStorage.getItem('refreshToken');
                const accessTkn = localStorage.getItem('accessToken');
          

                if (refreshTkn == "undefined") {
                    return Promise.reject("Refreshtoken is undefined!");
                }

                const response = await axios.post(baseURL + 'Account/Refresh', { refreshTkn, accessTkn });
                console.log(response);
                
                const { accessToken, refreshToken } = response.data;

                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('refreshToken', refreshToken);

                // Retry the original request with the new token
                originalRequest.headers.Authorization = `Bearer ${accessToken}`;
                return axios(originalRequest);
            } catch (error) {
                console.log("Intercept Error: " + error.code =="ERR_BAD_REQUEST");
                // Handle refresh token error or redirect to login
                
            }
        }

        return Promise.reject(error);
    }
);
