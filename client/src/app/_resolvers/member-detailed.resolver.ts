import { ResolveFn } from '@angular/router';
import { Member } from '../_models/member';
import { MembersService } from '../_services/MembersService';
import { inject } from '@angular/core';

export const memberDetailedResolver: ResolveFn<Member> = (route, state) => {
  const membersService = inject(MembersService);

  return membersService.getMember(route.paramMap.get('username')!);
};
