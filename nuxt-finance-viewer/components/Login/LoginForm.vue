<template>
  <div>
    <div 
      id="login-container" 
      class="container">
      <h1 class="welcome-message">Welcome <span id="messageForTheUser"/>{{ fullName }}</h1>


      <button @click="fadeMe">
        Toggle
      </button>
      <transition 
        name="fade" 
        @enter="enter">
        <p v-if="show">hello</p>
      </transition>


      <form 
        :class="{ closed: loginSucceeded}" 
        class="form">
        <input 
          id="user" 
          v-model="username" 
          type="text" 
          name="username" 
          placeholder="Username" 
          required>
        <input 
          id="pass"  
          v-model="password" 
          type="password" 
          name="password" 
          placeholder="Password" 
          required>
        <button 
          id="login-button" 
          @click.prevent="login">Login</button>
      </form>
    </div>
    <div 
      id="login_error"
      ref="loginError"
      :class="{ closed: errorMessage == '', error: errorAnimationActivated }"
      class="alert alert-danger"
      role="alert" >
      <span 
        class="glyphicon glyphicon-exclamation-sign" 
        aria-hidden="true"/>
      <span class="sr-only">Error:</span>
      <span 
        id="error_message" 
        v-html="errorMessage"/>
      <span 
        id="error-close" 
        class="close"
        @click="errorMessage = ''">X</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: '',
      password: '',
      fullName: '',
      errorMessage: '',
      errorAnimationActivated: false,
      loginSucceeded: false,
      show: false
    }
  },
  methods: {
    login() {
      let loginData = {
        username: this.username,
        password: this.password
      }

      if (loginData.username == '' || loginData.password == '') {
        this.loginError('Please fill out all fields.')
      } else {
        this.$axios
          .post('http://localhost:64674/login.php', loginData)
          .then(response => {
            this.loginSuccessfull(response.data.name, response.data.url)
          })
          .catch(error => {
            this.loginError(
              'The following error occured: <b>' + error.response.data + '</b>'
            )
          })
      }
    },
    loginError(contentForErrorMessage) {
      this.errorMessage = contentForErrorMessage
      this.errorAnimationActivated = true

      var self = this
      setTimeout(function() {
        self.errorAnimationActivated = false
      }, 1000)
    },
    loginSuccessfull(username, url) {
      var self = this
      setTimeout(function() {
        self.animateUsername(username, 0)
      }, 1000)
      this.errorMessage = ''
      this.loginSucceeded = true

      // console.log(response.data)
      //this.$router.replace('/admin')
      // window.setTimeout(
      //   'type("#messageForTheUser"," Administrator!",0);',
      //   1000
      // )
      // $('form').fadeOut(500)
      // $('#login_error').fadeOut(500)
      // $('body').addClass('form-success')

      // window.setTimeout('$("body").addClass("loggingEnd");', 3000)
      // window.setTimeout('$("#login-container").fadeOut(500);', 3200)

      // window.setTimeout('removeClass("body","form-success");', 4000)
    },
    animateUsername(newUsername, pos) {
      var cUsername = newUsername.substring(0, pos)
      this.fullName = cUsername
      if (pos == newUsername.length) {
        this.fullName = newUsername + '!'
      } else {
        var self = this
        setTimeout(function() {
          self.animateUsername(newUsername, pos + 1)
        }, 70)
      }
    },
    fadeMe: function() {
      this.show = !this.show
    },

    enter: function(el, done) {
      var that = this

      setTimeout(function() {
        that.show = false
      }, 3000) // hide the message after 3 seconds
    }
  }
}
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 1s;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}

.close:hover {
  cursor: pointer;
}

.closed {
  transform: translate(0px, -60px);
  transition: 0.7s;
}

#error-close {
  background: #e74c3c;
  padding: 5px;
  top: -6px;
  position: relative;
  border: 1px #371e1e solid;
}

.error {
  -moz-animation: error 0.35s linear;
  -webkit-animation: error 0.35s linear;
}

@-webkit-keyframes error {
  0% {
    -webkit-transform: translateX(0px);
  }
  25% {
    -webkit-transform: translateX(30px);
  }
  45% {
    -webkit-transform: translateX(-30px);
  }
  65% {
    -webkit-transform: translateX(30px);
  }
  82% {
    -webkit-transform: translateX(-30px);
  }
  94% {
    -webkit-transform: translateX(30px);
  }
  35%,
  55%,
  75%,
  87%,
  97%,
  100% {
    -webkit-transform: translateX(0px);
  }
}

@-moz-keyframes error {
  0% {
    -moz-transform: translateX(0px);
  }
  25% {
    -moz-transform: translateX(30px);
  }
  45% {
    -moz-transform: translateX(-30px);
  }
  65% {
    -moz-transform: translateX(30px);
  }
  82% {
    -moz-transform: translateX(-30px);
  }
  94% {
    -moz-transform: translateX(30px);
  }
  35%,
  55%,
  75%,
  87%,
  97%,
  100% {
    -moz-transform: translateX(0px);
  }
}

.alert {
  font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
  /* better font for error messages*/
}
</style>
