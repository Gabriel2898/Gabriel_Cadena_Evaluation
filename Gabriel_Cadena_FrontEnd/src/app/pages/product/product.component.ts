import { Product } from './../../models/product';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/api/product.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateEditProductComponent } from './create-edit-product/create-edit-product.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  standalone: false,
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'stock','price', 'category','description','acciones'];
  dataSource = new MatTableDataSource<Product>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private productService: ProductService,
    private matdialog:MatDialog,private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
this.mostrarProduct();
  }
  mostrarProduct(){
    this.productService.getProducts().subscribe({
      next:(response )=> {
      this.dataSource.data = response.data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error:(e)=>{}});
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  openDialog(){
    this.matdialog.open(CreateEditProductComponent,{
      disableClose:true,
      width:'400px'
    }).afterClosed().subscribe(resultado=>{
      if(resultado==='creado'){

      }
    });

  }
  showAlert(msg: string, action:string){
    this.snackBar.open(msg,action,{
      horizontalPosition:'end',
      verticalPosition:'top',
      duration:7000
    });
  }
  deleteProduct(product: Product) {
    if (product.id !== undefined) {
      this.productService.deldeleteProduct(product.id).subscribe({
        next: (data) => {
          this.showAlert('Producto eliminado Exitosamente', 'Listo');

        this.mostrarProduct();
        },
        error: (e) => {
          this.showAlert('No se pudo eliminar Producto', 'Error');
        }
      });
    } else {
      this.showAlert('ID de producto no válido', 'Error');
    }
  }

  openEditDialog(product:Product){
    this.matdialog.open(CreateEditProductComponent,{
      disableClose:true,
      width:'400px',
      data:product
    }).afterClosed().subscribe(resultado=>{
      if(resultado==='editado'){

        this.mostrarProduct();
      }
    });

  }
}
