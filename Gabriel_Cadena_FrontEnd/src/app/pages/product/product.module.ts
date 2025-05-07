import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog'
import {MatGridListModule} from '@angular/material/grid-list';
import { CreateEditProductComponent } from './create-edit-product/create-edit-product.component';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
@NgModule({
  imports: [
    RouterModule.forChild([{ path: '', component: ProductComponent }]),
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatIconModule,
    FormsModule,
    MatGridListModule,
    MatFormFieldModule,

  ],
  declarations: [ProductComponent, CreateEditProductComponent],
})
export class ProductModule { }
