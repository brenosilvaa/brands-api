import { Component, OnInit } from '@angular/core';
import { MenuItem } from './models/menu-item';
import { DrawerService } from 'src/app/shared/services/drawer.service';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss']
})
export class DefaultLayoutComponent implements OnInit {

  isDrawerOpened: boolean = true;

  menuItems: MenuItem[] = [
    new MenuItem('Home', 'house', ['/']),
    new MenuItem('Marcas', 'tags', ['/brands']),
  ];

  constructor(
    private _drawerService: DrawerService,
  ) { }

  ngOnInit(): void {
    // Controle do drawer
    this._drawerService.drawerToggle.subscribe({
      next: () => {
        this.isDrawerOpened = !this.isDrawerOpened;
      }
    });

  }
}
