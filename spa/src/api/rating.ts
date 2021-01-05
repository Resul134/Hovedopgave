import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export function GetRatings() {
    return axios.get(`${apiEndPoint}/ratings`);
}

export function GetRatingsByUserId(id: number) {
    return axios.get(`${apiEndPoint}/ratings/user/${id}`);
}

export function GetRatingByUserIdAndTaskId(userId: number, taskId: number) {
    return axios.get(`${apiEndPoint}/ratings/user/${userId}/task/${taskId}`);
}

export function CreateRating(UserID: number, TaskID: number, Date: Date, Rating: number, Message: string) {
    return axios.post(`${apiEndPoint}ratings`, {
        UserID: UserID,
        TaskID: TaskID,
        Date: Date,
        Rating: Rating,
        Message: Message
    });
}
