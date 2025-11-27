/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  darkMode: "class",
  theme: {
    extend: {
      colors: {
        background: "#0b1224",
        surface: "#0f172a",
        primary: "#2dd4bf",
        accent: "#3b82f6",
        muted: "#94a3b8",
      },
    },
  },
  plugins: [],
};
