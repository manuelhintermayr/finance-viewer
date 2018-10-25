<template>
  <div :class="{ formSuccess: loginAnimationGetTextDown, loggingEnd: loginAnimationGetTextUp, fadeEnter: loginIsFinished, fadeEnterActive: loginIsFinished }">
    <div 
      id="login-container" 
      class="container">
      <h1 class="welcome-message">Welcome <span id="messageForTheUser"/>{{ fullName }}</h1>

      <transition 
        name="fade">
        <form 
          v-if="!loginSucceeded"
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
      </transition>

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
      loginAnimationGetTextDown: false,
      loginAnimationGetTextUp: false,
      loginIsFinished: false
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
          .post('/login.php', loginData)
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
      setTimeout(() => {
        this.animateUsername(username, 0)
      }, 1000) //Wait 1 second for transition
      this.errorMessage = ''
      this.loginSucceeded = true
      this.loginAnimationGetTextDown = true

      setTimeout(() => {
        this.loginAnimationGetTextUp = true
      }, 3000) //Wait 3 Seconds to go up again
      setTimeout(() => {
        this.loginIsFinished = true
      }, 3200) //Disable whole login form

      setTimeout(() => {
        this.$router.replace('/' + url)
      }, 3500)
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
    }
  }
}
</script>

<style scoped>
@charset "UTF-8";

/* Code party shared from:
 * http://codepen.io/vubon/pen/bdmvbY
 */

@import url(http://fonts.googleapis.com/css?family=Source+Sans+Pro:200);

.container {
  max-width: 600px;
  text-align: center;
  text-align: -webkit-center;
  position: absolute;
  left: 50%;
  top: 40%;
  margin: -144px 0px 0px -300px;
}
.container h1 {
  font-size: 40px;
  -webkit-transition-duration: 1s;
  transition-duration: 1s;
  -webkit-transition-timing-function: ease-in-put;
  transition-timing-function: ease-in-put;
  font-weight: 200;
}
form {
  padding: 20px 0;
  position: relative;
  z-index: 2;
}
form input {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  outline: 0;
  border: 1px solid rgba(255, 255, 255, 0.4);
  background-color: rgba(255, 255, 255, 0.2);
  width: 250px;
  border-radius: 3px;
  padding: 10px 15px;
  margin: 0 auto 10px auto;
  display: block;
  text-align: center;
  font-size: 18px;
  color: white;
  -webkit-transition-duration: 0.25s;
  transition-duration: 0.25s;
  font-weight: 300;
}
form input:hover {
  background-color: rgba(255, 255, 255, 0.4);
}
form input:focus {
  background-color: white;
  width: 300px;
  color: #53e3a6;
}
form button {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  outline: 0;
  background-color: white;
  border: 0;
  padding: 10px 15px;
  color: #53e3a6;
  border-radius: 3px;
  width: 250px;
  cursor: pointer;
  font-size: 18px;
  -webkit-transition-duration: 0.25s;
  transition-duration: 0.25s;
}
form button:hover {
  background-color: #f5f7f9;
}

::-webkit-input-placeholder {
  color: white;
}
:-moz-placeholder {
  color: white;
  opacity: 1;
}
:-ms-input-placeholder {
  color: white;
}

/** Correct Login style */
.fade-enter-active,
.fadeEnterActive,
.fade-leave-active {
  transition: opacity 0.5s;
}

.fade-enter,
.fadeEnter,
.fade-leave-to {
  opacity: 0;
}

.formSuccess .container h1 {
  -webkit-transform: translateY(85px);
  transform: translateY(85px);
}
.loggingEnd .container h1 {
  -webkit-transform: translateY(20px);
  transform: translateY(20px);
}

/** Error Messages style */
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
