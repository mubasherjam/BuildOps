import { Component, OnInit, inject, signal, computed, DestroyRef } from '@angular/core';

import { DatePipe } from '@angular/common';

import { RouterLink } from '@angular/router';

import { HttpErrorResponse } from '@angular/common/http';

import { finalize } from 'rxjs';

import { takeUntilDestroyed } from
  '@angular/core/rxjs-interop';

import { EquipmentCategoryService } from
  '../../services/equipment-category.service';

import { EquipmentCategory } from
  '../../models/equipment-category.model';

@Component({
  selector: 'app-category-list',
  imports: [RouterLink, DatePipe],
  templateUrl: './category-list.html',
  styleUrl: './category-list.scss'
})
export class CategoryList implements OnInit {

  private readonly categoryService =
    inject(EquipmentCategoryService);

  private readonly destroyRef = inject(DestroyRef);

  // Category listing signals

  categories = signal<EquipmentCategory[]>([]);

  isLoading = signal(false);

  errorMessage = signal('');

  successMessage = signal('');

  // Delete operation signals

  deleteError = signal('');

  isDeleting = signal(false);

  selectedCategory =
    signal<EquipmentCategory | null>(null);

  // Dashboard summary counts

  totalCategories = computed(
    () => this.categories().length
  );

  activeCategories = computed(
    () => this.categories()
      .filter(category => category.isActive).length
  );

  inactiveCategories = computed(
    () => this.categories()
      .filter(category => !category.isActive).length
  );

  // Component lifecycle

  ngOnInit(): void {

    this.loadCategories();

  }

  // READ operation

  loadCategories(): void {

    if (this.isLoading() || this.isDeleting()) {
      return;
    }

    this.isLoading.set(true);

    this.errorMessage.set('');

    this.successMessage.set('');

    this.categoryService.getAll()
      .pipe(

        takeUntilDestroyed(this.destroyRef),

        finalize(() => {
          this.isLoading.set(false);
        })

      )
      .subscribe({

        next: (response) => {

          this.categories.set(response);

        },

        error: (error: HttpErrorResponse) => {

          this.errorMessage.set(
            error.error?.detail ||
            'Unable to load equipment categories.'
          );

        }

      });

  }

  // Open delete confirmation dialog

  openDeleteDialog(
    category: EquipmentCategory
  ): void {

    if (this.isDeleting() || this.isLoading()) {
      return;
    }

    this.successMessage.set('');

    this.deleteError.set('');

    this.selectedCategory.set(category);

  }

  // Close delete confirmation dialog

  closeDeleteDialog(): void {

    if (this.isDeleting()) {
      return;
    }

    this.selectedCategory.set(null);

    this.deleteError.set('');

  }

  // DELETE operation

  confirmDelete(): void {

    const category = this.selectedCategory();

    if (!category || this.isDeleting()) {
      return;
    }

    this.isDeleting.set(true);

    this.deleteError.set('');

    this.categoryService.delete(category.id)
      .pipe(

        takeUntilDestroyed(this.destroyRef),

        finalize(() => {
          this.isDeleting.set(false);
        })

      )
      .subscribe({

        next: () => {

          this.categories.update(categories =>
            categories.filter(
              item => item.id !== category.id
            )
          );

          this.selectedCategory.set(null);

          this.successMessage.set(
            `Category "${category.name}" deleted successfully.`
          );

        },

        error: (error: HttpErrorResponse) => {

          this.deleteError.set(
            error.error?.detail ||
            'Unable to delete equipment category.'
          );

        }

      });

  }

}