import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './pages/product/product.component';
import { TransactionComponent } from './pages/transaction/transaction.component';
import { StartComponent } from './pages/start/start.component';
const routes: Routes = [
  { path: '', component: StartComponent },
  { path: '', loadChildren: () => import('./pages/start/start.module').then(m => m.StartModule) }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
