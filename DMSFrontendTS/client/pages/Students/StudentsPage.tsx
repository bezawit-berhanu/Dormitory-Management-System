import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

import { useStudents } from "@/hooks/useStudents";
import type {
  Student,
  StudentStatus,
} from "@/types/student";

import StudentTable from "@/components/students/StudentTable";
import StudentSearch from "@/components/students/StudentSearch";
import StudentFilters from "@/components/students/StudentFilters";

export default function StudentsPage() {
  const navigate = useNavigate();

  const {
    students,
    loading,
    error,
    refetch,
  } = useStudents();

  const [search, setSearch] = useState("");
  const [status, setStatus] =
    useState<StudentStatus | "all">("all");

  const [gender, setGender] = useState("");

  const filteredStudents = useMemo(() => {
    const searchValue = search
      .trim()
      .toLowerCase();

    return students.filter((student) => {
      const matchesSearch =
        !searchValue ||
        student.name
          .toLowerCase()
          .includes(searchValue) ||
        student.studentId
          .toLowerCase()
          .includes(searchValue) ||
        student.departmentName
          .toLowerCase()
          .includes(searchValue);

      const matchesStatus =
        status === "all" ||
        student.status === status;

      const matchesGender =
        !gender ||
        student.gender.toLowerCase() ===
          gender.toLowerCase();

      return (
        matchesSearch &&
        matchesStatus &&
        matchesGender
      );
    });
  }, [students, search, status, gender]);

  const handleStudentClick = (
    student: Student
  ) => {
    navigate(`/students/${student.sId}`);
  };

  if (loading) {
    return (
      <div className="p-6">
        <div className="flex min-h-[400px] items-center justify-center">
          <p className="text-sm text-muted-foreground">
            Loading students...
          </p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="p-6">
        <div className="rounded-xl border border-border p-8 text-center">
          <h2 className="font-semibold">
            Unable to load students
          </h2>

          <p className="mt-2 text-sm text-muted-foreground">
            {error}
          </p>

          <button
            type="button"
            onClick={refetch}
            className="mt-4 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-primary-foreground"
          >
            Try Again
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 p-6">
      <div>
        <h1 className="text-2xl font-semibold">
          Students
        </h1>

        <p className="mt-1 text-sm text-muted-foreground">
          View and search registered students.
        </p>
      </div>

      <div className="flex flex-col gap-4 lg:flex-row lg:items-center lg:justify-between">
        <StudentSearch
          value={search}
          onChange={setSearch}
        />

        <StudentFilters
          status={status}
          gender={gender}
          onStatusChange={setStatus}
          onGenderChange={setGender}
        />
      </div>

      <div className="flex items-center justify-between">
        <p className="text-sm text-muted-foreground">
          Showing{" "}
          <span className="font-medium text-foreground">
            {filteredStudents.length}
          </span>{" "}
          of{" "}
          <span className="font-medium text-foreground">
            {students.length}
          </span>{" "}
          students
        </p>
      </div>

      <StudentTable
        students={filteredStudents}
        onStudentClick={handleStudentClick}
      />
    </div>
  );
}