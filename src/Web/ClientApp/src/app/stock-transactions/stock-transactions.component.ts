import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { StockTransactionsClient, StockTransactionDto, CreateStockTransactionCommand, UpdateStockTransactionCommand } from '../web-api-client';

@Component({
  selector: 'app-stock-transactions',
  templateUrl: './stock-transactions.component.html'
})
export class StockTransactionsComponent implements OnInit {
  transactions: StockTransactionDto[] = [];
  selected?: StockTransactionDto;
  editor: any = {};
  modalRef?: BsModalRef;
  title = '';

  constructor(private client: StockTransactionsClient, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getStockTransactions(1, 10).subscribe({
      next: r => this.transactions = r.items,
      error: err => console.error(err)
    });
  }

  openCreateIn(template: TemplateRef<any>): void {
    this.title = 'New Giriş Transaction';
    this.selected = undefined;
    this.editor = { type: 0 };
    this.modalRef = this.modalService.show(template);
  }

  openCreateOut(template: TemplateRef<any>): void {
    this.title = 'New Çıkış Transaction';
    this.selected = undefined;
    this.editor = { type: 1 };
    this.modalRef = this.modalService.show(template);
  }

  openEdit(template: TemplateRef<any>, item: StockTransactionDto): void {
    this.title = 'Edit Transaction';
    this.selected = item;
    this.editor = { ...item };
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.selected) {
      const cmd: UpdateStockTransactionCommand = { id: this.selected.id, ...this.editor };
      this.client.updateStockTransaction(this.selected.id, cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    } else {
      const cmd: CreateStockTransactionCommand = { ...this.editor };
      this.client.createStockTransaction(cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    }
  }

  confirmDelete(template: TemplateRef<any>, item: StockTransactionDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteStockTransaction(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
