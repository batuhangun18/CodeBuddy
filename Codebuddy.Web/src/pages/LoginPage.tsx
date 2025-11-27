import type { FormEvent } from "react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const LoginPage = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      await login(email, password);
      navigate("/challenges");
    } catch (err) {
      console.error(err);
      setError("Could not login. Check your credentials.");
    }
  };

  return (
    <div className="mx-auto max-w-md space-y-6">
      <div className="space-y-2 text-center">
        <p className="text-sm uppercase tracking-[0.2em] text-primary">Welcome back</p>
        <h1 className="text-3xl font-bold">Login to Codebuddy</h1>
        <p className="text-sm text-muted">Continue practicing or hop into a buddy session.</p>
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
          Login
        </button>
        <p className="text-center text-sm text-muted">
          No account?{" "}
          <Link className="text-primary hover:underline" to="/register">
            Register
          </Link>
        </p>
      </form>
    </div>
  );
};

export default LoginPage;
