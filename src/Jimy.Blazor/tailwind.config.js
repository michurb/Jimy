module.exports = {
  content: [
    './**/*.razor',
    './**/*.html',
    './**/*.cshtml',
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/aspect-ratio'),
    require('@tailwindcss/typography'),
    require('tailwindcss-children'),
  ],
}