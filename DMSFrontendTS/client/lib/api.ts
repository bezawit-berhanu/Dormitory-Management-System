export type AuthRole = "admin" | "maintenance" | "proctor" | "dorm manager" | "student";

export type AuthCredentials = {
  identifier: string;
  password: string;
};

export type StudentRegistration = AuthCredentials & {
  accountType: "student";
  studentId: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  confirmPassword: string;
};

export type StaffRegistration = AuthCredentials & {
  accountType: "staff";
  employeeId: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  confirmPassword: string;
};

export type AuthUser = {
  id?: string;
  userId?: string | number;
  name?: string;
  fullName?: string;
  email?: string;
  role?: string;
  campus?: string;
};

export type AuthResponse = {
  token?: string;
  accessToken?: string;
  user?: AuthUser | { token?: string; user?: AuthUser };
};

const apiRoot = (import.meta.env.VITE_API_BASE_URL ?? "/api").replace(/\/$/, "");

const endpoint = (name: string, fallback: string) => import.meta.env[name] ?? fallback;

async function request<T>(path: string, init: RequestInit = {}): Promise<T> {
  const token = window.sessionStorage.getItem("dwell_access_token");
  const headers = new Headers(init.headers);
  headers.set("Accept", "application/json");
  if (init.body && !headers.has("Content-Type")) headers.set("Content-Type", "application/json");
  if (token) headers.set("Authorization", `Bearer ${token}`);

  const response = await fetch(`${apiRoot}${path.startsWith("/") ? path : `/${path}`}`, { ...init, headers });
  const text = await response.text();
  let body: unknown = undefined;
  if (text) {
    try {
      body = JSON.parse(text);
    } catch {
      body = text;
    }
  }
  if (!response.ok) {
    let message = `Request failed with status ${response.status}`;
    if (typeof body === "object" && body !== null) {
      if ("message" in body) message = String(body.message);
      else if ("detail" in body) message = String(body.detail);
      else if ("title" in body) message = String(body.title);
      else if ("errors" in body && typeof body.errors === "object" && body.errors !== null) {
        message = Object.values(body.errors).flat().map(String).join(" ");
      }
    }
    throw new Error(message);
  }
  return body as T;
}

