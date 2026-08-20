import type { Student } from "@/types/student";
import StudentStatusBadge from "./StudentStatusBadge";

interface StudentDetailsProps {
  student: Student;
}

export default function StudentDetails({
  student,
}: StudentDetailsProps) {
  return (
    <div className="rounded-xl border border-border p-6">
      <div className="mb-6 flex items-center justify-between">
        <div>
          <h2 className="text-xl font-semibold">
            {student.name}
          </h2>

          <p className="mt-1 text-sm text-muted-foreground">
            {student.studentId}
          </p>
        </div>

        <StudentStatusBadge status={student.status} />
      </div>

      <div className="grid gap-5 sm:grid-cols-2">
        <div>
          <p className="text-xs font-medium text-muted-foreground">
            Department
          </p>
          <p className="mt-1 text-sm">
            {student.departmentName}
          </p>
        </div>

        <div>
          <p className="text-xs font-medium text-muted-foreground">
            Gender
          </p>
          <p className="mt-1 text-sm">
            {student.gender}
          </p>
        </div>

        <div>
          <p className="text-xs font-medium text-muted-foreground">
            Year of Study
          </p>
          <p className="mt-1 text-sm">
            Year {student.yearOfStudy}
          </p>
        </div>

        <div>
          <p className="text-xs font-medium text-muted-foreground">
            Date of Birth
          </p>
          <p className="mt-1 text-sm">
            {new Date(
              student.dateOfBirth
            ).toLocaleDateString()}
          </p>
        </div>

        <div>
          <p className="text-xs font-medium text-muted-foreground">
            Emergency Contact
          </p>
          <p className="mt-1 text-sm">
            {student.emergencyContactNumber || "Not provided"}
          </p>
        </div>
      </div>
    </div>
  );
}