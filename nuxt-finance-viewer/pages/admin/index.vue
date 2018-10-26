<template>
  <div>
    <bobbles />
    <div id="content">
      <navbar />
      <main 
        role="main" 
        class="container">
        <h1 class="mt-5">Admin Dashboard</h1>
        <p class="lead">Update, edit, delete, create users and set a new password for them. <a href="..\logout.php">Logout</a></p>
        <div class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal">
          <h3 class="border-bottom border-gray pb-2 mb-0">Create new user</h3>
          <br >
          <p class="lead">Add a new user:</p>
          <hr>
          <form 
            class="needs-validation" 
            novalidate="">
            <div class="row">
              <div class="col-md-6 mb-3">
                <label for="firstName">First name</label>
                <input 
                  id="firstName" 
                  v-model="itemFirstname" 
                  type="text" 
                  class="form-control" 
                  placeholder="" 
                  value="" 
                  required="">
                <div class="invalid-feedback">
                  Valid first name is required.
                </div>
              </div>
              <div class="col-md-6 mb-3">
                <label for="lastName">Last name</label>
                <input 
                  id="lastName" 
                  v-model="itemLastname" 
                  type="text" 
                  class="form-control" 
                  placeholder="" 
                  value="" 
                  required="" >
                <div class="invalid-feedback">
                  Valid last name is required.
                </div>
              </div>
            </div>

            <div class="mb-3">
              <label for="username">Username</label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <span class="input-group-text">@</span>
                </div>
                <input 
                  id="username" 
                  v-model="itemUsername" 
                  type="text" 
                  class="form-control" 
                  placeholder="Username" 
                  required="">
                <div 
                  class="invalid-feedback" 
                  style="width: 100%;">
                  Your username is required.
                </div>
              </div>
            </div>
             
            <div class="mb-3">
              <label for="address">Password</label>
              <input 
                id="password" 
                v-model="itemPassword" 
                type="text" 
                class="form-control" 
                placeholder="Password"
                required="" >
              <div class="invalid-feedback">
                Please enter your password.
              </div>
            </div>
            <hr class="mb-4">
            <div class="custom-control custom-checkbox">
              <input 
                id="is-Locked" 
                v-model="itemIsLocked" 
                type="checkbox" 
                class="custom-control-input">
              <label 
                class="custom-control-label" 
                for="is-Locked">Lock this user</label>
            </div>
            <hr class="mb-4">
            <button 
              class="btn btn-secondary btn-lg btn-block" 
              @click.prevent="addItem">Add User</button>
          </form>
        </div>
        <br>

        <div class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal">
          <h3 class="border-bottom border-gray pb-2 mb-0">Change user</h3>
          <br>
          <p class="lead">Change a current user:</p>
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
                <td>
                  <input  
                    v-model="u.username" 
                    type="text" 
                    class="form-control" 
                    required="" >
                </td>
                <td>
                  <input  
                    v-model="u.firstname" 
                    type="text" 
                    class="form-control" 
                    required="" >
                </td>
                <td>
                  <input  
                    v-model="u.lastname" 
                    type="text" 
                    class="form-control" 
                    required="" >
                </td>
                <td><button class="btn btn-secondary">Update</button></td>
                <td><button 
                  class="btn btn-secondary" 
                  @click="removeItem(u)">Remove</button></td>
                <td><button class="btn btn-secondary">Set Password</button></td>
                <td><button class="btn btn-secondary">View FinanceView</button></td>
              </tr>
            </tbody>
          </table>
          <hr class="mb-4">
          <button 
            class="btn btn-secondary btn-lg btn-block">Go to top</button>
        </div>
      </main>
      
      
    </div>
  </div>
</template>

<script>
import Bobbles from '~/components/UI/bobbles'
import Navbar from '~/components/UI/navbar'

export default {
  components: {
    Bobbles,
    Navbar
  },
  data() {
    return {
      users: [],
      itemUsername: '',
      itemPassword: '',
      itemFirstname: '',
      itemLastname: '',
      itemIsLocked: false,
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
        username: this.itemUsername,
        origianlUsername: this.itemUsername,
        firstname: this.itemFirstname,
        lastname: this.itemLastname
      })
      this.itemUsername = this.itemPassword = this.itemFirstname = this.itemLastname = this.itemIsLocked =
        ''
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
              origianlUsername: element.username,
              firstname: element.firstname,
              lastname: element.lastname,
              isLocked: element.isLocked
            })
          })
          console.log(api)
        })
        .catch(error => {
          alert(
            'Could not load users. The following error occured: ' +
              error.response.data
          )
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
  height: 100%;
  overflow-y: scroll;
}
.transparentModal {
  background-color: #ffffff45 !important;
}
.btn-secondary {
  background-color: #6c757d8c;
}

table input {
  background-color: #ffffff73;
}
</style>
