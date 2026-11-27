import { Routes } from '@angular/router';

import { CreateCategory } from
  './features/equipment/categories/pages/create-category/create-category';

import { CategoryList } from
  './features/equipment/categories/pages/category-list/category-list';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'equipment/categories',
    pathMatch: 'full'
  },

  {
    path: 'equipment/categories',
    component: CategoryList
  },

  {
    path: 'equipment/categories/create',
    component: CreateCategory
  }

];