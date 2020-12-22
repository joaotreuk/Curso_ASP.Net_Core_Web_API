import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-barra-navegacao',
  templateUrl: './barraNavegacao.component.html',
  styleUrls: ['./barraNavegacao.component.scss']
})
export class BarraNavegacaoComponent implements OnInit {

  constructor(public authService: AuthService, public router: Router) { }

  ngOnInit() {}

  loggedIn() {
    return this.authService.loggedIn();
  }

  entrar() {
    this.router.navigate(['/user/login']);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
