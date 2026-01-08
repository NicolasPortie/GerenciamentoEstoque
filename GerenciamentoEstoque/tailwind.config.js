/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './**/*.{razor,html,cshtml}'
  ],
  theme: {
    extend: {
      colors: {
        "primary": "#1e3c66",
        "primary-hover": "#162d4d",
        "accent": "#5AB9C1",
        "background-light": "#f7f7f8",
        "background-dark": "#22252a",
        "surface-dark": "#2c3036",
      },
      fontFamily: {
        "display": ["Manrope", "sans-serif"]
      },
    },
  },
  plugins: [],
}
