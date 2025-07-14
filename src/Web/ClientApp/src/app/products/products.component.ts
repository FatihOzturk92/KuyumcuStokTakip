import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsClient, ProductDto, CreateProductCommand } from '../web-api-client';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  products: ProductDto[] = [];
  form!: FormGroup;
  modalRef?: BsModalRef;
  selected?: ProductDto;

  constructor(private client: ProductsClient, private modalService: BsModalService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.load();
    this.form = this.fb.group({
      name: ['', Validators.required],
      productType: [0, Validators.required],
      purity: [14, Validators.required],
      modelName: [''],
      trackingType: [0, Validators.required],
      photoUrl: [''],
      certificateNumber: [''],
      notes: ['']
    });
  }

  load(): void {
    this.client.getProducts(1, 50).subscribe({
      next: r => this.products = r.items,
      error: err => console.error(err)
    });
  }

  openCreate(template: TemplateRef<any>): void {
    this.selected = undefined;
    this.form.reset({ productType: 0, purity: 14, trackingType: 0, photoUrl: '', certificateNumber: '', notes: '' });
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.form.invalid) return;
    const cmd: CreateProductCommand = { ...this.form.value } as CreateProductCommand;
    this.client.createProduct(cmd).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }

  confirmDelete(template: TemplateRef<any>, item: ProductDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteProduct(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
