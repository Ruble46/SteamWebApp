//Angular Imports
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
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
import {MatMenuModule} from '@angular/material/menu';
import {MatDividerModule} from '@angular/material/divider';

//Component Imports
import { RootComponent } from './Root.component';
import { LoginComponent } from './components/Login/Login.component';
import { AppComponent } from './components/App/App.component';

//Dialog Imports


//Helpers & Service Imports


//External Imports
import { NgxParticlesModule } from "@tsparticles/angular";
import { AuthGuard } from './services/RouteAuthGuard';
import { UnauthorizedInterceptor } from './services/UnauthorizedInterceptor';


const appRoutes: Routes = [
    {path: '', component: LoginComponent},
    {path: 'app', component: AppComponent, canActivate: [AuthGuard]}
  ];

@NgModule({ 
    declarations: [
        RootComponent,
        LoginComponent,
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
        MatExpansionModule,
        MatMenuModule,
        MatDividerModule,
        NgxParticlesModule
    ], 
    providers: [
        provideHttpClient(withInterceptorsFromDi()), 
        provideAnimationsAsync(),
        {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
        {provide: HTTP_INTERCEPTORS, useClass: UnauthorizedInterceptor, multi: true}
    ]})

export class AppModule { }
