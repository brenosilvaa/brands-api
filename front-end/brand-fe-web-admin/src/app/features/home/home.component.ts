import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  
  user !: User | null;

  constructor(private _loginService: LoginService) {}

  ngOnInit() { 
    this.findUser();
  }

  findUser(){
    this.user = this._loginService.getUser()
  }
}
