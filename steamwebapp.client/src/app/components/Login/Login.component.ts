import { Component, OnInit } from '@angular/core';
import { NgParticlesService } from "@tsparticles/angular";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Container, Engine, MoveDirection, OutMode, tsParticles} from "@tsparticles/engine";
import { loadPolygonPath, polygonPathName } from "@tsparticles/path-polygon";
import { loadFull } from 'tsparticles';

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
        particles: {
            color: {
                value: "#9ae19d",
                // animation: {
                //     enable: true,
                //     speed: 10
                // }
            },
            move: {
                direction: MoveDirection.none,
                enable: true,
                outModes: OutMode.destroy,
                path: {
                    clamp: false,
                    enable: true,
                    delay: {
                        value: 0
                    },
                    generator: polygonPathName,
                    options: {
                        sides: 6,
                        turnSteps: 30,
                        angle: 30
                    }
                },
                speed: 3,
                trail: {
                    fill: {
                        color: "#000"
                    },
                    length: 20,
                    enable: true
                }
            },
            number: {
                density: {
                    enable: true
                },
                value: 0
            },
            opacity: {
                value: 1
            },
            shape: {
                type: "circle"
            },
            size: {
                value: 2
            }
        },
        background: {
            color: {
                value: "#171f2b",
            } 
        },
        fullScreen: {
            zIndex: -1
        },
        emitters: {
            direction: "none",
            rate: {
                quantity: 1,
                delay: 0.25
            },
            size: {
                width: 0,
                height: 0
            },
            position: {
                x: 50,
                y: 50
            }
        }
    };

    constructor(private readonly ngParticlesService: NgParticlesService, private http: HttpClient, private router: Router) {

    }

    ngOnInit(): void {
        this.ngParticlesService.init(async (engine: Engine) => {
            console.log("Particles initialized");
            console.log(tsParticles);
      
            await loadPolygonPath(tsParticles);
            console.log("Polygon path loaded");

            await loadFull(tsParticles);
            console.log("Load slim called");
        });

        //TODO: do auth check and redirect to app if 200 back
    }

    particlesLoaded(container: Container): void {
        console.log(container);
    }
}


// particles: {
//     color: {
//     value: "#FF0000",
//     animation: {
//         enable: true,
//         speed: 10
//     }
//     },
//     effect: {
//     type: "trail",
//     options: {
//         trail: {
//         length: 50,
//         minWidth: 4
//         }
//     }
//     },
//     move: {
//     direction: "none",
//     enable: true,
//     outModes: {
//         default: "destroy"
//     },
//     path: {
//         clamp: false,
//         enable: true,
//         delay: {
//         value: 0
//         },
//         generator: "polygonPathGenerator",
//         options: {
//         sides: 6,
//         turnSteps: 30,
//         angle: 30
//         }
//     },
//     random: false,
//     speed: 3,
//     straight: false
//     },
//     number: {
//     value: 0
//     },
//     opacity: {
//     value: 1
//     },
//     shape: {
//     type: "circle"
//     },
//     size: {
//     value: 2
//     }
// },
// background: {
//     color: "#000"
// },
// fullScreen: {
//     zIndex: -1
// },
// emitters: {
//     direction: "none",
//     rate: {
//     quantity: 1,
//     delay: 0.25
//     },
//     size: {
//     width: 0,
//     height: 0
//     },
//     position: {
//     x: 50,
//     y: 50
//     }
// }

  
  

// fpsLimit: 60,
// fullScreen: { enable: true },
// particles: {
// number: {
//     value: 50
// },
// shape: {
//     type: "circle"
// },
// opacity: {
//     value: 0.5
// },
// size: {
//     value: 400,
//     random: {
//     enable: true,
//     minimumValue: 200
//     }
// },
// move: {
//     enable: true,
//     speed: 10,
//     direction: MoveDirection.top,
//     outModes: {
//     default: OutMode.out,
//     top: OutMode.destroy,
//     bottom: OutMode.none
//     }
// }
// },
// interactivity: {
// detectsOn: InteractivityDetect.canvas,
// },
// detectRetina: true,
// themes: [
// {
//     name: "light",
//     default: {
//     value: true,
//     mode: "light"
//     },
//     options: {
//     background: {
//         color: "#f7f8ef"
//     },
//     particles: {
//         color: {
//         value: ["#5bc0eb", "#fde74c", "#9bc53d", "#e55934", "#fa7921"]
//         }
//     }
//     }
// },
// {
//     name: "dark",
//     default: {
//     value: true,
//     mode: "dark"
//     },
//     options: {
//     background: {
//         color: "#080710"
//     },
//     particles: {
//         color: {
//         value: ["#004f74", "#5f5800", "#245100", "#7d0000", "#810c00"]
//         }
//     }
//     }
// }
// ],
// emitters: {
// direction: "top",
// position: {
//     x: 50,
//     y: 150
// },
// rate: {
//     delay: 0.2,
//     quantity: 2
// },
// size: {
//     width: 100,
//     height: 0
// }
// }