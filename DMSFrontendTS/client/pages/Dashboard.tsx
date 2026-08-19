import {
  FormEvent,
  ReactNode,
  useCallback,
  useEffect,
  useMemo,
  useState,
} from "react";
import { useNavigate } from "react-router-dom";
import {
  AlertTriangle,
  ArrowLeftRight,
  BedDouble,
  Bell,
  Building2,
  CalendarDays,
  Check,
  ChevronDown,
  ChevronRight,
  CircleHelp,
  ClipboardCheck,
  ClipboardList,
  DoorOpen,
  FileBarChart,
  FileText,
  Filter,
  House,
  LayoutDashboard,
  LogOut,
  Menu,
  MessageSquareWarning,
  Plus,
  Search,
  Settings,
  Shield,
  Trash2,
  UserRound,
  UsersRound,
  Wrench,
  X,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { dmsApi, type AuthRole } from "@/lib/api";

type View =
  | "overview"
  | "structure"
  | "students"
  | "requests"
  | "transfers"
  | "check-in-out"
  | "maintenance"
  | "complaints"
  | "inspections"
  | "violations"
  | "security"
  | "reports"
  | "audit"
  | "team"
  | "notifications"
  | "qr-code"
  | "settings"
  | "room"
  | "assignments"
  | "announcements"
  | "work-orders";
type StructureKind =
  "campuses" | "buildings" | "blocks" | "floors" | "rooms" | "beds";
type RecordMap = Record<string, unknown>;

const roleLabels: Record<AuthRole, string> = {
  admin: "Administrator",
  maintenance: "Maintenance staff",
  proctor: "Proctor",
  "dorm manager": "Dorm manager",
  student: "Student",
};

const structurePaths: Record<StructureKind, string> = {
  campuses: import.meta.env.VITE_CAMPUSES_PATH ?? "/campuses",
  buildings: import.meta.env.VITE_BUILDINGS_PATH ?? "/buildings",
  blocks: import.meta.env.VITE_BLOCKS_PATH ?? "/blocks",
  floors: import.meta.env.VITE_FLOORS_PATH ?? "/floors",
  rooms: import.meta.env.VITE_ROOMS_PATH ?? "/rooms",
  beds: import.meta.env.VITE_BEDS_PATH ?? "/beds",
};

const pathFor = (name: string, fallback: string) =>
  import.meta.env[name] ?? fallback;
const studentPath = pathFor("VITE_STUDENTS_PATH", "/Registrar/students");
const requestsPath = pathFor("VITE_REQUESTS_PATH", "/requests");
const transfersPath = pathFor("VITE_TRANSFERS_PATH", "/transfers");
const checkInOutPath = pathFor("VITE_CHECK_IN_OUT_PATH", "/check-in-out");
const maintenancePath = pathFor("VITE_MAINTENANCE_PATH", "/maintenance-requests");
const complaintsPath = pathFor("VITE_COMPLAINTS_PATH", "/complaints");
const inspectionsPath = pathFor("VITE_INSPECTIONS_PATH", "/inspections");
const violationsPath = pathFor("VITE_VIOLATIONS_PATH", "/violations");
const securityPath = pathFor("VITE_SECURITY_PATH", "/security-incidents");
const notificationsPath = pathFor("VITE_NOTIFICATIONS_PATH", "/notifications");
const qrCodePath = pathFor("VITE_QR_CODE_PATH", "/QRCode");
const auditPath = pathFor("VITE_AUDIT_LOGS_PATH", "/audit-logs");
const assignmentPath = pathFor("VITE_ASSIGNMENTS_PATH", "/RoomAssignment");
const myAssignmentPath = pathFor(
  "VITE_MY_ASSIGNMENT_PATH",
  "/RoomAssignment/student",
);

function text(record: RecordMap, ...keys: string[]) {
  for (const key of keys) {
    const value = record[key];
    if (value !== undefined && value !== null && String(value).trim())
      return String(value);
  }
  return "";
}

function recordId(record: RecordMap) {
  return text(
    record,
    "id",
    "Id",
    "ID",
    "studentId",
    "studentID",
    "roomId",
    "roomID",
    "buildingId",
    "blockId",
    "floorId",
    "bedId",
    "requestId",
  );
}

function roleFromStoredUser(): AuthRole {
  const storedRole = window.sessionStorage.getItem("dwell_role")?.toLowerCase();
  if (
    storedRole === "admin" ||
    storedRole === "maintenance" ||
    storedRole === "proctor" ||
    storedRole === "dorm manager" ||
    storedRole === "student"
  )
    return storedRole;
  try {
    const stored = JSON.parse(
      window.sessionStorage.getItem("dwell_user") ?? "null",
    ) as RecordMap | null;
    const raw = text(
      stored ?? {},
      "role",
      "userRole",
      "roleName",
    ).toLowerCase();
    if (raw.includes("admin")) return "admin";
    if (raw.includes("maintenance")) return "maintenance";
    if (raw.includes("proctor") || raw.includes("security")) return "proctor";
    if (
      raw.includes("manager") ||
      raw.includes("dormitory staff") ||
      raw.includes("staff")
    )
      return "dorm manager";
  } catch {
    // The dashboard still uses the authenticated session when profile metadata is unavailable.
  }
  return "student";
}

function useRemoteRecords(resource: string | null) {
  const [records, setRecords] = useState<RecordMap[]>([]);
  const [loading, setLoading] = useState(Boolean(resource));
  const [error, setError] = useState("");
  const refresh = useCallback(async () => {
    if (!resource) return;
    setLoading(true);
    setError("");
    try {
      setRecords(await dmsApi.list<RecordMap>(resource));
    } catch (requestError) {
      setRecords([]);
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load records from the API.",
      );
    } finally {
      setLoading(false);
    }
  }, [resource]);
  useEffect(() => {
    void refresh();
  }, [refresh]);
  return { records, setRecords, loading, error, refresh };
}

function BrandMark() {
  return (
    <div className="flex items-center gap-3">
      <div className="relative flex h-9 w-9 items-center justify-center overflow-hidden rounded-xl bg-emerald-300 text-[#062b25]">
        <span className="absolute -right-2 -top-3 h-8 w-8 rotate-45 rounded-[100%_0] border-[5px] border-[#062b25]/75" />
        <span className="absolute -bottom-3 -left-2 h-8 w-8 -rotate-45 rounded-[100%_0] border-[5px] border-[#062b25]/75" />
      </div>
      <p className="text-[8px] font-bold uppercase tracking-[0.25em] text-emerald-300/65">
        Residence OS
      </p>
    </div>
  );
}

