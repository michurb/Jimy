import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingSessionsComponent } from './training-sessions.component';

describe('TrainingSessionsComponent', () => {
  let component: TrainingSessionsComponent;
  let fixture: ComponentFixture<TrainingSessionsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TrainingSessionsComponent]
    });
    fixture = TestBed.createComponent(TrainingSessionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
