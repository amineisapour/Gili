export interface MenuItem {
    id: number;
    title: string;
    type: 'item' | 'collapse';
    url: string;
    icon: string;
    lineNeed: boolean;
    children?: MenuItem[] | null;
}
