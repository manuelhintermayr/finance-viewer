<template>
  <div class="fill-height">
    <bobbles />
    <div 
      id="content">
      <div 
        id="menu" 
        class="row fill-height">
        <div  
          id="menu_left" 
          class="col-md-3 scrollbar scrollbarLeft fill-height">
          <span 
            v-for="u in tableViews" 
            :key="u.id">
            <input 
              :id="'contact-'+u.count" 

              type="radio"
              name="contatcs">
            <div 
              class="item" 
              @click="disableAll(), u.viewEnabled=true">
              <div 
                :id="u.id" 
                class="portrait"/>
              <div class="details">
                <p class="name">{{ u.name }}</p>
                <p class="description">{{ u.description }}</p>
                <div class="actions">
                  <div class="info"/>
                  <div> 
                    <p class="description"> {{ u.notes }} </p> 
                  </div>
                </div>
              </div>
              <label :for="'contact-'+u.count"/>
            </div>
            <style>
              #contact-{{ u.count }}:checked ~ #menu_right {
              background-image: url('{{ u.profile_url }}');
              }
              #{{ u.id }} {
              background-image: url('{{ u.profile_url }}');
              }
            </style>
          </span>
        </div>

        <div 
          id="menu_right" 
          class="col-md-8 center-block scrollbar scrollbarRight fill-height">

          <!-- start area for total overview -->
          <!-- end area for total overview -->

          <!-- start area for specific view -->
          <span 
            v-for="u in tableViews" 
            v-if="u.viewEnabled" 
            :key="u.id"> {{ u.name }} - is shown <br>
          </span>
          <!-- end area for specific view -->
          
          <!-- start area for create new entry -->
          <span v-if="createNewViewEnabled" >
            <div 
              class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal">
              <h3 class="border-bottom border-gray pb-2 mb-0">Set password of user</h3>
              <br>
              <p class="lead">Set the password of a current user:</p>
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
                        type="text" 
                        class="form-control"
                        required=""
                        maxlength="45">
                    </td>
                    <td><button class="btn btn-secondary">Update Password</button></td>
                  </tr>
                </tbody>
              </table>
              <hr class="mb-4">
              <button 
                class="btn btn-secondary btn-lg btn-block" 
                @click="createNewViewEnabled=!createNewViewEnabled">Hide &amp; go to top</button>
            </div>
            <p class="lead center-block"> Dashboard for normal user. <a href="..\logout.php">Logout</a> </p>
          </span>
          <!-- end area for create new entry -->

        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Bobbles from '~/components/UI/bobbles'
import jquery from 'jquery'

