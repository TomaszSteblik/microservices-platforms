import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandComponent } from './command/command.component';
import { HomeComponent } from './home/home.component';
import { PlatformComponent} from './platform/platform.component';

const routes: Routes = [
  { path: 'commands', component: CommandComponent },
  { path: 'platforms', component: PlatformComponent },
  { path: 'home', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
