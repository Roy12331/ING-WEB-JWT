import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest } from '../../../models/auth/login-request';
import { AuthService } from '../../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-auth',
    imports: [ReactiveFormsModule],
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AuthComponent {

    _authServices = inject(AuthService);
    _router = inject(Router);



    readonly submitting = signal(false);

    readonly loginForm = new FormGroup({
        username: new FormControl('', {
            nonNullable: true,
            validators: [Validators.required],
        }),
        password: new FormControl('', {
            nonNullable: true,
            validators: [Validators.required],
        }),
    });

    submit(): void {
        if (this.loginForm.invalid) {
            this.loginForm.markAllAsTouched();
            return;
        }

        this.submitting.set(true);

        let loginRequest: LoginRequest = this.loginForm.getRawValue();

        this._authServices.login(loginRequest).subscribe({
            next: (response) => {
                console.log('Login successful', response);

                //hacer uso de las variables de sessión
                sessionStorage.setItem('token', response.token);
                
                //navega a la ruta de lista de personas
                this._router.navigate(['/personas']);
            },
            error: (error) => { console.error('Login failed', error); }
        });

        console.log('Login payload', this.loginForm.getRawValue());
        this.submitting.set(false);
    }
}
