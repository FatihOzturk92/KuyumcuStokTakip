import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { StockTransactionsComponent } from './stock-transactions.component';

describe('StockTransactionsComponent', () => {
  let fixture: ComponentFixture<StockTransactionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockTransactionsComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockTransactionsComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
