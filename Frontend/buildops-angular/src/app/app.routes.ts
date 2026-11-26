import { Routes } from '@angular/router';
import { CreateCategory } from
  './features/equipment/categories/pages/create-category/create-category';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'equipment/categories/create',
    pathMatch: 'full'
  },

  {
    path: 'equipment/categories/create',
    component: CreateCategory
  }

];