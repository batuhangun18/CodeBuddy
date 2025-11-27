import { Link } from "react-router-dom";
import ChallengeCard from "../components/ChallengeCard";

const sampleChallenges = [
  {
    title: "FizzBuzz Reimagined",
    slug: "fizzbuzz-reimagined",
    difficulty: "Beginner",
    language: "C#",
    description: "Write a cleaner, extensible FizzBuzz to learn control flow and small refactors.",
  },
  {
    title: "Array Chunker",
    slug: "array-chunker",
    difficulty: "Intermediate",
    language: "JavaScript",
    description: "Split arrays into even chunks and compare approaches with your buddy.",
  },
  {
    title: "Palindrome Inspector",
    slug: "palindrome-inspector",
    difficulty: "Beginner",
    language: "Python",
    description: "Check strings while ignoring punctuation and casing; focus on readable code.",
  },
];

const LandingPage = () => {
  return (
    <section className="space-y-12">
      <div className="rounded-2xl border border-white/10 bg-gradient-to-br from-surface via-[#101a30] to-[#0b1224] p-8 shadow-2xl shadow-black/40">
        <div className="flex flex-col gap-6 md:flex-row md:items-center md:justify-between">
          <div className="max-w-2xl space-y-4">
            <p className="text-sm uppercase tracking-[0.3em] text-primary">newbie friendly</p>
            <h1 className="text-4xl font-bold leading-tight md:text-5xl">
              Practice coding with a buddy-friendly dark playground.
            </h1>
            <p className="text-lg text-slate-300">
              Codebuddy gives beginners challenges with hints, beginner mode explanations, and a simple way to team up
              when you get stuck.
            </p>
            <div className="flex flex-wrap gap-4">
              <Link
                to="/challenges"
                className="rounded-xl bg-gradient-to-r from-primary to-accent px-5 py-3 text-slate-900 font-semibold shadow-xl shadow-primary/30 transition hover:shadow-accent/40"
              >
                Solo Practice
              </Link>
              <Link
                to="/buddy"
                className="rounded-xl border border-white/15 px-5 py-3 font-semibold text-slate-100 transition hover:border-primary hover:text-primary"
              >
                Find a Buddy
              </Link>
            </div>
          </div>
          <div className="grid grid-cols-2 gap-3 rounded-2xl bg-white/5 p-4 backdrop-blur">
            {["Level 0-3", "Daily picks", "Beginner mode", "Buddy Q&A"].map((item) => (
              <div key={item} className="rounded-xl bg-white/5 px-4 py-3 text-sm text-slate-200">
                {item}
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="space-y-4">
        <div className="flex items-center justify-between">
          <h2 className="text-2xl font-semibold">Featured challenges</h2>
          <Link to="/challenges" className="text-sm text-primary hover:underline">
            Browse all →
          </Link>
        </div>
        <div className="grid gap-4 md:grid-cols-3">
          {sampleChallenges.map((challenge) => (
            <ChallengeCard key={challenge.slug} {...challenge} />
          ))}
        </div>
      </div>
    </section>
  );
};

export default LandingPage;
