import { EventEmitter, Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DrawerService {

  @Output() drawerToggle: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  toggleDrawer(): void {
    this.drawerToggle.emit();
  }

}
