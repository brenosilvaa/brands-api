import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-card-form',
  templateUrl: './card-form.component.html',
  styleUrls: ['./card-form.component.scss']
})

export class CardFormComponent {

  @Input() title!: string;
  @Input() isLoading: boolean = false;

  ngOnInit(): void {
  }
}
