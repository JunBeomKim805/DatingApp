import { Component } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/MembersService';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css'],
})
export class ListsComponent {
  members: Member[] | undefined;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;
  currentPage = 1;

  constructor(private memberSerivce: MembersService) {
    this.loadLikes();
  }

  loadLikes() {
    this.memberSerivce
      .getLikes(this.predicate, this.pageNumber, this.pageSize)
      .subscribe({
        next: (res) => {
          this.members = res.result;
          this.pagination = res.pagination;
        },
      });
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.currentPage = event.page;
      this.loadLikes();
    }
  }
}
