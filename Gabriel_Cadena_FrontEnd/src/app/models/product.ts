export interface ProductList {
data: Product[];
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
}
export interface Product {
  id?: number;
  name: string;
  description: string;
  category: string;
  imageUrl?: string;
  price: number;
  stock: number;
}
