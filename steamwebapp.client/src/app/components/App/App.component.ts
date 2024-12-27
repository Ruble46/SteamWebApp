import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app',
  templateUrl: './App.component.html',
  styleUrls: ['./App.component.css']
})
export class AppComponent implements OnInit {
    public profile: any = {};
    public groups: Array<Group> = [
        {name: "AAA", useImage: true, image: "https://akm-img-a-in.tosshub.com/sites/itgaming/resources/202401/untitled-design-9250124051342.png", backgroundColor: "#ffffff"},
        {name: "BBB", useImage: true, image: "https://monsterhunter.tools/img/projects/monsterhunter.tools/assets/img/highlights/mhr-monster-tigrex-gallery-absolutePower-45_46-54_43-323_323-TLFnVt8ZF3o.jpg", backgroundColor: "#000292"},
        {name: "CCC", useImage: false, image: "", backgroundColor: "#0b5e00"},
        {name: "DDD", useImage: true, image: "https://www.rocketleague.com/_next/image?url=https%3A%2F%2Fmedia.graphassets.com%2Fresize%3Dfit%3Aclip%2Cheight%3A1080%2Cwidth%3A1920%2Foutput%3Dformat%3Awebp%2FfLenQ7lsSMSTddNwQ1NA&w=3840&q=75", backgroundColor: "#000000"},
        {name: "EEE", useImage: false, image: "", backgroundColor: "#ee9b01"},
    ];

    constructor(private http: HttpClient) {

    }

    ngOnInit(): void {
        this.http.get("https://localhost:7100/api/SteamUser/SteamUserSummary", { observe: "response", withCredentials: true}).subscribe(result => {
            this.profile = result.body;
        });
    }

    goToSteamProfile() {
        this.goToURL(this.profile.profileurl);
    }

    goToWishlist() {
        this.goToURL("https://store.steampowered.com/wishlist/profiles/" + this.profile.steamid + "/");
    }

    goToSteamStore() {
        this.goToURL("https://store.steampowered.com/");
    }

    goToSteamLibrary() {
        this.goToURL("https://steamcommunity.com/profiles/" + this.profile.steamid + "/games")
    }

    goToURL(URL: string) {
        const w = window.open(URL, '_blank');

        if (w) {
            w.focus();
        }
    }

    Test() {
        this.http.get("https://localhost:7100/api/SteamUser/SteamUserSummary", { observe: "response", withCredentials: true}).subscribe(result => {

        });
    }
}

export class Group {
    name: string = "";
    useImage: boolean = false;
    image: string = "";
    backgroundColor = "#000000";
}
