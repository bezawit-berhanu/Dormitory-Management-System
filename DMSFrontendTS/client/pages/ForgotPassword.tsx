import { useState, type FormEvent } from "react";
import { ArrowLeft, ArrowRight, Mail, ShieldCheck } from "lucide-react";
import { Link } from "react-router-dom";
import { dmsApi } from "@/lib/api";

export default function ForgotPassword() {
  const [email, setEmail] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitting(true);
    setError("");
    try {
      await dmsApi.forgotPassword(email.trim());
      setSubmitted(true);
    } catch (requestError) {
      setError(requestError instanceof Error ? requestError.message : "Unable to send the reset email.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <main className="min-h-screen bg-[#062b25] text-white">
      <div className="mx-auto flex min-h-screen w-full max-w-6xl items-center justify-center px-5 py-10 sm:px-10">
        <section className="grid w-full overflow-hidden rounded-3xl border border-white/[0.08] bg-[#f3efe5] text-[#143832] shadow-2xl lg:grid-cols-[.85fr_1.15fr]">
          <div className="relative hidden min-h-[620px] overflow-hidden bg-[#0a4439] p-10 text-white lg:flex lg:flex-col lg:justify-between">
            <div className="absolute -left-28 top-1/2 h-[32rem] w-[32rem] rounded-full border border-emerald-300/10 [box-shadow:0_0_0_70px_rgba(52,211,153,.025),0_0_0_140px_rgba(52,211,153,.02)]" />
            <div className="relative">
              <p className="font-display text-2xl font-semibold tracking-[-0.06em]">Dwell</p>
              <p className="mt-1 text-[9px] font-bold uppercase tracking-[0.25em] text-emerald-300/70">Residence OS</p>
            </div>
            <div className="relative max-w-sm">
              <ShieldCheck className="h-8 w-8 text-emerald-300" />
              <h1 className="mt-6 font-display text-4xl font-semibold leading-[1.02] tracking-[-0.07em]">A secure way back into your workspace.</h1>
              <p className="mt-5 text-sm leading-6 text-emerald-50/60">We will send a time-limited link to the email address connected to your residence account.</p>
            </div>
            <p className="relative text-[10px] uppercase tracking-[0.18em] text-emerald-100/35">Organization-grade access controls</p>
          </div>
          <div className="flex min-h-[620px] items-center px-6 py-10 sm:px-12 lg:px-16">
            <div className="w-full max-w-md">
              <Link to="/" className="inline-flex items-center gap-2 text-xs font-semibold text-[#718179] transition hover:text-[#17856b]"><ArrowLeft className="h-4 w-4" />Back to sign in</Link>
              <div className="mt-12">
                <p className="mb-3 text-[11px] font-bold uppercase tracking-[0.2em] text-[#87958d]">Account recovery</p>
                <h2 className="font-display text-4xl font-semibold tracking-[-0.06em]">Forgot your password?</h2>
                <p className="mt-3 text-sm leading-6 text-[#63766e]">Enter your organization email and we will send instructions to reset your password.</p>
              </div>
              {submitted ? (
                <div className="mt-8 rounded-2xl border border-emerald-200 bg-emerald-50 px-4 py-4 text-sm leading-6 text-emerald-800">If an account exists for that email, a reset link has been sent. Check your inbox and spam folder.</div>
              ) : (
                <form onSubmit={handleSubmit} className="mt-8 space-y-5">
                  <label className="block"><span className="mb-2 block text-xs font-semibold text-[#3e574e]">University email</span><div className="relative"><Mail className="pointer-events-none absolute left-3.5 top-3.5 h-4 w-4 text-[#93a39b]" /><input required type="email" value={email} onChange={(event) => setEmail(event.target.value)} className="field pl-10" placeholder="you@organization.com" /></div></label>
                  {error && <div role="alert" className="rounded-xl border border-rose-200 bg-rose-50 px-3.5 py-3 text-xs leading-5 text-rose-700">{error}</div>}
                  <button type="submit" disabled={submitting} className="group flex h-12 w-full items-center justify-center gap-2 rounded-xl bg-[#0d594a] text-sm font-semibold text-white shadow-[0_10px_25px_rgba(13,89,74,.18)] transition hover:bg-[#0a473c] disabled:cursor-wait disabled:opacity-70">{submitting ? "Sending..." : "Send reset link"}{!submitting && <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />}</button>
                </form>
              )}
              {submitted && <Link to="/" className="mt-6 flex h-12 w-full items-center justify-center rounded-xl bg-[#0d594a] text-sm font-semibold text-white transition hover:bg-[#0a473c]">Return to sign in</Link>}
              <p className="mt-10 flex items-center justify-center gap-1.5 text-[11px] text-[#9ba7a0]"><ShieldCheck className="h-3.5 w-3.5" />Reset links expire after 30 minutes</p>
            </div>
          </div>
        </section>
      </div>
    </main>
  );
}
