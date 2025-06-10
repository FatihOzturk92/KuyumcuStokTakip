import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ExpensesComponent } from './expenses.component';

describe('ExpensesComponent', () => {
  let fixture: ComponentFixture<ExpensesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ExpensesComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpensesComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
