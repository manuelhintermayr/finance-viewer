<template>
  <div>
    <button @click="loggedOut= !loggedOut">Hide</button>
    <transition name="fade">
      <span v-if="!loggedOut">
        <nuxt/>
      </span>
    </transition>
    <div 
      id="byeMessage" 
      class="container dynamischeAenderungen">Bye Administrator!
    </div>
  </div>
</template>

<script>
export default {
  components: {},

  data() {
    return {
      loggedOut: false
    }
  }
}
</script>

<style>
#byeMessage {
  text-align: -webkit-center;
  font: normal normal 200 normal 40px / 44px 'Source Sans Pro', sans-serif;
  margin-top: 13%;
  display: none;
}

.dynamischeAenderungen {
  -webkit-transition-duration: 1s;
  transition-duration: 1s;
  -webkit-transition-timing-function: ease-in-put;
  transition-timing-function: ease-in-put;
  /* Damit CSS Aenderungen Smooth ineinander uebergehen. */
}

/* Animationen */
.logoutEnd {
  -webkit-transform: translateY(-300px);
  transform: translateY(-300px);
  color: rgba(255, 255, 255, 0);
}
</style>
