import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionComponent } from '../transaction/transaction.component';
import { StartComponent } from './start.component';

const routes: Routes = [
  {
    path: '',
    component: StartComponent
  },
  {
    path: 'product',
    loadChildren: () => import('../product/product.module').then(m => m.ProductModule)
  },
  {
    path: 'transaction',
    loadChildren: () => import('../transaction/transaction.module').then(m => m.TransactionModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StartRoutingModule {}
