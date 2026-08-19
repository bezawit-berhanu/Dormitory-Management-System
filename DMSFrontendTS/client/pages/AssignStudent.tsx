import { useEffect, useMemo, useState, type FormEvent, type ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import {
  AlertTriangle,
  ArrowLeft,
  BedDouble,
  Building2,
  Layers,
  Plus,
  RefreshCw,
  ShieldX,
  UserRound,
  X,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { dmsApi, type RoomAssignmentRecord } from "@/lib/api";
import {
  roleFromStoredUser,
  storedUserId,
  text,
} from "@/lib/dms-helpers";

type Option = Record<string, unknown>;

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

export default function AssignStudent() {
  const navigate = useNavigate();
  const [role] = useState(() => roleFromStoredUser());

  const [blocks, setBlocks] = useState<Option[]>([]);
  const [floors, setFloors] = useState<Option[]>([]);
  const [rooms, setRooms] = useState<Option[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const [blockId, setBlockId] = useState("");
  const [floorId, setFloorId] = useState("");
  const [roomId, setRoomId] = useState("");
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    if (!window.sessionStorage.getItem("dwell_access_token"))
      navigate("/", { replace: true });
    if (role !== "admin" && role !== "dorm manager" && role !== "proctor")
      navigate("/dashboard", { replace: true });
    void loadStructure();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [role]);

  const loadStructure = async () => {
    setLoading(true);
    setError("");
    try {
      const [b, f, r] = await Promise.all([
        dmsApi.list<Option>("/blocks"),
        dmsApi.list<Option>("/floors"),
        dmsApi.list<Option>("/rooms"),
      ]);
      setBlocks(b);
      setFloors(f);
      setRooms(r);
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to load the campus structure.",
      );
    } finally {
      setLoading(false);
    }
  };

  const floorsForBlock = useMemo(
    () =>
      blockId
        ? floors.filter(
            (f) => text(f, "blockId", "BlockId") === blockId,
          )
        : floors,
    [floors, blockId],
  );

  const roomsForFloor = useMemo(
    () =>
      floorId
        ? rooms.filter((r) => text(r, "floorId", "FloorId") === floorId)
        : rooms,
    [rooms, floorId],
  );

  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError("");
    setSuccess("");
    setSubmitting(true);
    const data = new FormData(event.currentTarget);
    const payload = {
      RoomAssignmentId: 0,
      StudentId: String(data.get("studentId") ?? "").trim(),
      RoomId: Number(roomId || data.get("roomId")),
      BedId: Number(data.get("bedId")),
      StudentName: String(data.get("studentName") ?? "").trim(),
      AssignedDate: new Date().toISOString(),
      AssignedByUserId: Number(storedUserId() || 1),
      Status: "Active",
    };
    try {
      await dmsApi.roomAssignment.create(payload);
      setSuccess("Student assigned to the dormitory successfully.");
      setBlockId("");
      setFloorId("");
      setRoomId("");
      (event.target as HTMLFormElement).reset();
    } catch (requestError) {
      setError(
        requestError instanceof Error
          ? requestError.message
          : "Unable to assign the student.",
      );
    } finally {
      setSubmitting(false);
    }
  };

  if (role === "maintenance" || role === "student") {
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
            Only proctors and managers can assign students to dormitories.
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
      <div className="mx-auto max-w-[760px]">
        <div className="mb-7 flex items-end justify-between gap-4">
          <div>
            <p className="mb-1.5 text-[10px] font-bold uppercase tracking-[0.2em] text-emerald-300/55">
              Dormitory allocation
            </p>
            <h1 className="font-display text-3xl font-semibold tracking-[-0.06em]">
              Assign student to dormitory
            </h1>
            <p className="mt-2 max-w-2xl text-sm text-emerald-50/45">
              Enter the resident details, then choose the block, floor, and room
              for their allocation.
            </p>
          </div>
          <button
            type="button"
            onClick={() => navigate("/students")}
            className="hidden items-center gap-2 rounded-xl border border-white/10 bg-white/[0.06] px-4 py-2.5 text-xs font-semibold text-emerald-50/80 hover:bg-white/10 sm:flex"
          >
            <ArrowLeft className="h-4 w-4" />
            Students
          </button>
        </div>

        {error && <ApiNotice message={error} />}
        {success && (
          <div className="mb-5 rounded-xl border border-emerald-300/20 bg-emerald-300/[0.08] px-4 py-3 text-xs leading-5 text-emerald-100">
            {success}
          </div>
        )}

        <div className="rounded-2xl border border-white/[0.08] bg-white/[0.035] p-5 sm:p-6">
          {loading ? (
            <div className="flex items-center justify-center py-16 text-sm text-emerald-50/45">
              <RefreshCw className="mr-2 h-4 w-4 animate-spin" />
              Loading campus structure…
            </div>
          ) : (
            <form onSubmit={submit} className="space-y-5">
              <div className="grid gap-4 sm:grid-cols-2">
                <label className="block">
                  <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                    Student ID
                  </span>
                  <input
                    required
                    name="studentId"
                    className="field"
                    placeholder="e.g. AAU20240001"
                  />
                </label>
                <label className="block">
                  <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                    Student name
                  </span>
                  <input
                    required
                    name="studentName"
                    className="field"
                    placeholder="Full name"
                  />
                </label>
              </div>

              <div className="grid gap-4 sm:grid-cols-3">
                <label className="block">
                  <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                    Block
                  </span>
                  <select
                    name="blockId"
                    value={blockId}
                    onChange={(event) => {
                      setBlockId(event.target.value);
                      setFloorId("");
                      setRoomId("");
                    }}
                    className="field"
                    required
                  >
                    <option value="">Select block</option>
                    {blocks.map((b) => (
                      <option key={text(b, "id", "Id", "blockId", "BlockId")} value={text(b, "id", "Id", "blockId", "BlockId")}>
                        {text(b, "name", "Name", "blockName", "BlockName") ||
                          `Block ${text(b, "id", "Id", "blockId", "BlockId")}`}
                      </option>
                    ))}
                  </select>
                </label>
                <label className="block">
                  <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                    Floor
                  </span>
                  <select
                    name="floorId"
                    value={floorId}
                    onChange={(event) => {
                      setFloorId(event.target.value);
                      setRoomId("");
                    }}
                    className="field"
                    required
                    disabled={!blockId && floorsForBlock.length === 0}
                  >
                    <option value="">Select floor</option>
                    {floorsForBlock.map((f) => (
                      <option key={text(f, "id", "Id", "floorId", "FloorId")} value={text(f, "id", "Id", "floorId", "FloorId")}>
                        {text(f, "name", "Name", "floorName", "FloorName") ||
                          `Floor ${text(f, "id", "Id", "floorId", "FloorId")}`}
                      </option>
                    ))}
                  </select>
                </label>
                <label className="block">
                  <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                    Room
                  </span>
                  <select
                    name="roomId"
                    value={roomId}
                    onChange={(event) => setRoomId(event.target.value)}
                    className="field"
                    required
                    disabled={!floorId && roomsForFloor.length === 0}
                  >
                    <option value="">Select room</option>
                    {roomsForFloor.map((r) => (
                      <option key={text(r, "id", "Id", "roomId", "RoomId")} value={text(r, "id", "Id", "roomId", "RoomId")}>
                        {text(r, "name", "Name", "roomName", "RoomNumber", "roomNumber") ||
                          `Room ${text(r, "id", "Id", "roomId", "RoomId")}`}
                      </option>
                    ))}
                  </select>
                </label>
              </div>

              <label className="block">
                <span className="mb-2 block text-xs font-semibold text-[#3e574e]">
                  Bed ID
                </span>
                <input
                  required
                  name="bedId"
                  type="number"
                  min="1"
                  className="field"
                  placeholder="Bed number"
                />
              </label>

              <div className="flex justify-end gap-2 pt-2">
                <button
                  type="submit"
                  disabled={submitting}
                  className="inline-flex items-center gap-2 rounded-xl bg-emerald-300 px-4 py-2.5 text-xs font-bold text-[#07352d] hover:bg-emerald-200 disabled:opacity-60"
                >
                  <Plus className="h-4 w-4" />
                  {submitting ? "Assigning…" : "Assign student"}
                </button>
              </div>
            </form>
          )}
        </div>

        <div className="mt-5 grid gap-3 sm:grid-cols-3">
          <div className="flex items-center gap-3 rounded-xl border border-white/[0.08] bg-white/[0.035] p-4">
            <Building2 className="h-5 w-5 text-emerald-300" />
            <div>
              <p className="text-xs font-semibold text-white">{blocks.length}</p>
              <p className="text-[10px] text-emerald-50/40">Blocks loaded</p>
            </div>
          </div>
          <div className="flex items-center gap-3 rounded-xl border border-white/[0.08] bg-white/[0.035] p-4">
            <Layers className="h-5 w-5 text-emerald-300" />
            <div>
              <p className="text-xs font-semibold text-white">{floors.length}</p>
              <p className="text-[10px] text-emerald-50/40">Floors loaded</p>
            </div>
          </div>
          <div className="flex items-center gap-3 rounded-xl border border-white/[0.08] bg-white/[0.035] p-4">
            <BedDouble className="h-5 w-5 text-emerald-300" />
            <div>
              <p className="text-xs font-semibold text-white">{rooms.length}</p>
              <p className="text-[10px] text-emerald-50/40">Rooms loaded</p>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
