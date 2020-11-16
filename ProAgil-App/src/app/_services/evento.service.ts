import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable()
export class EventoService {
  baseURL = 'https://localhost:5001/evento';

  constructor(private http: HttpClient) { }

  obterTodosEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  obterEventoPorTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/obterPorTema/${tema}`);
  }

  selecionarEvento(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  enviarEvento(evento: Evento): any {
    return this.http.post(`${this.baseURL}`, evento);
  }

  atualizarEvento(evento: Evento): any {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }

  deletarEvento(id: number): any {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
