import Vue from 'vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

// ICONS (node_modules\@fortawesome\free-solid-svg-icons)
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons'

library.add(faSignOutAlt)
Vue.component('font-awesome-icon', FontAwesomeIcon)