function Modal({
  title,
  subtitle,
  onClose,
  children,
  wide = false,
}: {
  title: string;
  subtitle?: string;
  onClose: () => void;
  children: ReactNode;
  wide?: boolean;
}) {
  return (
    <div
      className="fixed inset-0 z-50 flex items-end justify-center bg-[#062b25]/75 p-0 backdrop-blur-sm sm:items-center sm:p-6"
      onMouseDown={(event) => {
        if (event.target === event.currentTarget) onClose();
      }}
    >
      <div
        className={cn(
          "max-h-[92vh] w-full overflow-y-auto rounded-t-3xl border border-[#dfe8e1] bg-[#f8f7f2] p-6 text-[#183d34] shadow-2xl sm:rounded-3xl sm:p-8",
          wide ? "max-w-3xl" : "max-w-xl",
        )}
      >
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
    normalized.includes("active") ||
    normalized.includes("available") ||
    normalized.includes("complete") ||
    normalized.includes("approved")
      ? "bg-emerald-100 text-emerald-800"
      : normalized.includes("pending") ||
          normalized.includes("review") ||
          normalized.includes("reserved")
        ? "bg-amber-100 text-amber-800"
        : normalized.includes("progress") || normalized.includes("assigned")
          ? "bg-sky-100 text-sky-800"
          : "bg-slate-100 text-slate-600";
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
  action,
  onAction,
}: {
  icon: typeof FileText;
  title: string;
  body: string;
  action?: string;
  onAction?: () => void;
}) {
  return (
    <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-white/10 bg-white/[0.025] px-6 py-16 text-center">
      <div className="mb-4 flex h-12 w-12 items-center justify-center rounded-2xl bg-emerald-300/10 text-emerald-300">
        <Icon className="h-5 w-5" />
      </div>
      <h3 className="font-display text-lg font-semibold text-white">{title}</h3>
      <p className="mt-2 max-w-sm text-sm leading-6 text-emerald-50/45">
        {body}
      </p>
      {action && (
        <button
          type="button"
          onClick={onAction}
          className="mt-5 rounded-lg bg-emerald-300 px-4 py-2 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
        >
          {action}
        </button>
      )}
    </div>
  );
}

function StatCard({
  label,
  value,
  detail,
  icon: Icon,
  tone = "mint",
}: {
  label: string;
  value: string;
  detail: string;
  icon: typeof UsersRound;
  tone?: "mint" | "blue" | "gold" | "rose";
}) {
  const tones = {
    mint: "bg-emerald-300/10 text-emerald-300",
    blue: "bg-blue-300/10 text-blue-200",
    gold: "bg-amber-300/10 text-amber-200",
    rose: "bg-rose-300/10 text-rose-200",
  };
  return (
    <div className="group relative overflow-hidden rounded-2xl border border-white/[0.08] bg-white/[0.045] p-5 transition hover:bg-white/[0.07]">
      <div
        className={cn(
          "flex h-9 w-9 items-center justify-center rounded-xl",
          tones[tone],
        )}
      >
        <Icon className="h-[18px] w-[18px]" />
      </div>
      <p className="mt-5 text-3xl font-semibold tracking-[-0.06em] text-white">
        {value}
      </p>
      <p className="mt-1 text-xs font-medium text-emerald-50/45">{label}</p>
      <div className="mt-4 border-t border-white/[0.07] pt-3 text-[10px] text-emerald-50/35">
        {detail}
      </div>
    </div>
  );
}

function SectionTitle({
  eyebrow,
  title,
  action,
  onAction,
}: {
  eyebrow?: string;
  title: string;
  action?: string;
  onAction?: () => void;
}) {
  return (
    <div className="mb-5 flex items-end justify-between gap-4">
      <div>
        {eyebrow && (
          <p className="mb-1.5 text-[10px] font-bold uppercase tracking-[0.2em] text-emerald-300/55">
            {eyebrow}
          </p>
        )}
        <h2 className="font-display text-xl font-semibold tracking-[-0.045em] text-white">
          {title}
        </h2>
      </div>
      {action && (
        <button
          type="button"
          onClick={onAction}
          className="hidden items-center gap-1.5 text-xs font-semibold text-emerald-300 transition hover:text-emerald-200 sm:flex"
        >
          {action}
          <ChevronRight className="h-3.5 w-3.5" />
        </button>
      )}
    </div>
  );
}

function Overview({
  role,
  onNavigate,
  openCreate,
}: {
  role: AuthRole;
  onNavigate: (view: View) => void;
  openCreate: (kind: "request" | "structure" | "student") => void;
}) {
  const students = useRemoteRecords(role === "student" ? null : studentPath);
  const rooms = useRemoteRecords(
    role === "student" ? null : structurePaths.rooms,
  );
  const requests = useRemoteRecords(
    role === "student" ? myAssignmentPath : requestsPath,
  );
  const meta = roleLabels[role];
  const isStudent = role === "student";
  const total = (value: number) => (value ? value.toLocaleString() : "—");
  return (
    <div className="space-y-7">
      <div className="relative overflow-hidden rounded-3xl border border-emerald-300/15 bg-gradient-to-br from-[#0d5548] via-[#0a4439] to-[#08372f] p-6 sm:p-8">
        <div className="absolute -right-24 -top-32 h-80 w-80 rounded-full border border-blue-200/10 [box-shadow:0_0_0_35px_rgba(59,130,246,.03),0_0_0_70px_rgba(110,231,183,.02)]" />
        <div className="relative max-w-2xl">
          <span className="inline-flex items-center gap-2 rounded-full bg-emerald-300/10 px-2.5 py-1 text-[10px] font-bold uppercase tracking-[0.18em] text-emerald-200">
            <span className="h-1.5 w-1.5 rounded-full bg-emerald-300" />
            {meta}
          </span>
          <h1 className="mt-5 max-w-xl font-display text-3xl font-semibold leading-[1.02] tracking-[-0.065em] text-white">
            Residence dashboard
            <br />
            <span className="text-emerald-200/65">
              Review current records and residence activity.
            </span>
          </h1>
          <div className="mt-6 flex flex-wrap gap-3">
            <button
              type="button"
              onClick={() =>
                openCreate(
                  isStudent
                    ? "request"
                    : role === "admin" || role === "dorm manager"
                      ? "structure"
                      : "request",
                )
              }
              className="inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] shadow-lg transition hover:bg-emerald-200"
            >
              <Plus className="h-4 w-4" />
              {isStudent
                ? "Create request"
                : role === "admin" || role === "dorm manager"
                  ? "Add structure"
                  : "Create request"}
            </button>
            <button
              type="button"
              onClick={() =>
                onNavigate(
                  isStudent
                    ? "room"
                    : role === "proctor"
                      ? "assignments"
                      : "structure",
                )
              }
              className="inline-flex items-center gap-2 rounded-xl border border-white/15 bg-white/[0.06] px-4 py-2.5 text-xs font-semibold text-emerald-50/80 transition hover:bg-white/10"
            >
              {isStudent
                ? "View my room"
                : role === "proctor"
                  ? "View available rooms"
                  : "Open structure"}
              <ChevronRight className="h-4 w-4" />
            </button>
          </div>
        </div>
      </div>
      <div className="grid gap-3 sm:grid-cols-2 xl:grid-cols-4">
        {isStudent ? (
          <>
            <StatCard
              label="Room assignment"
              value="—"
              detail="Live assignment data"
              icon={House}
            />
            <StatCard
              label="Open requests"
              value="—"
              detail="Live request data"
              icon={ClipboardList}
              tone="gold"
            />
            <StatCard
              label="Unread updates"
              value="—"
              detail="Live notification data"
              icon={Bell}
              tone="blue"
            />
            <StatCard
              label="Residence status"
              value="—"
              detail="Live profile data"
              icon={Shield}
            />
          </>
        ) : (
          <>
            <StatCard
              label="Students"
              value={total(students.records.length)}
              detail={
                students.error ? "API connection needed" : "Live from backend"
              }
              icon={UsersRound}
            />
            <StatCard
              label="Rooms & beds"
              value={total(rooms.records.length)}
              detail={
                rooms.error ? "API connection needed" : "Live from backend"
              }
              icon={BedDouble}
              tone="gold"
            />
            <StatCard
              label="Open requests"
              value={total(requests.records.length)}
              detail={
                requests.error ? "API connection needed" : "Live from backend"
              }
              icon={ClipboardList}
              tone="blue"
            />
            <StatCard
              label="API status"
              value={
                students.error || rooms.error || requests.error ? "—" : "Live"
              }
              detail={
                students.error || rooms.error || requests.error
                  ? "Add your API base URL"
                  : "Connected"
              }
              icon={Shield}
            />
          </>
        )}
      </div>
      <div className="grid gap-6 xl:grid-cols-[1.2fr_.8fr]">
        <div className="rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5 sm:p-6">
          <SectionTitle
            eyebrow="Live data"
            title={
              isStudent ? "Your residence at a glance" : "Current records"
            }
            action="Open detail"
            onAction={() => onNavigate(isStudent ? "room" : "students")}
          />
          {isStudent ? (
            <EmptyState
              icon={House}
              title="No assignment loaded"
              body="Your room and bed assignment will appear here once the API returns it."
              action="View my room"
              onAction={() => onNavigate("room")}
            />
          ) : students.loading || rooms.loading ? (
            <div className="flex items-center justify-center py-20 text-sm text-emerald-50/45">
              Loading records from the API…
            </div>
          ) : students.records.length || rooms.records.length ? (
            <div className="grid gap-3 sm:grid-cols-2">
              <div className="rounded-xl border border-white/[0.08] bg-white/[0.03] p-4">
                <p className="text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/35">
                  Students loaded
                </p>
                <p className="mt-3 font-display text-3xl font-semibold text-white">
                  {students.records.length}
                </p>
                <button
                  type="button"
                  onClick={() => onNavigate("students")}
                  className="mt-4 text-xs font-semibold text-emerald-300"
                >
                  Open students{" "}
                  <ChevronRight className="ml-1 inline h-3.5 w-3.5" />
                </button>
              </div>
              <div className="rounded-xl border border-white/[0.08] bg-white/[0.03] p-4">
                <p className="text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/35">
                  Rooms loaded
                </p>
                <p className="mt-3 font-display text-3xl font-semibold text-white">
                  {rooms.records.length}
                </p>
                <button
                  type="button"
                  onClick={() =>
                    onNavigate(role === "proctor" ? "assignments" : "structure")
                  }
                  className="mt-4 text-xs font-semibold text-emerald-300"
                >
                  Open rooms{" "}
                  <ChevronRight className="ml-1 inline h-3.5 w-3.5" />
                </button>
              </div>
            </div>
          ) : (
            <EmptyState
              icon={Building2}
              title="No records returned"
              body="No records are currently available for this section."
              action={
                role === "admin" || role === "dorm manager"
                  ? "Add structure"
                  : undefined
              }
              onAction={() => openCreate("structure")}
            />
          )}
        </div>
        <div className="rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5 sm:p-6">
          <SectionTitle eyebrow="System overview" title="Residence operations" />
          <p className="text-sm leading-6 text-emerald-50/55">
            Use the navigation to manage the records available for your role.
          </p>
        </div>
      </div>
    </div>
  );
}

