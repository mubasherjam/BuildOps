import {
  Component,
  OnInit,
  inject,
  signal,
  DestroyRef
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  ActivatedRoute,
  Router,
  RouterLink
} from '@angular/router';

import { HttpErrorResponse } from '@angular/common/http';

import {
  finalize,
  switchMap,
  tap,
  catchError,
  EMPTY
} from 'rxjs';

import { takeUntilDestroyed } from
  '@angular/core/rxjs-interop';

import { EquipmentCategoryService } from
  '../../services/equipment-category.service';

import { UpdateEquipmentCategory } from
  '../../models/equipment-category.model';

import { whitespaceValidator } from
  '../../../../../core/validators/whitespace.validator';

@Component({
  selector: 'app-edit-category',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.scss'
})
export class EditCategory implements OnInit {

  private readonly fb = inject(FormBuilder);

  private readonly route = inject(ActivatedRoute);

  private readonly router = inject(Router);

  private readonly destroyRef = inject(DestroyRef);

  private readonly categoryService =
    inject(EquipmentCategoryService);

  categoryId = signal(0);

  isLoading = signal(true);

  isSaving = signal(false);

  categoryLoaded = signal(false);

  errorMessage = signal('');

  successMessage = signal('');

  readonly categoryForm = this.fb.nonNullable.group({

    name: ['', [
      Validators.required,
      Validators.maxLength(100),
      whitespaceValidator
    ]],

    description: ['', [
      Validators.maxLength(500)
    ]],

    isActive: [true]

  });

  get name() {
    return this.categoryForm.controls.name;
  }

  get description() {
    return this.categoryForm.controls.description;
  }

  ngOnInit(): void {

    this.route.paramMap
      .pipe(

        switchMap(params => {

          const id = Number(params.get('id'));

          this.categoryLoaded.set(false);
          this.errorMessage.set('');
          this.successMessage.set('');

          if (!Number.isInteger(id) || id <= 0) {

            this.isLoading.set(false);

            this.errorMessage.set(
              'Invalid category ID.'
            );

            return EMPTY;
          }

          this.categoryId.set(id);

          this.isLoading.set(true);

          return this.categoryService.getById(id)
            .pipe(

              tap(category => {

                this.categoryForm.reset({
                  name: '',
                  description: '',
                  isActive: true
                });

                this.categoryForm.patchValue({

                  name: category.name,

                  description:
                    category.description ?? '',

                  isActive: category.isActive

                });

                this.categoryLoaded.set(true);

              }),

              catchError((error: HttpErrorResponse) => {

                this.errorMessage.set(
                  error.error?.detail ||
                  'Unable to load equipment category.'
                );

                return EMPTY;

              }),

              finalize(() => {
                this.isLoading.set(false);
              })

            );

        }),

        takeUntilDestroyed(this.destroyRef)

      )
      .subscribe();

  }

  updateCategory(): void {

    if (
      this.isSaving() ||
      this.isLoading() ||
      !this.categoryLoaded()
    ) {
      return;
    }

    this.errorMessage.set('');
    this.successMessage.set('');

    if (this.categoryForm.invalid) {

      this.categoryForm.markAllAsTouched();

      return;
    }

    const formValue =
      this.categoryForm.getRawValue();

    const request: UpdateEquipmentCategory = {

      name: formValue.name.trim(),

      description:
        formValue.description.trim() || null,

      isActive: formValue.isActive

    };

    this.isSaving.set(true);

    this.categoryForm.disable();

    this.categoryService.update(
      this.categoryId(),
      request
    )
    .pipe(

      takeUntilDestroyed(this.destroyRef),

      finalize(() => {

        this.isSaving.set(false);

        this.categoryForm.enable();

      })

    )
    .subscribe({

      next: (response) => {

        this.categoryForm.reset({

          name: response.name,

          description:
            response.description ?? '',

          isActive: response.isActive

        });

        this.successMessage.set(
          'Category updated successfully.'
        );

      },

      error: (error: HttpErrorResponse) => {

        this.errorMessage.set(
          error.error?.detail ||
          'Unable to update equipment category.'
        );

      }

    });

  }

  cancel(): void {

    if (!this.isSaving()) {

      this.router.navigate([
        '/equipment/categories'
      ]);

    }

  }

}