<template>
  <div 
    id="homeContent" 
    class="fill-height">
    <bobbles/>
    <navbar />
    <div 
      id="content">
      <div 
        id="menu" 
        class="row fill-height">
        <div  
          id="menu_left" 
          class="col-md-3 scrollbar fill-height">
          <span 
            v-for="u in tableViews" 
            :key="u.id">
            <input 
              :id="'contact-'+u.count" 
              type="radio" 
              name="contatcs">
            <div class="item">
              <div 
                :id="u.id" 
                class="portrait"/>
              <div class="details">
                <p class="name">{{ u.name }}</p>
                <p class="description">{{ u.description }}</p>
                <div class="actions">
                  <div class="mail">&#9993;</div>
                  <div class="text">&#59168;</div>
                  <div class="call">&#128222;</div>
                  <div class="info"/>
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
          class="col-md-8 center-block">
          <p class="lead center-block"> Dashboard for normal user. <a href="..\logout.php">Logout</a> </p>
        </div>

      </div>
    </div>
    <div 
      id="byeMessage" 
      class="container dynamischeAenderungen">Bye Administrator!</div>
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
      tableViews: [
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy',
          count: 1,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Mara Mascaro',
          description: 'Video Editor',
          id: 'Mara',
          count: 2,
          profile_url:
            'https://lh6.googleusercontent.com/-iJYfqnaDuxg/Uc-C9u0u70I/AAAAAAAB4wI/TTxYpcfPTW8/s747-no/DSC_5282.JPG'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy3',
          count: 3,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy4',
          count: 4,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy5',
          count: 5,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy6',
          count: 6,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy7',
          count: 7,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy8',
          count: 8,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy',
          description: 'Developer',
          id: 'chris-lacy9',
          count: 9,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        },
        {
          name: 'Chris Lacy Last',
          description: 'Developer',
          id: 'chris-lacy10',
          count: 10,
          profile_url:
            'https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png'
        }
      ]
    }
  }
}
</script>

<style>
#content {
  height: 93.5%;
  height: calc(100% - 59px);
}

#homeContent {
  position: absolute;
  width: 100%;
  left: 0;
}

.fill-height {
  height: 100%;
}

#main_nav {
  border-radius: 0;
  background: rgba(34, 34, 34, 0.86);
  border-color: #080808;
  border-bottom: 2px #080808 solid;
}

/*menu Links*/
#menu_left {
  border-right: 2px #080808 solid;
  position: relative;
  max-width: 100%;
  overflow-x: hidden;
  padding-right: 0px;
}

input {
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

label {
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

  background-color: rgba(0, 0, 0, 0.6); /**/
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
  font-family: Entypo;
  font-size: 2.1em;

  position: absolute;
  width: 25%;

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
.item:hover > .portrait:after {
  border: 1px solid rgba(116, 226, 21, 1);
  box-shadow: 0 0 3px rgba(116, 226, 21, 0.5);
}

input:checked + .item {
  background-color: rgba(0, 0, 0, 0.3); /**/
  padding-top: 0px;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  border-bottom: 1px solid rgba(0, 0, 0, 0.5);
}

input:checked + .item > .details {
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
  background: #204b42;
}

.scrollbar::-webkit-scrollbar-thumb {
  border-radius: 10px;
  background: #52d0a5;
}
.scrollbar::-webkit-scrollbar-thumb:window-inactive {
  background: #3fb68d;
}

#byeMessage {
  text-align: -webkit-center;
  font: normal normal 200 normal 40px / 44px 'Source Sans Pro', sans-serif;
  margin-top: 13%;
  display: none;
}

.dynamischeAenderungen {
  -webkit-transition-duration: 1s;
  transition-duration: 1s;
  -webkit-transition-timing-function: ease-in-put;
  transition-timing-function: ease-in-put;
  /* Damit CSS Aenderungen Smooth ineinander uebergehen. */
}

/* Animationen */
.logoutEnd {
  -webkit-transform: translateY(-300px);
  transform: translateY(-300px);
  color: rgba(255, 255, 255, 0);
}

.fill-height {
  height: 100%;
}
</style>
