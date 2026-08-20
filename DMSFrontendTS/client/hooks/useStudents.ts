import { useCallback, useEffect, useState } from "react";
import { studentService } from "@/services/studentService";
import type { Student } from "@/types/student";

export function useStudents() {
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchStudents = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);

      const data = await studentService.getAll();

      setStudents(data);
    } catch (error) {
      console.error("Failed to fetch students:", error);

      setError("Failed to load students.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchStudents();
  }, [fetchStudents]);

  return {
    students,
    loading,
    error,
    refetch: fetchStudents,
  };
}