import axios from "axios";
import apiEndPoint from "../api/apiEndPoint";

export function OpretBruger(FirstName: string, LastName: string, Birthday: Date, Gender: string, Phone: number, Email: string, Username: string, Password: string) {
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
        Suspended: false
    });
}
