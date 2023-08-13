import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {
  private _apiCdbUrl: string = 'https://localhost:7217/api/v1/Calculator/cdb/';

  get apiCdbUrl(): string {
    return this._apiCdbUrl;
  }
}
