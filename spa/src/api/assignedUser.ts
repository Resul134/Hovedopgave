import axios from "axios";
import apiEndPoint from "../api/apiEndPoint";

export function GetAssignedUserById(id: number) {
    return axios.get(`${apiEndPoint}/assignedusers/${id}`);
}

export function GetAssignedUsersOnMyTask(taskID: number) {
    return axios.get(`${apiEndPoint}assignedusers/AssignedUsersMyTask/${taskID}`);
}

export function GetAssignedUserMatch(taskID: number, userID: number) {
    return axios.get(`${apiEndPoint}/assignedusers/task/${taskID}/user/${userID}`);
}

export function RemoveAssignedUser(userid: number) {
    return axios.delete(`${apiEndPoint}/assignedusers/task/RemoveAssignedUser/${userid}`);
}

export async function GetMyAcceptedAssignments(userid: number) {
    return await axios.get(`${apiEndPoint}/assignedusers/GetAcceptedAssignments/${userid}`);
}

export async function GetMyDeniedAssignments(userid: number) {
    return await axios.get(`${apiEndPoint}/assignedusers/GetDeniedAssignments/${userid}`);
}

export function OpretAssignedUser(taskID: number, userID: number, accepted: boolean) {
    return axios.post(`${apiEndPoint}/assignedusers`, {
        TaskID: taskID,
        UserID: userID,
        Accepted: accepted
    });
}

export function RemoveUsersWhenAccepted(taskid: number) {
    return axios.delete(`${apiEndPoint}/assignedusers/deniedUsers/${taskid}`);
}

export function UpdateAssignedUser(id: number, taskID: number, userID: number, accepted: boolean) {
    return axios.put(`${apiEndPoint}/assignedusers/${id}`, {
        TaskID: taskID,
        UserID: userID,
        Accepted: accepted
    });
}

export function DeleteAssignedUser(id: number) {
    return axios.delete(`${apiEndPoint}/assignedusers/${id}`);
}
