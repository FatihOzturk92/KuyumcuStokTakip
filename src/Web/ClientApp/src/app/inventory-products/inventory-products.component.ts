import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { InventoryProductsClient, InventoryProductDto, CreateInventoryProductCommand, UpdateInventoryProductCommand } from '../web-api-client';

@Component({
  selector: 'app-inventory-products',
  templateUrl: './inventory-products.component.html'
})
export class InventoryProductsComponent implements OnInit {
  products: InventoryProductDto[] = [];
  selected?: InventoryProductDto;
  editor: any = {};
  modalRef?: BsModalRef;

  constructor(private client: InventoryProductsClient, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getInventoryProducts(1, 10).subscribe({
      next: r => this.products = r.items,
      error: err => console.error(err)
    });
  }

  openCreate(template: TemplateRef<any>): void {
    this.selected = undefined;
    this.editor = {};
    this.modalRef = this.modalService.show(template);
  }

  openEdit(template: TemplateRef<any>, item: InventoryProductDto): void {
    this.selected = item;
    this.editor = { ...item };
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.selected) {
      const cmd: UpdateInventoryProductCommand = { id: this.selected.id, ...this.editor };
      this.client.updateInventoryProduct(this.selected.id, cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    } else {
      const cmd: CreateInventoryProductCommand = { ...this.editor };
      this.client.createInventoryProduct(cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    }
  }

  confirmDelete(template: TemplateRef<any>, item: InventoryProductDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteInventoryProduct(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
