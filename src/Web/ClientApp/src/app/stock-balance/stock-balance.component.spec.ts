import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { StockBalanceComponent } from './stock-balance.component';

describe('StockBalanceComponent', () => {
  let fixture: ComponentFixture<StockBalanceComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [StockBalanceComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockBalanceComponent);
    const component = fixture.componentInstance;
    component.balances = [
      { productName: 'Gold', totalIn: 1, totalOut: 0, net: 1, productId: 1 } as any,
      { productName: 'Silver', totalIn: 2, totalOut: 1, net: 1, productId: 2 } as any
    ];
    component.filterText = 'Gold';
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });

  it('filters balances by filterText', () => {
    const rows = fixture.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(1);
    expect(rows[0].textContent).toContain('Gold');
  });
});
