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
            :key="u.count">
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
            :key="u.id">

            <div class="my-3 p-3 bg-white rounded shadow-sm text-dark transparentModal">
              <h3 class="border-bottom border-gray pb-2 mb-0">Change content for <b>{{ u.name }}</b>:</h3> 
              <br> 
              <p class="lead">Edit current entries:</p> 
              <table class="table table-striped table-hover">
                <thead>
                  <tr> 
                    <th scope="col">Month</th> 
                    <th scope="col">New Value</th> 
                    <th scope="col">Update</th>
                    <th scope="col">Current Value</th>          
                  </tr>
                </thead> 
                <tbody>

                  <tr> 
                    <td>January</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempJanuary" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateJanuary(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.january) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>February</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempFebruary" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateFebruary(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.february) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>March</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempMarch" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateMarch(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.march) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>April</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempApril" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateApril(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.april) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>May</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempMay" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateMay(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.may) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>June</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempJune" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateJune(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.june) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>July</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempJuly" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateJuly(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.july) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>August</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempAugust" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateAugust(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.august) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>September</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempSeptember" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateSeptember(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.september) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>October</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempOctober" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateOctober(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.october) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>November</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempNovember" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateNovember(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.november) }}</b></td>                    
                  </tr>

                  <tr> 
                    <td>December</td> 
                    <td>                  
                      <input  
                        v-model="u.data.tempDecember" 
                        type="text" 
                        class="form-control" 
                        required="" 
                        maxlength="1000">
                    </td>
                    <td> 
                      <button 
                        class="btn btn-secondary" 
                        @click="updateDecember(u)">Update</button> 
                    </td>         
                    <td>&pound; <b>{{ eval(u.data.december) }}</b></td>                    
                  </tr>

                </tbody>
              </table>
            </div>

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
                      maxlength="180"
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
      newView_name: 'FVMarket2',
      newView_description: 'Supermarket',
      newView_id: 'fvmarket2',
      newView_notes: 'This is my local market',
      newView_profile_url:
        'https://cdn.stocksnap.io/img-thumbs/960w/AWJD4WV6W1.jpg',
      tableViews: []
    }
  },
  mounted() {
    this.loadAll()
  },
  methods: {
    loadAll() {
      this.$axios
        .get('dashboard/options.php?action=getViews')
        .then(response => {
          let api = response.data
          api.forEach(element => {
            this.tableViews.push({
              name: element.name,
              description: element.description,
              id: element.id,
              count: element.count,
              notes: element.notes,
              viewEnabled: false,
              profile_url: element.profile_url,
              data: {
                january: element.data.january,
                tempJanuary: element.data.january,
                february: element.data.february,
                tempFebruary: element.data.february,
                march: element.data.march,
                tempMarch: element.data.march,
                april: element.data.april,
                tempApril: element.data.april,
                may: element.data.may,
                tempMay: element.data.may,
                june: element.data.june,
                tempJune: element.data.june,
                july: element.data.july,
                tempJuly: element.data.july,
                august: element.data.august,
                tempAugust: element.data.august,
                september: element.data.september,
                tempSeptember: element.data.september,
                october: element.data.october,
                tempOctober: element.data.october,
                november: element.data.november,
                tempNovember: element.data.november,
                december: element.data.december,
                tempDecember: element.data.december
              }
            })
          })
          console.log(api)
        })
        .catch(error => {
          console.log(error)
          alert(
            'Could not load views. An error calling the middleware occured. Look in the console for further information.'
          )
        })
    },
    disableAll() {
      this.tableViews.forEach(element => {
        element.viewEnabled = false
      })
      this.overviewIsEnabled = false
      this.createNewViewEnabled = false
    },
    addView() {
      let newView = {
        name: this.newView_name,
        description: this.newView_description,
        id: this.newView_id,
        count: this.newView_id,
        notes: this.newView_notes,
        viewEnabled: false,
        profile_url: this.newView_profile_url,
        data: {
          january: '0',
          tempJanuary: '0',
          february: '0',
          tempFebruary: '0',
          march: '0',
          tempMarch: '0',
          april: '0',
          tempApril: '0',
          may: '0',
          tempMay: '0',
          june: '0',
          tempJune: '0',
          july: '0',
          tempJuly: '0',
          august: '0',
          tempAugust: '0',
          september: '0',
          tempSeptember: '0',
          october: '0',
          tempOctober: '0',
          november: '0',
          tempNovember: '0',
          december: '0',
          tempDecember: '0'
        }
      }
      if (
        newView.name == '' ||
        newView.name == ' ' ||
        newView.description == '' ||
        newView.description == ' ' ||
        newView.id == '' ||
        newView.id == ' ' ||
        newView.notes == '' ||
        newView.notes == ' ' ||
        newView.profile_url == '' ||
        newView.profile_url == ' '
      ) {
        alert('Please fill out all fields')
      } else if (this.doesViewIdAlreadyExists(newView.id)) {
        alert('A view with the same id does already exist.')
      } else if (this.doesViewNameAlreadyExists(newView.name)) {
        alert('A view with the same name does already exist.')
      } else if (newView.id.includes(' ')) {
        alert('ID is not allowed to have wite spaces.')
      } else if (newView.profile_url.includes(' ')) {
        alert('Profile Url is not allowed to have wite spaces.')
      } else {
        this.$axios
          .post('dashboard/options.php?action=addView', newView)
          .then(response => {
            let api = response.data
            if (api.name != null) {
              this.tableViews.push(api)
              this.newView_name = this.newView_description = this.newView_id = this.newView_notes = this.newView_profile_url =
                ''
            } else {
              console.log(reponse)
              alert('Could not add a new view. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not add a new view. Check console for more info.')
          })
      }
    },
    doesViewIdAlreadyExists(newViewID) {
      let exists = false
      this.tableViews.forEach(element => {
        if (newViewID === element.id) {
          exists = true
        }
      })
      return exists
    },
    doesViewNameAlreadyExists(newViewName) {
      let exists = false
      this.tableViews.forEach(element => {
        if (newViewName === element.name) {
          exists = true
        }
      })
      return exists
    },
    removeView(view) {
      if (confirm('Are you sure you want to delte view ' + view.name + '?')) {
        this.$axios
          .post('dashboard/options.php?action=removeView', {
            view_id: view.count
          })
          .then(response => {
            let api = response.data
            if (api.message == 'View deleted.') {
              this.tableViews.splice(this.tableViews.indexOf(view), 1)
            } else {
              console.log(reponse)
              alert('Could not delete view. Check console for more info.')
            }
          })
          .catch(error => {
            alert('Could not delete view. Check console for more info.')
          })
      }
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
    },
    updateJanuary(view) {
      try {
        let result = this.eval(view.data.tempJanuary)
        if (view.data.tempJanuary.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.january = '' + view.data.tempJanuary
        } else {
          alert('New January value is invalid: ' + result)
        }
      } catch (err) {
        alert('New January value is invalid: ' + err)
        console.log(err)
      }
    },
    updateFebruary(view) {
      try {
        let result = this.eval(view.data.tempFebruary)
        if (view.data.tempFebruary.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.february = '' + view.data.tempFebruary
        } else {
          alert('New February value is invalid: ' + result)
        }
      } catch (err) {
        alert('New February value is invalid: ' + err)
        console.log(err)
      }
    },
    updateMarch(view) {
      try {
        let result = this.eval(view.data.tempMarch)
        if (view.data.tempMarch.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.march = '' + view.data.tempMarch
        } else {
          alert('New March value is invalid: ' + result)
        }
      } catch (err) {
        alert('New March value is invalid: ' + err)
        console.log(err)
      }
    },
    updateApril(view) {
      try {
        let result = this.eval(view.data.tempApril)
        if (view.data.tempApril.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.april = '' + view.data.tempApril
        } else {
          alert('New April value is invalid: ' + result)
        }
      } catch (err) {
        alert('New April value is invalid: ' + err)
        console.log(err)
      }
    },
    updateMay(view) {
      try {
        let result = this.eval(view.data.tempMay)
        if (view.data.tempMay.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.may = '' + view.data.tempMay
        } else {
          alert('New May value is invalid: ' + result)
        }
      } catch (err) {
        alert('New May value is invalid: ' + err)
        console.log(err)
      }
    },
    updateJune(view) {
      try {
        let result = this.eval(view.data.tempJune)
        if (view.data.tempJune.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.june = '' + view.data.tempJune
        } else {
          alert('New June value is invalid: ' + result)
        }
      } catch (err) {
        alert('New June value is invalid: ' + err)
        console.log(err)
      }
    },
    updateJuly(view) {
      try {
        let result = this.eval(view.data.tempJuly)
        if (view.data.tempJuly.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.july = '' + view.data.tempJuly
        } else {
          alert('New July value is invalid: ' + result)
        }
      } catch (err) {
        alert('New July value is invalid: ' + err)
        console.log(err)
      }
    },
    updateAugust(view) {
      try {
        let result = this.eval(view.data.tempAugust)
        if (view.data.tempAugust.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.august = '' + view.data.tempAugust
        } else {
          alert('New August value is invalid: ' + result)
        }
      } catch (err) {
        alert('New August value is invalid: ' + err)
        console.log(err)
      }
    },
    updateSeptember(view) {
      try {
        let result = this.eval(view.data.tempSeptember)
        if (view.data.tempSeptember.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.september = '' + view.data.tempSeptember
        } else {
          alert('New September value is invalid: ' + result)
        }
      } catch (err) {
        alert('New September value is invalid: ' + err)
        console.log(err)
      }
    },
    updateOctober(view) {
      try {
        let result = this.eval(view.data.tempOctober)
        if (view.data.tempOctober.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.october = '' + view.data.tempOctober
        } else {
          alert('New October value is invalid: ' + result)
        }
      } catch (err) {
        alert('New October value is invalid: ' + err)
        console.log(err)
      }
    },
    updateNovember(view) {
      try {
        let result = this.eval(view.data.tempNovember)
        if (view.data.tempNovember.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.november = '' + view.data.tempNovember
        } else {
          alert('New November value is invalid: ' + result)
        }
      } catch (err) {
        alert('New November value is invalid: ' + err)
        console.log(err)
      }
    },
    updateDecember(view) {
      try {
        let result = this.eval(view.data.tempDecember)
        if (view.data.tempDecember.includes(',')) {
          alert('Please do not use , use . instead')
        } else if (isFinite(result)) {
          view.data.december = '' + view.data.tempDecember
        } else {
          alert('New December value is invalid: ' + result)
        }
      } catch (err) {
        alert('New December value is invalid: ' + err)
        console.log(err)
      }
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

#menu_right > span > div > table > thead > tr > th:nth-child(4),
#menu_right > span > div > table > tbody > tr > td:nth-child(4) {
  text-align: right;
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
  height: 14px;
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
