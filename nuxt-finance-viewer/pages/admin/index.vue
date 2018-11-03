<template>
  <div>
    <bobbles />
    <div id="content">
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
                  maxlength="45"
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
                  maxlength="45"
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
                  required=""
                  maxlength="45">
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
                maxlength="45" 
                type="password" 
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
            <!-- <b-form-checkbox 
              id="is-Locked"
              v-model="itemIsLocked"
              value="accepted"
              unchecked-value="not_accepted">
              Lock this user
            </b-form-checkbox> -->
            <hr class="mb-4">
            <button 
              class="btn btn-secondary btn-lg btn-block" 
              @click.prevent="addUser">Add User</button>
          </form>
        </div>
        <br>

        <div 
          class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal"> 
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
                <th scope="col">Locked</th>
                <th scope="col">Update</th>
                <th scope="col">Remove</th>
                <th scope="col">Password/Views</th>
                <th scope="col">FinanceView</th>
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
                    required="" 
                    maxlength="45"
                    disabled>
                </td>
                <td>
                  <input  
                    v-model="u.firstname" 
                    type="text" 
                    class="form-control" 
                    required="" 
                    maxlength="45">
                </td>
                <td>
                  <input  
                    v-model="u.lastname" 
                    type="text" 
                    class="form-control" 
                    required="" 
                    maxlength="45">
                </td>
                <td>

                  <div class="custom-control custom-checkbox">
                    <input 
                      :id="'is-Locked-'+u.id" 
                      v-model="u.isLocked" 
                      type="checkbox" 
                      class="custom-control-input">
                    <label 
                      :for="'is-Locked-'+u.id" 
                      class="custom-control-label"/>
                  </div>
                 
                </td>
                <td><button 
                  class="btn btn-secondary" 
                  @click="updateUser(u)">Update</button></td>
                <td>
                  <button 
                    class="btn btn-secondary" 
                    @click="removeUser(u)">Remove</button>
                </td>
                <td>
                  <button 
                    class="btn btn-secondary" 
                    @click="setPasswordView(u)">Set Password/Views</button>
                </td>
                <td><button 
                  class="btn btn-secondary" 
                  @click="setView(u)">View FinanceView</button></td>
              </tr>
            </tbody>
          </table>
          <hr class="mb-4">
          <button 
            class="btn btn-secondary btn-lg btn-block" 
            @click="scrollToTop()">Go to top</button>
        </div>

        <transition 
          name="fade">
          <div 
            v-if="passwordChangeIsActivated" 
            class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal">
            <h3 class="border-bottom border-gray pb-2 mb-0">Set password/years of user <b>{{ currentUserToChangePassword.username }}</b></h3>
            <br>
            <p class="lead">Set the password of the current user:</p>
            <table class="table table-striped table-hover">
              <thead class="">
                <tr>
                  <th scope="col">#</th>
                  <th scope="col">Username</th>
                  <th scope="col">New Password</th>
                  <th scope="col">Update</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <th scope="row">{{ currentUserToChangePassword.id }}</th>
                  <td>{{ currentUserToChangePassword.username }}</td>
                  <td>
                    <input   
                      v-model="currentUserToChangePassword.password" 
                      type="password" 
                      class="form-control"
                      required=""
                      maxlength="45">
                  </td>
                  <td><button 
                    :disabled="currentUserToChangePassword.password==''" 
                    class="btn btn-secondary" 
                    @click="setNewPassword()">Update Password</button></td>
                </tr>
              </tbody>
            </table>
            <br >
            <p class="lead">Add and remove years for current user:</p>
            <table class="table table-striped table-hover">
              <thead class="">
                <tr>
                  <th scope="col">Viewname</th>
                  <th scope="col">Action</th>
                </tr>
              </thead>
              <tbody>
                <tr 
                  v-for="y in currentUserToChangePassword.years" 
                  :key="y">
                  <th scope="row">{{ y }}</th>
                  <td><button 
                    class="btn btn-secondary" 
                    @click="removeYear(y)">Remove Year</button></td>
                </tr>
                <tr class="lastRow">
                  <th><input   
                    v-model="newYear" 
                    type="text" 
                    class="form-control"
                    required=""
                    maxlength="45"></th>
                  <td>
                    <button 
                      :disabled="newYear==''" 
                      class="btn btn-secondary"
                      @click="addYear()">Add Year {{ newYear }}</button>
                  </td>
                </tr>
              </tbody>
            </table>
            <hr class="mb-4">
            <button 
              class="btn btn-secondary btn-lg btn-block" 
              @click="scrollToTop(),passwordChangeIsActivated = false">Hide &amp; go to top</button>
          </div>
        </transition>

      </main>
      
      
    </div>
  </div>
