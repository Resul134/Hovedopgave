import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export function GetRatings() {
    return axios.get(`${apiEndPoint}/ratings`);
}

export function GetRatingsByUserId(id: number) {
    return axios.get(`${apiEndPoint}/ratings/user/${id}`);
}
