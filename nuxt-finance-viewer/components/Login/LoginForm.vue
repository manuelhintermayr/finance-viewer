<template>
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
        placeholder="Username">
      <input 
        id="pass"  
        v-model="password" 
        type="password" 
        name="password" 
        placeholder="Password">
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
  methods: {
    login() {
      this.$axios
        .get('http://api.icndb.com/jokes/random/10')
        .then(response => {
          // this.jokes = response.data.value
          console.log('first funzt')
          //this.$router.replace('/admin')
        })
        .catch(error => {
          console.log('funzt net')
        })

      let loginData = {
        user: this.username,
        pass: this.password
      }

      this.$axios
        .post('https://httpbin.org/post', loginData)
        .then(response => {
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
