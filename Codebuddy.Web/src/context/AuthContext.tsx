import { createContext, useContext, useEffect, useState } from "react";
import api from "../api/client";
import type { AuthResponse, UserProfile } from "../types";

type AuthContextType = {
  user: AuthResponse | null;
  profile: UserProfile | null;
  login: (email: string, password: string) => Promise<void>;
  register: (email: string, userName: string, password: string) => Promise<void>;
  logout: () => void;
  refreshProfile: () => Promise<void>;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<AuthResponse | null>(() => {
    const stored = localStorage.getItem("codebuddy_user");
    return stored ? (JSON.parse(stored) as AuthResponse) : null;
  });
  const [profile, setProfile] = useState<UserProfile | null>(null);

  useEffect(() => {
    if (user) {
      refreshProfile();
    }
  }, [user]);

  const persist = (auth: AuthResponse) => {
    localStorage.setItem("codebuddy_token", auth.token);
    localStorage.setItem("codebuddy_user", JSON.stringify(auth));
    setUser(auth);
  };

  const login = async (email: string, password: string) => {
    const { data } = await api.post<AuthResponse>("/Auth/login", { email, password });
    persist(data);
  };

  const register = async (email: string, userName: string, password: string) => {
    const { data } = await api.post<AuthResponse>("/Auth/register", {
      email,
      userName,
      password,
    });
    persist(data);
  };

  const refreshProfile = async () => {
    if (!user) return;
    try {
      const { data } = await api.get<UserProfile>("/users/me");
      setProfile(data);
    } catch {
      // swallow errors for now
    }
  };

  const logout = () => {
    localStorage.removeItem("codebuddy_token");
    localStorage.removeItem("codebuddy_user");
    setUser(null);
    setProfile(null);
  };

  const value: AuthContextType = {
    user,
    profile,
    login,
    register,
    logout,
    refreshProfile,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) {
    throw new Error("useAuth must be used within AuthProvider");
  }
  return ctx;
};
