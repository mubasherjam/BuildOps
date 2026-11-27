import {
  Component,
  OnInit,
  inject,
  signal,
  computed,
  DestroyRef
} from '@angular/core';

import { RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

import { finalize } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

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

  categories = signal<EquipmentCategory[]>([]);

  isLoading = signal(false);

  errorMessage = signal('');

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

  ngOnInit(): void {

    this.loadCategories();

  }

  loadCategories(): void {

    if (this.isLoading()) {
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');

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
}