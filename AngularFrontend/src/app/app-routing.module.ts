import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandComponent } from './command/command.component';
import { PlatformComponent} from './platform/platform.component';

const routes: Routes = [
  { path: 'first-component', component: CommandComponent },
  { path: 'second-component', component: PlatformComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
