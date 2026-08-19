import { useEffect, useState } from "react";
import { dmsApi, type AuthRole } from "@/lib/api";

type RecordMap = Record<string, unknown>;

export function roleFromStoredUser(): AuthRole {
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
    // The helper still uses the authenticated session when profile metadata is unavailable.
  }
  return "student";
}

export function text(record: RecordMap, ...keys: string[]): string {
  for (const key of keys) {
    const value = record[key];
    if (value !== undefined && value !== null && String(value).trim())
      return String(value);
  }
  return "";
}

export function recordId(record: RecordMap): string {
  return text(
    record,
    "id",
    "Id",
    "ID",
    "sId",
    "SId",
    "studentId",
    "studentID",
    "roomId",
    "roomID",
    "roomAssignmentId",
    "RoomAssignmentId",
    "checkInId",
    "CheckInId",
    "checkOutId",
    "CheckOutId",
  );
}

export function storedUser(): RecordMap {
  try {
    return (
      (JSON.parse(
        window.sessionStorage.getItem("dwell_user") ?? "null",
      ) as RecordMap) ?? {}
    );
  } catch {
    return {};
  }
}

export function storedUserId(): string {
  return text(storedUser(), "userId", "UserId", "id", "Id");
}

export function storedCampus(): string {
  const direct = window.sessionStorage.getItem("dwell_campus");
  if (direct) return direct;
  const haystack = text(storedUser(), "email", "Email", "identifier").toLowerCase();
  if (haystack.includes("4kilo")) return "4kilo";
  if (haystack.includes("6kilo") || haystack.includes("six kilos")) return "6kilo";
  return "";
}

export function useRemoteJson<T>(resource: string | null) {
  const [records, setRecords] = useState<T[]>([]);
  const [loading, setLoading] = useState(Boolean(resource));
  const [error, setError] = useState("");
  const refresh = async () => {
    if (!resource) return;
    setLoading(true);
    setError("");
    try {
      setRecords(await dmsApi.list<T>(resource));
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
  };
  useEffect(() => {
    void refresh();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [resource]);
  return { records, setRecords, loading, error, refresh };
}

export function formatDateTime(value?: string | null): string {
  if (!value) return "—";
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) return String(value);
  return date.toLocaleString(undefined, {
    year: "numeric",
    month: "short",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });
}

export function toLocalInputValue(date = new Date()): string {
  const local = new Date(date.getTime() - date.getTimezoneOffset() * 60000);
  return local.toISOString().slice(0, 16);
}
