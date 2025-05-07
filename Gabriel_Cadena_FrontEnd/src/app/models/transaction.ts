export interface Transaction {
  id?: string;
  transactionDate?: Date;
  transactionType: string;
  productId: number;
  productName?: string;
  quantity: number;
  unitPrice: number;
  totalPrice?: number;
  details: string;
}

export interface TransactionList {
  data: Transaction[];
    pageNumber: number;
    pageSize: number;
    totalRecords: number;
    totalPages: number;
  }
