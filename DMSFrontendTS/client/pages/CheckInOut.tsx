import { useEffect, useMemo, useState, type FormEvent, type ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import {
  AlertTriangle,
  ArrowLeft,
  CalendarCheck,
  CalendarClock,
  DoorOpen,
  LogIn,
  LogOut,
  RefreshCw,
  Search,
  ShieldX,
  UserRound,
  X,
} from "lucide-react";
import { cn } from "@/lib/utils";
import {
  dmsApi,
  type CheckInHistory,
  type CheckOutRecord,
  type DmsStudent,
  type RoomAssignmentRecord,
} from "@/lib/api";
import {
  formatDateTime,
  roleFromStoredUser,
  storedUserId,
  storedUser,
  text,
  toLocalInputValue,
} from "@/lib/dms-helpers";

type Status = "in" | "out" | "none";

function Modal({
  title,
  subtitle,
  onClose,
  children,
}: {
  title: string;
  subtitle?: string;
  onClose: () => void;
  children: ReactNode;
}) {
  return (
    <div
      className="fixed inset-0 z-50 flex items-end justify-center bg-[#062b25]/75 p-0 backdrop-blur-sm sm:items-center sm:p-6"
      onMouseDown={(event) => {
        if (event.target === event.currentTarget) onClose();
      }}
    >
      <div className="max-h-[92vh] w-full max-w-xl overflow-y-auto rounded-t-3xl border border-[#dfe8e1] bg-[#f8f7f2] p-6 text-[#183d34] shadow-2xl sm:rounded-3xl sm:p-8">
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

function StatusPill({ value, tone }: { value: string; tone?: "in" | "out" | "muted" }) {
  const color =
    tone === "in"
      ? "bg-emerald-100 text-emerald-800"
      : tone === "out"
        ? "bg-slate-100 text-slate-600"
        : "bg-amber-100 text-amber-800";
  return (
    <span
      className={cn(
        "inline-flex items-center gap-1.5 rounded-full px-2.5 py-1 text-[10px] font-bold",
        color,
      )}
    >
      <span className="h-1.5 w-1.5 rounded-full bg-current opacity-60" />
      {value || "Not set"}
    </span>
  );
}

function EmptyState({
  icon: Icon,
  title,
  body,
}: {
  icon: typeof DoorOpen;
  title: string;
  body: string;
}) {
  return (
    <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-white/10 bg-white/[0.025] px-6 py-16 text-center">
      <div className="mb-4 flex h-12 w-12 items-center justify-center rounded-2xl bg-emerald-300/10 text-emerald-300">
        <Icon className="h-5 w-5" />
      </div>
      <h3 className="font-display text-lg font-semibold text-white">{title}</h3>
      <p className="mt-2 max-w-sm text-sm leading-6 text-emerald-50/45">{body}</p>
    </div>
  );
}

function LoadingState() {
  return (
    <div className="flex items-center justify-center rounded-2xl border border-white/[0.08] bg-white/[0.035] py-20 text-sm text-emerald-50/45">
      Loading live records…
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

export default function CheckInOut() {
  const navigate = useNavigate();
  const [role] = useState(() => roleFromStoredUser());
  const canManage = role === "admin" || role === "dorm manager" || role === "proctor";

  const [students, setStudents] = useState<DmsStudent[]>([]);
  const [selectedSId, setSelectedSId] = useState("");
  const [ownSId, setOwnSId] = useState("");
  const [ownStudent, setOwnStudent] = useState<DmsStudent | null>(null);

  const [checkIns, setCheckIns] = useState<CheckInHistory[]>([]);
  const [checkOuts, setCheckOuts] = useState<CheckOutRecord[]>([]);
  const [assignments, setAssignments] = useState<RoomAssignmentRecord[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [modal, setModal] = useState<null | "checkin" | "checkout">(null);

  useEffect(() => {
    if (!window.sessionStorage.getItem("dwell_access_token"))
      navigate("/", { replace: true });
  }, [navigate]);

  useEffect(() => {
    if (role === "student" || canManage) {
      void dmsApi.students
        .list<DmsStudent>()
        .then((list) => {
          setStudents(list);
          if (role === "student") {
            const me = storedUser();
            const email = text(me, "email", "Email").toLowerCase();
            const identifier = (
              window.sessionStorage.getItem("dwell_identifier") ?? ""
            ).toLowerCase();
            const match =
              list.find(
                (s) =>
                  (email && text(s, "email", "Email").toLowerCase() === email) ||
                  text(s, "studentId", "StudentId").toLowerCase() === identifier,
              ) ?? null;
            if (match) {
              setOwnStudent(match);
              setOwnSId(text(match, "sId", "SId", "id", "Id"));
            }
          }
        })
        .catch(() => setStudents([]));
    }
  }, [role, canManage]);

  const targetSId = role === "student" ? ownSId : selectedSId;

  const loadData = async (sId: string) => {
    if (!sId) return;
    setLoading(true);
    setError("");
    try {
      const [ci, co, ra] = await Promise.all([
        dmsApi.checkIn.history<CheckInHistory>(sId),
        dmsApi.checkOut.history<CheckOutRecord>(sId),
        dmsApi.roomAssignment.student<RoomAssignmentRecord>(sId),
      ]);
      setCheckIns(ci);
      setCheckOuts(co);
      setAssignments(ra);
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load check-in / check-out records.",
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (targetSId) void loadData(targetSId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [targetSId]);

  const currentStatus: Status = useMemo(() => {
    if (!checkIns.length) return "none";
    const latest = [...checkIns].sort(
      (a, b) =>
        new Date(text(b, "checkInDate", "CheckInDate")).getTime() -
        new Date(text(a, "checkInDate", "CheckInDate")).getTime(),
    )[0];
    const checkedOut = text(latest, "checkOutDate", "CheckOutDate");
    return checkedOut ? "out" : "in";
  }, [checkIns]);

  const timeline = useMemo(() => {
    const rows: Array<{
      key: string;
      type: "Check-in" | "Check-out";
      date: string;
      room: string;
      dormitory: string;
      status: string;
      reason: string;
      by: string;
    }> = [];
    checkIns.forEach((ci) => {
      rows.push({
        key: `ci-${text(ci, "checkInId", "CheckInId")}`,
        type: "Check-in",
        date: text(ci, "checkInDate", "CheckInDate"),
        room: text(ci, "roomNumber", "RoomNumber"),
        dormitory: text(ci, "dormitoryName", "DormitoryName"),
        status: text(ci, "status", "Status"),
        reason: "",
        by: text(ci, "checkedInBy", "CheckedInBy"),
      });
    });
    checkOuts.forEach((co) => {
      rows.push({
        key: `co-${text(co, "checkOutId", "CheckOutId")}`,
        type: "Check-out",
        date: text(co, "checkOutDate", "CheckOutDate"),
        room: "",
        dormitory: "",
        status: "Checked out",
        reason: text(co, "reason", "Reason"),
        by: "",
      });
    });
    return rows.sort(
      (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime(),
    );
  }, [checkIns, checkOuts]);

  const defaultAssignmentId = assignments.length
    ? String(text(assignments[0], "roomAssignmentId", "RoomAssignmentId"))
    : "";

  const submitCheckIn = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError("");
    const data = new FormData(event.currentTarget);
    const payload = {
      StudentId: Number(targetSId),
      RoomAssignmentId: Number(data.get("roomAssignmentId")),
      CheckInDate: new Date(String(data.get("checkInDate"))).toISOString(),
      CheckedInByUserId: Number(storedUserId() || 1),
    };
    try {
      await dmsApi.checkIn.create(payload);
      setModal(null);
      await loadData(targetSId);
    } catch (requestError) {
      setError(
        requestError instanceof Error ? requestError.message : "Check-in failed.",
      );
    }
  };

  const submitCheckOut = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError("");
    const data = new FormData(event.currentTarget);
    const payload = {
      CheckOutId: 0,
      RoomAssignmentId: Number(data.get("roomAssignmentId")),
      CheckOutDate: new Date(String(data.get("checkOutDate"))).toISOString(),
      Reason: String(data.get("reason") ?? ""),
    };
    try {
      await dmsApi.checkOut.create(payload);
      setModal(null);
      await loadData(targetSId);
    } catch (requestError) {
      setError(
        requestError instanceof Error ? requestError.message : "Check-out failed.",
      );
    }
  };

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
            Maintenance staff do not have access to check-in, check-out, or room
            assignment records.
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

  return (
    <main className="min-h-screen bg-[#062b25] px-5 py-7 text-white sm:px-8 sm:py-9">
      <div className="mx-auto max-w-[1200px]">
        <div className="mb-7 flex items-end justify-between gap-4">
          <div>
            <p className="mb-1.5 text-[10px] font-bold uppercase tracking-[0.2em] text-emerald-300/55">
              Residence movement
            </p>
            <h1 className="font-display text-3xl font-semibold tracking-[-0.06em]">
              Check-in &amp; check-out
            </h1>
            <p className="mt-2 max-w-2xl text-sm text-emerald-50/45">
              {canManage
                ? "Record arrivals and departures for assigned residents, and review their movement history."
                : "Review your check-in and check-out movement history."}
            </p>
          </div>
          {targetSId && (
            <button
              type="button"
              onClick={() => loadData(targetSId)}
              className="hidden items-center gap-2 rounded-xl border border-white/10 bg-white/[0.06] px-4 py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10 sm:flex"
            >
              <RefreshCw className="h-4 w-4" />
              Refresh
            </button>
          )}
        </div>

        {error && <ApiNotice message={error} />}

        {canManage && (
          <div className="mb-6 rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5">
            <label className="mb-2 block text-[10px] font-bold uppercase tracking-[0.18em] text-emerald-300/55">
              Select resident
            </label>
            <div className="relative">
              <Search className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-emerald-50/25" />
              <select
                value={selectedSId}
                onChange={(event) => setSelectedSId(event.target.value)}
                className="field-dark w-full pl-10"
              >
                <option value="">Choose a student…</option>
                {students.map((s) => (
                  <option key={text(s, "sId", "SId", "id", "Id")} value={text(s, "sId", "SId", "id", "Id")}>
                    {studentDisplayName(s)} · {text(s, "studentId", "StudentId")}
                  </option>
                ))}
              </select>
            </div>
          </div>
        )}

        {!targetSId ? (
          <EmptyState
            icon={UserRound}
            title={role === "student" ? "No resident profile found" : "Select a resident"}
            body={
              role === "student"
                ? "We could not match your account to a student record. Confirm you signed in with your registered email."
                : "Pick a resident from the list above to view and manage their check-in / check-out activity."
            }
          />
        ) : loading ? (
          <LoadingState />
        ) : (
          <div className="space-y-6">
            <div className="rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5">
              <div className="flex flex-col gap-5 sm:flex-row sm:items-center sm:justify-between">
                <div className="flex items-center gap-4">
                  <div className="flex h-12 w-12 items-center justify-center rounded-full bg-emerald-300/10 text-emerald-300">
                    <UserRound className="h-5 w-5" />
                  </div>
                  <div>
                    <h2 className="font-display text-lg font-semibold text-white">
                      {role === "student"
                        ? text(storedUser(), "fullName", "name", "Name") || "Your residence"
                        : studentDisplayName(
                            students.find(
                              (s) =>
                                text(s, "sId", "SId", "id", "Id") === targetSId,
                            ) ?? ownStudent ?? {},
                          )}
                    </h2>
                    <p className="mt-0.5 text-xs text-emerald-50/40">
                      Student ID {text(ownStudent, "studentId", "StudentId") || targetSId}
                    </p>
                  </div>
                </div>
                <div className="flex items-center gap-4">
                  <div className="flex items-center gap-2">
                    <span className="text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/35">
                      Status
                    </span>
                    <StatusPill
                      value={
                        currentStatus === "in"
                          ? "Checked in"
                          : currentStatus === "out"
                            ? "Checked out"
                            : "Not checked in"
                      }
                      tone={currentStatus === "in" ? "in" : currentStatus === "out" ? "out" : "muted"}
                    />
                  </div>
                  {(canManage || role === "student") && (
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() => setModal("checkin")}
                        className="inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-3.5 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
                      >
                        <LogIn className="h-4 w-4" />
                        Check in
                      </button>
                      <button
                        type="button"
                        onClick={() => setModal("checkout")}
                        className="inline-flex items-center gap-2 rounded-xl border border-white/15 bg-white/[0.06] px-3.5 py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10"
                      >
                        <LogOut className="h-4 w-4" />
                        Check out
                      </button>
                    </div>
                  )}
                </div>
              </div>
            </div>

            <div className="overflow-hidden rounded-2xl border border-white/[0.08] bg-white/[0.035]">
              <div className="flex items-center justify-between border-b border-white/[0.08] p-4">
                <div>
                  <h2 className="font-display text-lg font-semibold text-white">
                    Movement history
                  </h2>
                  <p className="mt-1 text-xs text-emerald-50/40">
                    {timeline.length} record{timeline.length === 1 ? "" : "s"} returned by
                    the API
                  </p>
                </div>
              </div>
              {timeline.length ? (
                <div className="overflow-x-auto">
                  <table className="w-full min-w-[720px] text-left">
                    <thead className="bg-white/[0.025] text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/30">
                      <tr>
                        <th className="px-5 py-3 font-semibold">Type</th>
                        <th className="px-5 py-3 font-semibold">Date</th>
                        <th className="px-5 py-3 font-semibold">Room</th>
                        <th className="px-5 py-3 font-semibold">Dormitory</th>
                        <th className="px-5 py-3 font-semibold">Status</th>
                        <th className="px-5 py-3 font-semibold">Reason</th>
                        <th className="px-5 py-3 font-semibold">Recorded by</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-white/[0.06]">
                      {timeline.map((row) => (
                        <tr key={row.key} className="group hover:bg-white/[0.03]">
                          <td className="px-5 py-4 text-xs">
                            <span
                              className={cn(
                                "inline-flex items-center gap-1.5 rounded-full px-2.5 py-1 text-[10px] font-bold",
                                row.type === "Check-in"
                                  ? "bg-emerald-300/10 text-emerald-300"
                                  : "bg-rose-300/10 text-rose-200",
                              )}
                            >
                              {row.type === "Check-in" ? (
                                <LogIn className="h-3 w-3" />
                              ) : (
                                <LogOut className="h-3 w-3" />
                              )}
                              {row.type}
                            </span>
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {formatDateTime(row.date)}
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {row.room || "—"}
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {row.dormitory || "—"}
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {row.status ? <StatusPill value={row.status} /> : "—"}
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {row.reason || "—"}
                          </td>
                          <td className="px-5 py-4 text-xs text-emerald-50/60">
                            {row.by || "—"}
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              ) : (
                <div className="px-5 py-16 text-center text-sm text-emerald-50/40">
                  No movement records returned for this resident yet.
                </div>
              )}
            </div>
          </div>
        )}
      </div>

      {modal === "checkin" && (
        <Modal
          title="Check in resident"
          subtitle="Record an arrival for the selected resident."
          onClose={() => setModal(null)}
        >
          <form onSubmit={submitCheckIn} className="space-y-4">
            {assignments.length ? (
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room assignment
                </span>
                <select name="roomAssignmentId" className="field" required defaultValue={defaultAssignmentId}>
                  {assignments.map((a) => (
                    <option key={text(a, "roomAssignmentId", "RoomAssignmentId")} value={text(a, "roomAssignmentId", "RoomAssignmentId")}>
                      Room {text(a, "roomId", "RoomId")} · Bed {text(a, "bedId", "BedId")} (
                      {text(a, "status", "Status")})
                    </option>
                  ))}
                </select>
              </label>
            ) : (
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room assignment ID
                </span>
                <input name="roomAssignmentId" className="field" type="number" required placeholder="Assignment ID from API" />
              </label>
            )}
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Check-in date
              </span>
              <input name="checkInDate" className="field" type="datetime-local" required defaultValue={toLocalInputValue()} />
            </label>
            <div className="flex justify-end gap-2 pt-3">
              <button
                type="button"
                onClick={() => setModal(null)}
                className="rounded-xl border border-[#d5e0d8] px-4 py-2.5 text-xs font-semibold text-[#5a7468]"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="inline-flex items-center gap-2 rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white"
              >
                <CalendarCheck className="h-4 w-4" />
                Confirm check-in
              </button>
            </div>
          </form>
        </Modal>
      )}

      {modal === "checkout" && (
        <Modal
          title="Check out resident"
          subtitle="Record a departure for the selected resident."
          onClose={() => setModal(null)}
        >
          <form onSubmit={submitCheckOut} className="space-y-4">
            {assignments.length ? (
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room assignment
                </span>
                <select name="roomAssignmentId" className="field" required defaultValue={defaultAssignmentId}>
                  {assignments.map((a) => (
                    <option key={text(a, "roomAssignmentId", "RoomAssignmentId")} value={text(a, "roomAssignmentId", "RoomAssignmentId")}>
                      Room {text(a, "roomId", "RoomId")} · Bed {text(a, "bedId", "BedId")} (
                      {text(a, "status", "Status")})
                    </option>
                  ))}
                </select>
              </label>
            ) : (
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room assignment ID
                </span>
                <input name="roomAssignmentId" className="field" type="number" required placeholder="Assignment ID from API" />
              </label>
            )}
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Check-out date
              </span>
              <input name="checkOutDate" className="field" type="datetime-local" required defaultValue={toLocalInputValue()} />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Reason
              </span>
              <textarea
                name="reason"
                className="field min-h-24 resize-none"
                placeholder="Optional reason for departure"
              />
            </label>
            <div className="flex justify-end gap-2 pt-3">
              <button
                type="button"
                onClick={() => setModal(null)}
                className="rounded-xl border border-[#d5e0d8] px-4 py-2.5 text-xs font-semibold text-[#5a7468]"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="inline-flex items-center gap-2 rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white"
              >
                <CalendarClock className="h-4 w-4" />
                Confirm check-out
              </button>
            </div>
          </form>
        </Modal>
      )}
    </main>
  );
}
