import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { TipoAcao } from '../util/TipoAcao.enum';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  providers: [EventoService]
})
export class EventosComponent implements OnInit {

  FiltroLista: string;
  get filtroLista(): string {
    return this.FiltroLista;
  }
  set filtroLista(valor: string) {
    this.FiltroLista = valor;
    this.eventosFiltrados = this.FiltroLista ? this.filtrarEventos(this.FiltroLista) : this.eventos;
  }

  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  eventosFiltrados: Evento[];
  formulario: FormGroup;
  tipoAcao: TipoAcao;
  bodyDeletarEvento = '';

  constructor(
    private servicoEvento: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
  ) {
    this.localeService.use('pt-br');
  }

  abrirModal(template: any): void {
    this.formulario.reset();
    this.tipoAcao = TipoAcao.Inserir;
    template.show();
  }

  editar(template: any, evento: Evento): void {
    this.tipoAcao = TipoAcao.Atualizar;
    this.evento = evento;
    this.formulario.patchValue(evento);
    template.show();
  }

  excluirEvento(evento: Evento, template: any): void {
    this.abrirModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  confirmeDelete(template: any): void {
    this.servicoEvento.deletarEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.obterEventos();
        }, error => {
          console.log(error);
        }
    );
  }

  ngOnInit(): void {
    this.validacao();
    this.obterEventos();
  }

  alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  obterEventos(): void {
    this.servicoEvento.obterTodosEvento().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error => {
        console.log(error);
      }
    );
  }

  salvarAlteracao(template: any): any {
    if (this.formulario.valid) {
      if (this.tipoAcao === TipoAcao.Inserir) {
        this.evento = Object.assign({}, this.formulario.value);
        this.servicoEvento.enviarEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.obterEventos();
          }, erro => {
            console.log(erro);
          }
        );
      } else {
        this.evento = Object.assign({id: this.evento.id}, this.formulario.value);
        this.servicoEvento.atualizarEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.obterEventos();
          }, erro => {
            console.log(erro);
          }
        );
      }
    }
  }

  validacao(): any {
    this.formulario = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(50000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required]
    });
  }
}
