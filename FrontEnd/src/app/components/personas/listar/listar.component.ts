import { Component,ViewChild,OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Res_Personas } from 'src/app/models/personas.models';
import { PersonasService } from 'src/app/services/personas.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.scss']
})
export class ListarComponent implements OnInit {
  displayedColumns: string[] = ['Nombre Completo', 'Identificación', 'E-Mail', 'Usuario','Clave'];
  dataSource!: MatTableDataSource<Res_Personas>;


  constructor(private Persona_Serv : PersonasService){

  }
  // Paginadores y MatShort para las tablas autores y editoriales
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  //

  // detectar evento de insercion de personas
  private suscription !: Subscription;


  ngOnInit(): void {
    this.getPersona();

    // cada que haya un next o detecte un evento este refrescara la lista
    this.suscription = this.Persona_Serv.refrescar$.subscribe(()=>{
      this.getPersona();
    });
  }
  

  // Consulta todas las personas y asigna el datasource 
  getPersona(){
    try {
      this.Persona_Serv.getPersonas().subscribe(res =>{
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
      
    } catch (error) {
      
    }
  }

  applyFilterEditorial(event: Event) {

    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


}
