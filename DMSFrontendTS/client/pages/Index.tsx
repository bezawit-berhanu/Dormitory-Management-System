import { useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { ArrowRight, Building2, Check, Eye, EyeOff, LockKeyhole, Mail, ShieldCheck, Sparkles, UserRound, UsersRound } from "lucide-react";
import { dmsApi, getAuthToken, getAuthUser, type AuthRole, type StaffRegistration, type StudentRegistration } from "@/lib/api";

type AuthMode = "login" | "register";
type AccountType = "student" | "staff";

const staffRoles: AuthRole[] = ["admin", "maintenance", "proctor", "dorm manager"];

const BrandMark = ({ compact = false }: { compact?: boolean }) => (
  <div className={`flex items-center gap-3 ${compact ? "scale-90 origin-left" : ""}`}>
    <div className="relative flex h-10 w-10 items-center justify-center overflow-hidden rounded-xl bg-emerald-300 text-[#062b25] shadow-[0_10px_30px_rgba(52,211,153,.2)]">
      <span className="absolute -right-2 -top-3 h-8 w-8 rotate-45 rounded-[100%_0] border-[6px] border-[#062b25]/80" />
      <span className="absolute -bottom-3 -left-2 h-8 w-8 -rotate-45 rounded-[100%_0] border-[6px] border-[#062b25]/80" />
      <span className="relative text-sm font-black tracking-tighter">D</span>
    </div>
    <div>
      <p className="mt-1 text-[9px] font-bold uppercase tracking-[0.25em] text-emerald-300/70">Residence OS</p>
    </div>
  </div>
);

export default function Index() {
  const navigate = useNavigate();
  const [mode, setMode] = useState<AuthMode>("login");
  const [accountType, setAccountType] = useState<AccountType>("student");
  const [staffRole, setStaffRole] = useState<AuthRole>("dorm manager");
  const [showPassword, setShowPassword] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitting(true);
    setError("");
    const data = new FormData(event.currentTarget);
    const identifier = String(data.get("email") ?? "").trim();
    const password = String(data.get("password") ?? "");
    const payload = mode === "login"
      ? { identifier, password }
      : accountType === "staff"
        ? {
            accountType,
            identifier,
            employeeId: String(data.get("employeeId") ?? "").trim(),
            fullName: String(data.get("name") ?? "").trim(),
            email: identifier,
            phoneNumber: String(data.get("phoneNumber") ?? "").trim(),
            password,
            confirmPassword: String(data.get("confirmPassword") ?? ""),
          }
        : {
            accountType,
            identifier,
            studentId: String(data.get("studentId") ?? "").trim(),
            fullName: String(data.get("name") ?? "").trim(),
            email: identifier,
            phoneNumber: String(data.get("phoneNumber") ?? "").trim(),
            password,
            confirmPassword: String(data.get("confirmPassword") ?? ""),
          };

    try {
      const response = mode === "login"
        ? await dmsApi.login(payload)
        : await dmsApi.register(payload as StudentRegistration | StaffRegistration);
      const token = getAuthToken(response);
      if (!token) throw new Error("The API response did not include an access token.");
      window.sessionStorage.setItem("dwell_access_token", token);
      window.sessionStorage.setItem("dwell_identifier", identifier);
      const user = getAuthUser(response);
      if (user) {
        window.sessionStorage.setItem("dwell_user", JSON.stringify(user));
        const roleValue = String(
          (user as Record<string, unknown>).Role ??
            (user as Record<string, unknown>).role ??
            "student",
        )
          .toLowerCase()
          .trim();
        window.sessionStorage.setItem("dwell_role", roleValue);
        const campusValue = String(
          (user as Record<string, unknown>).campus ??
            (user as Record<string, unknown>).Campus ??
            "",
        );
        if (campusValue) window.sessionStorage.setItem("dwell_campus", campusValue);
        else window.sessionStorage.removeItem("dwell_campus");
      }
      navigate("/dashboard");
    } catch (requestError) {
      setError(requestError instanceof Error ? requestError.message : "Unable to connect to the residence API.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <main className="min-h-screen overflow-hidden bg-[#062b25] text-white">
      <div className="flex min-h-screen flex-col lg:flex-row">
        <section className="relative flex min-h-[580px] flex-1 flex-col justify-between overflow-hidden px-6 py-7 sm:px-10 lg:min-h-screen lg:px-16 lg:py-10 xl:px-24">
          <div className="pointer-events-none absolute inset-0 opacity-80 [background-image:radial-gradient(circle_at_20%_10%,rgba(52,211,153,.16),transparent_28%),radial-gradient(circle_at_85%_75%,rgba(59,130,246,.12),transparent_25%)]" />
          <div className="pointer-events-none absolute -left-28 top-1/2 h-[35rem] w-[35rem] rounded-full border border-emerald-300/10 [box-shadow:0_0_0_70px_rgba(52,211,153,.025),0_0_0_140px_rgba(52,211,153,.02)]" />
          <div className="pointer-events-none absolute bottom-[-18rem] right-[-10rem] h-[34rem] w-[34rem] rounded-full border border-blue-300/10" />
          <div className="relative z-10 flex items-center justify-between">
            <BrandMark />
            <div className="hidden items-center gap-2 text-xs font-medium text-emerald-100/60 sm:flex"><span className="h-2 w-2 rounded-full bg-emerald-300 shadow-[0_0_12px_#6ee7b7]" />Secure residence portal</div>
          </div>
          <div className="relative z-10 max-w-2xl py-12 lg:py-0">
            <div className="mb-7 inline-flex items-center gap-2 rounded-full border border-emerald-200/15 bg-emerald-100/[0.06] px-3 py-1.5 text-[11px] font-semibold uppercase tracking-[0.18em] text-emerald-200"><Sparkles className="h-3.5 w-3.5" />A calmer way to run residence</div>
            <h1 className="max-w-2xl font-display text-5xl font-semibold leading-[.98] tracking-[-0.07em] text-white sm:text-6xl lg:text-7xl">Make every room feel <span className="text-emerald-300">accounted for.</span></h1>
            <p className="mt-7 max-w-xl text-base leading-7 text-emerald-50/65 sm:text-lg">Manage campus operations, resident support, and everyday building work for Ethiopian Airlines residences.</p>
            <div className="mt-9 grid max-w-lg gap-3 sm:grid-cols-3 sm:gap-4">
              {[{ icon: Building2, label: "Campus structure" }, { icon: UsersRound, label: "Resident services" }, { icon: ShieldCheck, label: "Role-based access" }].map(({ icon: Icon, label }) => <div key={label} className="rounded-2xl border border-white/[0.08] bg-white/[0.035] px-4 py-4 backdrop-blur-sm"><Icon className="h-4 w-4 text-emerald-300" /><p className="mt-3 text-xs font-semibold leading-5 text-white">{label}</p><p className="mt-0.5 text-[10px] uppercase tracking-[0.14em] text-emerald-100/40">Connected to your data</p></div>)}
            </div>
          </div>
          <div className="relative z-10 hidden max-w-xl gap-4 border-t border-white/10 pt-6 md:grid md:grid-cols-3">
            {["Manage campuses, rooms, and beds", "Support residents from request to resolution", "Keep every action accountable"].map((benefit, index) => <div key={benefit} className="flex gap-2 text-xs leading-5 text-emerald-50/55"><span className="mt-0.5 flex h-4 w-4 shrink-0 items-center justify-center rounded-full bg-emerald-300/15 text-emerald-300"><Check className="h-2.5 w-2.5" /></span><span><span className="mr-1 text-emerald-300/50">0{index + 1}</span>{benefit}</span></div>)}
          </div>
        </section>

        <section className="relative flex w-full items-center justify-center bg-[#f3efe5] px-5 py-10 text-[#143832] sm:px-10 lg:w-[46%] lg:max-w-[690px] lg:px-14 xl:px-20">
          <div className="absolute right-0 top-0 h-full w-2 bg-emerald-300/80" />
          <div className="w-full max-w-md">
            <div className="mb-10 flex items-center justify-between lg:hidden"><BrandMark compact /><span className="text-[11px] font-semibold uppercase tracking-[0.18em] text-[#6f827a]">Secure portal</span></div>
            <div className="mb-8"><p className="mb-3 text-[11px] font-bold uppercase tracking-[0.2em] text-[#87958d]">{mode === "login" ? "Welcome back" : "Create your account"}</p><h2 className="font-display text-4xl font-semibold tracking-[-0.06em] text-[#143832]">{mode === "login" ? "Sign in" : "Join your residence"}</h2><p className="mt-3 text-sm leading-6 text-[#63766e]">{mode === "login" ? "Use the credentials from your residence system to continue." : "Choose your account type. Staff access is assigned by role and campus."}</p></div>
            <form onSubmit={handleSubmit} className="space-y-5">
              {mode === "register" && <><div className="grid grid-cols-2 gap-2 rounded-xl bg-[#e5ebe4] p-1"><button type="button" onClick={() => setAccountType("student")} className={`rounded-lg py-2.5 text-xs font-bold transition ${accountType === "student" ? "bg-white text-[#0d594a] shadow-sm" : "text-[#75877e]"}`}><UserRound className="mr-1.5 inline h-3.5 w-3.5" />Student</button><button type="button" onClick={() => setAccountType("staff")} className={`rounded-lg py-2.5 text-xs font-bold transition ${accountType === "staff" ? "bg-white text-[#0d594a] shadow-sm" : "text-[#75877e]"}`}><ShieldCheck className="mr-1.5 inline h-3.5 w-3.5" />Staff</button></div><label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Full name</span><div className="relative"><UsersRound className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required name="name" className="field" placeholder="e.g. Bezawit Berhanu" /></div></label>{accountType === "staff" ? <><label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Employee ID</span><input required name="employeeId" className="field" placeholder="e.g. EMP-4K-001" /></label><label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Staff role</span><select name="role" value={staffRole} onChange={(event) => setStaffRole(event.target.value as AuthRole)} className="field"><option value="admin">Admin</option><option value="maintenance">Maintenance</option><option value="proctor">Proctor</option><option value="dorm manager">Dorm manager</option></select></label></> : <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Student ID</span><input required name="studentId" className="field" placeholder="e.g. AAU20240001" /></label>}<label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Phone number</span><input required name="phoneNumber" type="tel" className="field" placeholder="e.g. 0911000000" /></label></>}
              <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">University email or ID</span><div className="relative"><Mail className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required name="email" type={mode === "login" ? "text" : "email"} className="field pl-10" placeholder={mode === "login" ? "you@ethiopianairlines.com or student ID" : "name@organization.com"} /></div></label>
              <label className="block"><div className="mb-2 flex items-center justify-between"><span className="text-xs font-semibold text-[#3e574e]">Password</span>{mode === "login" && <button type="button" onClick={() => navigate("/forgot-password")} className="text-xs font-semibold text-[#17856b] hover:text-[#10634f]">Forgot password?</button>}</div><div className="relative"><LockKeyhole className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required name="password" type={showPassword ? "text" : "password"} minLength={mode === "register" ? 8 : undefined} className="field pl-10 pr-11" placeholder="Enter your password" /><button type="button" aria-label="Toggle password visibility" onClick={() => setShowPassword((value) => !value)} className="absolute right-3 top-3 text-[#93a39b] hover:text-[#41685d]">{showPassword ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}</button></div>{mode === "register" && <p className="mt-2 text-[11px] text-[#63766e]">Use 8+ characters with uppercase, lowercase, a number, and a symbol.</p>}</label>
              {mode === "register" && <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Confirm password</span><div className="relative"><LockKeyhole className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required name="confirmPassword" type="password" className="field pl-10" placeholder="Repeat your password" /></div></label>}
              {mode === "register" && <label className="flex items-start gap-2 text-xs leading-5 text-[#63766e]"><input type="checkbox" required className="mt-1 accent-[#17856b]" />I agree to the residence data and privacy guidelines.</label>}
              {error && <div role="alert" className="rounded-xl border border-rose-200 bg-rose-50 px-3.5 py-3 text-xs leading-5 text-rose-700">{error}</div>}
              <button type="submit" disabled={submitting} className="group flex h-12 w-full items-center justify-center gap-2 rounded-xl bg-[#0d594a] text-sm font-semibold text-white shadow-[0_10px_25px_rgba(13,89,74,.18)] transition hover:bg-[#0a473c] disabled:cursor-wait disabled:opacity-70">{submitting ? "Connecting..." : mode === "login" ? "Continue" : "Create account"}{!submitting && <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />}</button>
            </form>
            <div className="my-8 flex items-center gap-3"><div className="h-px flex-1 bg-[#d8ddd5]" /><span className="text-[10px] font-bold uppercase tracking-[0.18em] text-[#a0aca4]">secure access</span><div className="h-px flex-1 bg-[#d8ddd5]" /></div>
            <p className="text-center text-sm text-[#718179]">{mode === "login" ? "Need an account?" : "Already have an account?"} <button type="button" onClick={() => { setMode(mode === "login" ? "register" : "login"); setError(""); }} className="font-semibold text-[#17856b] hover:underline">{mode === "login" ? "Create an account" : "Sign in instead"}</button></p>
            <p className="mt-10 flex items-center justify-center gap-1.5 text-[11px] text-[#9ba7a0]"><ShieldCheck className="h-3.5 w-3.5" />Protected by organization-grade access controls</p>
          </div>
        </section>
      </div>
    </main>
  );
}
