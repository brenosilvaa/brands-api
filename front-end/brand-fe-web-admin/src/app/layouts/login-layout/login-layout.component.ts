import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-login-layout',
  templateUrl: './login-layout.component.html',
  styleUrls: ['./login-layout.component.scss']
})
export class LoginLayoutComponent implements OnInit {

  form!: FormGroup;
  isLoggingIn: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _service: LoginService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required]
    });
  }

  login(): void {
    this.isLoggingIn = true;
    this._service.login(this.form.value).subscribe({
      next: _ => {
        this._router.navigate(['/']);
        this.isLoggingIn = false;
      },
      error: (err) => {
        this.isLoggingIn = false;
      }
    });
  }


}
