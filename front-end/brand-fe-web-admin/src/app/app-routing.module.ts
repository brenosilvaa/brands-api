import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandFormComponent } from './features/brand/brand-form/brand-form.component';
import { BrandListComponent } from './features/brand/brand-list/brand-list.component';
import { HomeComponent } from './features/home/home.component';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { LoginLayoutComponent } from './layouts/login-layout/login-layout.component';
import { AppGuard } from './app-guard';



const routes: Routes = [
  { path: 'login', component: LoginLayoutComponent },
  {
    path: '', canActivate: [AppGuard], canActivateChild: [AppGuard], component: DefaultLayoutComponent, children: [
      { path: '', component: HomeComponent },

      // Brands
      { path: 'brands', component: BrandListComponent },
      { path: 'brands/form', component: BrandFormComponent },
      { path: 'brands/form/:id', component: BrandFormComponent },
      { path: '**', component: NotFoundComponent }
    ]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
