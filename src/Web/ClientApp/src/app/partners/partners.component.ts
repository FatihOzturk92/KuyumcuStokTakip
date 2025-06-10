import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PartnersClient, PartnerDto, CreatePartnerCommand, UpdatePartnerCommand } from '../web-api-client';

@Component({
  selector: 'app-partners',
  templateUrl: './partners.component.html'
})
export class PartnersComponent implements OnInit {
  partners: PartnerDto[] = [];
  selected?: PartnerDto;
  editor: any = {};
  modalRef?: BsModalRef;

  constructor(private client: PartnersClient, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getPartners(1, 10).subscribe({
      next: r => this.partners = r.items,
      error: err => console.error(err)
    });
  }

  openCreate(template: TemplateRef<any>): void {
    this.selected = undefined;
    this.editor = {};
    this.modalRef = this.modalService.show(template);
  }

  openEdit(template: TemplateRef<any>, item: PartnerDto): void {
    this.selected = item;
    this.editor = { ...item };
    this.modalRef = this.modalService.show(template);
  }

  save(): void {
    if (this.selected) {
      const cmd: UpdatePartnerCommand = { id: this.selected.id, ...this.editor };
      this.client.updatePartner(this.selected.id, cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    } else {
      const cmd: CreatePartnerCommand = { ...this.editor };
      this.client.createPartner(cmd).subscribe({
        next: () => { this.modalRef?.hide(); this.load(); },
        error: err => console.error(err)
      });
    }
  }

  confirmDelete(template: TemplateRef<any>, item: PartnerDto): void {
    this.selected = item;
    this.modalRef = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    if (!this.selected) return;
    this.client.deletePartner(this.selected.id).subscribe({
      next: () => { this.modalRef?.hide(); this.load(); },
      error: err => console.error(err)
    });
  }
}
