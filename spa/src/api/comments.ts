import axios, { AxiosResponse } from "axios";
import apiEndPoint from "./apiEndPoint";
import Comment from "../types/comments";
import moment from "moment";

export function OpretComment(taskID: number, userID: number, date: Date, message: string): Promise<AxiosResponse<Comment>> {
    return axios.post(`${apiEndPoint}/comments`, {
        taskID: taskID,
        userID: userID,
        date: date,
        message: message
    } as Comment);
}

export function GetCommentsForTask(taskID: number) {
    return axios.get(`${apiEndPoint}/comments/task/${taskID}`);
}
