import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Router } from '@angular/router'; 
import { DatePipe } from '@angular/common'; 
import { FormsModule } from '@angular/forms'; // AÑADIDO para el filtrado
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { MantenimientoPersonaEditarComponent } from '../mantenimiento-persona-editar/mantenimiento-persona-editar.component';
import { PersonTipoDocumentoServices } from '../../../services/personTipoDocumento/person-tipo-documento.service';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';
import { PersonaTipoSexoService } from '../../../services/personaTipoSexo/PersonaTipoSexo.service';
import { PersonaTipoSexoDto } from '../../../models/persona/PersonaTipoSexoDto.model';
import { PersonaTipoService } from '../../../services/personTipo/persona-tipo.service';
import { PersonaTipoSangreService } from '../../../services/personaTipoSangre/personaTipoSangre.service';

@Component({
  selector: 'app-mantenimiento-persona-list',
  standalone: true, 
  // Se añade FormsModule para que funcione el buscador
  imports: [MantenimientoPersonaEditarComponent, DatePipe, FormsModule], 
  templateUrl: './mantenimiento-persona-list.component.html',
  styleUrls: ['./mantenimiento-persona-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoPersonaListComponent implements OnInit {

  _personaService = inject(PersonaService);
  _personaTipoDocumentoService = inject(PersonTipoDocumentoServices);
  _personaTipoSexoService = inject(PersonaTipoSexoService);
  _personaTipoService = inject(PersonaTipoService);
  _personaTipoSangreService = inject(PersonaTipoSangreService);
  _router = inject(Router); 

  personas = signal<PersonaDto[]>([]);
  personasOriginal = signal<PersonaDto[]>([]); // Respaldo para la limpieza
  
  // Variables para los inputs de búsqueda
  filtroNombre: string = '';
  filtroApellido: string = '';

  tiposDocumentos: PersonaTipoDocumentoDto[] = [];
  tiposSexos: PersonaTipoSexoDto[] = [];
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  personaSeleccionada: PersonaDto | null = null;

  constructor() { }

  ngOnInit() {
    this.getAllPersonas();
    this.getTiposRxjs();
  }

  logout(): void {
    sessionStorage.removeItem('token');
    this._router.navigate(['/']); 
  }

  // Lógica de búsqueda
  onBuscar(): void {
    const nombre = this.filtroNombre.toLowerCase().trim();
    const apellido = this.filtroApellido.toLowerCase().trim();

    const filtrados = this.personasOriginal().filter(p => 
      (p.nombres?.toLowerCase().includes(nombre)) &&
      (p.apellidoPaterno?.toLowerCase().includes(apellido))
    );
    
    this.personas.set(filtrados);
  }

  // Lógica de limpieza
  onLimpiar(): void {
    this.filtroNombre = '';
    this.filtroApellido = '';
    this.personas.set(this.personasOriginal());
  }

  getTiposRxjs() {
    forkJoin({
      tiposDocumentos: this._personaTipoDocumentoService.getAll(),
      tiposSexos: this._personaTipoSexoService.getAll(),
      tiposSangre: this._personaTipoSangreService.getAll(),
      tiposPersona: this._personaTipoService.getAll(),
    }).subscribe({
      next: ({ tiposDocumentos, tiposSexos, tiposSangre, tiposPersona }) => {
        this.tiposDocumentos = tiposDocumentos;
        this.tiposSexos = tiposSexos;
      },
      error: (err) => { console.log('ocurrio un error', err); },
    });
  }

  getAllPersonas() {
    this._personaService.getAll().subscribe({
      next: (data) => {
        this.personas.set(data);
        this.personasOriginal.set(data); // Guardamos copia original
      },
      error: (err) => { console.log("ocurrio un error", err); },
      complete: () => { console.log('getAllPersonas completed'); }
    });
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.personaSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(persona: PersonaDto): void {
    this.modoEdicion = 'editar';
    this.personaSeleccionada = { ...persona };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.personaSeleccionada = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    this.getAllPersonas();
  }

  eliminarPersona(persona: PersonaDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar el registro de ${persona.nombres ?? ''} ${persona.apellidoPaterno ?? ''}?`
    );

    if (!confirmado) return;

    this._personaService.delete(persona.id).subscribe({
      next: () => { this.getAllPersonas(); },
      error: (err) => { console.log('ocurrio un error al eliminar', err); },
    });
  }
}