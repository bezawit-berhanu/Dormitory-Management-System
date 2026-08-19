import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  AlertTriangle,
  ArrowLeft,
  Build,
  Plus,
  RefreshCw,
  Search,
  ShieldX,
  UserRound,
  UsersRound,
  X,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { dmsApi, type DmsStudent, type RoomAssignmentRecord } from "@/lib/api";
import {
  formatDateTime,
  roleFromStoredUser,
  storedCampus,
  text,
} from "@/lib/dms-helpers";

const campusLabels: Record<string, string> = {
  "4kilo": "4 Kilos",
  "6kilo": "6 Kilos",
};

function Modal({
  title,
  subtitle,
  onClose,
  children,
}: {
  title: string;
  subtitle?: string;
  onClose: () => void;
  children: React.ReactNode;
}) {
  return (
    <div
      className="fixed inset-0 z-50 flex items-end justify-center bg-[#062b25]/75 p-0 backdrop-blur-sm sm:items-center sm:p-6"
      onMouseDown={(event) => {
        if (event.target === event.currentTarget) onClose();
      }}
    >
      <div className="max-h-[92vh] w-full max-w-2xl overflow-y-auto rounded-t-3xl border border-[#dfe8e1] bg-[#f8f7f2] p-6 text-[#183d34] shadow-2xl sm:rounded-3xl sm:p-8">
        <div className="mb-6 flex items-start justify-between gap-6">
          <div>
            <p className="mb-1 text-[10px] font-bold uppercase tracking-[0.2em] text-[#82968d]">
              Residence system
            </p>
            <h2 className="font-display text-2xl font-semibold tracking-[-0.05em]">
              {title}
            </h2>
            {subtitle && (
              <p className="mt-2 text-sm text-[#6a7e74]">{subtitle}</p>
            )}
          </div>
          <button
            type="button"
            onClick={onClose}
            className="rounded-xl p-2 text-[#7c9187] transition hover:bg-[#eaf0eb] hover:text-[#183d34]"
          >
            <X className="h-5 w-5" />
          </button>
        </div>
        {children}
      </div>
    </div>
  );
}

function ApiNotice({ message }: { message: string }) {
  return (
    <div className="flex items-start gap-3 rounded-xl border border-amber-200/20 bg-amber-300/[0.07] px-4 py-3 text-xs leading-5 text-amber-100">
      <AlertTriangle className="mt-0.5 h-4 w-4 shrink-0 text-amber-200" />
      <span>
        {message}. Check that the DMS API and its registrar service are running.
      </span>
    </div>
  );
}

function studentDisplayName(student: DmsStudent): string {
  return (
    text(student, "name", "Name", "fullName") ||
    `Student ${text(student, "studentId", "StudentId") || text(student, "sId", "SId")}`
  );
}

