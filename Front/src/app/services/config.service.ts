import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as cfg from "./config.json";

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private appConfig: any;
  private http : HttpClient;
  
  constructor(http: HttpClient) {
	this.http = http;
  }

  loadAppConfig() {
    this.appConfig = cfg;
  }

  get apiBaseUrl() : string {
    return this.appConfig.default.apiBaseUrl;
  }
}
