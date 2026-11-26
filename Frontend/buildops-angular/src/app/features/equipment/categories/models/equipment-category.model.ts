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