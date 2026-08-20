import { Search } from "lucide-react";

interface StudentSearchProps {
  value: string;
  onChange: (value: string) => void;
}

export default function StudentSearch({
  value,
  onChange,
}: StudentSearchProps) {
  return (
    <div className="relative w-full max-w-md">
      <Search
        className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground"
        size={18}
      />

      <input
        type="text"
        value={value}
        onChange={(event) => onChange(event.target.value)}
        placeholder="Search students..."
        className="h-10 w-full rounded-lg border border-border bg-background pl-10 pr-4 text-sm outline-none transition focus:border-primary"
      />
    </div>
  );
}