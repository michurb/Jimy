import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-training-sessions',
  templateUrl: './training-sessions.component.html',
  styleUrls: ['./training-sessions.component.css']
})
export class TrainingSessionsComponent implements OnInit {

  trainingSessions: any[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
      this.apiService.getTrainingSessions().subscribe(data => {
          this.trainingSessions = data;
      });
  }
    expandedSession: any = null;

    toggleDetails(session: any) {
        if (this.expandedSession === session) {
            this.expandedSession = null; // Collapse the expanded session if clicked again
        } else {
            this.expandedSession = session;
        }
    }

}
