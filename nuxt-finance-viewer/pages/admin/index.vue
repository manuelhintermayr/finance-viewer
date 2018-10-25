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
      <table class="table table-striped table-hover">
        <thead class="">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Username</th>
            <th scope="col">Firstname</th>
            <th scope="col">Lastname</th>
            <th scope="col">Update</th>
            <th scope="col">Remove</th>
            <th scope="col">Password</th>
            <th scope="col">View</th>
          </tr>
        </thead>
        <tbody>
          <tr 
            v-for="u in users" 
            :key="u.username">
            <th scope="row">{{ u.id }}</th>
            <td>{{ u.username }}</td>
            <td>{{ u.firstname }}</td>
            <td>{{ u.lastname }}</td>
            <td><button>Update</button></td>
            <td><button @click="removeItem(u)">Remove</button></td>
            <td><button>Set Password</button></td>
            <td><button>View FinanceView</button></td>
          </tr>
        </tbody>
      </table>
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
