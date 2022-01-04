import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandComponent } from './command/command.component';
import { HomeComponent } from './home/home.component';
import { PlatformsComponent} from './platforms/platforms.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { CommandsComponent} from './commands/commands.component';

const routes: Routes = [
  { path: 'commands', component: CommandsComponent },
  { path: 'platforms', component: PlatformsComponent },
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '/404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
