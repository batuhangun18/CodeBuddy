import { Link } from "react-router-dom";

type Props = {
  title: string;
  slug: string;
  difficulty: string;
  language: string;
  description: string;
};

const badges: Record<string, string> = {
  Beginner: "bg-emerald-500/20 text-emerald-300",
  Intermediate: "bg-amber-500/20 text-amber-300",
  Advanced: "bg-rose-500/20 text-rose-300",
};

const ChallengeCard = ({ title, slug, difficulty, language, description }: Props) => (
  <Link
    to={`/challenges/${slug}`}
    className="card group flex flex-col gap-3 transition hover:-translate-y-1 hover:border-primary/60"
  >
    <div className="flex items-center justify-between">
      <h3 className="text-xl font-semibold">{title}</h3>
      <span className="rounded-full bg-slate-800 px-3 py-1 text-xs text-muted">
        {language}
      </span>
    </div>
    <p className="line-clamp-2 text-sm text-slate-300">{description}</p>
    <div className="flex items-center justify-between text-xs">
      <span className={`rounded-full px-3 py-1 ${badges[difficulty] ?? "bg-accent/20 text-accent"}`}>
        {difficulty}
      </span>
      <span className="text-muted opacity-80 group-hover:text-primary">
        View details →
      </span>
    </div>
  </Link>
);

export default ChallengeCard;
