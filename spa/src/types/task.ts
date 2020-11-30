export interface Task {
    id: number;
    userId: number;
    categoryId: number;
    date: Date;
    title: string;
    price: number;
    description: string;
    status: string;
    promoted: boolean;
    region: string;
    promotedEnd: Date;
    pageViews: number;
}

export interface SnippetTask {
    id: number;
    title: string;
    price: string;
    category: string;
    description: string;
    promoted: boolean;
    region: string;
}