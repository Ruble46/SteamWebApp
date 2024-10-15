//Angular Imports
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { RouterModule, Routes } from '@angular/router';

//Material Imports
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule, MAT_FORM_FIELD_DEFAULT_OPTIONS} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatTreeModule} from '@angular/material/tree';
import {MatExpansionModule} from '@angular/material/expansion';

//Component Imports
import { RootComponent } from './Root.component';
import { LoginComponent } from './components/Login/Login.component';
import { RegisterComponent } from './components/Register/Register.component';
import { ForgotPasswordComponent } from './components/ForgotPassword/ForgotPassword.component';
import { AppComponent } from './components/App/App.component';

//Dialog Imports


//Helpers & Service Imports


const appRoutes: Routes = [
    {path: '', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'password', children: [
        {path: 'reset', component: ForgotPasswordComponent}
    ]},
    {path: 'app', component: AppComponent}
  ];

@NgModule({ 
    declarations: [
        RootComponent,
        LoginComponent,
        RegisterComponent,
        ForgotPasswordComponent,
        AppComponent
    ],
    bootstrap: [
        RootComponent
    ], 
    imports: [
        BrowserModule,
        RouterModule.forRoot(
            appRoutes,
            {
              enableTracing: false, // <-- debugging purposes only
            }
        ),
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSidenavModule,
        MatToolbarModule,
        MatIconModule,
        MatTreeModule,
        MatExpansionModule
    ], 
    providers: [
        provideHttpClient(withInterceptorsFromDi()), 
        provideAnimationsAsync(),
        {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}
    ]})

export class AppModule { }
