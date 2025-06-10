import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { PartnersComponent } from './partners.component';

describe('PartnersComponent', () => {
  let fixture: ComponentFixture<PartnersComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PartnersComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartnersComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(fixture.componentInstance).toBeTruthy();
  });
});
