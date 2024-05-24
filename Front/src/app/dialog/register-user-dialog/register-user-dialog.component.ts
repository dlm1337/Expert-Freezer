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

export class RegisterUserDialogComponent implements OnInit {
  form: FormGroup;

  constructor(public dialogRef: MatDialogRef<RegisterUserDialogComponent>, private fb: FormBuilder,
    public cdr: ChangeDetectorRef, public restSvc: RestService) { }


  ngOnInit(): void {
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      companyName: [''],
      profilePic: [null],
      extraPics: [null],
      extraPicsDesc: this.fb.array([]),
      companyDescription: [''],
      services: [''],
      address: [''],
      pricing: ['']
    }
      , {
        validators: passwordMatchValidator // Attach the custom validator function here
      });
  }

  submitForm(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const formData = new FormData();

      const filePromises: Promise<void>[] = [];

      // Loop through form controls and append values to FormData
      Object.keys(formValue).forEach(key => {
        if (key === 'profilePic') {
          const file: File = formValue[key];
          if (file && file instanceof File) {
            const filePromise = this.convertFileToBase64(file).then(base64String => {
              formData.append(key, base64String);
            });
            filePromises.push(filePromise);
          }
        } else if (key === 'extraPics') {
          const files: FileList = formValue[key];
          if (files && files instanceof FileList) {
            const base64Promises = Array.from(files).map(file => {
              if (file instanceof File) {
                return this.convertFileToBase64(file);
              }
              return Promise.resolve(''); // Handle the case where it's not a File
            });
            const filesPromise = Promise.all(base64Promises).then(base64Strings => {
              base64Strings.forEach(base64String => formData.append(key, base64String));
            });
            filePromises.push(filesPromise);
          }
        } else {
          const controlValue = formValue[key];
          formData.append(key, controlValue);
        }
      });

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
  private convertFileToBase64(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = error => reject(error);
      reader.readAsDataURL(file);
    });
  }

  close(): void {
    this.dialogRef.close();
  }

}
