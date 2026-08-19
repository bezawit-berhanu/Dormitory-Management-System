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
  forgotPassword: (email: string) => request<{ message: string }>(endpoint("VITE_FORGOT_PASSWORD_PATH", "/password/forgot"), { method: "POST", body: JSON.stringify({ email }) }),
  resetPassword: (payload: { token: string; newPassword: string; confirmPassword: string }) => request<{ message: string }>(endpoint("VITE_RESET_PASSWORD_PATH", "/password/reset"), { method: "POST", body: JSON.stringify(payload) }),
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
