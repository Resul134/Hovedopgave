import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export async function GetTasks() {
    return axios.get(`${apiEndPoint}/tasks`, {
    });
}
export function GetTasksByCategory(id: number) {
    return axios.get(`${apiEndPoint}/tasks/category/${id}`);
}
export function GetTasksByFilter(categoryId?: string, region?: string, minPrice?: string, maxPrice?: string, minDate?: string, maxDate?: string) {
    let apiString = `${apiEndPoint}/tasks/filter?`;
    if (categoryId) {
        apiString += `categoryId=${categoryId}&`;
    }
    if (region) {
        apiString += `region=${region}&`;
    }
    if (minPrice && maxPrice) {
        apiString += `priceLow=${minPrice}&priceHigh=${maxPrice}&`;
    }
    if (minDate && maxDate) {
        apiString += `dateStart=${minDate}&dateEnd=${maxDate}`;
    }
    console.log(apiString);
    return axios.get(apiString);
}
