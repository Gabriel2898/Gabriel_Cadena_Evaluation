import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction.component';
import { RouterModule } from '@angular/router';
import { ProductComponent } from '../product/product.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { BuysComponent } from './buys/buys.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { SellComponent } from './sell/sell.component';

@NgModule({
  imports: [
    RouterModule.forChild([{ path: '', component: TransactionComponent }]),
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatIconModule,
    FormsModule,
    MatFormFieldModule,CommonModule,
    MatSelectModule,
    MatGridListModule,
    MatFormFieldModule,
  ],
  declarations: [TransactionComponent, BuysComponent, SellComponent],
})
export class TransactionModule { }
