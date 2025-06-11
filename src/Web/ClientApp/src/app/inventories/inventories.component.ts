import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { InventoriesClient, InventoryDto, CreateInventoryCommand, UpdateInventoryCommand } from '../web-api-client';

@Component({
  selector: 'app-inventories',
  templateUrl: './inventories.component.html'
})
export class InventoriesComponent implements OnInit {
  inventories: InventoryDto[] = [];
  selected?: InventoryDto;
  editor: any = {};
  modalRef?: BsModalRef;

  constructor(private client: InventoriesClient, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getInventories(1, 10).subscribe({
      next: r => this.inventories = r.items,
      error: err => console.error(err)
    });
  }

  openCreate(template: TemplateRef<any>): void {
    this.selected = undefined;
    this.editor = {};
    this.modalRef = this.modalService.show(template);
  }

  openEdit(template: TemplateRef<any>, item: InventoryDto): void {
    this.selected = item;
    this.editor = { ...item };
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.selected) {
      const cmd: UpdateInventoryCommand = { id: this.selected.id, ...this.editor };
      this.client.updateInventory(this.selected.id, cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    } else {
      const cmd: CreateInventoryCommand = { ...this.editor };
      this.client.createInventory(cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    }
  }

  confirmDelete(template: TemplateRef<any>, item: InventoryDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteInventory(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
