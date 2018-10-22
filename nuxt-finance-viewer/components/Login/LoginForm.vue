<template>
  <div 
    id="login-container" 
    class="container">
    <h1 class="welcome-message">Welcome <span id="messageForTheUser"/></h1>
    <form 
      class="form" 
      action="index.php" 
      method="post">
      <input 
        id="user" 
        type="text" 
        name="username" 
        placeholder="Username">
      <input 
        id="pass" 
        type="password" 
        name="password" 
        placeholder="Password">
      <button 
        id="login-button" 
        type="submit">Login</button>
    </form>
  </div>
</template>

<script>
export default {}
</script>

<style>
</style>
