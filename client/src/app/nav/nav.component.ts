import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent {
  model: any = {};

  constructor(
    public accountSerivce: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {}

  login() {
    this.accountSerivce.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: (error) => this.toastr.error(error.error),
    });
  }

  logout() {
    this.accountSerivce.logout();
    this.router.navigateByUrl('/');
  }
}
