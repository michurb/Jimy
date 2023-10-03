import { Injectable } from '@angular/core';
import { HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5002/api/v1';
  constructor(private http: HttpClient) { }

  getTrainingSessions()
  {
    return this.http.get<any>(`${this.apiUrl}/trainingsessions`)
  }
}
