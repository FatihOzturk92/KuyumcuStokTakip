import { Component, OnInit } from '@angular/core';
import { StockBalancesClient, StockBalanceDto } from '../web-api-client';

@Component({
  selector: 'app-stock-balance',
  templateUrl: './stock-balance.component.html'
})
export class StockBalanceComponent implements OnInit {
  balances: StockBalanceDto[] = [];

  constructor(private client: StockBalancesClient) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.client.getStockBalances().subscribe({
      next: r => (this.balances = r),
      error: err => console.error(err)
    });
  }
}
