import { Product } from './../../models/product';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/api/product.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BuysComponent } from './buys/buys.component';
import { TransactionService } from '../../services/api/transaction.service';
import { Transaction } from '../../models/transaction';
import { SellComponent } from './sell/sell.component';
@Component({
  selector: 'app-transaction',
  standalone: false,
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  displayedColumns: string[] = ['transactionType', 'productId', 'quantity','unitPrice', 'totalPrice','details'];
  dataSource = new MatTableDataSource<Transaction>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private transactionService: TransactionService,
    private matdialog:MatDialog,private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
this.mostrarTransaction();
  }
  mostrarTransaction(){
    this.transactionService.getTransaction().subscribe({
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
  openDialogBuy(){
    this.matdialog.open(BuysComponent,{
      disableClose:true,
      width:'400px'
    }).afterClosed().subscribe(resultado=>{
      if(resultado==='purchase'){

        this.mostrarTransaction();
      }

    });
  }
  openDialogSell(){
    this.matdialog.open(SellComponent,{
      disableClose:true,
      width:'400px'
    }).afterClosed().subscribe(resultado=>{
      if(resultado==='sale'){

        this.mostrarTransaction();
      }

    });
  }

}
