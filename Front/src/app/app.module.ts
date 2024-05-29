import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainContentComponent } from './main-content/main-content.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ConfigService } from './services/config.service';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { SearchResultComponent } from './search-result/search-result.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ProfileComponent } from './profile/profile.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { MatButtonModule } from '@angular/material/button';
import { FeedComponent } from './feed/feed.component';
import { MatTabsModule } from '@angular/material/tabs';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth.guard';
import { TokenInterceptor } from './services/token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    FooterComponent,
    MainContentComponent,
    LoginComponent,
    LogoutComponent,
    RegisterUserComponent,
    SearchResultComponent,
    EditUserComponent,
    ProfileComponent,
    SideBarComponent,
    FeedComponent
  ],
  imports: [
    BrowserModule,
    // import HttpClientModule after BrowserModule.
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([{ path: 'main', component: MainContentComponent }]),
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatTabsModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatSelectModule,
    MatGridListModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
  ],
  providers: [
    AuthService,
    AuthGuard,
    {
      provide: APP_INITIALIZER,
      useClass: TokenInterceptor,
      multi: true,
      deps: [ConfigService],
      useFactory: (configService: ConfigService) => () =>
        configService.loadAppConfig(),
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
