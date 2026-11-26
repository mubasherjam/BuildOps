import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  CreateEquipmentCategory,
  EquipmentCategory
} from '../models/equipment-category.model';

import { API_BASE_URL } from '../../../../core/config/api.config';

@Injectable({
  providedIn: 'root'
})
export class EquipmentCategoryService {

  private readonly apiUrl =
    `${API_BASE_URL}/equipment-categories`;

  constructor(private http: HttpClient) {
  }

  create(
    request: CreateEquipmentCategory
  ): Observable<EquipmentCategory> {

    return this.http.post<EquipmentCategory>(
      this.apiUrl,
      request
    );
  }
}