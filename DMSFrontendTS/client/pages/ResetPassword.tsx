import { useState, type FormEvent } from "react";
import { ArrowLeft, ArrowRight, LockKeyhole, ShieldCheck } from "lucide-react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { dmsApi } from "@/lib/api";

export default function ResetPassword() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");
  const [completed, setCompleted] = useState(false);
  const token = searchParams.get("token") ?? "";

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!token) {
      setError("This reset link is missing its token.");
      return;
    }
    setSubmitting(true);
    setError("");
    try {
      await dmsApi.resetPassword({ token, newPassword, confirmPassword });
      setCompleted(true);
    } catch (requestError) {
      setError(requestError instanceof Error ? requestError.message : "Unable to reset your password.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <main className="min-h-screen bg-[#062b25] text-white">
      <div className="mx-auto flex min-h-screen w-full max-w-3xl items-center justify-center px-5 py-10 sm:px-10">
        <section className="w-full rounded-3xl bg-[#f3efe5] px-6 py-10 text-[#143832] shadow-2xl sm:px-14 sm:py-14">
          <Link to="/" className="inline-flex items-center gap-2 text-xs font-semibold text-[#718179] transition hover:text-[#17856b]"><ArrowLeft className="h-4 w-4" />Back to sign in</Link>
          <div className="mx-auto mt-12 max-w-md">
            <div className="flex h-12 w-12 items-center justify-center rounded-2xl bg-[#dff0e3] text-[#0d7359]"><LockKeyhole className="h-5 w-5" /></div>
            <p className="mb-3 mt-7 text-[11px] font-bold uppercase tracking-[0.2em] text-[#87958d]">Secure account recovery</p>
            <h1 className="font-display text-4xl font-semibold tracking-[-0.06em]">Create a new password</h1>
            <p className="mt-3 text-sm leading-6 text-[#63766e]">Use at least 8 characters, then confirm the new password below.</p>
            {completed ? (
              <div className="mt-8 space-y-5"><div className="rounded-2xl border border-emerald-200 bg-emerald-50 px-4 py-4 text-sm leading-6 text-emerald-800">Your password has been reset successfully.</div><button type="button" onClick={() => navigate("/")} className="group flex h-12 w-full items-center justify-center gap-2 rounded-xl bg-[#0d594a] text-sm font-semibold text-white transition hover:bg-[#0a473c]">Continue to sign in<ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" /></button></div>
            ) : (
              <form onSubmit={handleSubmit} className="mt-8 space-y-5">
                <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">New password</span><div className="relative"><LockKeyhole className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required minLength={8} type="password" value={newPassword} onChange={(event) => setNewPassword(event.target.value)} className="field pl-10" placeholder="Enter a new password" /></div></label>
                <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">Confirm password</span><div className="relative"><LockKeyhole className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required minLength={8} type="password" value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)} className="field pl-10" placeholder="Repeat your new password" /></div></label>
                {error && <div role="alert" className="rounded-xl border border-rose-200 bg-rose-50 px-3.5 py-3 text-xs leading-5 text-rose-700">{error}</div>}
                <button type="submit" disabled={submitting || !token} className="group flex h-12 w-full items-center justify-center gap-2 rounded-xl bg-[#0d594a] text-sm font-semibold text-white transition hover:bg-[#0a473c] disabled:cursor-wait disabled:opacity-70">{submitting ? "Updating..." : "Reset password"}{!submitting && <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />}</button>
              </form>
            )}
            <p className="mt-10 flex items-center justify-center gap-1.5 text-[11px] text-[#9ba7a0]"><ShieldCheck className="h-3.5 w-3.5" />Your reset link is private and time-limited</p>
          </div>
        </section>
      </div>
    </main>
  );
}
