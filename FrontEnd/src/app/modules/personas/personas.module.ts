import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PersonasRoutingModule } from './personas-routing.module';
import { PersonasComponent } from 'src/app/components/personas/personas.component';
import { MaterialModule } from 'src/app/core/material.module';
import { ListarComponent } from 'src/app/components/personas/listar/listar.component';
import { PersonasService } from 'src/app/services/personas.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [PersonasComponent,ListarComponent],
  imports: [
    CommonModule,
    PersonasRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers:[PersonasService]
})
export class PersonasModule { }