export default {
  components: {
    Bobbles
  },
  layout: 'loggedIn',
  data() {
    return {
      loggedOut: false,
      createNewViewEnabled: true,
      currentUserToChangePassword: {
        id: 0,
        username: '',
        origianlUsername: '',
        firstname: '',
        lastname: '',
        isLocked: false,
        password: ''
      },
      tableViews: [
        {
          name: 'Aldi',
          description: 'Supermarket',
          id: 'adli',
          count: 1,
          notes: 'This is the Aldi supermarket. I use it to buy groceries.',
          viewEnabled: false,
          profile_url:
            'https://cdn.aldi-digital.co.uk/32FDVWu4Lhbxgj9Z3v03ji0pGJIp?'
        },
        {
          name: 'Wilko',
          description: 'Supermarket',
          id: 'wilko',
          count: 2,
          notes:
            'Wilko is like the austrian IKEA. You can buy practically everything.',
          viewEnabled: false,
          profile_url:
            'https://pbs.twimg.com/profile_images/1018756618635431936/9rRo7jvO_400x400.jpg'
        },
        {
          name: 'Amazon',
          description: 'Online E-Commerce',
          id: 'amazon',
          count: 3,
          notes:
            'Amazon is an online eCommerce. I use it to buy stuff and proteins.',
          viewEnabled: false,
          profile_url:
            'http://mediad.publicbroadcasting.net/p/wkar/files/styles/x_large/public/201706/10398927_9465478123_740_n.jpg'
        },
        {
          name: 'Food',
          description: 'Food not included in supermarket',
          id: 'food',
          count: 4,
          notes:
            'Everything I eat (besides from what I buy at the supermarket) is included in this list.',
          viewEnabled: false,
          profile_url:
            'https://ichef.bbci.co.uk/food/ic/food_16x9_832/recipes/spaghetti_bolognese_with_23409_16x9.jpg'
        },
        {
          name: 'Car',
          description: 'Car costs, insurance, gasoline',
          id: 'car',
          count: 5,
          notes:
            'A car doesn`t come cheap. Everything is spend for this car is included here.',
          viewEnabled: false,
          profile_url:
            'https://lumiere-a.akamaihd.net/v1/images/open-uri20150422-20810-1fndzcd_41017374.jpeg'
        },
        {
          name: 'Invoices',
          description: 'Monthly costs, rent, etc.',
          id: 'invoices',
          count: 6,
          notes:
            'All my invoices, like rent, gas, electricity and else is included in this list.',
          viewEnabled: false,
          profile_url:
            'https://www.zervant.com/wp-content/uploads/2016/07/sole-trader-invoice-vat.png'
        },
        {
          name: 'Phone',
          description: 'Costs for the phone provider',
          id: 'phone',
          count: 7,
          notes:
            'A phone doesn`t come cheat. The monthly costs for a phone are included here.',
          viewEnabled: false,
          profile_url:
            'https://i5.walmartimages.com/asr/f8bb1886-fb1a-4bd4-8b0c-3107bdd8c9aa_1.25d0865f300a0ec45affe204985f243b.jpeg?odnHeight=450&odnWidth=450&odnBg=FFFFFF'
        },
        {
          name: 'Medicine',
          description: 'Medicine bought from the pharmacy',
          id: 'medicine',
          count: 8,
          notes:
            'Descriptions and everything I use medically is included here.',
          viewEnabled: false,
          profile_url:
            'https://www.flsouthern.edu/getattachment/5f1866fb-be3b-4b03-bce5-4ffcd2c8ffb0/pre-pharmacy.aspx'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy9',
          count: 9,
          notes: 'blabla',
          viewEnabled: false,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy Last',
          description: 'Developer',
          id: 'chris-lacy10',
          count: 10,
          notes: 'blabla',
          viewEnabled: false,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        }
      ]
    }
  },
  methods: {
    disableAll() {
      this.tableViews.forEach(element => {
        element.viewEnabled = false
      })
      this.createNewViewEnabled = false
    }
  }
}
</script>

<style>
#content {
  height: 93.5%;
  height: calc(100% - 59px);
}

#main_nav {
  border-radius: 0;
  background: rgba(34, 34, 34, 0.86);
  border-color: #080808;
  border-bottom: 2px #080808 solid;
}

#menu_left {
  border-right: 2px #080808 solid;
  position: relative;
  max-width: 100%;
  overflow-x: hidden;
  padding-right: 0px;
  background-color: rgba(0, 0, 0, 0.6);
}

#menu_right {
  position: relative;
  overflow: scroll;
  overflow-x: hidden;
  flex: 0 0 74.17%;
  max-width: 100% !important;
}

#menu_left input {
  position: absolute;
  visibility: hidden;
}

#background {
  position: absolute;
  width: 102%;
  height: 100%;
  background-size: cover;
  background-position: center;
  -webkit-filter: blur(3px);
  z-index: 0;
}

#menu_left label {
  position: absolute;
  left: 0;
  height: 100%;
  width: 100%;
  cursor: pointer;
}

.item {
  position: relative;
  float: left;

  width: 100%;
  height: 6em;
  padding: 1px 0;

  z-index: 3;
  overflow: hidden;
  -webkit-transition: background-color 0.7s;
}

