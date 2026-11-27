export interface CreateEquipmentCategory {
  name: string;
  description?: string | null;
}

export interface EquipmentCategory {
  id: number;
  name: string;
  description: string | null;
  isActive: boolean;
  createdAt: string;
}
export interface UpdateEquipmentCategory {

  name: string;

  description: string | null;

  isActive: boolean;

}