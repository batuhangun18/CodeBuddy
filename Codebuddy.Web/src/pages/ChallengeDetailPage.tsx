import { useMemo, useState } from "react";
import { useParams } from "react-router-dom";

const mock = {
  title: "String Compressor",
  description:
    "Compress repeated characters in a string using run-length encoding. Beginner mode explains each step with examples.",
  difficulty: "Intermediate",
  language: "C#",
  hints: [
    "Think about tracking the current run of characters as you iterate.",
    "Build the result in a StringBuilder for performance.",
  ],
};

const ChallengeDetailPage = () => {
  const { slug } = useParams();
  const [code, setCode] = useState("// paste your solution here");
  const [language, setLanguage] = useState(mock.language);
  const [message, setMessage] = useState<string | null>(null);

  const displayTitle = useMemo(
    () => (slug ? slug.replaceAll("-", " ").replace(/^\w/, (c) => c.toUpperCase()) : mock.title),
    [slug],
  );

  const handleSubmit = () => {
    setMessage("Submitted! (Dummy evaluation passes all tests for now.)");
  };

  return (
    <section className="space-y-8">
      <div className="card space-y-2">
        <p className="text-sm uppercase tracking-[0.2em] text-primary">{mock.difficulty}</p>
        <h1 className="text-3xl font-bold">{displayTitle}</h1>
        <p className="text-muted">{mock.description}</p>
        <div className="flex items-center gap-3 text-xs text-muted">
          <span className="rounded-full border border-white/10 px-3 py-1">{mock.language}</span>
          <span className="rounded-full border border-white/10 px-3 py-1">Buddy-ready</span>
        </div>
      </div>

      <div className="grid gap-6 md:grid-cols-[2fr,1fr]">
        <div className="card space-y-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-2 text-sm text-muted">
              <span>Language</span>
              <select
                value={language}
                onChange={(e) => setLanguage(e.target.value)}
                className="rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none"
              >
                {["C#", "JavaScript", "Python", "TypeScript"].map((lang) => (
                  <option key={lang}>{lang}</option>
                ))}
              </select>
            </div>
            <button
              onClick={handleSubmit}
              className="rounded-lg bg-gradient-to-r from-primary to-accent px-4 py-2 text-sm font-semibold text-slate-900 shadow-lg shadow-primary/30 transition hover:shadow-accent/30"
            >
              Submit
            </button>
          </div>
          <textarea
            value={code}
            onChange={(e) => setCode(e.target.value)}
            rows={12}
            className="w-full rounded-xl border border-white/10 bg-[#0a1426] px-3 py-3 font-mono text-sm text-slate-100 outline-none focus:border-primary"
          />
          {message && <div className="rounded-lg border border-primary/40 bg-primary/10 px-3 py-2 text-sm">{message}</div>}
        </div>

        <div className="card space-y-4">
          <h3 className="text-lg font-semibold">Hints</h3>
          <ul className="space-y-2 text-sm text-muted">
            {mock.hints.map((hint, idx) => (
              <li key={hint} className="rounded-lg border border-white/10 bg-white/5 px-3 py-2">
                <span className="text-primary mr-2">Hint {idx + 1}.</span>
                {hint}
              </li>
            ))}
          </ul>
          <div className="space-y-2">
            <h4 className="text-sm font-semibold text-slate-200">Q&A</h4>
            <p className="text-sm text-muted">Threads will appear here. (Stub for now.)</p>
            <button className="w-full rounded-lg border border-white/10 px-3 py-2 text-sm text-muted transition hover:border-primary hover:text-primary">
              Start a thread
            </button>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ChallengeDetailPage;
