import type { FormEvent } from "react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const RegisterPage = () => {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      await register(email, userName, password);
      navigate("/challenges");
    } catch (err) {
      console.error(err);
      setError("Could not register. Try a different email/username.");
    }
  };

  return (
    <div className="mx-auto max-w-md space-y-6">
      <div className="space-y-2 text-center">
        <p className="text-sm uppercase tracking-[0.2em] text-primary">Start free</p>
        <h1 className="text-3xl font-bold">Create your Codebuddy account</h1>
        <p className="text-sm text-muted">Beginner-friendly practice with optional buddy sessions.</p>
      </div>
      <form onSubmit={handleSubmit} className="card space-y-4">
        {error && <div className="rounded-lg border border-rose-500/40 bg-rose-500/10 px-3 py-2 text-sm">{error}</div>}
        <label className="space-y-2 text-sm">
          <span className="text-muted">Email</span>
          <input
            className="w-full rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none transition focus:border-primary"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            type="email"
            required
          />
        </label>
        <label className="space-y-2 text-sm">
          <span className="text-muted">Username</span>
          <input
            className="w-full rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none transition focus:border-primary"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            required
          />
        </label>
        <label className="space-y-2 text-sm">
          <span className="text-muted">Password</span>
          <input
            className="w-full rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none transition focus:border-primary"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            required
          />
        </label>
        <button
          type="submit"
          className="w-full rounded-lg bg-gradient-to-r from-primary to-accent px-4 py-2 font-semibold text-slate-900 shadow-lg shadow-primary/30 transition hover:shadow-accent/30"
        >
          Register
        </button>
        <p className="text-center text-sm text-muted">
          Already joined?{" "}
          <Link className="text-primary hover:underline" to="/login">
            Login
          </Link>
        </p>
      </form>
    </div>
  );
};

export default RegisterPage;
