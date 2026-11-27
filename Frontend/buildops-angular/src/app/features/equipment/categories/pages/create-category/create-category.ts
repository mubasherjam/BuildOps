import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { finalize } from 'rxjs';
import { RouterLink } from '@angular/router';
import { EquipmentCategoryService } from
  '../../services/equipment-category.service';

import { CreateEquipmentCategory } from
  '../../models/equipment-category.model';

@Component({
  selector: 'app-create-category',
  imports: [FormsModule],
  templateUrl: './create-category.html',
  styleUrl: './create-category.scss'
})
export class CreateCategory {

  pageTitle = 'Create Equipment Category';

  categoryName = '';
  description = '';

  successMessage = signal('');
  errorMessage = signal('');
  isLoading = signal(false);

  constructor(
    private categoryService: EquipmentCategoryService
  ) {
  }

  createCategory(): void {

    if (this.isLoading()) {
      return;
    }

    this.successMessage.set('');
    this.errorMessage.set('');

    if (!this.categoryName.trim()) {
      this.errorMessage.set('Category name is required.');
      return;
    }

    const request: CreateEquipmentCategory = {
      name: this.categoryName.trim(),
      description: this.description.trim() || null
    };

    this.isLoading.set(true);

    this.categoryService.create(request)
      .pipe(
        finalize(() => {
          this.isLoading.set(false);
        })
      )
      .subscribe({

        next: (response) => {

          this.categoryName = '';
          this.description = '';

          this.successMessage.set(
            `Category "${response.name}" created successfully.`
          );
        },

        error: (error: HttpErrorResponse) => {

          this.errorMessage.set(
            error.error?.detail ||
            'Unable to create category. Please try again.'
          );
        }

      });
  }

  resetForm(): void {

    this.categoryName = '';
    this.description = '';

    this.successMessage.set('');
    this.errorMessage.set('');
  }
}