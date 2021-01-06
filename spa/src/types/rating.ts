export interface Rating {
    id: number;
    userID: number;
    taskID: number;
    date: Date;
    rating: number;
    message: string;
}

export interface RatingPlus {
    name: string;
    date: Date;
    rating: number;
    message: string;
}
