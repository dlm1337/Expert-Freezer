import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainContentComponent } from './main-content/main-content.component';
// import { EditUserComponent } from './edit-user/edit-user.component';
// import { LoginComponent } from './login/login.component';
// import { LogoutComponent } from './logout/logout.component';
// import { RegisterUserComponent } from './register-user/register-user.component';
// import { ProfileComponent } from './profile/profile.component';
// import { FeedComponent } from './feed/feed.component';
// import { authenticationGuard } from './services/auth.guard';


/* taking a dynamic aproach which will not require routing and allow more flexibility.
See main-content for tile layout, components are directly plugged in. Leaving routes commented
incase I change my mind in the future. The dynamic approach can become hard to maintain in larger scale apps. */


const routes: Routes = [
  { path: 'main', component: MainContentComponent },
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  // { path: 'edit-user', component: EditUserComponent },
  // { path: 'login', component: LoginComponent },
  // { path: 'logout', component: LogoutComponent },
  // { path: 'register-user', component: RegisterUserComponent },
  // { path: 'profile', component: ProfileComponent, canActivate: [authenticationGuard] },
  // { path: 'feed', component: FeedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
