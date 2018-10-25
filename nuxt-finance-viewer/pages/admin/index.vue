<template>
  <div>
    <bobbles />
    <div id="content">

      <p>Dashboard for admin user. <a href="..\logout.php">Logout</a></p>
      <form>
        <input 
          v-model="itemUsername" 
          type="text">
        <input 
          v-model="itemPassword" 
          type="text">
        <button @click.prevent="addItem">Add</button>
      </form>

      <ul>
        <li 
          v-for="u in users" 
          :key="u.username">
          [{{ u.id }}]: {{ u.username }} - {{ u.password }}  
          <button @click="removeItem(u)">remove</button>
        </li>
      </ul>
    
    </div>
  </div>
</template>

<script>
import Bobbles from '~/components/UI/bobbles'

export default {
  components: {
    Bobbles
  },
  data() {
    return {
      users: [],
      itemUsername: '',
      itemPassword: '',
      currentId: 0
    }
  },
  methods: {
    addItem() {
      this.users.push({
        id: this.currentId++,
        username: this.itemUsername,
        password: this.itemPassword
      })
      this.itemUsername = this.itemPassword = ''
    },
    removeItem(user) {
      this.users.splice(this.users.indexOf(user), 1)
    }
  }
}
</script>

<style scoped>
#content {
  position: absolute;
  width: 100%;
  left: 0;
}
</style>