export default function Students() {
  const navigate = useNavigate();
  const [role] = useState(() => roleFromStoredUser());
  const canManage = role === "admin" || role === "dorm manager" || role === "proctor";
  const isProctor = role === "proctor";
  const isStaffManager = role === "dorm manager";

  const [students, setStudents] = useState<DmsStudent[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [query, setQuery] = useState("");
  const [assignedIds, setAssignedIds] = useState<Set<string>>(new Set());
  const [detail, setDetail] = useState<DmsStudent | null>(null);

  const campus = isStaffManager ? storedCampus() : "";

  useEffect(() => {
    if (!window.sessionStorage.getItem("dwell_access_token"))
      navigate("/", { replace: true });
  }, [navigate]);

  useEffect(() => {
    if (!canManage && role !== "student") return;
    void load();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [role]);

  const load = async () => {
    setLoading(true);
    setError("");
    try {
      const list = await dmsApi.students.list<DmsStudent>();
      setStudents(list);
      if (isProctor) {
        const ids = await Promise.all(
          list.map(async (s) => {
            const sid = text(s, "sId", "SId", "id", "Id");
            if (!sid) return null;
            try {
              const assignments = await dmsApi.roomAssignment.student<RoomAssignmentRecord>(sid);
              return assignments.length ? sid : null;
            } catch {
              return null;
            }
          }),
        );
        setAssignedIds(new Set(ids.filter(Boolean) as string[]));
      }
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load students from the API.",
      );
    } finally {
      setLoading(false);
    }
  };

  const visible = useMemo(() => {
    let list = students;
    if (isProctor) {
      list = list.filter((s) =>
        assignedIds.has(text(s, "sId", "SId", "id", "Id")),
      );
    } else if (isStaffManager && campus) {
      const label = campusLabels[campus] ?? campus;
      list = list.filter(
        (s) =>
          text(s, "campus", "Campus", "campusName").toLowerCase() ===
          label.toLowerCase(),
      );
    }
    const q = query.trim().toLowerCase();
    if (q)
      list = list.filter((s) =>
        studentDisplayName(s).toLowerCase().includes(q) ||
        text(s, "studentId", "StudentId").toLowerCase().includes(q),
      );
    return list;
  }, [students, isProctor, isStaffManager, campus, assignedIds, query]);

  if (role === "maintenance") {
    return (
      <main className="min-h-screen bg-[#062b25] px-5 py-10 text-white sm:px-8">
        <div className="mx-auto max-w-2xl rounded-3xl border border-white/[0.08] bg-white/[0.035] p-8">
          <div className="flex h-12 w-12 items-center justify-center rounded-2xl bg-rose-300/10 text-rose-300">
            <ShieldX className="h-6 w-6" />
          </div>
          <h1 className="mt-5 font-display text-2xl font-semibold tracking-[-0.04em]">
            Access restricted
          </h1>
          <p className="mt-2 text-sm text-emerald-50/55">
            Maintenance staff do not have access to the student directory.
          </p>
          <button
            type="button"
            onClick={() => navigate("/dashboard")}
            className="mt-6 inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
          >
            <ArrowLeft className="h-4 w-4" />
            Back to dashboard
          </button>
        </div>
      </main>
    );
  }

  const eyebrow =
    role === "admin"
      ? "Resident directory"
      : isStaffManager
        ? `Residents · ${campusLabels[campus] ?? campus || "All campuses"}`
        : isProctor
          ? "Residents with dormitory"
          : "My residence";

  return (
    <main className="min-h-screen bg-[#062b25] px-5 py-7 text-white sm:px-8 sm:py-9">
      <div className="mx-auto max-w-[1200px]">
        <div className="mb-7 flex items-end justify-between gap-4">
          <div>
            <p className="mb-1.5 text-[10px] font-bold uppercase tracking-[0.2em] text-emerald-300/55">
              {eyebrow}
            </p>
            <h1 className="font-display text-3xl font-semibold tracking-[-0.06em]">
              Students
            </h1>
            <p className="mt-2 max-w-2xl text-sm text-emerald-50/45">
              {role === "admin"
                ? "All residents sourced from the registrar service."
                : isStaffManager
                  ? "Residents assigned to your campus."
                  : isProctor
                    ? "Residents who already have a dormitory allocation."
                    : "Your residence record."}
            </p>
          </div>
          <div className="flex items-center gap-2">
            {isProctor && (
              <button
                type="button"
                onClick={() => navigate("/assign-student")}
                className="inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
              >
                <Plus className="h-4 w-4" />
                Assign student
              </button>
            )}
            <button
              type="button"
              onClick={() => load()}
              className="inline-flex items-center gap-2 rounded-xl border border-white/10 bg-white/[0.06] px-4 py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10"
            >
              <RefreshCw className="h-4 w-4" />
              Refresh
            </button>
          </div>
        </div>

        {error && <ApiNotice message={error} />}

        <div className="mb-5 flex gap-2 overflow-x-auto rounded-xl border border-white/[0.08] bg-white/[0.035] p-1">
          <div className="relative flex-1">
            <Search className="pointer-events-none absolute left-3.5 top-3 h-4 w-4 text-emerald-50/25" />
            <input
              value={query}
              onChange={(event) => setQuery(event.target.value)}
              placeholder="Search by name or student ID"
              className="field-dark w-full pl-10"
            />
          </div>
        </div>

        {loading ? (
          <div className="flex items-center justify-center rounded-2xl border border-white/[0.08] bg-white/[0.035] py-20 text-sm text-emerald-50/45">
            Loading residents…
          </div>
        ) : visible.length ? (
          <div className="overflow-hidden rounded-2xl border border-white/[0.08] bg-white/[0.035]">
            <div className="flex items-center justify-between border-b border-white/[0.08] p-4">
              <div>
                <h2 className="font-display text-lg font-semibold text-white">
                  Resident list
                </h2>
                <p className="mt-1 text-xs text-emerald-50/40">
                  {visible.length} resident{visible.length === 1 ? "" : "s"} returned
                  by the API
                </p>
              </div>
            </div>
            <div className="overflow-x-auto">
              <table className="w-full min-w-[640px] text-left">
                <thead className="bg-white/[0.025] text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/30">
                  <tr>
                    <th className="px-5 py-3 font-semibold">Name</th>
                    <th className="px-5 py-3 font-semibold">Student ID</th>
                    <th className="px-5 py-3 font-semibold">Campus</th>
                    <th className="px-5 py-3 font-semibold">Year</th>
                    <th className="px-5 py-3 font-semibold">Status</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-white/[0.06]">
                  {visible.map((s) => {
                    const sid = text(s, "sId", "SId", "id", "Id");
                    return (
                      <tr key={sid} className="group hover:bg-white/[0.03]">
                        <td className="px-5 py-4 text-xs font-semibold text-white">
                          {studentDisplayName(s)}
                        </td>
                        <td className="px-5 py-4 text-xs text-emerald-50/60">
                          {text(s, "studentId", "StudentId") || "—"}
                        </td>
                        <td className="px-5 py-4 text-xs text-emerald-50/60">
                          {text(s, "campus", "Campus", "campusName") || "—"}
                        </td>
                        <td className="px-5 py-4 text-xs text-emerald-50/60">
                          {text(s, "yearOfStudy", "YearOfStudy") || "—"}
                        </td>
                        <td className="px-5 py-4 text-xs text-emerald-50/60">
                          {text(s, "status", "Status") || "—"}
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
        ) : (
          <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-white/10 bg-white/[0.025] px-6 py-16 text-center">
            <div className="mb-4 flex h-12 w-12 items-center justify-center rounded-2xl bg-emerald-300/10 text-emerald-300">
              <UsersRound className="h-5 w-5" />
            </div>
            <h3 className="font-display text-lg font-semibold text-white">
              No residents found
            </h3>
            <p className="mt-2 max-w-sm text-sm leading-6 text-emerald-50/45">
              {isProctor
                ? "No residents with an active dormitory allocation were returned."
                : "No residents matched the current filters."}
            </p>
          </div>
        )}
      </div>

      {detail && (
        <Modal
          title={studentDisplayName(detail)}
          subtitle={text(detail, "studentId", "StudentId")}
          onClose={() => setDetail(null)}
        >
          <dl className="space-y-3 text-sm">
            <div className="flex justify-between border-b border-[#e3e9e4] pb-2">
              <dt className="text-[#6a7e74]">Student ID</dt>
              <dd className="font-semibold text-[#183d34]">
                {text(detail, "studentId", "StudentId") || "—"}
              </dd>
            </div>
            <div className="flex justify-between border-b border-[#e3e9e4] pb-2">
              <dt className="text-[#6a7e74]">Campus</dt>
              <dd className="font-semibold text-[#183d34]">
                {text(detail, "campus", "Campus", "campusName") || "—"}
              </dd>
            </div>
            <div className="flex justify-between border-b border-[#e3e9e4] pb-2">
              <dt className="text-[#6a7e74]">Year of study</dt>
              <dd className="font-semibold text-[#183d34]">
                {text(detail, "yearOfStudy", "YearOfStudy") || "—"}
              </dd>
            </div>
            <div className="flex justify-between">
              <dt className="text-[#6a7e74]">Status</dt>
              <dd className="font-semibold text-[#183d34]">
                {text(detail, "status", "Status") || "—"}
              </dd>
            </div>
          </dl>
          <div className="mt-6 flex justify-end">
            <button
              type="button"
              onClick={() => setDetail(null)}
              className="rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white"
            >
              Close
            </button>
          </div>
        </Modal>
      )}
    </main>
  );
}
