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

          <span>
            <input 
              id="overview-view" 
              type="radio" 
              name="views"> 
            <div 
              class="item" 
              @click="disableAll(), overviewIsEnabled=true">
              <div class="details">
                <p class="name">Overview</p> 
                <p class="description">Overview of all views.</p> 
                <div class="actions">
                  <div class="info equals"/> 
                  <div>
                    <p class="description">This view shows all views in an overall view.</p>
                  </div>
                </div>
              </div> 
              <label for="overview-view"/>
            </div> 
          </span>

          <span>
            <input 
              id="addNew-view" 
              type="radio" 
              name="views"> 
            <div 
              class="item" 
              @click="disableAll(), createNewViewEnabled=true">
              <div class="details">
                <p class="name">Add view</p> 
                <p 
                  class="description" 
                  style="width: 230px;">Add a new view to your current entries.</p> 
                <div class="actions">
                  <div class="info plus"/> 
                  <div>
                    <p 
                      class="description" 
                      style="width: 250px;"> This creates a new view for FinanceViewer with the current selected year. </p>
                  </div>
                </div>
              </div> 
              <label for="addNew-view"/>
            </div> 
          </span>

          <span 
            v-for="u in tableViews" 
            :key="u.id">
            <input 
              :id="'view-'+u.count" 
              type="radio"
              name="views">
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
              <label :for="'view-'+u.count"/>
            </div>
            <style>
              #view-{{ u.count }}:checked ~ #menu_right {
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
          <span v-if="overviewIsEnabled">
            <h1> Overview for all views </h1>
            <table class="table table-striped table-hover">
              <thead class="">
                <tr>
                  <th scope="col"><b>Name</b></th>
                  <th 
                    scope="col" 
                    class="tableBorderLeft">January</th>
                  <th scope="col">February</th>
                  <th scope="col">March</th>
                  <th scope="col">April</th>
                  <th scope="col">May</th>
                  <th scope="col">June</th>
                  <th scope="col">July</th>
                  <th scope="col">August</th>
                  <th scope="col">September</th>
                  <th scope="col">October</th>
                  <th scope="col">November</th>
                  <th scope="col">December</th>
                  <th 
                    scope="col" 
                    class="tableBorderLeft">Sum</th>
                  <th 
                    scope="col" 
                    class="tableBorderLeft">Delete</th>
                </tr>
              </thead>
              <tbody>
                <tr 
                  v-for="u in tableViews"
                  :key="u.name" 
                  class="text-right">
                  <th 
                    scope="row" 
                    class="text-left">{{ u.name }}</th>
                  <td class="tableBorderLeft">&pound;{{ eval(u.data.january) }}</td>
                  <td>&pound;{{ eval(u.data.february) }}</td>
                  <td>&pound;{{ eval(u.data.march) }}</td>
                  <td>&pound;{{ eval(u.data.april) }}</td>
                  <td>&pound;{{ eval(u.data.may) }}</td>
                  <td>&pound;{{ eval(u.data.june) }}</td>
                  <td>&pound;{{ eval(u.data.july) }}</td>
                  <td>&pound;{{ eval(u.data.august) }}</td>
                  <td>&pound;{{ eval(u.data.september) }}</td>
                  <td>&pound;{{ eval(u.data.october) }}</td>
                  <td>&pound;{{ eval(u.data.november) }}</td>
                  <td>&pound;{{ eval(u.data.december) }}</td>
                  <td class="tableBorderLeft"><b>&pound;{{ sumForYear(u.data) }}</b></td>
                  <td class="tableBorderLeft"><button 
                    class="btn btn-secondary" 
                    @click="removeView(u)">Delete</button></td>
                </tr>
                <tr 
                  class="lastRow text-right">
                  <th 
                    scope="row" 
                    class="text-left">Sum</th>
                  <td class="tableBorderLeft"><b>&pound;{{ getSumJanuary() }}</b></td>
                  <td><b>&pound;{{ getSumFebruary() }}</b></td>
                  <td><b>&pound;{{ getSumMarch() }}</b></td>
                  <td><b>&pound;{{ getSumApril() }}</b></td>
                  <td><b>&pound;{{ getSumMay() }}</b></td>
                  <td><b>&pound;{{ getSumJune() }}</b></td>
                  <td><b>&pound;{{ getSumJuly() }}</b></td>
                  <td><b>&pound;{{ getSumAugust() }}</b></td>
                  <td><b>&pound;{{ getSumSeptember() }}</b></td>
                  <td><b>&pound;{{ getSumOctober() }}</b></td>
                  <td><b>&pound;{{ getSumNovember() }}</b></td>
                  <td><b>&pound;{{ getSumDecember() }}</b></td>
                  <td class="tableBorderLeft"><b>{{ getTotalSum() }}</b></td>
                  <td class="tableBorderLeft text-center">-</td>
                </tr>
              </tbody>
            </table>
          </span>
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
              <h3 class="border-bottom border-gray pb-2 mb-0">Add a new view</h3>
              <br>
              <p class="lead">Add a new view:</p>
              <hr class="mb-4">
              <p class="lead">Preview:</p>

              <span 
                id="menu_left" 
                style="border: none;">
                <input 
                  id="view-x" 
                  type="radio" 
                  name="sample">
                <div 
                  id="sampleItem" 
                  class="item">
                  <div 
                    id="sampleView" 
                    class="portrait"/> 
                  <div class="details">
                    <p class="name">{{ newView_name }}</p> 
                    <p class="description">{{ newView_description }}</p>
                    <div class="actions">
                      <div class="info"/> 
                      <div>
                        <p class="description"> {{ newView_notes }} </p>
                      </div>
                    </div>
                  </div>
                  <label for="view-x"/>
                </div> 
                <style>
                  #view-x:checked ~ #menu_right {
                  background-image: url('{{ newView_profile_url }}');
                  }
                  #sampleView {
                  background-image: url('{{ newView_profile_url }}');
                  }
                </style>
              </span>

              <div class="custom-control custom-radio">
                <input 
                  id="sampleReset" 
                  name="sample" 
                  type="radio" 
                  class="custom-control-input" 
                  checked="" 
                  required="">
                <label 
                  class="custom-control-label" 
                  for="sampleReset">Reset</label>
              </div>


            </div>

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
      overviewIsEnabled: true,
      createNewViewEnabled: false,
      newView_name: 'FVMarkt',
      newView_description: 'Supermarket',
      newView_id: 'fvmarket',
      newView_notes: 'This is my local market',
      newView_profile_url:
        'https://cdn.aldi-digital.co.uk/32FDVWu4Lhbxgj9Z3v03ji0pGJIp?',
      tableViews: [
        {
          name: 'Aldi',
          description: 'Supermarket',
          id: 'adli',
          count: 1,
          notes: 'This is the Aldi supermarket. I use it to buy groceries.',
          viewEnabled: false,
          profile_url:
            'https://cdn.aldi-digital.co.uk/32FDVWu4Lhbxgj9Z3v03ji0pGJIp?',
          data: {
            january: '1+1+1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://pbs.twimg.com/profile_images/1018756618635431936/9rRo7jvO_400x400.jpg',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'http://mediad.publicbroadcasting.net/p/wkar/files/styles/x_large/public/201706/10398927_9465478123_740_n.jpg',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://ichef.bbci.co.uk/food/ic/food_16x9_832/recipes/spaghetti_bolognese_with_23409_16x9.jpg',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://lumiere-a.akamaihd.net/v1/images/open-uri20150422-20810-1fndzcd_41017374.jpeg',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://www.zervant.com/wp-content/uploads/2016/07/sole-trader-invoice-vat.png',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://i5.walmartimages.com/asr/f8bb1886-fb1a-4bd4-8b0c-3107bdd8c9aa_1.25d0865f300a0ec45affe204985f243b.jpeg?odnHeight=450&odnWidth=450&odnBg=FFFFFF',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
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
            'https://www.flsouthern.edu/getattachment/5f1866fb-be3b-4b03-bce5-4ffcd2c8ffb0/pre-pharmacy.aspx',
          data: {
            january: '1',
            february: '2',
            march: '3',
            april: '4',
            may: '5',
            june: '6',
            july: '7',
            august: '8',
            september: '9',
            october: '10',
            november: '11',
            december: '12'
          }
        }
      ]
    }
  },
  methods: {
    disableAll() {
      this.tableViews.forEach(element => {
        element.viewEnabled = false
      })
      this.overviewIsEnabled = false
      this.createNewViewEnabled = false
    },
    removeView(view) {
      this.tableViews.splice(this.tableViews.indexOf(view), 1)
    },
    eval(content) {
      return eval(content)
    },
    sumForYear(yearArray) {
      return (
        eval(yearArray.january) +
        eval(yearArray.february) +
        eval(yearArray.march) +
        eval(yearArray.april) +
        eval(yearArray.may) +
        eval(yearArray.june) +
        eval(yearArray.july) +
        eval(yearArray.august) +
        eval(yearArray.september) +
        eval(yearArray.october) +
        eval(yearArray.november) +
        eval(yearArray.december)
      )
    },
    getSumJanuary() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.january)
      })
      return result
    },
    getSumFebruary() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.february)
      })
      return result
    },
    getSumMarch() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.march)
      })
      return result
    },
    getSumApril() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.april)
      })
      return result
    },
    getSumMay() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.may)
      })
      return result
    },
    getSumJune() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.june)
      })
      return result
    },
    getSumJuly() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.july)
      })
      return result
    },
    getSumAugust() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.august)
      })
      return result
    },
    getSumSeptember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.september)
      })
      return result
    },
    getSumOctober() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.october)
      })
      return result
    },
    getSumNovember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.november)
      })
      return result
    },
    getSumDecember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.december)
      })
      return result
    },
    getTotalSum() {
      return (
        this.getSumJanuary() +
        this.getSumFebruary() +
        this.getSumMarch() +
        this.getSumApril() +
        this.getSumMay() +
        this.getSumJune() +
        this.getSumJuly() +
        this.getSumAugust() +
        this.getSumSeptember() +
        this.getSumOctober() +
        this.getSumNovember() +
        this.getSumDecember()
      )
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
  /* left-menu code partly used from https://codepen.io/meChrisReed/pen/mtHna?love_type=gamma& */
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
  width: 95%;
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
.plus:after {
  content: '+';
}
.equals:after {
  content: '=';
}
.info:hover:after {
  background-color: rgba(255, 255, 255, 0.2);
}

#sampleItem {
  float: none;
  width: 370px;
  left: 35%;
  left: calc(50% - 185px);
  background-color: rgba(0, 0, 0, 0.6);
  color: white;
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
