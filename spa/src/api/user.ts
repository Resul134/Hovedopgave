import axios from "axios";
import apiEndPoint from "../api/apiEndPoint";

export function OpretBruger(FirstName: string, LastName: string, Birthday: Date, Gender: string, Phone: number, Email: string, Username: string, Password: string, Description: string) {
    return axios.post(`${apiEndPoint}/users`, {
        FirstName: FirstName,
        LastName: LastName,
        Birthday: Birthday,
        Gender: Gender,
        Phone: Phone,
        Email: Email,
        Username: Username,
        Password: Password,
        Rating: 0,
        Suspended: false,
        Description: Description
    });
}

export function RedigerBruger(id: number, FirstName: string, LastName: string, Birthday: Date, Gender: string, Phone: number, Email: string, Username: string, Password: string, Description: string) {
    return axios.put(`${apiEndPoint}/users/${id}`, {
        FirstName: FirstName,
        LastName: LastName,
        Birthday: Birthday,
        Gender: Gender,
        Phone: Phone,
        Email: Email,
        Username: Username,
        Password: Password,
        Rating: 0,
        Suspended: false,
        Description: Description
    });
}

export function GetBrugerById(id: number) {
    return axios.get(`${apiEndPoint}/users/${id}`);
}

export function DeleteBrugerById(id: number) {
    return axios.delete(`${apiEndPoint}/users/${id}`);
}

export function GetUserByLogin(username: string, password: string) {
    return axios.get(`${apiEndPoint}/users/login/${username},${password}`);
}

export function GetLoggedInId() {
    return localStorage.getItem("user") as any;
}

export function Logout() {
    localStorage.removeItem("user");
}
