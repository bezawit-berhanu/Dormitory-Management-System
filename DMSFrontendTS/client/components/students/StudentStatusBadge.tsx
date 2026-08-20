import type { StudentStatus } from "@/types/student";

interface StudentStatusBadgeProps {
  status: StudentStatus;
}

const statusConfig = {
  1: {
    label: "Active",
    className:
      "bg-green-500/10 text-green-600 border border-green-500/20",
  },
  2: {
    label: "Inactive",
    className:
      "bg-gray-500/10 text-gray-600 border border-gray-500/20",
  },
  3: {
    label: "Graduated",
    className:
      "bg-purple-500/10 text-purple-600 border border-purple-500/20",
  },
  4: {
    label: "Suspended",
    className:
      "bg-red-500/10 text-red-600 border border-red-500/20",
  },
} as const;

export default function StudentStatusBadge({
  status,
}: StudentStatusBadgeProps) {
  const config =
    statusConfig[status] ?? {
      label: "Unknown",
      className:
        "bg-gray-500/10 text-gray-600 border border-gray-500/20",
    };

  return (
    <span
      className={`inline-flex items-center rounded-full px-2.5 py-1 text-xs font-medium ${config.className}`}
    >
      {config.label}
    </span>
  );
}