function StructurePage({
  role,
  openCreate,
}: {
  role: AuthRole;
  openCreate: (kind: "structure") => void;
}) {
  const canManage =
    role === "admin" || role === "dorm manager" || role === "proctor";
  const [kind, setKind] = useState<StructureKind>(
    role === "proctor" ? "rooms" : "buildings",
  );
  const resource = structurePaths[kind];
  const { records, loading, error, refresh } = useRemoteRecords(resource);
  const remove = async (record: RecordMap) => {
    const id = recordId(record);
    if (!id || !window.confirm("Delete this record? This cannot be undone."))
      return;
    try {
      await dmsApi.remove(`${resource}/${encodeURIComponent(id)}`);
      await refresh();
    } catch (requestError) {
      window.alert(
        requestError instanceof Error
          ? requestError.message
          : "Unable to delete the record.",
      );
    }
  };
  const labels: Record<StructureKind, string> = {
    campuses: "Campuses",
    buildings: "Buildings",
    blocks: "Blocks",
    floors: "Floors",
    rooms: "Rooms",
    beds: "Beds",
  };
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow="Asset management"
        title="Campus structure"
        description="Manage the hierarchy your backend exposes. Empty states stay empty until your API returns records."
        action={
          canManage
            ? `Add ${labels[kind].slice(0, -1).toLowerCase()}`
            : undefined
        }
        onAction={() => openCreate("structure")}
      />
      <div className="flex gap-1 overflow-x-auto rounded-xl border border-white/[0.08] bg-white/[0.035] p-1">
        {(role === "proctor"
          ? (["rooms", "beds"] as StructureKind[])
          : ([
              "campuses",
              "buildings",
              "blocks",
              "floors",
              "rooms",
              "beds",
            ] as StructureKind[])
        ).map((item) => (
          <button
            type="button"
            key={item}
            onClick={() => setKind(item)}
            className={cn(
              "whitespace-nowrap rounded-lg px-3.5 py-2.5 text-xs font-semibold capitalize transition",
              kind === item
                ? "bg-emerald-300 text-[#07352d]"
                : "text-emerald-50/50 hover:text-white",
            )}
          >
            {labels[item]}
          </button>
        ))}
      </div>
      {error && <ApiNotice message={error} />}
      {loading ? (
        <LoadingState />
      ) : records.length ? (
        <RecordTable
          records={records}
          kind={labels[kind]}
          canDelete={canManage}
          onDelete={remove}
        />
      ) : (
        <EmptyState
          icon={kind === "rooms" ? DoorOpen : Building2}
          title={`No ${labels[kind].toLowerCase()} yet`}
          body={`Your API returned no ${labels[kind].toLowerCase()}. Use the add action to create one and give it a name.`}
          action={
            canManage
              ? `Add ${labels[kind].slice(0, -1).toLowerCase()}`
              : undefined
          }
          onAction={() => openCreate("structure")}
        />
      )}
    </div>
  );
}

function RoomsAssignmentPage({
  openCreate,
}: {
  openCreate: (kind: "assignment") => void;
}) {
  const { records, loading, error, refresh } = useRemoteRecords(
    structurePaths.rooms,
  );
  const grouped = useMemo(
    () =>
      records.reduce<Record<string, RecordMap[]>>((groups, room) => {
        const block =
          text(room, "blockName", "block", "blockId") || "Unassigned block";
        const floor =
          text(room, "floorName", "floor", "floorId") || "Unassigned floor";
        const key = `${block} · ${floor}`;
        (groups[key] ||= []).push(room);
        return groups;
      }, {}),
    [records],
  );
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow="Proctor workflow"
        title="Room availability"
        description="Available rooms are grouped by block and floor so you can review occupancy before assigning a student."
        action="Assign student"
        onAction={() => openCreate("assignment")}
      />
      {error && <ApiNotice message={error} />}
      {loading ? (
        <LoadingState />
      ) : Object.keys(grouped).length ? (
        <div className="space-y-5">
          {Object.entries(grouped).map(([group, rooms]) => (
            <section
              key={group}
              className="rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5"
            >
              <div className="mb-4 flex items-center justify-between">
                <div>
                  <p className="text-[10px] font-bold uppercase tracking-[0.18em] text-emerald-300/55">
                    Block / floor
                  </p>
                  <h2 className="mt-1 font-display text-lg font-semibold text-white">
                    {group}
                  </h2>
                </div>
                <span className="rounded-full bg-blue-300/10 px-2.5 py-1 text-[10px] font-bold text-blue-200">
                  {rooms.length} rooms
                </span>
              </div>
              <div className="grid gap-3 md:grid-cols-2 xl:grid-cols-3">
                {rooms.map((room) => (
                  <RoomCard
                    key={recordId(room) || JSON.stringify(room)}
                    room={room}
                    onAssign={() => openCreate("assignment")}
                  />
                ))}
              </div>
            </section>
          ))}
        </div>
      ) : (
        <EmptyState
          icon={DoorOpen}
          title="No rooms available"
          body="Rooms will appear here grouped by their block and floor once your API returns them."
          action="Refresh rooms"
          onAction={() => refresh()}
        />
      )}
    </div>
  );
}

