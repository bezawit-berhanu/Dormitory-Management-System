import type { Student } from "@/types/student";
import StudentRow from "./StudentRow";

interface StudentTableProps {
  students: Student[];
  onStudentClick?: (student: Student) => void;
}

export default function StudentTable({
  students,
  onStudentClick,
}: StudentTableProps) {
  if (students.length === 0) {
    return (
      <div className="flex min-h-[300px] items-center justify-center rounded-xl border border-border">
        <div className="text-center">
          <p className="text-sm font-medium">
            No students found
          </p>

          <p className="mt-1 text-sm text-muted-foreground">
            There are no students matching your search.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="overflow-hidden rounded-xl border border-border">
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead className="bg-muted/40">
            <tr className="border-b border-border">
              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Student ID
              </th>

              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Name
              </th>

              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Department
              </th>

              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Gender
              </th>

              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Year
              </th>

              <th className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wide">
                Status
              </th>
            </tr>
          </thead>

          <tbody>
            {students.map((student) => (
              <StudentRow
                key={student.sId}
                student={student}
                onClick={onStudentClick}
              />
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}