</template>

<script>
import Bobbles from '~/components/UI/bobbles'
// import BootstrapToggle from 'vue-bootstrap-toggle'

export default {
  components: {
    Bobbles
    // BootstrapToggle
  },
  layout: 'loggedIn',
  data() {
    return {
      users: [],
      itemUsername: '',
      itemPassword: '',
      itemFirstname: '',
      itemLastname: '',
      itemIsLocked: false,
      currentId: 1,
      passwordChangeIsActivated: false,
      newYear: '',
      currentUserToChangePassword: {}
    }
  },
  mounted() {
    this.loadAll()
  },
  methods: {
    loadAll() {
      this.$axios
        .get('admin/options.php?action=getUsers')
        .then(response => {
          let api = response.data
          api.forEach(element => {
            this.users.push({
              id: this.currentId++,
              username: element.username,
              originalUsername: element.username,
              firstname: element.firstname,
              lastname: element.lastname,
              isLocked: element.isLocked,
              password: '',
              years: element.years
            })
          })
          console.log(api)
        })
        .catch(error => {
          alert(
            'Could not load users. An error calling the middleware occured. Look in the console for further information.'
          )
        })
    },
    addUser() {
      let newUser = {
        id: this.currentId++,
        username: this.itemUsername,
        firstname: this.itemFirstname,
        lastname: this.itemLastname,
        isLocked: this.itemIsLocked,
        password: this.itemPassword
      }

      if (
        newUser.username == '' ||
        newUser.firstname == '' ||
        newUser.lastname == '' ||
        newUser.password == '' ||
        newUser.password == ' '
      ) {
        alert('Please fill out all fields.')
      } else if (newUser.username.includes(' ')) {
        alert('Username is not allowed to have wite spaces.')
      } else if (this.doesUserAlreadyExists(newUser.username)) {
        alert('Username "' + newUser.username + '" exists already.')
      } else {
        this.$axios
          .post('admin/options.php?action=addUser', newUser)
          .then(response => {
            let api = response.data
            if (api.username != null) {
              this.users.push(api)
              this.itemUsername = this.itemPassword = this.itemFirstname = this.itemLastname = this.itemIsLocked =
                ''
              this.scrollToEnd()
            } else {
              console.log(reponse)
              alert('Could not add a new User. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not add a new User. Check console for more info.')
          })
      }
    },
    doesUserAlreadyExists(username) {
      let exists = false
      this.users.forEach(element => {
        if (username === element.username) {
          exists = true
        }
      })
      return exists
    },
    removeUser(user) {
      if (
        confirm('Are you sure you want to delte user ' + user.username + '?')
      ) {
        this.$axios
          .post('admin/options.php?action=removeUser', {
            username: user.username
          })
          .then(response => {
            let api = response.data
            if (api.message == 'User deleted.') {
              this.users.splice(this.users.indexOf(user), 1)
              this.passwordChangeIsActivated = false
            } else {
              console.log(reponse)
              alert('Could not delete user. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not delete user. Check console for more info.')
          })
      }
    },
    updateUser(user) {
      this.$axios
        .post('admin/options.php?action=updateUser', user)
        .then(response => {
          let api = response.data
          if (api.username != null) {
            user.firstname = api.firstname
            user.lastname = api.lastname
            user.isLocked = api.isLocked
            setTimeout(() => {
              alert('User ' + user.username + ' was successfully updated!')
            }, 100)
          } else {
            console.log(reponse)
            alert('Could not update user. Check console for more info.')
          }
        })
        .catch(error => {
          alert('Could not update user. Check console for more info.')
        })
    },
    setPasswordView(user) {
      this.currentUserToChangePassword = user
      this.passwordChangeIsActivated = true
      setTimeout(() => {
        this.scrollToEnd()
      }, 100)
    },
    setNewPassword() {
      this.$axios
        .post('admin/options.php?action=setPassword', {
          username: this.currentUserToChangePassword.username,
          newPassword: this.currentUserToChangePassword.password
        })
        .then(response => {
          let api = response.data
          if (api.message == 'Password set.') {
            this.currentUserToChangePassword.password = ''
            setTimeout(() => {
              alert(
                'Pasword for user ' +
                  this.currentUserToChangePassword.username +
                  ' was successfully set!'
              )
            }, 100)
          } else {
            console.log(reponse)
            alert('Could not set password. Check console for more info.')
          }
        })
        .catch(error => {
          alert('Could not set password. Check console for more info.')
        })
    },
    addYear() {
      let newYearObject = {
        username: this.currentUserToChangePassword.username,
        year: this.newYear
      }

      if (
        !this.doesYearForUserAlreadyExists(
          this.currentUserToChangePassword.username,
          this.newYear
        )
      ) {
        this.$axios
          .post('admin/options.php?action=addYear', newYearObject)
          .then(response => {
            let api = response.data
            if (api.newYear != null) {
              this.currentUserToChangePassword.years.push(api.newYear)
              this.newYear = ''
            } else {
              console.log(reponse)
              alert('Could not add a new year. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not add a new year. Check console for more info.')
          })
      } else {
        alert('Year ' + this.newYear + ' does already exist.')
      }
    },
    doesYearForUserAlreadyExists(username, year) {
      let exists = false
      this.users.forEach(element => {
        if (username === element.username) {
          element.years.forEach(yearElement => {
            if (year == yearElement) {
              exists = true
            }
          })
        }
      })
      return exists
    },
    removeYear(year) {
      //if(this.currentUserToChangePassword.years>1) ==> spaeter noch implementieren mit fehlermeldung
      this.currentUserToChangePassword.years.splice(
        this.currentUserToChangePassword.years.indexOf(year),
        1
      )
    },
    setView(user) {
      this.$axios
        .post('admin/options.php?action=setView', { username: user.username })
        .then(response => {
          let api = response.data
          if (api.message == 'Username for view set.') {
            this.$router.replace('/dashboard')
          } else {
            console.log(reponse)
            alert('Could not set View. Check console for more info.')
          }
        })
        .catch(error => {
          alert('Could not set View. Check console for more info.')
        })
    },
    scrollToEnd() {
      var container = this.$el.querySelector('#content')
      container.scrollTop = container.scrollHeight
    },
    scrollToTop() {
      var container = this.$el.querySelector('#content')
      container.scrollTop = 0
    }
  }
}
</script>

<style scoped>
#content {
  position: absolute;
  width: 100%;
  left: 0;
  height: 93.5%;
  height: calc(100% - 59px);
  overflow-y: scroll;
}

.table thead th {
  border-top: none;
}

.table .custom-checkbox .custom-control-label::before {
  height: 31px;
  width: 31px;
  position: relative;
}
.table .custom-checkbox .custom-control-label::after {
  width: 100%;
  height: 100%;
}
.custom-checkbox {
  padding-left: 2.5rem;
}
</style>
