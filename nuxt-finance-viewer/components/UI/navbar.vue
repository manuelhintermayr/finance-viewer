<template>

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
            class="nav-link disabled" 
            href="/admin">Admin</a>
        </li>
        <!-- <dropdown>
          <template slot="btn">Custom btn</template>
          <template slot="icon">Custom icon</template>
          <template slot="body">Custom body</template>
        </dropdown> -->
        <li class="nav-item dropdown">
          <a 
            id="navbarDropdown" 
            class="nav-link dropdown-toggle" 
            href="#" 
            role="button" 
            data-toggle="dropdown" 
            aria-haspopup="true" 
            aria-expanded="false">
            Years
          </a>
          <div 
            class="dropdown-menu" 
            aria-labelledby="navbarDropdown">
            <a 
              class="dropdown-item" 
              href="#">2017</a>
            <a 
              class="dropdown-item" 
              href="#">2018</a>
            <div class="dropdown-divider"/>
            <a 
              class="dropdown-item" 
              href="#">2019</a>
          </div>
        </li>
      </ul>
      <form class="form-inline my-2 my-lg-0">
        <button 
          class="btn btn-outline-info my-2 my-sm-0" 
          type="submit"><font-awesome-icon icon="coffee" /><i class="fas fa-sign-out"/> Logout</button>
      </form>
    </div>
  </nav>
  
</template>

<script>
export default {
  //   components: { Dropdown }
}
</script>

<style scoped>
#main_nav {
  border-radius: 0;
  background: rgba(34, 34, 34, 0.86);
  border-color: #080808;
  border-bottom: 2px #080808 solid;
}
</style>
