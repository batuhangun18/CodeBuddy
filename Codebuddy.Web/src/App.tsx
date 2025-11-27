import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "./components/Navbar";
import BuddyPage from "./pages/BuddyPage";
import ChallengeDetailPage from "./pages/ChallengeDetailPage";
import ChallengesPage from "./pages/ChallengesPage";
import LandingPage from "./pages/LandingPage";
import LoginPage from "./pages/LoginPage";
import ProfilePage from "./pages/ProfilePage";
import RegisterPage from "./pages/RegisterPage";
import { AuthProvider } from "./context/AuthContext";

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <div className="min-h-screen bg-gradient-to-b from-[#0b1224] via-[#0d1933] to-[#0b1224] text-slate-100">
          <Navbar />
          <main className="max-w-6xl mx-auto px-4 pb-16 pt-8">
            <Routes>
              <Route path="/" element={<LandingPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
              <Route path="/challenges" element={<ChallengesPage />} />
              <Route path="/challenges/:slug" element={<ChallengeDetailPage />} />
              <Route path="/profile/:username" element={<ProfilePage />} />
              <Route path="/buddy" element={<BuddyPage />} />
            </Routes>
          </main>
        </div>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
