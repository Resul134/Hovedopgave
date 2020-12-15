import axios from "axios";
import apiEndPoint from "../api/apiEndPoint";

export async function GetQualifications() {
    return axios.get(`${apiEndPoint}/qualifications`, {
    });
}

export function OpretBruger(UserID: number, Skill: string) {
    return axios.post(`${apiEndPoint}/qualifications`, {
        UserID: UserID,
        Skill: Skill
    });
}

export function RedigerQualification(id: number, UserID: number, Skill: string) {
    return axios.put(`${apiEndPoint}/qualifications/${id}`, {
        UserID: UserID,
        Skill: Skill
    });
}

export function GetQualificationById(id: number) {
    return axios.get(`${apiEndPoint}/qualifications/${id}`);
}

export function DeleteQualificationById(id: number) {
    return axios.delete(`${apiEndPoint}/qualifications/${id}`);
}
