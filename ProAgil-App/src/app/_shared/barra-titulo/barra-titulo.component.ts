import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-barra-titulo',
  templateUrl: './barra-titulo.component.html',
  styleUrls: ['./barra-titulo.component.scss']
})
export class BarraTituloComponent {
  @Input() titulo: string;
}
