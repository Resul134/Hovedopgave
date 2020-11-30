import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export function GetCategories() {
    return axios.get(`${apiEndPoint}/categories`);
}

export function GetCategory(id: number) {
    return axios.get(`${apiEndPoint}/categories/${id}`);
}
