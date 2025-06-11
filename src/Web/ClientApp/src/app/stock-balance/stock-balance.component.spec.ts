import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { StockBalanceComponent } from './stock-balance.component';

describe('StockBalanceComponent', () => {
  let fixture: ComponentFixture<StockBalanceComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockBalanceComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockBalanceComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
