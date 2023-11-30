import { Component, Input, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/MembersService';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent {
  @Input() member: Member | undefined;

  constructor(
    private memberSerivce: MembersService,
    private toastr: ToastrService
  ) {}

  addLike(member: Member) {
    this.memberSerivce.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You have liked' + member.knownAs),
    });
  }
}
