import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomersClient, CustomerDto, CreateCustomerCommand } from '../web-api-client';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {
  customers: CustomerDto[] = [];
  form!: FormGroup;
  searchForm!: FormGroup;
  modalRef?: BsModalRef;
  selected?: CustomerDto;

  constructor(private client: CustomersClient, private modalService: BsModalService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({ term: [''] });
    this.form = this.fb.group({
      firstName: [''],
      lastName: [''],
      phone: [''],
      email: [''],
      address: [''],
      notes: ['']
    });
    this.load();
  }

  load(): void {
    const term = this.searchForm.get('term')!.value ?? '';
    this.client.getCustomers(term, 1, 50).subscribe({
      next: r => this.customers = r.items ?? [],
      error: err => console.error(err)
    });
  }

  search(): void {
    this.load();
  }

  openCreate(template: TemplateRef<any>): void {
    this.form.reset();
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    const cmd: CreateCustomerCommand = { ...this.form.value } as CreateCustomerCommand;
    this.client.createCustomer(cmd).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }

  confirmDelete(template: TemplateRef<any>, item: CustomerDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteCustomer(this.selected.id!).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