.portrait {
  position: relative;
  float: left;

  height: 3em;
  width: 3em;

  border-radius: 50%;

  margin: 1.5em;

  background-size: cover;
}
.portrait:after {
  content: ' ';
  position: absolute;
  top: -0.3em;
  left: -0.3em;

  height: 3.5em;
  width: 3.5em;

  border: 1px solid rgba(255, 255, 255, 0.5);
  border-radius: 50%;
  -webkit-transition: border 0.3s;
}

.details {
  position: relative;
  float: left;
  top: 0;

  width: 55%;
  margin: 1em 0 0 0.75em;

  -webkit-transition: top 0.5s;
}

.name,
.description {
  position: relative;
  float: left;
  clear: left;
  margin: 0;
}

.name {
  font-size: 1.5em;
  font-weight: 300;
}
.description {
  font-size: 0.9em;
  color: rgba(255, 255, 255, 0.8);
}

.actions {
  position: absolute;
  top: 7em;

  width: 100%;
}
.actions > div {
  position: absolute;
  width: 100%;
  color: transparent;
  -webkit-text-stroke-width: 0.5px;
  -webkit-text-stroke-color: #fff;

  cursor: pointer;
}
.actions > div:nth-child(2) {
  left: 30%;
}
.actions > div:nth-child(3) {
  left: 60%;
}
.actions > div:nth-child(4) {
  left: 90%;
}

.actions > div:hover {
  -webkit-text-stroke-width: 0;
  color: #fff !important;
}

.info {
  font-size: 100% !important;
}
.info:after {
  font-size: 1.7em;
  content: 'i';
  font-family: Consolas !important;

  color: #fff;
  -webkit-text-stroke-width: 0;

  border: 1px solid #fff;
  border-radius: 50%;
  padding: 2px 11px;
}
.info:hover:after {
  background-color: rgba(255, 255, 255, 0.2);
}

/* Genreal Interaction */
#menu_left .item:hover > .portrait:after {
  border: 1px solid rgba(116, 226, 21, 1);
  box-shadow: 0 0 3px rgba(116, 226, 21, 0.5);
}

#menu_left input:checked + .item {
  background-color: rgba(0, 0, 0, 0.3); /**/
  padding-top: 0px;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  border-bottom: 1px solid rgba(0, 0, 0, 0.5);
}

#menu_left input:checked + .item > .details {
  top: -6em;
  z-index: 2;
}

/*bloede Bugfixes*/
.container {
  position: inherit;
  /*Beim laden mit den Ajax-Requests kam es vor, 
        dass dieser Wert vom style_login.css ueberschrieben
        wurde und dadurch der Style komplett anders war. Das wurde hiermit
        gefixt.*/
}

/* Scrollbar */
.scrollbar::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}

.scrollbar.thin::-webkit-scrollbar {
  width: 2px;
}

.scrollbar::-webkit-scrollbar-track,
.scrollbar::-webkit-scrollbar-track,
.scrollbar::-webkit-scrollbar-track-piece {
  border-radius: 0px;
}

.scrollbar::-webkit-scrollbar-thumb {
  border-radius: 10px;
}

.scrollbarLeft::-webkit-scrollbar-track,
.scrollbarLeft::-webkit-scrollbar-track,
.scrollbarLeft::-webkit-scrollbar-track-piece {
  background: #204b42;
}

.scrollbarLeft::-webkit-scrollbar-thumb {
  background: #52d0a5;
}
.scrollbarLeft::-webkit-scrollbar-thumb:window-inactive {
  background: #3fb68d;
}

.scrollbarRight::-webkit-scrollbar {
  width: 14px;
  height: 8px;
}
.scrollbarRight::-webkit-scrollbar-track,
.scrollbarRight::-webkit-scrollbar-track,
.scrollbarRight::-webkit-scrollbar-track-piece {
  background: #52d0a5;
}
.scrollbarRight::-webkit-scrollbar-thumb {
  background: #204b42;
}
.scrollbarRight::-webkit-scrollbar-thumb:window-inactive {
  background: #204b42;
}
</style>
