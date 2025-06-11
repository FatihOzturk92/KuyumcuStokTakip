import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { InventoriesComponent } from './inventories.component';

describe('InventoriesComponent', () => {
  let fixture: ComponentFixture<InventoriesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [InventoriesComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoriesComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
