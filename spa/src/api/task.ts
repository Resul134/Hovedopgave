import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export async function GetTasks() {
    return axios.get(`${apiEndPoint}/tasks`, {
    });
}
export function GetTasksByCategory(id: number) {
    return axios.get(`${apiEndPoint}/tasks/category/${id}`);
}
