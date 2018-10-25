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
          [{{ u.id }}]: {{ u.username }} - "{{ u.firstname }} {{ u.lastname }}". isLocked: {{ u.isLocked }}  
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
  mounted() {
    this.loadAll()
  },
  methods: {
    addItem() {
      this.users.push({
        id: this.currentId++,
        username: this.itemUsername
      })
      this.itemUsername = this.itemPassword = ''
    },
    removeItem(user) {
      this.users.splice(this.users.indexOf(user), 1)
    },
    loadAll() {
      this.$axios
        .get('admin/options.php?action=getUsers')
        .then(response => {
          let api = response.data
          api.forEach(element => {
            this.users.push({
              id: this.currentId++,
              username: element.username,
              firstname: element.firstname,
              lastname: element.lastname,
              isLocked: element.isLocked
            })
          })
          console.log(api)
        })
        .catch(error => {
          console.log('funzt net')
        })
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
