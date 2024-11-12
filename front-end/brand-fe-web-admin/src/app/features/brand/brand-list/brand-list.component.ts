import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { RemoveResult } from 'src/app/shared/models/remove-result';
import { BrandService } from '../brand.service';
import { Brand } from '../models/brand';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.scss']
})
export class BrandListComponent implements OnInit {

  brands: Brand[] = [];
  brandsDataSource!: MatTableDataSource<Brand>;
  columns: string[] = ['name', 'owner', 'actions'];

  isLoading: boolean = true;

  constructor(
    private _service: BrandService,
    private _snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this.isLoading = true;

    this._service.list().subscribe({
      next: (brands: Brand[]) => {
        this.brands = brands;

        this.brandsDataSource = new MatTableDataSource(brands);

        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;

        this._snackBar.open(err, 'Ok');
      }
    });
  }

  remove(brand: Brand): void {
    brand.isRemoving = true;

    this._service.remove(brand.id).subscribe({
      next: (result: RemoveResult) => {
        if (result.success) {
          this.brands = this.brands.filter(x => x.id != brand.id);
          this.brandsDataSource = new MatTableDataSource(this.brands);

          this._snackBar.open('Removido com sucesso', 'Ok');
        }
        else
          this._snackBar.open(result.message!, 'Ok');


        brand.isRemoving = false;
      },
      error: (err: any) => {
        brand.isRemoving = false;

        this._snackBar.open(err, 'Ok');
      }
    });
  }
}
