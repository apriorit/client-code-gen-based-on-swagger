import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client, WeatherForecast } from '../services/Core-api-v1';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(client: Client) {
    client.weatherForecast().subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}
