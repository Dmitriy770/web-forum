/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
      "./**/*.{razor,html,cshtml}",
      "!./**/{bin,obj}"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/forms'),
    require("@tailwindcss/typography"),
    require('daisyui'),
  ],
}

