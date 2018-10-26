import Vue from 'vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons'
import { faGlobeAmericas } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(faSignOutAlt, faGlobeAmericas)

Vue.component('font-awesome-icon', FontAwesomeIcon)

//look at node_modules\@fortawesome\free-solid-svg-icons
