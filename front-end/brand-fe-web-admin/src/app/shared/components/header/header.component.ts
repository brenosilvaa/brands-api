import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Input() title!: string;
  @Input() route!: string[];
  @Input() mode!: 'list' | 'form';

  @Input() disabled!: boolean;
  @Input() icon!: string;

  @Output() clicked: EventEmitter<any> = new EventEmitter<any>();

  constructor(
    private _location: Location
  ) { }

  ngOnInit(): void {
    if (!this.title)
      throw new Error('Title is required!');

    if (this.mode == 'list' && !this.route)
      throw new Error('Route is required!');
  }

  buttonClick(): void {
    this.clicked.emit();
  }

  back(): void {
    this._location.back();
  }

}
