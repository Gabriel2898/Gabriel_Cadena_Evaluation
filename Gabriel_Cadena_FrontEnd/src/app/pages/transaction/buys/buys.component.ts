import { Transaction } from './../../../models/transaction';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../../../services/api/product.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Product } from '../../../models/product';
import { TransactionService } from '../../../services/api/transaction.service';


@Component({
  selector: 'app-buys',
  standalone: false,
  templateUrl: './buys.component.html',
  styleUrl: './buys.component.scss'
})
export class BuysComponent implements OnInit {
  formBuy: FormGroup;
  action: string ='Comprar';
  product:Product []=[];
  botonAction: string ='Guardar';
  listTransaction: Transaction []= [];
  selectedProductId: number | null = null;
  id?:number = 0;
  constructor(@Inject(MAT_DIALOG_DATA) public dataProduct:Transaction,private productService: ProductService ,private dialogo:MatDialogRef<BuysComponent>,private fb:FormBuilder, private snackBar: MatSnackBar, private transactionService:TransactionService) {

    this.formBuy=this.fb.group({
      productId:['', Validators.required],
      quantity:['',Validators.required],
      unitPrice:['',Validators.required],
      detail:['',Validators.required]
    });
    this.transactionService.getTransaction().subscribe({
      next:(response )=> {
      this.listTransaction=response.data;
    }, error:(e)=>{}});
   }

  ngOnInit() {
    this.productService.getProducts().subscribe({
      next:(response )=> {
      this.product = response.data;
    }, error:(e)=>{}});
    }

  showAlert(msg: string, action:string){
    this.snackBar.open(msg,action,{
      horizontalPosition:'end',
      verticalPosition:'top',
      duration:7000
    });
  }
  addProduct(){

      const transactionPurchase: Transaction={
        transactionType: "Purchase",
        productId: this.formBuy.value.productId,
        quantity: this.formBuy.value.quantity,
        unitPrice: this.formBuy.value.unitPrice,
        details: this.formBuy.value.detail
      }
       this.transactionService.postTransactionCreate(transactionPurchase).subscribe({
        next:(data)=>{
          this.showAlert('Transaccion realizada Exitosamente','Listo');
          this.dialogo.close('purchase');
        },error:(e)=>{

          this.showAlert('No se pudeo actualizar realizar transaccion ','Error');
        }
      })


  }
}
