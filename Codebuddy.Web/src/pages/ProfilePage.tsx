import { useMemo } from "react";
import { useParams } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const ProfilePage = () => {
  const { username } = useParams();
  const { profile, user } = useAuth();

  const displayName = useMemo(() => profile?.userName ?? username ?? "Codebuddy", [profile, username]);

  return (
    <section className="space-y-6">
      <div className="card flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div className="flex items-center gap-4">
          <div className="h-16 w-16 rounded-2xl bg-gradient-to-br from-primary to-accent" />
          <div>
            <h1 className="text-2xl font-bold">{displayName}</h1>
            <p className="text-muted text-sm">{profile?.email ?? user?.email ?? "user@example.com"}</p>
          </div>
        </div>
        <div className="flex gap-3 text-xs">
          <span className="rounded-full border border-white/10 px-3 py-2">
            Plan: {profile?.subscriptionType ?? user?.subscriptionType ?? "Free"}
          </span>
          <span className="rounded-full border border-white/10 px-3 py-2">Solved: 12</span>
          <span className="rounded-full border border-white/10 px-3 py-2">Languages: C#, JS</span>
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div className="card space-y-3">
          <h3 className="text-lg font-semibold">Recent submissions</h3>
          <ul className="space-y-2 text-sm text-muted">
            {["String Compressor", "API Data Fetcher", "FizzBuzz Reimagined"].map((item) => (
              <li key={item} className="rounded-lg border border-white/10 bg-white/5 px-3 py-2">
                {item}
              </li>
            ))}
          </ul>
        </div>
        <div className="card space-y-3">
          <h3 className="text-lg font-semibold">Badges</h3>
          <div className="flex flex-wrap gap-2 text-sm">
            {["Starter", "Streak 7", "Buddy Explorer"].map((badge) => (
              <span key={badge} className="rounded-lg border border-primary/30 bg-primary/10 px-3 py-2 text-primary">
                {badge}
              </span>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
};

export default ProfilePage;
