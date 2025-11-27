const sessions = [
  { title: "Binary Search", status: "Active", buddy: "Mira", started: "10m ago" },
  { title: "Array Chunker", status: "Pending", buddy: "Waiting", started: "—" },
  { title: "FizzBuzz Reimagined", status: "Finished", buddy: "Leo", started: "Yesterday" },
];

const BuddyPage = () => {
  return (
    <section className="space-y-6">
      <div className="space-y-2">
        <p className="text-sm uppercase tracking-[0.2em] text-primary">Buddy mode</p>
        <h1 className="text-3xl font-bold">Your sessions</h1>
        <p className="text-sm text-muted">Create a session to pair up on any challenge. Real-time is coming soon.</p>
      </div>

      <div className="card space-y-3">
        <div className="flex flex-wrap items-center gap-3 text-sm">
          <button className="rounded-lg bg-gradient-to-r from-primary to-accent px-4 py-2 font-semibold text-slate-900 shadow-lg shadow-primary/30 transition hover:shadow-accent/30">
            New Buddy Session
          </button>
          <span className="text-muted">Pick a challenge and invite a friend or auto-match.</span>
        </div>
        <div className="grid gap-3 md:grid-cols-3">
          {sessions.map((session) => (
            <div key={session.title} className="rounded-xl border border-white/10 bg-white/5 p-4">
              <p className="text-sm text-muted">{session.status}</p>
              <h3 className="text-lg font-semibold">{session.title}</h3>
              <p className="text-sm text-muted">Buddy: {session.buddy}</p>
              <p className="text-xs text-muted/80">Started: {session.started}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default BuddyPage;
