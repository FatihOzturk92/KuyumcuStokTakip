import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomersClient, CustomerDto, InventoryProductsClient, InventoryProductDto, SalesClient, SaleDto, CreateSaleCommand } from '../web-api-client';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html'
})
export class SalesComponent implements OnInit {
  sales: SaleDto[] = [];
  customers: CustomerDto[] = [];
  products: InventoryProductDto[] = [];
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private salesClient: SalesClient,
    private customersClient: CustomersClient,
    private productsClient: InventoryProductsClient
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      customerId: [null],
      items: this.fb.array([])
    });
    this.load();
    this.loadCustomers();
    this.loadProducts();
    this.addItem();
  }

  get items(): FormArray {
    return this.form.get('items') as FormArray;
  }

  addItem(): void {
    const group = this.fb.group({
      inventoryProductId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(0.001)]],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
      total: [{ value: 0, disabled: true }]
    });
    group.valueChanges.subscribe(v => {
      group.get('total')!.setValue((v.quantity ?? 0) * (v.unitPrice ?? 0), { emitEvent: false });
    });
    this.items.push(group);
  }

  removeItem(i: number): void {
    this.items.removeAt(i);
  }

  getTotal(): number {
    return this.items.controls.reduce((sum, g) => sum + (g.get('quantity')!.value * g.get('unitPrice')!.value), 0);
  }

  load(): void {
    this.salesClient.getSales(1, 50).subscribe({
      next: r => (this.sales = r.items ?? []),
      error: err => console.error(err)
    });
  }

  loadCustomers(): void {
    this.customersClient.getCustomers('', 1, 50).subscribe({
      next: r => (this.customers = r.items ?? []),
      error: err => console.error(err)
    });
  }

  loadProducts(): void {
    this.productsClient.getInventoryProducts(1, 50).subscribe({
      next: r => (this.products = r.items ?? []),
      error: err => console.error(err)
    });
  }

  save(): void {
    if (this.form.invalid) return;
    const cmd: CreateSaleCommand = {
      saleDate: new Date(),
      customerId: this.form.value.customerId,
      items: this.items.controls.map(g => ({
        inventoryProductId: g.get('inventoryProductId')!.value,
        quantity: g.get('quantity')!.value,
        unitPrice: g.get('unitPrice')!.value
      }))
    } as CreateSaleCommand;
    this.salesClient.createSale(cmd).subscribe({
      next: () => {
        this.form.reset();
        this.items.clear();
        this.addItem();
        this.load();
      },
      error: err => console.error(err)
    });
  }

  getCustomerName(id?: number): string {
    const c = this.customers.find(x => x.id === id);
    return c ? `${c.firstName} ${c.lastName}` : '';
  }
}