function occupancyWidthClass(value: string) {
  const percentage = Number.parseFloat(value);
  if (!Number.isFinite(percentage)) return "occupancy-fill-0";
  const step = Math.min(100, Math.max(0, Math.round(percentage / 10) * 10));
  return `occupancy-fill-${step}`;
}

function RoomCard({
  room,
  onAssign,
}: {
  room: RecordMap;
  onAssign: () => void;
}) {
  const roomName =
    text(room, "name", "roomName", "roomNumber", "number") || "Unnamed room";
  const capacity = text(room, "capacity", "bedCount", "totalBeds") || "—";
  const occupied =
    text(room, "occupiedBeds", "occupied", "assignedBeds") || "—";
  const available =
    text(room, "availableBeds", "available", "vacantBeds") || "—";
  const occupancy = text(
    room,
    "occupancyRate",
    "occupancy",
    "occupancyPercentage",
  );
  return (
    <div className="rounded-2xl border border-white/[0.08] bg-white/[0.04] p-4">
      <div className="flex items-start justify-between">
        <div className="flex h-9 w-9 items-center justify-center rounded-xl bg-blue-300/10 text-blue-200">
          <DoorOpen className="h-4 w-4" />
        </div>
        <StatusPill
          value={
            available !== "—"
              ? `${available} available`
              : "Availability not set"
          }
        />
      </div>
      <h3 className="mt-5 font-display text-xl font-semibold tracking-[-0.04em] text-white">
        {roomName}
      </h3>
      <p className="mt-1 text-[11px] text-emerald-50/40">
        Capacity: {capacity} · Occupied: {occupied}
      </p>
      <div className="mt-5 h-1.5 rounded-full bg-white/[0.08]">
        <div
          className={cn(
            "h-1.5 rounded-full bg-blue-300",
            occupancyWidthClass(occupancy),
          )}
        />
      </div>
      <div className="mt-2 flex justify-between text-[10px] text-emerald-50/35">
        <span>Occupancy</span>
        <span>{occupancy || "Not returned"}</span>
      </div>
      <button
        type="button"
        onClick={onAssign}
        className="mt-5 flex w-full items-center justify-center gap-2 rounded-xl bg-emerald-300 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
      >
        <UsersRound className="h-4 w-4" />
        Assign student
      </button>
    </div>
  );
}

function EntityPage({
  title,
  description,
  eyebrow,
  resource,
  icon: Icon,
  allowCreate,
  allowDelete,
  createLabel,
  openCreate,
}: {
  title: string;
  description: string;
  eyebrow: string;
  resource: string;
  icon: typeof UsersRound;
  allowCreate: boolean;
  allowDelete: boolean;
  createLabel: string;
  openCreate: (kind: "student" | "request" | "inspection") => void;
}) {
  const { records, loading, error, refresh } = useRemoteRecords(resource);
  const remove = async (record: RecordMap) => {
    const id = recordId(record);
    if (!id || !window.confirm("Delete this record? This cannot be undone."))
      return;
    try {
      await dmsApi.remove(`${resource}/${encodeURIComponent(id)}`);
      await refresh();
    } catch (requestError) {
      window.alert(
        requestError instanceof Error
          ? requestError.message
          : "Unable to delete the record.",
      );
    }
  };
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow={eyebrow}
        title={title}
        description={description}
        action={allowCreate ? createLabel : undefined}
        onAction={() =>
          openCreate(
            title === "Students"
              ? "student"
              : title === "Inspections"
                ? "inspection"
                : "request",
          )
        }
      />
      {error && <ApiNotice message={error} />}
      {loading ? (
        <LoadingState />
      ) : records.length ? (
        <RecordTable
          records={records}
          kind={title}
          canDelete={allowDelete}
          onDelete={remove}
        />
      ) : (
        <EmptyState
          icon={Icon}
          title={`No ${title.toLowerCase()} returned`}
          body="This view is connected to the API but has no records to display yet."
          action={allowCreate ? createLabel : undefined}
          onAction={() =>
            openCreate(
              title === "Students"
                ? "student"
                : title === "Inspections"
                  ? "inspection"
                  : "request",
            )
          }
        />
      )}
    </div>
  );
}

function SrsModulePage({
  title,
  eyebrow,
  description,
  resource,
  icon: Icon,
  emptyTitle,
  emptyBody,
}: {
  title: string;
  eyebrow: string;
  description: string;
  resource: string;
  icon: typeof UsersRound;
  emptyTitle: string;
  emptyBody: string;
}) {
  const { records, loading, error, refresh } = useRemoteRecords(resource);
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow={eyebrow}
        title={title}
        description={description}
        action="Refresh"
        onAction={() => refresh()}
      />
      {error && <ApiNotice message={error} />}
      {loading ? (
        <LoadingState />
      ) : records.length ? (
        <RecordTable
          records={records}
          kind={title}
          canDelete={false}
          onDelete={() => undefined}
        />
      ) : (
        <EmptyState icon={Icon} title={emptyTitle} body={emptyBody} action="Refresh" onAction={() => refresh()} />
      )}
    </div>
  );
}

function RequestsPage({
  role,
  openCreate,
}: {
  role: AuthRole;
  openCreate: (kind: "request") => void;
}) {
  const canCreate =
    role === "student" ||
    role === "admin" ||
    role === "dorm manager" ||
    role === "proctor";
  return (
    <EntityPage
      title={
        role === "maintenance"
          ? "Work orders"
          : role === "student"
            ? "My requests"
            : "Requests & cases"
      }
      description="Track maintenance, complaints, transfers, and violations through the status returned by your backend."
      eyebrow="Operations inbox"
      resource={requestsPath}
      icon={ClipboardList}
      allowCreate={canCreate}
      allowDelete={false}
      createLabel="Create request"
      openCreate={openCreate}
    />
  );
}

