import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = sessionStorage.getItem('token');
  let clonedRequest = req;

  if (token) {
    clonedRequest = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
  }

  return next(clonedRequest).pipe(
    catchError((error) => {
      // ESTO SE EJECUTARÁ CUANDO FALLA PERSONA/GETALL
      console.error('ERROR DETECTADO:', error);
      
      // Forzamos el mensaje que pidió el profesor
      alert("⚠️ ALGO SALIÓ MAL: Error en el servidor (División por cero)");
      
      return throwError(() => error);
    })
  );
};