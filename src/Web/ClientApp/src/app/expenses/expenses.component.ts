import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ExpensesClient, ExpenseDto, CreateExpenseCommand, UpdateExpenseCommand } from '../web-api-client';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html'
})
export class ExpensesComponent implements OnInit {
  expenses: ExpenseDto[] = [];
  selected?: ExpenseDto;
  editor: any = {};
  modalRef?: BsModalRef;

  constructor(private client: ExpensesClient, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getExpenses(1, 10).subscribe({
      next: r => this.expenses = r.items,
      error: err => console.error(err)
    });
  }

  openCreate(template: TemplateRef<any>): void {
    this.selected = undefined;
    this.editor = {};
    this.modalRef = this.modalService.show(template);
  }

  openEdit(template: TemplateRef<any>, item: ExpenseDto): void {
    this.selected = item;
    this.editor = { ...item };
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.selected) {
      const cmd: UpdateExpenseCommand = { id: this.selected.id, ...this.editor };
      this.client.updateExpense(this.selected.id, cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    } else {
      const cmd: CreateExpenseCommand = { ...this.editor };
      this.client.createExpense(cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    }
  }

  confirmDelete(template: TemplateRef<any>, item: ExpenseDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deleteExpense(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
