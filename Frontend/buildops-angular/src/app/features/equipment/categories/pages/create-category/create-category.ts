import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-category',
  imports: [FormsModule],
  templateUrl: './create-category.html',
  styleUrl: './create-category.scss'
})
export class CreateCategory {

  pageTitle: string = 'Create Equipment Category';

  categoryName: string = '';

  description: string = '';

  message: string = '';

  createCategory(): void {

    if (!this.categoryName.trim()) {
      this.message = 'Category name is required.';
      return;
    }

    this.message =
      `Category "${this.categoryName.trim()}" is ready to submit.`;
  }

  resetForm(): void {
    this.categoryName = '';
    this.description = '';
    this.message = '';
  }
}