import { BrowserModule } from '@angular/platform-browser';
import { APP_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ExpensesComponent } from './expenses/expenses.component';
import { PartnersComponent } from './partners/partners.component';
import { InventoryProductsComponent } from './inventory-products/inventory-products.component';
import { InventoriesComponent } from './inventories/inventories.component';
import { StockTransactionsComponent } from './stock-transactions/stock-transactions.component';
import { ProductsComponent } from './products/products.component';
import { CustomersComponent } from './customers/customers.component';
import { SalesComponent } from './sales/sales.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        TodoComponent,
        ExpensesComponent,
        PartnersComponent,
        InventoryProductsComponent,
        InventoriesComponent,
        StockTransactionsComponent,
        ProductsComponent,
        CustomersComponent,
        SalesComponent
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'todo', component: TodoComponent },
            { path: 'expenses', component: ExpensesComponent },
            { path: 'partners', component: PartnersComponent },
            { path: 'inventory-products', component: InventoryProductsComponent },
            { path: 'inventories', component: InventoriesComponent },
            { path: 'stock-transactions', component: StockTransactionsComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'customers', component: CustomersComponent },
            { path: 'sales', component: SalesComponent }
        ]),
        BrowserAnimationsModule,
        ModalModule.forRoot()],
    providers: [
        { provide: APP_ID, useValue: 'ng-cli-universal' },
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
        provideHttpClient(withInterceptorsFromDi())
    ]
})
export class AppModule { }
