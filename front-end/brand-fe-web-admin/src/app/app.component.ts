import { Component, OnInit } from '@angular/core';
import { LoginService } from './shared/services/login.service';
import { DrawerService } from './shared/services/drawer.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {


  isLoggedIn: boolean = false;

  constructor(
    private _loginService: LoginService,
    private _drawerService: DrawerService
  ) { }

  ngOnInit(): void {
    this.isLoggedIn = this._loginService.isLoggedIn();

    this._loginService.onLoginChange.subscribe({
      next: (isLoggedIn: boolean) => this.isLoggedIn = isLoggedIn
    });

  }

  logout(): void {
    this._loginService.logout();
  }

  onDrawerToggle(): void {
    this._drawerService.toggleDrawer()
  }

}
