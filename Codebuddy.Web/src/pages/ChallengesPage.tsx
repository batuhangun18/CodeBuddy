import { useMemo, useState } from "react";
import ChallengeCard from "../components/ChallengeCard";

const data = [
  {
    title: "String Compressor",
    slug: "string-compressor",
    difficulty: "Intermediate",
    language: "C#",
    description: "Implement run-length encoding and compare readability with your buddy.",
  },
  {
    title: "Budget Tracker",
    slug: "budget-tracker",
    difficulty: "Beginner",
    language: "JavaScript",
    description: "Practice arrays, objects, and simple calculations in the browser.",
  },
  {
    title: "Binary Search",
    slug: "binary-search",
    difficulty: "Advanced",
    language: "Python",
    description: "Implement and test binary search with clear docs and beginner hints.",
  },
  {
    title: "API Data Fetcher",
    slug: "api-data-fetcher",
    difficulty: "Intermediate",
    language: "TypeScript",
    description: "Fetch and render data with error handling and loading states.",
  },
];

const ChallengesPage = () => {
  const [language, setLanguage] = useState("All");
  const [difficulty, setDifficulty] = useState("All");

  const filtered = useMemo(
    () =>
      data.filter(
        (item) =>
          (language === "All" || item.language === language) &&
          (difficulty === "All" || item.difficulty === difficulty),
      ),
    [language, difficulty],
  );

  return (
    <section className="space-y-6">
      <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
        <div>
          <p className="text-sm uppercase tracking-[0.2em] text-primary">Practice</p>
          <h1 className="text-3xl font-bold">Challenges</h1>
          <p className="text-sm text-muted">Filter by language and difficulty to find your next warmup.</p>
        </div>
        <div className="flex flex-wrap gap-3">
          <select
            className="rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none"
            value={language}
            onChange={(e) => setLanguage(e.target.value)}
          >
            {["All", "C#", "JavaScript", "Python", "TypeScript"].map((lang) => (
              <option key={lang}>{lang}</option>
            ))}
          </select>
          <select
            className="rounded-lg border border-white/10 bg-surface px-3 py-2 text-sm outline-none"
            value={difficulty}
            onChange={(e) => setDifficulty(e.target.value)}
          >
            {["All", "Beginner", "Intermediate", "Advanced"].map((d) => (
              <option key={d}>{d}</option>
            ))}
          </select>
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        {filtered.map((item) => (
          <ChallengeCard key={item.slug} {...item} />
        ))}
      </div>
    </section>
  );
};

export default ChallengesPage;
