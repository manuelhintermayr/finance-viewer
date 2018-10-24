<template>
  <div>
    <div 
      id="login-container" 
      class="container">
      <h1 class="welcome-message">Welcome <span id="messageForTheUser"/></h1>
      <form 
        class="form" >
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
      class="alert alert-danger closed" 
      role="alert">
      <span 
        class="glyphicon glyphicon-exclamation-sign" 
        aria-hidden="true"/>
      <span class="sr-only">Error:</span>
      <span id="error_message">{{ errorMessage }}</span>

      <span 
        id="error-close" 
        class="close">X</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: '',
      password: '',
      errorMessage: 'noError',
      fullName: ''
    }
  },
  methods: {
    login() {
      let loginData = {
        username: this.username,
        password: this.password
      }

      if (loginData.username == '' || loginData.password == '') {
        this.errorMessage = 'Please fill out all fields.'
      } else {
        this.$axios
          .post('http://localhost:64674/login.php', loginData)
          .then(response => {
            console.log(response.data)
            // this.jokes = response.data.value
            console.log('first funzt')
            //this.$router.replace('/admin')
          })
          .catch(error => {
            console.log('funzt net')
          })
      }
    }
  }
}
</script>

<style>
</style>
