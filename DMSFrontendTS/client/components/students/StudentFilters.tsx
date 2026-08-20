import type { StudentStatus } from "@/types/student";

interface StudentFiltersProps {
  status: StudentStatus | "all";
  gender: string;
  onStatusChange: (
    status: StudentStatus | "all"
  ) => void;
  onGenderChange: (gender: string) => void;
}

export default function StudentFilters({
  status,
  gender,
  onStatusChange,
  onGenderChange,
}: StudentFiltersProps) {
  return (
    <div className="flex flex-wrap gap-3">
      <select
        value={status}
        onChange={(event) =>
          onStatusChange(
            event.target.value === "all"
              ? "all"
              : Number(event.target.value) as StudentStatus
          )
        }
        className="h-10 rounded-lg border border-border bg-background px-3 text-sm outline-none"
      >
        <option value="all">All statuses</option>
        <option value={1}>Active</option>
        <option value={2}>Inactive</option>
        <option value={3}>Graduated</option>
        <option value={4}>Suspended</option>
      </select>

      <select
        value={gender}
        onChange={(event) =>
          onGenderChange(event.target.value)
        }
        className="h-10 rounded-lg border border-border bg-background px-3 text-sm outline-none"
      >
        <option value="">All genders</option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
      </select>
    </div>
  );
}