import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterUserDialogComponent } from './register-user-dialog.component';

describe('RegisterUserDialogComponent', () => {
  let component: RegisterUserDialogComponent;
  let fixture: ComponentFixture<RegisterUserDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterUserDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegisterUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
