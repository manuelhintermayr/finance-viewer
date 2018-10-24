<template>
  <div 
    id="login-container" 
    class="container">
    <h1 class="welcome-message">Welcome <span id="messageForTheUser"/></h1>
    {{ username }}
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
    <ul>
      <li 
        v-for="n in news" 
        :key="n.title">
        {{ n.title }} - {{ n.url }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: '',
      password: ''
    }
  },
  methods: {
    login() {
      let loginData = {
        username: this.username,
        password: this.password
      }

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
</script>

<style>
</style>
