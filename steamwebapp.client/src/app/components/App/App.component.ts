import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app',
  templateUrl: './App.component.html',
  styleUrls: ['./App.component.css']
})
export class AppComponent {
  constructor(private http: HttpClient) {

  }

  Test() {
    this.http.get("https://localhost:7100/api/SteamAuth/TestAuth", { observe: "response", withCredentials: true}).subscribe(result => {

    });
  }
}
