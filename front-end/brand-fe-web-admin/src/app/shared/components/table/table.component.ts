import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Component, ContentChild, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent {
  offset!: number;

  @Input() heightAuto: boolean = false;
  @Input() showAddButton: boolean = false;

  @Output() addClicked: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild('mainContent', { static: false }) mainContent!: ElementRef;

  @ContentChild(MatTable) table!: MatTable<any>;
  @ContentChild(MatSort) sort!: MatSort;

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event)
      this.renderHeight(event.target.innerHeight);
  }

  constructor(
    private _renderer: Renderer2,
    private _breakpointObserver: BreakpointObserver
  ) { }

  ngAfterContentInit(): void {
    let interval = setInterval(() => {
      if (this.table.dataSource) {

        (<MatTableDataSource<any>>this.table.dataSource).sort = this.sort;
        (<MatTableDataSource<any>>this.table.dataSource).paginator = this.paginator;

        clearInterval(interval);
      }
    }, 500);


    this._breakpointObserver
      .observe(Breakpoints.Handset)
      .subscribe((state: BreakpointState) => {
        if (state.matches) {
          this.offset = 340;
        } else {
          this.offset = 310;
        }
      });
  }

  ngAfterViewChecked(): void {
    this.renderHeight(window.innerHeight);
  }

  renderHeight(height: number) {
    if (!this.heightAuto)
      this._renderer.setStyle(this.mainContent.nativeElement, 'height', (height - this.offset) + 'px');
  }

  applyFilter(filterValue: string) {
    (<MatTableDataSource<any>>this.table.dataSource).filter = filterValue.trim().toLowerCase();
  }

  addClick() {
    this.addClicked.emit();
  }

}
