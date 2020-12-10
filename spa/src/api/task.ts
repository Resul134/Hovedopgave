import axios from "axios";
import apiEndPoint from "./apiEndPoint";

export async function GetTasks() {
    return axios.get(`${apiEndPoint}/tasks`, {
    });
}
export function GetTaskById(id: number) {
    return axios.get(`${apiEndPoint}/tasks/${id}`);
}
export function GetTasksByCategory(id: number) {
    return axios.get(`${apiEndPoint}/tasks/category/${id}`);
}
export function DeleteAllTaskByUserID(id: number) {
    return axios.delete(`${apiEndPoint}tasks/deleteTaskByID/${id}`);
}

export function CreateTask(UserID: number, CategoryID: number, DateCreated: string, Title: string, Price: number, Description: string, Promoted: boolean, Region: string, promotedEnd: string) {
    return axios.post(`${apiEndPoint}/tasks`, {
        UserID: UserID,
        CategoryID: CategoryID,
        Date: DateCreated,
        Title: Title,
        Price: Price,
        Description: Description,
        Status: "Ledig",
        Promoted: Promoted,
        Region: Region,
        PromotedEnd: promotedEnd,
        PageViews: 0
    });
}
export function GetTasksByFilter(categoryId: string, region: string, minPrice: string, maxPrice: string, minDate: string, maxDate: string, search: string) {
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
    if (search) {
        apiString += `search=${search}&`;
    }
    if (minDate && maxDate) {
        apiString += `dateStart=${minDate}&dateEnd=${maxDate}`;
    }
    //  console.log(apiString);
    return axios.get(apiString);
}
