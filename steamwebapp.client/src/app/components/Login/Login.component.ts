import { Component, OnInit } from '@angular/core';
import { NgParticlesService } from "@tsparticles/angular";
import { Container } from '@tsparticles/engine';
import { loadSlim } from "@tsparticles/slim";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {
    MoveDirection,
    OutMode,
  } from "@tsparticles/engine";

@Component({
  selector: 'login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {

    Login() {
      // Redirect to the Steam login endpoint
      window.location.href = 'https://localhost:7100/api/SteamAuth/login';
    }

    public id = "tsparticles";
    public particleOptions = {
        fpsLimit: 60,
        particles: {
          color: {
            value: "#ffffff",
          },
          links: {
            color: "#ffffff",
            distance: 200,
            enable: true,
            opacity: 0.5,
            width: 1,
          },
          move: {
            direction: MoveDirection.none,
            enable: true,
            outModes: {
              default: OutMode.bounce,
            },
            random: false,
            speed: 3,
            straight: false,
          },
          number: {
            density: {
              enable: true,
              area: 800,
            },
            value: 100,
          },
          opacity: {
            value: 0.5,
          },
          shape: {
            type: "circle",
          },
          size: {
            value: { min: 1, max: 5 },
          },
        },
        detectRetina: true,
      };

    constructor(private readonly ngParticlesService: NgParticlesService, private http: HttpClient, private router: Router) {

    }

    ngOnInit(): void {
        this.ngParticlesService.init(async (engine) => {
            console.log(engine);
      
            // Starting from 1.19.0 you can add custom presets or shape here, using the current tsParticles instance (main)
            // this loads the tsparticles package bundle, it's the easiest method for getting everything ready
            // starting from v2 you can add only the features you need reducing the bundle size
            //await loadFull(engine);
            await loadSlim(engine);
        });
    }

  SteamLogin() {
    let httpOptions: Object = { observe: "response" }

    //this.http.post<string>('https://localhost:7100/api/SteamAuth/login', null, httpOptions)
    this.http.post<string>('https://localhost:7100/api/SteamAuth/login', null, httpOptions).subscribe(
      (result) => {
        var test = result;
        console.log("OOGA: " + test);
        this.router.navigate(['/app']);
      }
    );
  }

    particlesLoaded(container: Container): void {
        console.log(container);
      }
}
