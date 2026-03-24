import { Routes } from '@angular/router';
import { MantenimientoPersonaListComponent } from './pages/mantenimiento/mantenimiento-persona-list/mantenimiento-persona-list.component';
import { AuthComponent } from './pages/auth/auth/auth.component';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [

    {
        path: '', component: AuthComponent
    },
    {
        path:'mantenimiento',
        children:[
            { path: 'personas', component: MantenimientoPersonaListComponent },
            { path: 'persona-tipo', component: MantenimientoPersonaListComponent },
            { path: 'persona-sexo', component: MantenimientoPersonaListComponent }
        ]

    },
    {
        path: 'personas', component: MantenimientoPersonaListComponent,
        canActivate: [authGuard]
    },
    //EJEMPLO CanDeactivate
    // CanDeactivate es un guard que se ejecuta antes de salir de una ruta, se puede usar para mostrar un mensaje de confirmación al usuario si tiene cambios sin guardar, por ejemplo.
    // Ej,. Practico => no vas a sale de esta opción hasta que confirmes el cambio de contraseña
    {
        path: 'change-password', component: MantenimientoPersonaListComponent
    },

];
