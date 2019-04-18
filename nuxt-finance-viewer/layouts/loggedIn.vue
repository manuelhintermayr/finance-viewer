<template>
  <div 
    id="homeContent" 
    class="fill-height">
    <transition 
      name="fade" 
      enter-active-class="fade-enter-active">
      <span v-if="!loggedOut">
        <!-- Start Navbar -->
        <nav 
          id="main_nav" 
          class="navbar navbar-expand-md bg-dark navbar-dark">
          <a 
            class="navbar-brand" 
            href="#">Finance Viewer</a>
          <button 
            class="navbar-toggler" 
            type="button" 
            data-toggle="collapse" 
            data-target="#navbarSupportedContent" 
            aria-controls="navbarSupportedContent" 
            aria-expanded="false" 
            aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"/>
          </button>

          <div 
            id="navbarSupportedContent" 
            class="collapse navbar-collapse">
            <ul class="navbar-nav mr-auto">
              <li class="nav-item">
                <a
                  v-if="isAdmin"
                  class="nav-link" 
                  href="/admin">Admin</a>
              </li>
              <li 
                v-if="years.length > 0"
                class="nav-item">
                <select 
                  id="years" 
                  class="custom-select dropdown dropdownNavbar" 
                  required="" 
                  @change="goToDashboard">
                  <option value="">Years</option>
                  <option 
                    v-for="y in years"
                    :key="y">{{ y }}</option>
                </select>
              </li>
            </ul>
            <form class="form-inline my-2 my-lg-0">
              <button 
                class="btn btn-outline-info my-2 my-sm-0" 
                @click.prevent="logout()">
                <font-awesome-icon icon="sign-out-alt" />
                Logout
              </button>
            </form>
          </div>
        </nav>
  
        <!-- End Navbar -->
        <nuxt/>
      </span>
    </transition>
    <div 
      id="byeMessage" 
      :class="{ logoutEnd: !startLogoutAnimations}"
      class="container dynamicChanges">Bye {{ name }}!
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      loggedOut: false,
      startLogoutAnimations: false,
      name: '',
      isAdmin: false,
      years: []
    }
  },
  mounted() {
    this.loadAll()
  },
  methods: {
    loadAll() {
      this.$axios
        .get('options.php?action=getInfo')
        .then(response => {
          let api = response.data
          this.name = api.name
          this.isAdmin = api.isAdmin
          this.years = api.years

          console.log(api)
        })
        .catch(error => {
          console.log(error)
          this.$router.replace('/')
        })
    },
    logout() {
      this.loggedOut = true
      this.startLogoutAnimations = true

      setTimeout(() => {
        this.startLogoutAnimations = false
      }, 1500)

      setTimeout(() => {
        window.location = '/logout.php'
      }, 2000)
    },
    goToDashboard: function(event) {
      if (event.target.value != '') {
        this.$axios
          .post('options.php?action=setViewYear', { year: event.target.value })
          .then(response => {
            let api = response.data
            if (api.message == 'Year for view set.') {
              window.location = '/dashboard'
            } else {
              console.log(reponse)
              alert('Could not set year for View. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not set year for View. Check console for more info.')
          })
      }
    }
  }
}
</script>

<style>
#homeContent {
  position: absolute;
  width: 100%;
  left: 0;
}
#byeMessage {
  font: normal normal 200 normal 40px / 44px 'Source Sans Pro', sans-serif;
  position: absolute;
  top: 25%;
  left: 35%;
  left: calc(40% - 44px);
}

.dynamicChanges {
  -webkit-transition-duration: 1s;
  transition-duration: 1s;
  -webkit-transition-timing-function: ease-in-put;
  transition-timing-function: ease-in-put;
  /* display: none; */
  /* Damit CSS Aenderungen Smooth ineinander uebergehen. */
}

.show {
  display: grid;
}

/* Animationen */
.logoutEnd {
  -webkit-transform: translateY(-300px);
  transform: translateY(-300px);
  color: rgba(255, 255, 255, 0);
}

/* Start Navbar */
#main_nav {
  border-radius: 0;
  background: rgba(34, 34, 34, 0.86) !important;
  border-color: #080808;
  border-bottom: 2px #080808 solid;
}
.navbar-brand {
  color: #9d9d9d;
}
.navbar-brand:hover,
.navbar-brand:focus {
  color: #fff;
  background-color: transparent;
}
.dropdownNavbar {
  background: transparent;
  border: none;
  padding-bottom: 3px;
  cursor: pointer;
}
.nav-link,
.dropdownNavbar {
  color: rgb(128, 128, 128) !important;
}
.nav-link:hover,
.dropdownNavbar:hover {
  color: #a2a0a0 !important;
}
/* End Navbar */
</style>
