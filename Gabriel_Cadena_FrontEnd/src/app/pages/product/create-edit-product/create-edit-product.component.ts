import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../../../services/api/product.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Product } from '../../../models/product';

@Component({
  selector: 'app-create-edit-product',
  templateUrl: './create-edit-product.component.html',
  standalone:false,
  styleUrls: ['./create-edit-product.component.css']
})
export class CreateEditProductComponent implements OnInit {
  formProduct: FormGroup;
  action: string ='Nuevo';
  botonAction: string ='Guardar';
  listProduct: Product []= [];
  id?:number = 0;
  constructor(@Inject(MAT_DIALOG_DATA) public dataProduct:Product,private dialogo:MatDialogRef<CreateEditProductComponent>, private fb:FormBuilder, private snackBar: MatSnackBar, private productService:ProductService) {

    this.formProduct=this.fb.group({
      name:['',Validators.required],
      description:['',Validators.required],
      category:['',Validators.required],
      price:['',Validators.required],
      stock:['',Validators.required],
    });
    this.productService.getProducts().subscribe({
      next:(response )=> {
      this.listProduct=response.data;
    }, error:(e)=>{}});
   }

  ngOnInit() {
    if(this.dataProduct){
      this.formProduct.patchValue({
        name:this.dataProduct.name,
        category:this.dataProduct.category,
        price:this.dataProduct.price,
        stock:this.dataProduct.stock,
        id:this.dataProduct.id,
        description: this.dataProduct.description

      })
      this.action='Editar';
      this.botonAction='Actualizar';
      if(this.dataProduct!=undefined){
        this.id=this.dataProduct.id;
      }
    }
  }

  showAlert(msg: string, action:string){
    this.snackBar.open(msg,action,{
      horizontalPosition:'end',
      verticalPosition:'top',
      duration:7000
    });
  }
  addProduct(){
    if(this.dataProduct != null && this.dataProduct.id != null) {
      const productEdit: Product={

        name:this.formProduct.value.name,
        description: this.formProduct.value.description,
       category: this.formProduct.value.category,
       price: this.formProduct.value.price,
       stock: this.formProduct.value.stock,

       id:this.id

       }
       this.productService.putUpdateProduct(this.dataProduct.id,productEdit).subscribe({
        next:(data)=>{
          this.showAlert('Producto actualizado Exitosamente','Listo');
          this.dialogo.close('editado');
        },error:(e)=>{

          this.showAlert('No se pudeo actualizar Producto ','Error');
        }
      })
    }else{
      const product: Product={

        name:this.formProduct.value.name,
        description: this.formProduct.value.description,
       category: this.formProduct.value.category,
       price: this.formProduct.value.price,
       stock: this.formProduct.value.stock

       }
       this.productService.postProductCreate(product).subscribe({
        next:(data)=>{
          this.showAlert('Producto Creado Exitosamente','Listo');
          this.dialogo.close('creado');
        },error:(e)=>{

          this.showAlert('No se pudeo crear Producto ','Error');
        }
      })
    }

  }
}
