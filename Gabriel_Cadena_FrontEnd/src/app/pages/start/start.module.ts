import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start.component';


import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { StartRoutingModule } from './start-routing.module';
import { ProductComponent } from '../product/product.component';
import { TransactionComponent } from '../transaction/transaction.component';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
    StartRoutingModule
  ],
  declarations: [StartComponent]
})
export class StartModule { }
