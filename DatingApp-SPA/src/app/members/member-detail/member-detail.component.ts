import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
user: User;
  constructor(private userService: UserService, private alert: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadUser();
  }
loadUser() {
  // tslint:disable-next-line:no-string-literal
  this.userService.getUser(+this.route.snapshot.params['id']).subscribe((user: User) => {
    this.user = user;
  }, error => {
    this.alert.Error(error);
  });
}
}
