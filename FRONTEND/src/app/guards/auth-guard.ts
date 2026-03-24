import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {


  let token = sessionStorage.getItem('token');

  // Si no hay token, retornamos false para que el usuario no pueda acceder a la ruta, y lo redireccionamos a la página de login.
  if (!token) {
    return false;
  }


  //RECORDEMOS QUE CUANDO RETORNAMOS UN TRUE, EL USUARIO PUEDE ACCEDER A LA RUTA,
  //SI RETORNAMOS UN FALSE, EL USUARIO NO PUEDE ACCEDER A LA RUTA,
  //Y SI RETORNAMOS UN URL TREE, EL USUARIO ES REDIRECCIONADO A ESA RUTA.
  return true;
};
