import type { Student } from "@/types/student";
import StudentStatusBadge from "./StudentStatusBadge";

interface StudentRowProps {
  student: Student;
  onClick?: (student: Student) => void;
}

export default function StudentRow({
  student,
  onClick,
}: StudentRowProps) {
  return (
    <tr
      onClick={() => onClick?.(student)}
      className={`border-b border-border transition-colors ${
        onClick
          ? "cursor-pointer hover:bg-muted/50"
          : ""
      }`}
    >
      <td className="px-4 py-4 text-sm font-medium">
        {student.studentId}
      </td>

      <td className="px-4 py-4 text-sm">
        {student.name}
      </td>

      <td className="px-4 py-4 text-sm">
        {student.departmentName}
      </td>

      <td className="px-4 py-4 text-sm">
        {student.gender}
      </td>

      <td className="px-4 py-4 text-sm">
        Year {student.yearOfStudy}
      </td>

      <td className="px-4 py-4">
        <StudentStatusBadge status={student.status} />
      </td>
    </tr>
  );
}