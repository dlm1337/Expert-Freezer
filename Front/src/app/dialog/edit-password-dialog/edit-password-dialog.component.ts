import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { throwError } from 'rxjs';
import { RestService } from 'src/app/services/rest.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { passwordMatchValidator } from 'src/app/validators/password-validator';

@Component({
  selector: 'app-edit-password-dialog',
  standalone: true,
  imports: [MatDialogContent, MatDialogActions, CommonModule, ReactiveFormsModule, FormsModule,
    MatFormFieldModule, MatInputModule],
  templateUrl: './edit-password-dialog.component.html',
  styleUrl: './edit-password-dialog.component.scss'
})

export class EditPasswordDialogComponent {
  form: FormGroup;
  profilePic: File | null = null;
  extraPics: File[] = [];

  constructor(private formBuilder: FormBuilder, private restSvc: RestService,
    private dialogRef: MatDialogRef<EditPasswordDialogComponent>) {
    this.form = this.formBuilder.group({
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    }, {
      validators: passwordMatchValidator // Attach the custom validator function here
    });
  }

  submitForm(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const formData = new FormData();

      Object.keys(formValue).forEach(key => {

        const controlValue = formValue[key];
        formData.append(key, controlValue);

      });

      console.log(formData);

      // Submit formData to the backend
      this.restSvc.login(formData).subscribe((resp) => {
        if (resp) {
          console.log('User info saved successfully:', resp);
        } else {
          const err = new Error('User info did not save');
          throwError(() => err);
        }
      });

    }
  }


  close(): void {
    this.dialogRef.close();
  }
}
