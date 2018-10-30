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

              <form 
                class="needs-validation" 
                novalidate="">
                <div class="row">
                  <div class="col-md-6 mb-3">
                    <label for="viewName">Name</label>
                    <input 
                      id="viewName" 
                      v-model="newView_name" 
                      type="text" 
                      class="form-control" 
                      placeholder="" 
                      value="" 
                      maxlength="45"
                      required="">
                    <div class="invalid-feedback">
                      Valid name is required.
                    </div>
                  </div>
                  <div class="col-md-6 mb-3">
                    <label for="viewDescription">Description</label>
                    <input 
                      id="viewDescription" 
                      v-model="newView_description" 
                      type="text" 
                      class="form-control" 
                      placeholder="" 
                      value="" 
                      maxlength="80"
                      required="" >
                    <div class="invalid-feedback">
                      Valid description is required.
                    </div>
                  </div>
                </div>

                <div class="mb-3">
                  <label for="viewID">ID</label>
                  <div class="input-group">
                    <div class="input-group-prepend">
                      <span class="input-group-text">@</span>
                    </div>
                    <input 
                      id="viewID" 
                      v-model="newView_id" 
                      type="text" 
                      class="form-control" 
                      placeholder="Username" 
                      required=""
                      maxlength="45">
                    <div 
                      class="invalid-feedback" 
                      style="width: 100%;">
                      A id is required.
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6 mb-3">
                    <label for="viewNotes">Notes</label>
                    <input 
                      id="viewNotes" 
                      v-model="newView_notes" 
                      type="text" 
                      class="form-control" 
                      placeholder="" 
                      value="" 
                      maxlength="80"
                      required="">
                    <div class="invalid-feedback">
                      Valid notes are required.
                    </div>
                  </div>
                  <div class="col-md-6 mb-3">
                    <label for="viewPicUrl">Profile Url</label>
                    <input 
                      id="viewPicUrl" 
                      v-model="newView_profile_url" 
                      type="text" 
                      class="form-control" 
                      placeholder="" 
                      value="" 
                      maxlength="80"
                      required="" >
                    <div class="invalid-feedback">
                      Valid profile url is required.
                    </div>
                  </div>
                </div>

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
                <hr class="mb-4">

                <button 
                  class="btn btn-secondary btn-lg btn-block" 
                  @click.prevent="addView">Add View</button>
              </form>




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
      newView_name: 'FVMarkt2',
      newView_description: 'Supermarket',
      newView_id: 'fvmarket2',
      newView_notes: 'This is my local market',
      newView_profile_url:
        'https://cdn.stocksnap.io/img-thumbs/960w/AWJD4WV6W1.jpg',
      tableViews: [
        {
          name: 'FVMarket',
          description: 'Supermarket',
          id: 'fvmarket',
          count: 1,
          notes: 'This is the FVMarket supermarket. I use it to buy groceries.',
          viewEnabled: false,
          profile_url:
            'https://cdn.stocksnap.io/img-thumbs/960w/AWJD4WV6W1.jpg',
          data: {
            january: '1+1+41.69+21.49+43.99+14.29',
            february: '13.99+52.39+10,79+21.49+23.49',
            march: '0,45+1,39+2,79+0,2+1,99+2,49+0,99+1,49+0,89',
            april:
              '43.54-6+16.55+15.42+3.58+6.86+10.17+17.76+-20+2.47+19.15+4.29+18.3+22.02+7.84+6.98',
            may: '23.97+2.75+16.39+10.53+18.99+5.34+4.31+5.85+24.34+4.74+12.93',
            june: '89.4',
            july: '120.3',
            august: '153.58',
            september: '194.01',
            october: '79.99',
            november: '112.31',
            december: '126.29'
          }
        },
        {
          name: 'Richard',
          description: 'Supermarket',
          id: 'richard',
          count: 2,
          notes:
            'Richard is like the austrian big supermarket. You can buy practically everything.',
          viewEnabled: false,
          profile_url:
            'https://cdn.stocksnap.io/img-thumbs/960w/R0CTZDRFPY.jpg',
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
          name: 'Amazing',
          description: 'Online E-Commerce',
          id: 'amazing',
          count: 3,
          notes:
            'Amazing is an online eCommerce. I use it to buy stuff and proteins.',
          viewEnabled: false,
          profile_url:
            'https://image.shutterstock.com/image-photo/boxes-trolley-on-laptop-keyboard-450w-580499569.jpg',
          data: {
            january: '123.45',
            february: '20+35.81+11.04+39.9',
            march: '30.2',
            april: '39.1+20+1.30+40.91',
            may: '88.20',
            june: '60.19',
            july: '7.91',
            august: '88.29',
            september: '90.69',
            october: '101.29',
            november: '119.14',
            december: '124.17'
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
            'https://image.shutterstock.com/image-photo/healthy-salad-bowl-quinoa-tomatoes-450w-521741356.jpg',
          data: {
            january: '49.25',
            february: '29.49',
            march: '90.19',
            april: '4+1+5+3+3.50+4.5+1+1+1+2+1',
            may: '5+5+6+7+9',
            june: '6+4.5',
            july: '7+70',
            august: '89.13',
            september: '99-1',
            october: '100.01+20.29',
            november: '111',
            december: '122'
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
            'https://image.shutterstock.com/image-photo/creative-background-highbeam-headlight-newest-450w-1191643888.jpg',
          data: {
            january: '90+450',
            february: '80+550',
            march: '90+350.55',
            april: '80+154.23',
            may: '90+431.49',
            june: '80+350',
            july: '90+210',
            august: '80+482',
            september: '90+211',
            october: '80+30',
            november: '91+40',
            december: '82+430'
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
            'https://image.shutterstock.com/image-vector/e-commerce-order-450w-1148589737.jpg',
          data: {
            january: '750+35+35+25+25',
            february: '750+35+35+25+30',
            march: '750+35+35+25+18.78',
            april: '750+35+35+25+69',
            may: '750+35+35+25+169',
            june: '750+35+35+25+385',
            july: '750+35+35+25+342',
            august: '750+35+35+25+295',
            september: '750+9+35+35+25+284',
            october: '750+10+35+35+25+284',
            november: '750+112+35+35+25+10',
            december: '712+35+35+25+130'
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
            'https://image.shutterstock.com/image-photo/hand-holding-black-phone-isolated-450w-386618683.jpg',
          data: {
            january: '14.2',
            february: '14.2',
            march: '14.2',
            april: '14.2',
            may: '14.2+30',
            june: '14.2',
            july: '14.2+2.60',
            august: '8+14.2',
            september: '19',
            october: '19',
            november: '19',
            december: '19'
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
            'https://cdn.stocksnap.io/img-thumbs/960w/LKM1T38B6S.jpg',
          data: {
            january: '12+4',
            february: '2+1.3',
            march: '3',
            april: '6+3.3+10.95+3+4.05+8.3',
            may: '12+5.2+2.2',
            june: '1+29+4.2',
            july: '15.4',
            august: '9.20',
            september: '9',
            october: '26.9+29.9+6',
            november: '12+8.99',
            december: '13.99+25.28'
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
    addView() {
      this.tableViews.push({
        name: this.newView_name,
        description: this.newView_description,
        id: this.newView_id,
        count: this.newView_id,
        notes: this.newView_notes,
        viewEnabled: false,
        profile_url: this.newView_profile_url,
        data: {
          january: '0',
          february: '0',
          march: '0',
          april: '0',
          may: '0',
          june: '0',
          july: '0',
          august: '0',
          september: '0',
          october: '0',
          november: '0',
          december: '0'
        }
      })
      this.newView_name = this.newView_description = this.newView_id = this.newView_notes = this.newView_profile_url =
        ''
    },
    removeView(view) {
      this.tableViews.splice(this.tableViews.indexOf(view), 1)
    },
    eval(content) {
      return Math.round(eval(content) * 100) / 100
    },
    sumForYear(yearArray) {
      return this.eval(
        this.eval(yearArray.january) +
          this.eval(yearArray.february) +
          this.eval(yearArray.march) +
          this.eval(yearArray.april) +
          this.eval(yearArray.may) +
          this.eval(yearArray.june) +
          this.eval(yearArray.july) +
          this.eval(yearArray.august) +
          this.eval(yearArray.september) +
          this.eval(yearArray.october) +
          this.eval(yearArray.november) +
          this.eval(yearArray.december)
      )
    },
    getSumJanuary() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.january)
      })
      return this.eval(result)
    },
    getSumFebruary() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.february)
      })
      return this.eval(result)
    },
    getSumMarch() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.march)
      })
      return this.eval(result)
    },
    getSumApril() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.april)
      })
      return this.eval(result)
    },
    getSumMay() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.may)
      })
      return this.eval(result)
    },
    getSumJune() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.june)
      })
      return this.eval(result)
    },
    getSumJuly() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.july)
      })
      return this.eval(result)
    },
    getSumAugust() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.august)
      })
      return this.eval(result)
    },
    getSumSeptember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.september)
      })
      return this.eval(result)
    },
    getSumOctober() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.october)
      })
      return this.eval(result)
    },
    getSumNovember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.november)
      })
      return this.eval(result)
    },
    getSumDecember() {
      let result = 0
      this.tableViews.forEach(element => {
        result += eval(element.data.december)
      })
      return this.eval(result)
    },
    getTotalSum() {
      return this.eval(
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
