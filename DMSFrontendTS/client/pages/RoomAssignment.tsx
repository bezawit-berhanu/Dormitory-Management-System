import { useEffect, useMemo, useState, type FormEvent, type ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import {
  AlertTriangle,
  ArrowLeft,
  BedDouble,
  DoorOpen,
  Pencil,
  Plus,
  RefreshCw,
  Search,
  ShieldX,
  Trash2,
  UserRound,
  UsersRound,
  X,
} from "lucide-react";
import { cn } from "@/lib/utils";
import {
  dmsApi,
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

function StatusPill({ value }: { value: string }) {
  const normalized = value.toLowerCase();
  const color =
    normalized.includes("active") || normalized.includes("assigned")
      ? "bg-emerald-100 text-emerald-800"
      : normalized.includes("pending") || normalized.includes("hold")
        ? "bg-amber-100 text-amber-800"
        : normalized.includes("inactive") || normalized.includes("ended")
          ? "bg-slate-100 text-slate-600"
          : "bg-sky-100 text-sky-800";
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

export default function RoomAssignmentPage() {
  const navigate = useNavigate();
  const [role] = useState(() => roleFromStoredUser());
  const canManage = role === "admin" || role === "dorm manager" || role === "proctor";

  const [students, setStudents] = useState<DmsStudent[]>([]);
  const [selectedSId, setSelectedSId] = useState("");
  const [ownSId, setOwnSId] = useState("");
  const [ownStudent, setOwnStudent] = useState<DmsStudent | null>(null);

  const [assignments, setAssignments] = useState<RoomAssignmentRecord[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [modal, setModal] = useState<null | "create" | { edit: RoomAssignmentRecord }>(
    null,
  );

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
      setAssignments(await dmsApi.roomAssignment.student<RoomAssignmentRecord>(sId));
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load room assignments.",
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (targetSId) void loadData(targetSId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [targetSId]);

  const selectedStudent = useMemo(
    () =>
      students.find((s) => text(s, "sId", "SId", "id", "Id") === targetSId) ??
      ownStudent ??
      null,
    [students, targetSId, ownStudent],
  );

  const remove = async (record: RoomAssignmentRecord) => {
    const id = text(record, "roomAssignmentId", "RoomAssignmentId");
    if (!id || !window.confirm("Delete this room assignment? This cannot be undone."))
      return;
    try {
      await dmsApi.roomAssignment.remove(id);
      await loadData(targetSId);
    } catch (requestError) {
      window.alert(
        requestError instanceof Error
          ? requestError.message
          : "Unable to delete the assignment.",
      );
    }
  };

  const submitAssignment = async (
    event: FormEvent<HTMLFormElement>,
    existing?: RoomAssignmentRecord,
  ) => {
    event.preventDefault();
    setError("");
    const data = new FormData(event.currentTarget);
    const id = text(existing ?? {}, "roomAssignmentId", "RoomAssignmentId");
    const payload = {
      RoomAssignmentId: existing ? Number(id) : 0,
      StudentId: String(data.get("studentId") ?? text(selectedStudent, "studentId", "StudentId")),
      RoomId: Number(data.get("roomId")),
      BedId: Number(data.get("bedId")),
      StudentName: String(data.get("studentName") ?? studentDisplayName(selectedStudent ?? {})),
      AssignedDate: new Date(String(data.get("assignedDate") || new Date())).toISOString(),
      AssignedByUserId: Number(storedUserId() || 1),
      Status: String(data.get("status") ?? "Active"),
    };
    try {
      if (existing) await dmsApi.roomAssignment.update(id, payload);
      else await dmsApi.roomAssignment.create(payload);
      setModal(null);
      await loadData(targetSId);
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to save the room assignment.",
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
            Maintenance staff do not have access to room assignment records.
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
              Accommodation
            </p>
            <h1 className="font-display text-3xl font-semibold tracking-[-0.06em]">
              Room assignments
            </h1>
            <p className="mt-2 max-w-2xl text-sm text-emerald-50/45">
              {canManage
                ? "Assign residents to rooms and beds, and manage their allocation status."
                : "Review your room and bed assignment."}
            </p>
          </div>
          {targetSId && (
            <div className="flex items-center gap-2">
              {canManage && (
                <button
                  type="button"
                  onClick={() => setModal("create")}
                  className="inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
                >
                  <Plus className="h-4 w-4" />
                  Assign resident
                </button>
              )}
              <button
                type="button"
                onClick={() => loadData(targetSId)}
                className="inline-flex items-center gap-2 rounded-xl border border-white/10 bg-white/[0.06] px-4 py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10"
              >
                <RefreshCw className="h-4 w-4" />
                Refresh
              </button>
            </div>
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
                : "Pick a resident from the list above to view their room assignment."
            }
          />
        ) : loading ? (
          <LoadingState />
        ) : assignments.length ? (
          <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-3">
            {assignments.map((a) => {
              const id = text(a, "roomAssignmentId", "RoomAssignmentId");
              return (
                <div
                  key={id || JSON.stringify(a)}
                  className="rounded-2xl border border-white/[0.08] bg-white/[0.04] p-5"
                >
                  <div className="flex items-start justify-between">
                    <div className="flex h-9 w-9 items-center justify-center rounded-xl bg-blue-300/10 text-blue-200">
                      <BedDouble className="h-4 w-4" />
                    </div>
                    <StatusPill value={text(a, "status", "Status") || "Active"} />
                  </div>
                  <h3 className="mt-4 font-display text-xl font-semibold tracking-[-0.04em] text-white">
                    Room {text(a, "roomId", "RoomId")} · Bed {text(a, "bedId", "BedId")}
                  </h3>
                  <dl className="mt-4 space-y-2 text-xs text-emerald-50/55">
                    <div className="flex justify-between">
                      <dt>Student</dt>
                      <dd className="text-emerald-50/80">
                        {text(a, "studentName", "StudentName") || text(a, "studentId", "StudentId") || "—"}
                      </dd>
                    </div>
                    <div className="flex justify-between">
                      <dt>Assigned</dt>
                      <dd className="text-emerald-50/80">
                        {formatDateTime(text(a, "assignedDate", "AssignedDate"))}
                      </dd>
                    </div>
                    <div className="flex justify-between">
                      <dt>Assigned by</dt>
                      <dd className="text-emerald-50/80">
                        {text(a, "assignedByUserId", "AssignedByUserId") || "—"}
                      </dd>
                    </div>
                  </dl>
                  {canManage && (
                    <div className="mt-5 flex gap-2">
                      <button
                        type="button"
                        onClick={() => setModal({ edit: a })}
                        className="flex flex-1 items-center justify-center gap-2 rounded-xl border border-white/15 bg-white/[0.06] py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10"
                      >
                        <Pencil className="h-3.5 w-3.5" />
                        Edit
                      </button>
                      <button
                        type="button"
                        onClick={() => remove(a)}
                        className="flex items-center justify-center gap-2 rounded-xl border border-rose-300/20 bg-rose-300/[0.06] px-3 py-2.5 text-xs font-semibold text-rose-200 hover:bg-rose-300/10"
                      >
                        <Trash2 className="h-3.5 w-3.5" />
                      </button>
                    </div>
                  )}
                </div>
              );
            })}
          </div>
        ) : (
          <EmptyState
            icon={DoorOpen}
            title="No room assignment"
            body={
              canManage
                ? "This resident has no active room assignment. Use “Assign resident” to create one."
                : "You do not have an active room assignment returned by the API yet."
            }
          />
        )}
      </div>

      {modal === "create" && (
        <Modal
          title="Assign resident"
          subtitle="Allocate a room and bed to the selected resident."
          onClose={() => setModal(null)}
        >
          <form onSubmit={(event) => submitAssignment(event)} className="space-y-4">
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Student ID
              </span>
              <input
                name="studentId"
                className="field"
                required
                defaultValue={text(selectedStudent, "studentId", "StudentId")}
                placeholder="Student ID from API"
              />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Student name
              </span>
              <input
                name="studentName"
                className="field"
                defaultValue={studentDisplayName(selectedStudent ?? {})}
                placeholder="Full name"
              />
            </label>
            <div className="grid gap-4 sm:grid-cols-2">
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room ID
                </span>
                <input name="roomId" className="field" type="number" required placeholder="Room ID" />
              </label>
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Bed ID
                </span>
                <input name="bedId" className="field" type="number" required placeholder="Bed ID" />
              </label>
            </div>
            <div className="grid gap-4 sm:grid-cols-2">
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Assigned date
                </span>
                <input
                  name="assignedDate"
                  className="field"
                  type="datetime-local"
                  defaultValue={toLocalInputValue()}
                />
              </label>
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Status
                </span>
                <select name="status" className="field" defaultValue="Active">
                  <option>Active</option>
                  <option>Pending</option>
                  <option>Inactive</option>
                </select>
              </label>
            </div>
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
                <UsersRound className="h-4 w-4" />
                Assign resident
              </button>
            </div>
          </form>
        </Modal>
      )}

      {modal !== "create" && modal !== null && (
        <Modal
          title="Edit room assignment"
          subtitle="Update the allocation details for this resident."
          onClose={() => setModal(null)}
        >
          <form onSubmit={(event) => submitAssignment(event, modal.edit)} className="space-y-4">
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Student ID
              </span>
              <input
                name="studentId"
                className="field"
                required
                defaultValue={text(modal.edit, "studentId", "StudentId")}
              />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Student name
              </span>
              <input
                name="studentName"
                className="field"
                defaultValue={text(modal.edit, "studentName", "StudentName")}
              />
            </label>
            <div className="grid gap-4 sm:grid-cols-2">
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Room ID
                </span>
                <input
                  name="roomId"
                  className="field"
                  type="number"
                  required
                  defaultValue={text(modal.edit, "roomId", "RoomId")}
                />
              </label>
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Bed ID
                </span>
                <input
                  name="bedId"
                  className="field"
                  type="number"
                  required
                  defaultValue={text(modal.edit, "bedId", "BedId")}
                />
              </label>
            </div>
            <div className="grid gap-4 sm:grid-cols-2">
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Assigned date
                </span>
                <input
                  name="assignedDate"
                  className="field"
                  type="datetime-local"
                  defaultValue={toLocalInputValue(
                    new Date(text(modal.edit, "assignedDate", "AssignedDate") || new Date()),
                  )}
                />
              </label>
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Status
                </span>
                <select
                  name="status"
                  className="field"
                  defaultValue={text(modal.edit, "status", "Status") || "Active"}
                >
                  <option>Active</option>
                  <option>Pending</option>
                  <option>Inactive</option>
                </select>
              </label>
            </div>
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
                <UsersRound className="h-4 w-4" />
                Save changes
              </button>
            </div>
          </form>
        </Modal>
      )}
    </main>
  );
}
