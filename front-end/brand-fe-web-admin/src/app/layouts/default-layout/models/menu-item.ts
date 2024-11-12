import { IconName } from "@fortawesome/fontawesome-svg-core";

export class MenuItem {
    title: string;
    route: string[];
    icon: IconName;

    constructor(title: string, icon: IconName, route: string[]) {
        this.title = title;
        this.icon = icon;
        this.route = route;
    }
}