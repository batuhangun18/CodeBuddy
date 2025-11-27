import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const Navbar = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <header className="sticky top-0 z-20 bg-[#0b1224]/80 backdrop-blur-xl border-b border-white/10">
      <div className="mx-auto flex max-w-6xl items-center justify-between px-4 py-4">
        <Link to="/" className="flex items-center gap-3">
          <div className="h-10 w-10 rounded-xl bg-gradient-to-br from-primary to-accent shadow-lg shadow-accent/30" />
          <div>
            <p className="text-lg font-semibold tracking-wide">Codebuddy</p>
            <p className="text-xs text-muted">Learn. Practice. Buddy up.</p>
          </div>
        </Link>
        <nav className="flex items-center gap-3 text-sm font-medium">
          <Link className="hover:text-primary transition" to="/challenges">
            Challenges
          </Link>
          <Link className="hover:text-primary transition" to="/buddy">
            Buddy
          </Link>
          {user ? (
            <>
              <Link
                className="hover:text-primary transition"
                to={`/profile/${user.userName}`}
              >
                Profile
              </Link>
              <button
                onClick={handleLogout}
                className="rounded-lg bg-white/10 px-3 py-2 text-xs font-semibold tracking-wide transition hover:bg-primary/20 border border-white/10"
              >
                Logout
              </button>
            </>
          ) : (
            <>
              <Link
                className="rounded-lg border border-white/10 px-3 py-2 text-xs font-semibold tracking-wide transition hover:border-primary hover:text-primary"
                to="/login"
              >
                Login
              </Link>
              <Link
                className="rounded-lg bg-gradient-to-r from-primary to-accent px-3 py-2 text-xs font-semibold text-slate-900 shadow-lg shadow-primary/30 transition hover:shadow-accent/30"
                to="/register"
              >
                Register
              </Link>
            </>
          )}
        </nav>
      </div>
    </header>
  );
};

export default Navbar;
