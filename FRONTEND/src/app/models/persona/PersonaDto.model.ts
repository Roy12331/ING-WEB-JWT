export class PersonaDto {
    id: number = 0;
    idTipoDocumento: number = 0;
    idPersonaTipo: number = 1;   // <--- FALTABA ESTO
    idTipoSexo: number = 1;       // <--- FALTABA ESTO
    idTipoSangre: number = 1;     // <--- FALTABA ESTO
    nombres: string | null = "";
    apellidoPaterno: string | null = "";
    apellidoMaterno: string | null = "";
    direccion: string | null = "";
    telefono: string | null = "";
    userCreate: number = 0;
    userUpdate: number | null = 0;
    dateCreated: string | null = "";
    dateUpdate: string | null = "";
}