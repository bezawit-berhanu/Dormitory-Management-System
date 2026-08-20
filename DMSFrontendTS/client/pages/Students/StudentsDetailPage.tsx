import { useEffect, useState } from "react";
import {
  Link,
  useParams,
} from "react-router-dom";

import { studentService } from "@/services/studentService";
import type { Student } from "@/types/student";

import StudentDetails from "@/components/students/StudentDetails";

export default function StudentDetailsPage() {
  const { id } = useParams<{ id: string }>();

  const [student, setStudent] =
    useState<Student | null>(null);

  const [loading, setLoading] = useState(true);

  const [error, setError] =
    useState<string | null>(null);

  useEffect(() => {
    const loadStudent = async () => {
      if (!id) {
        setError("Student ID is missing.");
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        setError(null);

        const data =
          await studentService.getById(
            Number(id)
          );

        setStudent(data);
      } catch (error) {
        console.error(
          "Failed to load student:",
          error
        );

        setError(
          "Unable to load student details."
        );
      } finally {
        setLoading(false);
      }
    };

    loadStudent();
  }, [id]);

  if (loading) {
    return (
      <div className="p-6">
        <p className="text-sm text-muted-foreground">
          Loading student...
        </p>
      </div>
    );
  }

  if (error || !student) {
    return (
      <div className="p-6">
        <div className="rounded-xl border border-border p-8 text-center">
          <h2 className="font-semibold">
            Student not found
          </h2>

          <p className="mt-2 text-sm text-muted-foreground">
            {error ??
              "The requested student could not be found."}
          </p>

          <Link
            to="/students"
            className="mt-4 inline-block text-sm font-medium underline"
          >
            Back to Students
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 p-6">
      <Link
        to="/students"
        className="text-sm text-muted-foreground hover:text-foreground"
      >
        ← Back to Students
      </Link>

      <div>
        <h1 className="text-2xl font-semibold">
          Student Details
        </h1>

        <p className="mt-1 text-sm text-muted-foreground">
          View student information.
        </p>
      </div>

      <StudentDetails student={student} />
    </div>
  );
}