import { ChangeDetectorRef, Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { MatDialogRef, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { throwError } from 'rxjs';
import { RestService } from 'src/app/services/rest.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { passwordMatchValidator } from 'src/app/validators/password-validator';

@Component({
  selector: 'app-register-user-dialog',
  standalone: true,
  imports: [MatDialogContent, MatDialogActions, CommonModule, ReactiveFormsModule, FormsModule,
    MatFormFieldModule, MatInputModule],
  templateUrl: './register-user-dialog.component.html',
  styleUrl: './register-user-dialog.component.scss'
})

export class RegisterUserDialogComponent {
  form: FormGroup;
  profilePic: File | null = null;
  extraPics: File[] = [];

  constructor(private formBuilder: FormBuilder, private restSvc: RestService,
    private dialogRef: MatDialogRef<RegisterUserDialogComponent>) {
    this.form = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      companyName: [''],
      profilePic: [null],
      extraPics: [null],
      extraPicsDesc: [''],
      companyDescription: ['', Validators.required],
      services: ['', Validators.required],
      address: [''],
      pricing: ['', Validators.required]
    }, {
      validators: passwordMatchValidator // Attach the custom validator function here
    });
  }

  onFileChange(event: any, controlName: string): void {
    const fileInput = event.target;
    if (fileInput.files.length > 0) {
      if (controlName === 'profilePic') {
        this.profilePic = fileInput.files[0];
      } else if (controlName === 'extraPics') {
        this.extraPics = Array.from(fileInput.files);
      }
    }
  }

  convertFileToBase64(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = error => reject(error);
      reader.readAsDataURL(file);
    });
  }

  submitForm(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const formData = new FormData();

      const filePromises: Promise<void>[] = [];

      Object.keys(formValue).forEach(key => {
        if (key === 'profilePic' && this.profilePic) {
          console.log('At profile pic.');
          const filePromise = this.convertFileToBase64(this.profilePic).then(base64String => {
            formData.append(key, base64String);
          });
          filePromises.push(filePromise);
        } else if (key === 'extraPics' && this.extraPics.length > 0) {
          console.log('At extra pics.');
          const base64Promises = this.extraPics.map(file => this.convertFileToBase64(file));
          const filesPromise = Promise.all(base64Promises).then(base64Strings => {
            base64Strings.forEach(base64String => formData.append(key, base64String));
          });
          filePromises.push(filesPromise);
        } else {
          const controlValue = formValue[key];
          formData.append(key, controlValue);
        }
      });

      console.log(formData);

      // Wait for all file conversion promises to resolve
      Promise.all(filePromises).then(() => {
        // Submit formData to the backend
        this.restSvc.saveUserInfo(formData).subscribe((resp) => {
          if (resp) {
            console.log('User info saved successfully:', resp);
          } else {
            const err = new Error('User info did not save');
            throwError(() => err);
          }
        });
      }).catch(error => {
        console.error('Error converting files to base64:', error);
      });
    }
  }


  close(): void {
    this.dialogRef.close();
  }
}