const pkg = require('./package')
const webpack = require('webpack')

module.exports = {
  mode: 'spa',

  /*
  ** Headers of the page
  */
  head: {
    title: 'Finance Viewer',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: pkg.description }
    ],
    link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }]
  },
  // script: [{ src: 'https://unpkg.com/jquery@3.2.1/dist/jquery.min.js' }],

  /*
  ** Customize the progress-bar color
  */
  loading: { color: '#fff' },

  /*
  ** Global CSS
  */
  css: [{ src: '~assets/style.css', lang: 'css' }],

  /*
  ** Plugins to load before mounting the App
  */
  plugins: ['~/plugins/icons', '~/plugins/bootstrap'],

  /*
  ** Nuxt.js modules
  */
  modules: [
    // Doc: https://github.com/nuxt-community/axios-module#usage
    '@nuxtjs/axios',
    'bootstrap-vue/nuxt',
    '@nuxtjs/moment'
  ],
  /*
  ** Axios module configuration
  */
  axios: {
    // See https://github.com/nuxt-community/axios-module#options
    baseURL: 'http://localhost:64674/', //php server url
    debug: true
  },

  /*
  ** Build configuration
  */
  build: {
    /*
    ** You can extend webpack config here
    */
    extend(config, ctx) {
      // Run ESLint on save
      if (ctx.isDev && ctx.isClient) {
        config.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /(node_modules)/
        })
      }
    }
  }
}

new webpack.ProvidePlugin({
  jQuery: 'jquery',
  $: 'jquery',
  'window.jQuery': 'jquery'
})
