import { Component } from '@angular/core';
import { FormBuilder, FormGroup, } from '@angular/forms';
import { MatDialogRef, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { throwError } from 'rxjs';
import { RestService } from 'src/app/services/rest.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-edit-profile-dialog',
  standalone: true,
  imports: [MatDialogContent, MatDialogActions, CommonModule, ReactiveFormsModule, FormsModule,
    MatFormFieldModule, MatInputModule],
  templateUrl: './edit-profile-dialog.component.html',
  styleUrl: './edit-profile-dialog.component.scss'
})

export class EditProfileDialogComponent {
  form: FormGroup;
  profilePic: File | null = null;
  extraPics: { file: File, desc: string }[] = [];

  constructor(private formBuilder: FormBuilder, private restSvc: RestService,
    private dialogRef: MatDialogRef<EditProfileDialogComponent>) {
    this.form = this.formBuilder.group({
      companyName: [''],
      profilePic: [null],
      extraPics: [null],
      extraPicsDesc: [''],
      companyDescription: [''],
      services: [''],
      address: [''],
      pricing: ['']

    });
  }

  onFileChange(event: any, controlName: string): void {
    const fileInput = event.target;
    if (fileInput.files.length > 0) {
      if (controlName === 'profilePic') {
        this.profilePic = fileInput.files[0];
      } else if (controlName === 'extraPics') {
        // Assuming multiple files can be selected for extraPics
        const files: File[] = Array.from(fileInput.files);
        const extraPicDesc = this.form.get('extraPicsDesc')?.value;
        if (extraPicDesc) {
          files.forEach(file => {
            this.extraPics.push({ file, desc: extraPicDesc });
          });
          this.resetExtraPicForm();
        }
      }
    }
  }

  addExtraPic(): void {
    const fileInput = document.getElementById('extraPics') as HTMLInputElement;
    const extraPicDesc = this.form.get('extraPicsDesc')?.value;
    if (fileInput && fileInput.files && extraPicDesc) {
      const files: File[] = Array.from(fileInput.files);
      files.forEach(file => {
        this.extraPics.push({ file, desc: extraPicDesc });
      });
      this.resetExtraPicForm();
    }
  }

  resetExtraPicForm(): void {
    this.form.patchValue({
      extraPicsDesc: ''
    });
    const fileInput = document.getElementById('extraPics') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  removeExtraPic(name: string) {
    this.extraPics = this.extraPics.filter(x => x.file.name !== name)
    console.log(this.extraPics);
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
          this.extraPics.map(file => formData.append('extraPicsDesc', file.desc));
          const base64Promises = this.extraPics.map(file => this.convertFileToBase64(file.file));
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
        this.restSvc.login(formData).subscribe((resp) => {
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
