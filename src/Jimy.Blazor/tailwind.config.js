module.exports = {
  content: [
    './**/*.html',
    './**/*.razor',
    './**/*.cshtml',
    './**/*.razor.cs',
    './**/*.js',
    './**/*.cs'
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