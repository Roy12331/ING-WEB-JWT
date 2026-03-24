import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';
import { JsonPipe } from '@angular/common';
import { PersonaTipoSexoDto } from '../../../models/persona/PersonaTipoSexoDto.model';

@Component({
  selector: 'app-mantenimiento-persona-editar',
  standalone: true,
  imports: [ReactiveFormsModule,],
  templateUrl: './mantenimiento-persona-editar.component.html',
  styleUrls: ['./mantenimiento-persona-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoPersonaEditarComponent {

  // Inputs desde el componente padre
  persona = input<PersonaDto | null>(null);
  tiposDocumentos = input<PersonaTipoDocumentoDto[] | null>([]);
  tiposSexos = input<PersonaTipoSexoDto[] | null>([]);
  modo = input<'crear' | 'editar'>('crear');

  // Outputs hacia el componente padre
  cancelado = output<void>();
  guardado = output<void>();

  private readonly personaService = inject(PersonaService);
  private readonly formBuilder = inject(FormBuilder);

  // Formulario reactivo
  readonly form = this.formBuilder.group({
    idTipoDocumento: [0, [Validators.min(1)]],
    idTipoSexo: [0, [Validators.min(1)]],
    nombres: ['', [Validators.required]],
    apellidoPaterno: ['', [Validators.required]],
    apellidoMaterno: [''],
    direccion: [''],
    telefono: [''],
  });

  cargando = false;

  constructor() {
    // El efecto reacciona cuando la señal 'persona' cambia (al abrir editar)
    effect(() => {
      const p = this.persona();
      if (p) {
        this.form.patchValue({
          idTipoDocumento: p.idTipoDocumento,
          idTipoSexo: p.idTipoSexo,
          nombres: p.nombres,
          apellidoPaterno: p.apellidoPaterno,
          apellidoMaterno: p.apellidoMaterno,
          direccion: p.direccion,
          telefono: p.telefono,
        });
      } else {
        this.form.reset({ 
          idTipoDocumento: 0, 
          idTipoSexo: 0,
          nombres: '',
          apellidoPaterno: '',
          apellidoMaterno: '',
          direccion: '',
          telefono: ''
        });
      }
    });
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    if (this.form.invalid || this.cargando) {
      this.form.markAllAsTouched();
      return;
    }

    this.cargando = true;
    const valores = this.form.getRawValue();
    const actual = this.persona();

    // CREACIÓN DEL PAYLOAD PASO A PASO PARA EVITAR ERRORES DE TIPADO
    const payload = new PersonaDto();
    
    payload.id = actual?.id ?? 0;
    payload.idTipoDocumento = Number(valores.idTipoDocumento);
    payload.idTipoSexo = Number(valores.idTipoSexo);
    
    // Estos campos deben existir en tu PersonaDto.model.ts
    payload.idPersonaTipo = actual?.idPersonaTipo ?? 1; 
    payload.idTipoSangre = actual?.idTipoSangre ?? 1;
    
    payload.nombres = valores.nombres ?? '';
    payload.apellidoPaterno = valores.apellidoPaterno ?? '';
    payload.apellidoMaterno = valores.apellidoMaterno ?? '';
    payload.direccion = valores.direccion ?? '';
    payload.telefono = valores.telefono ?? '';
    
    payload.userCreate = actual?.userCreate ?? 1;
    payload.userUpdate = 1;
    
    // Manejo de fechas para evitar errores en el Backend
    payload.dateCreated = actual?.dateCreated ?? new Date().toISOString();
    payload.dateUpdate = new Date().toISOString();

    // Lógica de guardado (Crear o Editar)
    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.personaService.update(payload.id, payload)
      : this.personaService.create(payload);

    request$.subscribe({
      next: () => {
        this.guardado.emit();
      },
      error: (error) => {
        console.error('Error al guardar la persona:', error);
        this.cargando = false;
      },
      complete: () => {
        this.cargando = false;
      },
    });
  }
}