import { NgModule } from "@angular/core";
import { MomentDateAdapter } from "@angular/material-moment-adapter";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from '@angular/material/card';
import { DateAdapter, MAT_DATE_FORMATS } from "@angular/material/core";
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSelectModule } from "@angular/material/select";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from "@angular/material/snack-bar";
import { MatTableModule } from "@angular/material/table";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatSortModule } from "@angular/material/sort";
import { MatAutocompleteModule } from "@angular/material/autocomplete";

@NgModule({
    imports: [
        MatTableModule,
        MatSnackBarModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatCardModule,
        MatDatepickerModule,
        MatSelectModule,
        MatPaginatorModule,
        MatSortModule,
        MatAutocompleteModule
    ],
    exports: [
        MatTableModule,
        MatSnackBarModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatCardModule,
        MatDatepickerModule,
        MatSelectModule,
        MatPaginatorModule,
        MatSortModule,
        MatAutocompleteModule
    ],
    providers: [
        { provide: DateAdapter, useClass: MomentDateAdapter },
        {
            provide: MAT_DATE_FORMATS, useValue: {
                parse: {
                    dateInput: 'DD/MM/YYYY',
                },

                display: {
                    dateInput: 'DD/MM/YYYY',
                    monthYearLabel: 'MMMM YYYY',
                    dateA11yLabel: 'DD/MM/YYYY',
                    monthYearA11yLabel: 'MMMM YYYY',
                },
            }
        },
        { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 3500 } },
    ]
})
export class MaterialModule { }