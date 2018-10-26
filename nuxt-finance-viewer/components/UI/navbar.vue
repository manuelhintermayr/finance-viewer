<template>

  <nav 
    id="main_nav" 
    class="navbar navbar-expand-md bg-dark navbar-dark">
    <!-- https://stackoverflow.com/questions/46041825/pass-object-as-prop-on-vue -->
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
            class="nav-link" 
            href="#">Admin</a>
        </li>
        <li class="nav-item">
          <select 
            id="years" 
            class="custom-select dropdown dropdownNavbar" 
            required="">
            <option value="">Years</option>
            <option>2017</option>
            <option>2018</option>
          </select>
        </li>
      </ul>
      <form class="form-inline my-2 my-lg-0">
        <button 
          class="btn btn-outline-info my-2 my-sm-0" 
          @click="loggedOut = !loggedOut">
          <font-awesome-icon icon="sign-out-alt" />
          <font-awesome-icon :icon="['fas', 'Ad']" />
          Logout
        </button>
      </form>
    </div>
  </nav>
  
</template>

<script>
export default {}
</script>

<style scoped>
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
</style>
