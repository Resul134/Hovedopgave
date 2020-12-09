import axios from "axios";
import apiEndPoint from "../api/apiEndPoint";

export function GetAssignedUserById(id: number) {
    return axios.get(`${apiEndPoint}/assignedusers/${id}`);
}

export function GetAssignedUserMatch(taskID: number, userID: number) {
    return axios.get(`${apiEndPoint}/assignedusers/task/${taskID}/user/${userID}`);
}

export function OpretAssignedUser(taskID: number, userID: number, accepted: boolean) {
    return axios.post(`${apiEndPoint}/assignedusers`, {
        TaskID: taskID,
        UserID: userID,
        Accepted: accepted
    });
}

export function DeleteAssignedUser(id: number) {
    return axios.delete(`${apiEndPoint}/assignedusers/${id}`);
}
