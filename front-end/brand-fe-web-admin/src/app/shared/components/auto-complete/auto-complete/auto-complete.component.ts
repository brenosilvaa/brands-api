import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrls: ['./auto-complete.component.scss']
})
export class AutoCompleteComponent<T> implements OnInit, OnChanges {

  myControl = new FormControl<string | T>('');
  filteredOptions!: Observable<T[] | undefined | null>;

  @Input() options: any[] = [];
  @Input() value!: T;
  @Input() label!: string;
  @Input() displayField!: string;

  @Output() changed: EventEmitter<T | null> = new EventEmitter<T | null>();

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['value']) {
      this.myControl.setValue(changes['value'].currentValue);
    }

    if (changes['options']) {
      this.setFilteredOptions();
    }
  }

  ngOnInit(): void {
    if (this.value)
      this.myControl.setValue(this.value);

    this.setFilteredOptions();
  }

  displayFn(option?: any): string {
    return option && option[this.displayField] ? option[this.displayField] : '';
  }

  setFilteredOptions(): void {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        console.log(value, typeof value === 'string');

        if (typeof value === 'string') {
          this.changed.emit(null);
          return value ? this._filter(value as string) : this.options?.slice();
        } else {
          console.log('aqui');

          this.changed.emit(value);
          return;
        }
      }),
    );
  }

  private _filter(name: string): T[] {
    return this.options.filter((option: any) => option[this.displayField].toLowerCase().includes(name.toLowerCase()));
  }
}
