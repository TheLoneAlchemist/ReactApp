export default function authHeader() {
    const token = localStorage.getItem("accessToken");
    console.log(token +"+++++++++++++++++++++++")
    if (token) {
        return { Authorization: 'Bearer ' + token}
    }
    else {
        return {}
    }
}