import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { InventoryProductsComponent } from './inventory-products.component';

describe('InventoryProductsComponent', () => {
  let fixture: ComponentFixture<InventoryProductsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [InventoryProductsComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryProductsComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