function RecordTable({
  records,
  kind,
  canDelete,
  onDelete,
}: {
  records: RecordMap[];
  kind: string;
  canDelete: boolean;
  onDelete: (record: RecordMap) => void;
}) {
  const columns = useMemo(() => {
    const keys = new Set<string>();
    records.slice(0, 8).forEach((record) =>
      Object.keys(record).forEach((key) => {
        if (
          !key.toLowerCase().includes("password") &&
          !key.toLowerCase().includes("token") &&
          typeof record[key] !== "object"
        )
          keys.add(key);
      }),
    );
    return Array.from(keys).slice(0, 5);
  }, [records]);
  return (
    <div className="overflow-hidden rounded-2xl border border-white/[0.08] bg-white/[0.035]">
      <div className="flex items-center justify-between border-b border-white/[0.08] p-4">
        <div>
          <h2 className="font-display text-lg font-semibold text-white">
            {kind}
          </h2>
          <p className="mt-1 text-xs text-emerald-50/40">
            {records.length} record{records.length === 1 ? "" : "s"} returned by
            the API
          </p>
        </div>
        <button
          type="button"
          className="rounded-lg border border-white/10 p-2 text-emerald-50/45 hover:bg-white/[0.05]"
        >
          <Filter className="h-4 w-4" />
        </button>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full min-w-[680px] text-left">
          <thead className="bg-white/[0.025] text-[10px] font-bold uppercase tracking-[0.15em] text-emerald-50/30">
            <tr>
              {columns.map((column) => (
                <th key={column} className="px-5 py-3 font-semibold">
                  {column.replace(/([A-Z])/g, " $1")}
                </th>
              ))}
              <th className="px-5 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-white/[0.06]">
            {records.map((record, index) => (
              <tr
                key={recordId(record) || index}
                className="group hover:bg-white/[0.03]"
              >
                {columns.map((column) => (
                  <td
                    key={column}
                    className="px-5 py-4 text-xs text-emerald-50/60"
                  >
                    {column.toLowerCase().includes("status") ? (
                      <StatusPill value={text(record, column)} />
                    ) : (
                      text(record, column) || "—"
                    )}
                  </td>
                ))}
                <td className="px-5 py-4 text-right">
                  {canDelete && (
                    <button
                      type="button"
                      aria-label={`Delete ${kind} record`}
                      onClick={() => onDelete(record)}
                      className="rounded-lg p-2 text-rose-200/60 hover:bg-rose-300/10 hover:text-rose-200"
                    >
                      <Trash2 className="h-4 w-4" />
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

function PageHeader({
  eyebrow,
  title,
  description,
  action,
  onAction,
}: {
  eyebrow: string;
  title: string;
  description: string;
  action?: string;
  onAction?: () => void;
}) {
  return (
    <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
      <div>
        <p className="mb-1.5 text-[10px] font-bold uppercase tracking-[0.2em] text-emerald-300/55">
          {eyebrow}
        </p>
        <h1 className="font-display text-3xl font-semibold tracking-[-0.06em] text-white">
          {title}
        </h1>
        <p className="mt-2 max-w-2xl text-sm text-emerald-50/45">
          {description}
        </p>
      </div>
      {action && (
        <button
          type="button"
          onClick={onAction}
          className="inline-flex items-center justify-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200"
        >
          <Plus className="h-4 w-4" />
          {action}
        </button>
      )}
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

function LoadingState() {
  return (
    <div className="flex items-center justify-center rounded-2xl border border-white/[0.08] bg-white/[0.035] py-20 text-sm text-emerald-50/45">
      Loading live records…
    </div>
  );
}

function CreateStructureModal({
  onClose,
  onCreated,
}: {
  onClose: () => void;
  onCreated: () => void;
}) {
  const [kind, setKind] = useState<StructureKind>("buildings");
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");
  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitting(true);
    setError("");
    const data = new FormData(event.currentTarget);
    const payload = {
      name: String(data.get("name")),
      code: String(data.get("code")),
      parentId: String(data.get("parentId") || "") || undefined,
      capacity: data.get("capacity") ? Number(data.get("capacity")) : undefined,
    };
    try {
      await dmsApi.create(structurePaths[kind], payload);
      onCreated();
      onClose();
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to create this record.",
      );
    } finally {
      setSubmitting(false);
    }
  };
  return (
    <Modal
      title={`Add ${kind.slice(0, -1)}`}
      subtitle="Name the structure and save it to the connected backend."
      onClose={onClose}
    >
      <form onSubmit={submit} className="space-y-4">
        <div className="flex gap-2 overflow-x-auto">
          {(
            [
              "campuses",
              "buildings",
              "blocks",
              "floors",
              "rooms",
              "beds",
            ] as StructureKind[]
          ).map((item) => (
            <button
              type="button"
              key={item}
              onClick={() => setKind(item)}
              className={cn(
                "whitespace-nowrap rounded-lg px-3 py-2 text-[10px] font-bold capitalize",
                kind === item
                  ? "bg-[#d9efe0] text-[#0d7359]"
                  : "bg-[#edf1ed] text-[#769084]",
              )}
            >
              {item}
            </button>
          ))}
        </div>
        <label className="block">
          <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
            Name
          </span>
          <input
            required
            name="name"
            className="field"
            placeholder={`Name this ${kind.slice(0, -1)}`}
          />
        </label>
        <div className="grid gap-4 sm:grid-cols-2">
          <label className="block">
            <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
              Code
            </span>
            <input name="code" className="field" placeholder="Optional code" />
          </label>
          {kind !== "campuses" && (
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Parent ID
              </span>
              <input
                name="parentId"
                className="field"
                placeholder="ID from your API"
              />
            </label>
          )}
        </div>
        {(kind === "rooms" || kind === "beds") && (
          <label className="block">
            <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
              Capacity
            </span>
            <input
              name="capacity"
              type="number"
              min="1"
              className="field"
              placeholder="Capacity returned by policy"
            />
          </label>
        )}
        {error && (
          <p className="rounded-xl bg-rose-50 px-3 py-2 text-xs text-rose-700">
            {error}
          </p>
        )}
        <div className="flex justify-end gap-2 pt-3">
          <button
            type="button"
            onClick={onClose}
            className="rounded-xl border border-[#d5e0d8] px-4 py-2.5 text-xs font-semibold text-[#5a7468]"
          >
            Cancel
          </button>
          <button
            disabled={submitting}
            type="submit"
            className="rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white disabled:opacity-60"
          >
            {submitting ? "Saving…" : "Create record"}
          </button>
        </div>
      </form>
    </Modal>
  );
}

function CreateEntityModal({
  type,
  onClose,
  onCreated,
}: {
  type: "student" | "request" | "inspection";
  onClose: () => void;
  onCreated: () => void;
}) {
  const path =
    type === "student"
      ? studentPath
      : type === "request"
        ? requestsPath
        : inspectionsPath;
  const [error, setError] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitting(true);
    setError("");
    const data = new FormData(event.currentTarget);
    const payload = Object.fromEntries(data.entries());
    try {
      await dmsApi.create(path, payload);
      onCreated();
      onClose();
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to create the record.",
      );
    } finally {
      setSubmitting(false);
    }
  };
  const title =
    type === "student"
      ? "Add student"
      : type === "request"
        ? "Create request"
        : "Schedule inspection";
  return (
    <Modal
      title={title}
      subtitle="This form submits directly to the configured API resource."
      onClose={onClose}
    >
      <form onSubmit={submit} className="space-y-4">
        {type === "student" && (
          <>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Full name
              </span>
              <input required name="name" className="field" />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Student ID
              </span>
              <input required name="studentId" className="field" />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Department
              </span>
              <input name="department" className="field" />
            </label>
          </>
        )}
        {type === "request" && (
          <>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Request type
              </span>
              <select name="type" className="field">
                <option>Maintenance</option>
                <option>Complaint</option>
                <option>Transfer</option>
                <option>Violation</option>
              </select>
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Title
              </span>
              <input required name="title" className="field" />
            </label>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Description
              </span>
              <textarea
                required
                name="description"
                className="field min-h-28 resize-none"
              />
            </label>
          </>
        )}
        {type === "inspection" && (
          <>
            <label className="block">
              <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                Room ID
              </span>
              <input required name="roomId" className="field" />
            </label>
            <div className="grid gap-4 sm:grid-cols-2">
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Date
                </span>
                <input required type="date" name="date" className="field" />
              </label>
              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Purpose
                </span>
                <input name="purpose" className="field" />
              </label>
            </div>
          </>
        )}
        {error && (
          <p className="rounded-xl bg-rose-50 px-3 py-2 text-xs text-rose-700">
            {error}
          </p>
        )}
        <div className="flex justify-end gap-2 pt-3">
          <button
            type="button"
            onClick={onClose}
            className="rounded-xl border border-[#d5e0d8] px-4 py-2.5 text-xs font-semibold text-[#5a7468]"
          >
            Cancel
          </button>
          <button
            disabled={submitting}
            type="submit"
            className="rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white disabled:opacity-60"
          >
            {submitting ? "Saving…" : "Save record"}
          </button>
        </div>
      </form>
    </Modal>
  );
}

function AssignmentModal({
  onClose,
  onCreated,
}: {
  onClose: () => void;
  onCreated: () => void;
}) {
  const [error, setError] = useState("");
  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError("");
    const payload = Object.fromEntries(
      new FormData(event.currentTarget).entries(),
    );
    try {
      await dmsApi.create(assignmentPath, payload);
      onCreated();
      onClose();
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to assign the student.",
      );
    }
  };
  return (
    <Modal
      title="Assign student"
      subtitle="Use the IDs returned by the rooms and students APIs to create an allocation."
      onClose={onClose}
    >
      <form onSubmit={submit} className="space-y-4">
        <label className="block">
          <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
            Student ID
          </span>
          <input
            required
            name="studentId"
            className="field"
            placeholder="Student ID from API"
          />
        </label>
        <label className="block">
          <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
            Room ID
          </span>
          <input
            required
            name="roomId"
            className="field"
            placeholder="Room ID from API"
          />
        </label>
        <label className="block">
          <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
            Bed ID
          </span>
          <input
            required
            name="bedId"
            className="field"
            placeholder="Available bed ID"
          />
        </label>
        {error && (
          <p className="rounded-xl bg-rose-50 px-3 py-2 text-xs text-rose-700">
            {error}
          </p>
        )}
        <div className="flex justify-end gap-2 pt-3">
          <button
            type="button"
            onClick={onClose}
            className="rounded-xl border border-[#d5e0d8] px-4 py-2.5 text-xs font-semibold text-[#5a7468]"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="rounded-xl bg-[#0d594a] px-4 py-2.5 text-xs font-semibold text-white"
          >
            Assign student
          </button>
        </div>
      </form>
    </Modal>
  );
}

function StudentRoomPage() {
  const storedUser = JSON.parse(
    window.sessionStorage.getItem("dwell_user") ?? "null",
  ) as RecordMap | null;
  const studentId = text(
    storedUser ?? {},
    "sId",
    "studentId",
    "studentID",
    "userId",
  );
  const resource = studentId
    ? `${myAssignmentPath}/${encodeURIComponent(studentId)}`
    : null;
  const { records, loading, error, refresh } = useRemoteRecords(resource);
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow="My accommodation"
        title="My room"
        description="Your current building, room, bed, and residence status are read from the authenticated API."
        action={resource ? "Refresh" : undefined}
        onAction={() => refresh()}
      />
      {error && <ApiNotice message={error} />}
      {loading ? (
        <LoadingState />
      ) : records.length ? (
        <RecordTable
          records={records}
          kind="Current assignment"
          canDelete={false}
          onDelete={() => undefined}
        />
      ) : (
        <EmptyState
          icon={House}
          title="No assignment returned"
          body={
            resource
              ? "Your room assignment will appear here after the residence API returns an active allocation."
              : "This account does not include a student assignment ID yet."
          }
        />
      )}
    </div>
  );
}

function SettingsPage() {
  const [notifications, setNotifications] = useState(true);
  const [saved, setSaved] = useState(false);
  return (
    <div className="space-y-7">
      <PageHeader
        eyebrow="Account preferences"
        title="Settings"
        description="These controls change how this account receives updates."
      />
      <div className="max-w-2xl rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5 sm:p-6">
        <div className="border-b border-white/[0.07] pb-5">
          <h2 className="font-display text-xl font-semibold text-white">
            Preferences
          </h2>
          <p className="mt-1 text-xs text-emerald-50/40">
            Notifications and presentation settings for your account.
          </p>
        </div>
        <div className="flex items-center justify-between gap-5 border-b border-white/[0.07] py-5">
          <div>
            <p className="text-sm font-semibold text-white">
              Email notifications
            </p>
            <p className="mt-1 text-xs leading-5 text-emerald-50/40">
              Receive updates for requests, assignments, and announcements.
            </p>
          </div>
          <button
            type="button"
            onClick={() => setNotifications(!notifications)}
            className={cn(
              "relative h-6 w-11 shrink-0 rounded-full p-1 transition",
              notifications ? "bg-emerald-300" : "bg-white/15",
            )}
          >
            <span
              className={cn(
                "block h-4 w-4 rounded-full bg-white shadow transition",
                notifications ? "translate-x-5" : "translate-x-0",
              )}
            />
          </button>
        </div>
        <div className="mt-6 flex justify-end gap-3">
          <span
            className={cn(
              "self-center text-xs font-semibold text-emerald-300",
              saved ? "opacity-100" : "opacity-0",
            )}
          >
            Saved
          </span>
          <button
            type="button"
            onClick={() => {
              setSaved(true);
              window.setTimeout(() => setSaved(false), 1500);
            }}
            className="rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d]"
          >
            Save preferences
          </button>
        </div>
      </div>
    </div>
  );
}

export default function Dashboard() {
  const navigate = useNavigate();
  const [ready, setReady] = useState(false);
  const [role] = useState<AuthRole>(() => roleFromStoredUser());
  const [view, setView] = useState<View>("overview");
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [modal, setModal] = useState<
    "structure" | "student" | "request" | "inspection" | "assignment" | null
  >(null);
  const [refreshKey, setRefreshKey] = useState(0);

  useEffect(() => {
    if (!window.sessionStorage.getItem("dwell_access_token"))
      navigate("/", { replace: true });
    else setReady(true);
  }, [navigate]);
  if (!ready) return <div className="min-h-screen bg-[#062b25]" />;

  const canManageStructure =
    role === "admin" || role === "dorm manager" || role === "proctor";
  const navItems =
    role === "student"
      ? [
          { id: "overview" as View, label: "Overview", icon: LayoutDashboard },
          { id: "room" as View, label: "My room", icon: House },
          { id: "requests" as View, label: "My requests", icon: ClipboardList },
          { id: "transfers" as View, label: "Room transfers", icon: ArrowLeftRight },
          { id: "maintenance" as View, label: "Maintenance", icon: Wrench },
          { id: "complaints" as View, label: "Complaints", icon: MessageSquareWarning },
          { id: "announcements" as View, label: "Announcements", icon: Bell },
          { id: "notifications" as View, label: "Notifications", icon: Bell },
          { id: "qr-code" as View, label: "My QR code", icon: Shield },
          { id: "settings" as View, label: "Settings", icon: Settings },
        ]
      : role === "proctor"
        ? [
            {
              id: "overview" as View,
              label: "Overview",
              icon: LayoutDashboard,
            },
            {
              id: "assignments" as View,
              label: "Room availability",
              icon: DoorOpen,
            },
            { id: "students" as View, label: "Residents", icon: UsersRound },
            { id: "requests" as View, label: "Incidents", icon: AlertTriangle },
            { id: "transfers" as View, label: "Room transfers", icon: ArrowLeftRight },
            { id: "check-in-out" as View, label: "Check-in / out", icon: DoorOpen },
            { id: "complaints" as View, label: "Complaints", icon: MessageSquareWarning },
            { id: "violations" as View, label: "Violations", icon: AlertTriangle },
            { id: "security" as View, label: "Security incidents", icon: Shield },
            {
              id: "inspections" as View,
              label: "Inspections",
              icon: ClipboardCheck,
            },
            { id: "announcements" as View, label: "Announcements", icon: Bell },
            { id: "notifications" as View, label: "Notifications", icon: Bell },
            { id: "qr-code" as View, label: "QR verification", icon: Shield },
            { id: "settings" as View, label: "Settings", icon: Settings },
          ]
        : role === "maintenance"
          ? [
              {
                id: "overview" as View,
                label: "Overview",
                icon: LayoutDashboard,
              },
              { id: "work-orders" as View, label: "Work orders", icon: Wrench },
              { id: "maintenance" as View, label: "Maintenance history", icon: Wrench },
              { id: "complaints" as View, label: "Complaints", icon: MessageSquareWarning },
              {
                id: "inspections" as View,
                label: "Inspections",
                icon: ClipboardCheck,
              },
              { id: "reports" as View, label: "Reports", icon: FileBarChart },
              { id: "settings" as View, label: "Settings", icon: Settings },
            ]
          : [
              {
                id: "overview" as View,
                label: "Overview",
                icon: LayoutDashboard,
              },
              {
                id: "structure" as View,
                label: "Campus structure",
                icon: Building2,
              },
              { id: "students" as View, label: "Students", icon: UsersRound },
              {
                id: "requests" as View,
                label: "Requests & cases",
                icon: ClipboardList,
              },
              { id: "transfers" as View, label: "Room transfers", icon: ArrowLeftRight },
              { id: "check-in-out" as View, label: "Check-in / out", icon: DoorOpen },
              { id: "maintenance" as View, label: "Maintenance", icon: Wrench },
              { id: "complaints" as View, label: "Complaints", icon: MessageSquareWarning },
              {
                id: "inspections" as View,
                label: "Inspections",
                icon: ClipboardCheck,
              },
              { id: "violations" as View, label: "Violations", icon: AlertTriangle },
              { id: "security" as View, label: "Security incidents", icon: Shield },
              { id: "announcements" as View, label: "Announcements", icon: Bell },
              { id: "notifications" as View, label: "Notifications", icon: Bell },
              { id: "reports" as View, label: "Reports", icon: FileBarChart },
              ...(role === "admin"
                ? [
                    { id: "audit" as View, label: "Audit log", icon: FileText },
                    {
                      id: "team" as View,
                      label: "Team members",
                      icon: UserRound,
                    },
                  ]
                : []),
              { id: "settings" as View, label: "Settings", icon: Settings },
            ];
  const openCreate = (
    kind: "structure" | "student" | "request" | "inspection" | "assignment",
  ) => setModal(kind);
  const refresh = () => setRefreshKey((value) => value + 1);
  const renderView = () => {
    if (view === "overview")
      return (
        <Overview
          role={role}
          onNavigate={setView}
          openCreate={(kind) => openCreate(kind)}
        />
      );
    if (view === "structure")
      return (
        <StructurePage role={role} openCreate={() => openCreate("structure")} />
      );
    if (view === "assignments")
      return (
        <RoomsAssignmentPage openCreate={() => openCreate("assignment")} />
      );
    if (view === "room") return <StudentRoomPage />;
    if (view === "students")
      return (
        <EntityPage
          title={role === "proctor" ? "Residents" : "Students"}
          description="Resident records are supplied by the Registrar service connected to your mock registrar data."
          eyebrow="Resident directory"
          resource={studentPath}
          icon={UsersRound}
          allowCreate={false}
          allowDelete={false}
          createLabel="Add student"
          openCreate={() => openCreate("student")}
        />
      );
    if (view === "requests" || view === "work-orders")
      return (
        <RequestsPage role={role} openCreate={() => openCreate("request")} />
      );
    if (view === "transfers")
      return (
        <SrsModulePage
          title="Room transfers"
          eyebrow="Accommodation changes"
          description="Submit, review, and track room-transfer requests according to the SRS workflow."
          resource={transfersPath}
          icon={ArrowLeftRight}
          emptyTitle="No room transfers returned"
          emptyBody="Transfer requests will appear here when the transfer API is available."
        />
      );
    if (view === "check-in-out")
      return (
        <SrsModulePage
          title="Check-in and check-out"
          eyebrow="Residence movement"
          description="Record arrivals, departures, reasons, and movement history for assigned residents."
          resource={checkInOutPath}
          icon={CalendarDays}
          emptyTitle="No movement records returned"
          emptyBody="Check-in and check-out history will appear here when the movement API is available."
        />
      );
    if (view === "maintenance")
      return (
        <SrsModulePage
          title="Maintenance requests"
          eyebrow="Facilities operations"
          description="Track facility issues from submission through assignment, repair, completion, and reopening."
          resource={maintenancePath}
          icon={Wrench}
          emptyTitle="No maintenance requests returned"
          emptyBody="Maintenance requests will appear here when the maintenance API is available."
        />
      );
    if (view === "complaints")
      return (
        <SrsModulePage
          title="Complaints"
          eyebrow="Resident support"
          description="Review complaint categories, confidential reports, responses, and resolution status."
          resource={complaintsPath}
          icon={MessageSquareWarning}
          emptyTitle="No complaints returned"
          emptyBody="Complaint records will appear here when the complaint API is available."
        />
      );
    if (view === "inspections")
      return (
        <EntityPage
          title="Inspections"
          description="Schedule and review room inspections returned by the backend."
          eyebrow="Quality & safety"
          resource={inspectionsPath}
          icon={ClipboardCheck}
          allowCreate={role !== "maintenance"}
          allowDelete={role === "admin" || role === "dorm manager"}
          createLabel="Schedule inspection"
          openCreate={() => openCreate("inspection")}
        />
      );
    if (view === "violations")
      return (
        <SrsModulePage
          title="Violations"
          eyebrow="Conduct and discipline"
          description="Record rule violations, evidence, warnings, penalties, and review status for authorized staff."
          resource={violationsPath}
          icon={AlertTriangle}
          emptyTitle="No violations returned"
          emptyBody="Violation records will appear here when the disciplinary API is available."
        />
      );
    if (view === "security")
      return (
        <SrsModulePage
          title="Security incidents"
          eyebrow="Safety operations"
          description="Document incidents, people involved, immediate actions, severity, and resolution status."
          resource={securityPath}
          icon={Shield}
          emptyTitle="No security incidents returned"
          emptyBody="Security incident records will appear here when the security API is available."
        />
      );
    if (view === "notifications")
      return (
        <SrsModulePage
          title="Notifications"
          eyebrow="Resident communication"
          description="Review assignment, transfer, maintenance, inspection, announcement, and security notifications."
          resource={notificationsPath}
          icon={Bell}
          emptyTitle="No notifications returned"
          emptyBody="Notifications will appear here when the notification API is available."
        />
      );
    if (view === "qr-code")
      return (
        <SrsModulePage
          title="QR identification"
          eyebrow="Identity verification"
          description="Access student QR identification and authorized verification activity for residence operations."
          resource={qrCodePath}
          icon={Shield}
          emptyTitle="No QR records returned"
          emptyBody="QR records will appear here when the QR identification API is available."
        />
      );
    if (view === "announcements")
      return (
        <SrsModulePage
          title="Announcements"
          eyebrow="Residence communication"
          description="Publish and review rules, maintenance schedules, important notices, and general information."
          resource={pathFor("VITE_ANNOUNCEMENTS_PATH", "/announcements")}
          icon={Bell}
          emptyTitle="No announcements returned"
          emptyBody="Announcements will appear here when the announcements API is available."
        />
      );
    if (view === "reports")
      return (
        <EntityPage
          title="Reports"
          description="Report records and exports should be supplied by the reporting API."
          eyebrow="Insights & exports"
          resource={pathFor("VITE_REPORTS_PATH", "/reports")}
          icon={FileBarChart}
          allowCreate={false}
          allowDelete={false}
          createLabel=""
          openCreate={() => undefined}
        />
      );
    if (view === "audit")
      return (
        <EntityPage
          title="Audit log"
          description="Review the audit events returned for your administrator account."
          eyebrow="Governance"
          resource={auditPath}
          icon={FileText}
          allowCreate={false}
          allowDelete={false}
          createLabel=""
          openCreate={() => undefined}
        />
      );
    if (view === "team")
      return (
        <EntityPage
          title="Team members"
          description="Staff accounts and role assignments returned by the backend."
          eyebrow="People & access"
          resource={pathFor("VITE_USERS_PATH", "/users")}
          icon={UserRound}
          allowCreate={role === "admin"}
          allowDelete={role === "admin"}
          createLabel="Add user"
          openCreate={() => openCreate("student")}
        />
      );
    if (view === "settings") return <SettingsPage />;
    return (
      <EmptyState
        icon={Bell}
        title="Announcements"
        body="Announcements will appear here once returned by the notification API."
      />
    );
  };
  return (
    <div className="min-h-screen bg-[#062b25] text-white">
      <aside
        className={cn(
          "fixed inset-y-0 left-0 z-40 flex w-[258px] flex-col border-r border-white/[0.08] bg-[#062b25] px-4 py-5 transition-transform duration-300 lg:translate-x-0",
          sidebarOpen ? "translate-x-0" : "-translate-x-full",
        )}
      >
        <div className="px-3">
          <BrandMark />
        </div>
        <div className="mt-9 rounded-xl border border-emerald-300/10 bg-emerald-300/[0.05] p-3">
          <p className="text-[9px] font-bold uppercase tracking-[0.18em] text-emerald-300/55">
            Authenticated account
          </p>
          <div className="mt-2 flex items-center justify-between">
            <span className="text-xs font-semibold text-emerald-50/80">
              {roleLabels[role]}
            </span>
            <Shield className="h-3.5 w-3.5 text-emerald-300" />
          </div>
        </div>
        <nav className="mt-7 flex-1 space-y-1">
          {navItems.map(({ id, label, icon: Icon }) => (
            <button
              type="button"
              key={id}
              onClick={() => {
                setView(id);
                setSidebarOpen(false);
              }}
              className={cn(
                "group flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-left text-xs font-semibold transition",
                view === id
                  ? "bg-emerald-300 text-[#07352d] shadow-[0_8px_22px_rgba(110,231,183,.13)]"
                  : "text-emerald-50/45 hover:bg-white/[0.05] hover:text-emerald-50/80",
              )}
            >
              <Icon className="h-[17px] w-[17px]" />
              <span className="flex-1">{label}</span>
            </button>
          ))}
        </nav>
        <div className="space-y-1 border-t border-white/[0.08] pt-4">
          <button
            type="button"
            onClick={() => setView("settings")}
            className="flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-xs font-semibold text-emerald-50/45 hover:bg-white/[0.05] hover:text-white"
          >
            <CircleHelp className="h-[17px] w-[17px]" />
            Help center
          </button>
          <button
            type="button"
            onClick={() => {
              window.sessionStorage.removeItem("dwell_access_token");
              window.sessionStorage.removeItem("dwell_user");
              window.sessionStorage.removeItem("dwell_role");
              navigate("/");
            }}
            className="flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-xs font-semibold text-emerald-50/45 hover:bg-white/[0.05] hover:text-white"
          >
            <LogOut className="h-[17px] w-[17px]" />
            Sign out
          </button>
        </div>
        <div className="mt-5 flex items-center gap-3 rounded-xl bg-white/[0.04] p-3">
          <span className="flex h-8 w-8 items-center justify-center rounded-full bg-blue-300/15 text-[10px] font-bold text-blue-200">
            {roleLabels[role].slice(0, 2).toUpperCase()}
          </span>
          <div className="min-w-0 flex-1">
            <p className="truncate text-xs font-semibold text-white">
              Authenticated user
            </p>
            <p className="mt-0.5 truncate text-[10px] text-emerald-50/35">
              {roleLabels[role]}
            </p>
          </div>
          <button
            type="button"
            onClick={() => setView("settings")}
            className="text-emerald-50/35 hover:text-emerald-200"
          >
            <Settings className="h-3.5 w-3.5" />
          </button>
        </div>
      </aside>
      {sidebarOpen && (
        <button
          aria-label="Close menu"
          type="button"
          onClick={() => setSidebarOpen(false)}
          className="fixed inset-0 z-30 bg-[#062b25]/70 lg:hidden"
        />
      )}
      <div className="lg:pl-[258px]">
        <header className="sticky top-0 z-20 flex h-[74px] items-center justify-between border-b border-white/[0.08] bg-[#062b25]/95 px-5 backdrop-blur-xl sm:px-8">
          <div className="flex items-center gap-3">
            <button
              type="button"
              aria-label="Open menu"
              onClick={() => setSidebarOpen(true)}
              className="rounded-lg p-2 text-emerald-50/60 hover:bg-white/[0.05] lg:hidden"
            >
              <Menu className="h-5 w-5" />
            </button>
            <div className="hidden items-center gap-2 text-xs text-emerald-50/35 sm:flex">
              <span>Dashboard</span>
              <ChevronRight className="h-3.5 w-3.5" />
              <span className="font-semibold text-emerald-50/60">
                {navItems.find((item) => item.id === view)?.label || "Overview"}
              </span>
            </div>
          </div>
          <div className="flex items-center gap-2 sm:gap-4">
            <div className="relative hidden md:block">
              <Search className="pointer-events-none absolute left-3 top-2.5 h-4 w-4 text-emerald-50/25" />
              <input
                className="h-9 w-44 rounded-lg border border-white/[0.08] bg-white/[0.035] pl-9 pr-3 text-xs text-white outline-none placeholder:text-emerald-50/25 focus:border-emerald-300/30"
                placeholder="Search records"
              />
            </div>
            <button
              type="button"
              className="relative rounded-lg p-2 text-emerald-50/55 hover:bg-white/[0.05] hover:text-white"
            >
              <Bell className="h-[18px] w-[18px]" />
              <span className="absolute right-1.5 top-1.5 h-1.5 w-1.5 rounded-full bg-blue-200 ring-2 ring-[#062b25]" />
            </button>
            <span className="hidden items-center gap-2 rounded-lg border border-emerald-300/10 bg-emerald-300/[0.05] px-3 py-2 text-[10px] font-semibold text-emerald-200 sm:flex">
              <span className="h-1.5 w-1.5 rounded-full bg-emerald-300" />
              {roleLabels[role]}
            </span>
            <button
              type="button"
              onClick={() => setView("settings")}
              className="flex h-9 w-9 items-center justify-center rounded-full bg-blue-300/15 text-[10px] font-bold text-blue-200"
            >
              {roleLabels[role].slice(0, 2).toUpperCase()}
            </button>
          </div>
        </header>
        <main
          className="mx-auto max-w-[1500px] px-5 py-7 sm:px-8 sm:py-9"
          key={refreshKey}
        >
          {renderView()}
        </main>
        <footer className="px-5 pb-7 text-center text-[10px] text-emerald-50/20 sm:px-8">
          Residence OS
        </footer>
      </div>
      {modal === "structure" && (
        <CreateStructureModal
          onClose={() => setModal(null)}
          onCreated={refresh}
        />
      )}
      {modal === "student" && (
        <CreateEntityModal
          type="student"
          onClose={() => setModal(null)}
          onCreated={refresh}
        />
      )}
      {modal === "request" && (
        <CreateEntityModal
          type="request"
          onClose={() => setModal(null)}
          onCreated={refresh}
        />
      )}
      {modal === "inspection" && (
        <CreateEntityModal
          type="inspection"
          onClose={() => setModal(null)}
          onCreated={refresh}
        />
      )}
      {modal === "assignment" && (
        <AssignmentModal onClose={() => setModal(null)} onCreated={refresh} />
      )}
    </div>
  );
}
