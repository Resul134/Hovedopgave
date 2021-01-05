export interface Rating {
    id: number;
    userID: number;
    taskID: number;
    date: Date;
    rating: number;
    message: string;
}
