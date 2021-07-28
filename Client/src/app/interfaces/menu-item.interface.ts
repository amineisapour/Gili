export interface MenuItem {
    id: number;
    title: string;
    icon: string;
    path: string;
    lineNeed?: boolean;
    children?: MenuItem[] | null | undefined;
}
