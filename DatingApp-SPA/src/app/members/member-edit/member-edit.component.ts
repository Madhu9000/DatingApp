import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { User } from '../../_models/user';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';
import { NgForm } from '../../../../node_modules/@angular/forms';
import { Type } from '../../../../node_modules/@angular/compiler/src/core';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm', {static: false}) editForm: NgForm;
  user: User;
  photoUrl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      // tslint:disable-next-line:no-string-literal
      this.user = data['user'];
    });
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }
  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.Success('Profile updated successfully');
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.Error(error);
    });
  }
  updateMainPhoto(photoUrl) {
    this.user.photoUrl = photoUrl;
  }
}
