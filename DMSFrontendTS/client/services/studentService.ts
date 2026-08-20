import { api } from "@/lib/api";
import type { Student } from "@/types/student";

export const studentService = {
  async getAll(): Promise<Student[]> {
    const response = await api.get<Student[]>("/Student");
    return response.data;
  },

  async getById(id: number): Promise<Student> {
    const response = await api.get<Student>(`/Student/${id}`);
    return response.data;
  },
};