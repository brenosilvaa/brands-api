import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrandListComponent } from './features/brand/brand-list/brand-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './features/home/home.component';
import { MaterialModule } from './shared/modules/material.module';
import { BrandFormComponent } from './features/brand/brand-form/brand-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FaConfig, FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { TableComponent } from './shared/components/table/table.component';
import { AutoCompleteComponent } from './shared/components/auto-complete/auto-complete/auto-complete.component';
import { CardFormComponent } from './shared/components/card-form/card-form/card-form.component';
import { LoginLayoutComponent } from './layouts/login-layout/login-layout.component';
import { AppInterceptor } from './app-interceptor';


import { NgxCurrencyModule } from 'ngx-currency';

@NgModule({
  declarations: [
    AppComponent,
    BrandListComponent,
    HomeComponent,
    BrandFormComponent,
    DefaultLayoutComponent,
    NotFoundComponent,
    HeaderComponent,
    TableComponent,
    AutoCompleteComponent,
    TableComponent,
    TableComponent,
    CardFormComponent,
    LoginLayoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    NgxCurrencyModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(library: FaIconLibrary, config: FaConfig) {
    library.addIconPacks(fas, far, fab);
    config.defaultPrefix = 'fas';
  }
}
