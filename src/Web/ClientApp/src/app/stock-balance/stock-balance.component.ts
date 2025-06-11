import { Component, OnInit } from '@angular/core';
import { StockBalancesClient, StockBalanceDto } from '../web-api-client';

@Component({
  selector: 'app-stock-balance',
  templateUrl: './stock-balance.component.html'
})
export class StockBalanceComponent implements OnInit {
  balances: StockBalanceDto[] = [];
  filterText = '';

  get filteredBalances(): StockBalanceDto[] {
    const filter = this.filterText?.toLowerCase() ?? '';
    return this.balances.filter(b => b.productName.toLowerCase().includes(filter));
  }

  constructor(private client: StockBalancesClient) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getStockBalances(this.filterText).subscribe({
      next: r => (this.balances = r),
      error: err => console.error(err)
    });
  }
}