export const dmsApi = {
  login: (payload: AuthCredentials) => request<AuthResponse>(endpoint("VITE_AUTH_LOGIN_PATH", "/Authentication/login"), { method: "POST", body: JSON.stringify(payload) }),
  forgotPassword: (email: string) => request<{ message: string }>(endpoint("VITE_FORGOT_PASSWORD_PATH", "/password/forgot"), { method: "POST", body: JSON.stringify({ Email: email }) }),
  resetPassword: (payload: { token: string; newPassword: string; confirmPassword: string }) => request<{ message: string }>(endpoint("VITE_RESET_PASSWORD_PATH", "/password/reset"), { method: "POST", body: JSON.stringify({ Token: payload.token, NewPassword: payload.newPassword, ConfirmPassword: payload.confirmPassword }) }),
  register: (payload: StudentRegistration | StaffRegistration) => {
    const path = payload.accountType === "staff"
      ? endpoint("VITE_STAFF_AUTH_REGISTER_PATH", "/staff-authentication/register")
      : endpoint("VITE_AUTH_REGISTER_PATH", "/Authentication/register");
    const { accountType: _accountType, identifier: _identifier, ...requestPayload } = payload;
    return request<AuthResponse>(path, { method: "POST", body: JSON.stringify(requestPayload) });
  },
  list: async <T>(resource: string) => {
    const response = await request<unknown>(resource);
    if (Array.isArray(response)) return response as T[];
    if (response && typeof response === "object" && "data" in response && Array.isArray(response.data)) return response.data as T[];
    return [];
  },
  create: <T>(resource: string, payload: unknown) => request<T>(resource, { method: "POST", body: JSON.stringify(payload) }),
  remove: (resource: string) => request<void>(resource, { method: "DELETE" }),
  students: {
    list: <T = unknown>() => request<T[]>(`/Student`),
  },
  checkIn: {
    history: <T = unknown>(studentId: string | number) =>
      request<T[]>(`/CheckIn/history/${encodeURIComponent(String(studentId))}`),
    create: <T = unknown>(payload: unknown) =>
      request<T>(`/CheckIn`, { method: "POST", body: JSON.stringify(payload) }),
  },
  checkOut: {
    history: <T = unknown>(studentId: string | number) =>
      request<T[]>(`/CheckOut/student/${encodeURIComponent(String(studentId))}`),
    create: <T = unknown>(payload: unknown) =>
      request<T>(`/CheckOut`, { method: "POST", body: JSON.stringify(payload) }),
    update: <T = unknown>(id: string | number, payload: unknown) =>
      request<T>(`/CheckOut/${encodeURIComponent(String(id))}`, { method: "PUT", body: JSON.stringify(payload) }),
    remove: (id: string | number) =>
      request<void>(`/CheckOut/${encodeURIComponent(String(id))}`, { method: "DELETE" }),
  },
  roomAssignment: {
    student: <T = unknown>(sId: string | number) =>
      request<T[]>(`/RoomAssignment/student/${encodeURIComponent(String(sId))}`),
    create: <T = unknown>(payload: unknown) =>
      request<T>(`/RoomAssignment`, { method: "POST", body: JSON.stringify(payload) }),
    update: <T = unknown>(id: string | number, payload: unknown) =>
      request<T>(`/RoomAssignment/${encodeURIComponent(String(id))}`, { method: "PUT", body: JSON.stringify(payload) }),
    remove: (id: string | number) =>
      request<void>(`/RoomAssignment/${encodeURIComponent(String(id))}`, { method: "DELETE" }),
  },
};

export type DmsStudent = {
  sId?: number | string;
  SId?: number | string;
  id?: number | string;
  name?: string;
  Name?: string;
  fullName?: string;
  studentId?: string;
  StudentId?: string;
  email?: string;
  Email?: string;
  departmentId?: number;
  DepartmentId?: number;
  gender?: string;
  Gender?: string;
  yearOfStudy?: number;
  YearOfStudy?: number;
  status?: string;
  Status?: string;
};

export type CheckInHistory = {
  checkInId?: number;
  CheckInId?: number;
  studentId?: number;
  StudentId?: number;
  studentName?: string;
  StudentName?: string;
  roomAssignmentId?: number;
  RoomAssignmentId?: number;
  roomNumber?: string;
  RoomNumber?: string;
  dormitoryName?: string;
  DormitoryName?: string;
  checkInDate?: string;
  CheckInDate?: string;
  checkOutDate?: string | null;
  CheckOutDate?: string | null;
  status?: string;
  Status?: string;
  checkedInBy?: string;
  CheckedInBy?: string;
};

export type CheckOutRecord = {
  checkOutId?: number;
  CheckOutId?: number;
  roomAssignmentId?: number;
  RoomAssignmentId?: number;
  checkOutDate?: string;
  CheckOutDate?: string;
  reason?: string;
  Reason?: string;
};

export type RoomAssignmentRecord = {
  roomAssignmentId?: number;
  RoomAssignmentId?: number;
  studentId?: string;
  StudentId?: string;
  roomId?: number;
  RoomId?: number;
  bedId?: number;
  BedId?: number;
  studentName?: string;
  StudentName?: string;
  assignedDate?: string;
  AssignedDate?: string;
  assignedByUserId?: number;
  AssignedByUserId?: number;
  status?: string;
  Status?: string;
};

export function getAuthToken(response: AuthResponse) {
  return response.accessToken ?? response.token;
}

export function getAuthUser(response: AuthResponse): AuthUser | undefined {
  const user = response.user;
  if (!user) return undefined;
  if ("user" in user) return user.user;
  return user as AuthUser;
}
