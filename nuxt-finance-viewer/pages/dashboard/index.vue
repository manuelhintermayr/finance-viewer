<template>
  <div>
    <bobbles/>
    <!-- Based on http://www.manuelweb.at/old/login1/home.html -->
    <link 
      rel="stylesheet" 
      href="http://www.manuelweb.at/old/login1/css/bootstrap.css">
    <link 
      rel="stylesheet" 
      href="http://www.manuelweb.at/old/login1/css/bootstrap-theme.css">
    <div 
      id="homeContent" 
      class="fill-height">
    

      <nav 
        id="main_nav" 
        class="navbar-inverse">
        <div>
          <div class="navbar-header">
            <!-- wird fuer mobile Anzeigen benutzt-->
            <button 
              type="button" 
              class="navbar-toggle collapsed" 
              data-toggle="collapse" 
              data-target="#navbar" 
              aria-expanded="false" 
              aria-controls="navbar">
              <span class="icon-bar"/>
              <span class="icon-bar"/>
              <span class="icon-bar"/>
            </button>
            <a 
              class="navbar-brand" 
              href="#">Somap</a>
          </div>
          <div 
            id="navbar" 
            class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
              <li class="active">
                <a href="#">
                  <span 
                    class="glyphicon glyphicon-globe" 
                    aria-hidden="true"/>
                  Friend List
                </a>
              </li>
              <li class="disabled">
                <a href="#">
                  <span 
                    class="glyphicon glyphicon-user" 
                    aria-hidden="true"/>
                  My Account
                </a>
              </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
              <li class="disabled">
                <a href="#">
                  <span 
                    class="glyphicon glyphicon-cog" 
                    aria-hidden="true"/>
                  Settings
                </a>
              </li>
              <li>
                <a 
                  id="logout_button" 
                  href ="#logout">
                  <span 
                    class="glyphicon glyphicon-log-out" 
                    aria-hidden="true"/>
                  Logout
                </a>
              </li>
            </ul>
          </div>

        </div>
      </nav>


      <div 
        id="menu" 
        class="row">
        <div 
          id="menu_left" 
          class="col-md-3 scrollbar fill-height">
			
          <!-- Backstage Controlls -->
          <input 
            id="contact-1" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="chris-lacy" 
              class="portrait"/>
            <div class="details">
              <p class="name">Chris Lacy</p>
              <p class="description">Developer</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-1"/>
          </div>

          <input 
            id="contact-10" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Mara" 
              class="portrait"/>
            <div class="details">
              <p class="name">Mara Mascaro</p>
              <p class="description">Video Editor</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-10"/>
          </div>

          <input 
            id="contact-2" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="marq" 
              class="portrait"/>
            <div class="details">
              <p class="name">Marques Brownlee</p>
              <p class="description">Producer</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-2"/>
          </div>

          <input 
            id="contact-3" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Neila" 
              class="portrait"/>
            <div class="details">
              <p class="name">Neila Rey</p>
              <p class="description">Fitness Community</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-3"/>
          </div>

          <input 
            id="contact-4" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Tim" 
              class="portrait"/>
            <div class="details">
              <p class="name">Tim O'Reilly</p>
              <p class="description">CEO, O'Reilly Media</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-4"/>
          </div>

          <input 
            id="contact-9" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Ana" 
              class="portrait"/>
            <div class="details">
              <p class="name">Ana Svanadze</p>
              <p class="description">Patriot</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-9"/>
          </div>

          <input 
            id="contact-5" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Ross" 
              class="portrait"/>
            <div class="details">
              <p class="name">Derek Ross</p>
              <p class="description">Systems Administrator</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-5"/>
          </div>

          <input 
            id="contact-6" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Richard" 
              class="portrait"/>
            <div class="details">
              <p class="name">Richard Branson</p>
              <p class="description">Founder of Virgin Group</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-6"/>
          </div>

          <input 
            id="contact-7" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="George" 
              class="portrait"/>
            <div class="details">
              <p class="name">George Takei</p>
              <p class="description">Come on</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-7"/>
          </div>

          <input 
            id="contact-8" 
            type="radio" 
            name="contatcs">
          <div class="item">
            <div 
              id="Beaufort" 
              class="portrait"/>
            <div class="details">
              <p class="name">François Beaufort</p>
              <p class="description">Happiness Evangelist</p>
              <div class="actions">
                <div class="mail">&#9993;</div>
                <div class="text">&#59168;</div>
                <div class="call">&#128222;</div>
                <div class="info"/>
              </div>
            </div>
            <label for="contact-8"/>
          </div>


		
			
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

export default {
  components: {
    Bobbles
  }
}
</script>

<style>
@charset "UTF-8";

/*!
 * style.css for Somap
 * (c) 2016 Manuel
 */

body {
  margin: 0;
  color: white;
  font-family: Oxygen;
}

#homeContent {
  position: absolute;
  width: 100%;
  left: 0;
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
  visibility: hidden; /**/
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

/* Local Details */

#chris-lacy {
  background-image: url(https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png);
}
#marq {
  background-image: url(https://lh6.googleusercontent.com/-HM-tr3UEejY/UTkJfpIKB0I/AAAAAAAB4Qw/Auc20I2DW50/s207-p-no/PO8A0788.jpg);
}
#Neila {
  background-image: url(https://lh4.googleusercontent.com/-gi7ff5iHmfc/AAAAAAAAAAI/AAAAAAAAAAA/SAt2FLwWnqQ/s170-c-k-no/photo.jpg);
}
#Tim {
  background-image: url(https://lh3.googleusercontent.com/-aldjCkivfjM/UiLJGp9uWkI/AAAAAAACZI0/SzF8u1Sn9Yg/s374-no/2010inField.jpg);
}
#Ross {
  background-image: url(https://lh5.googleusercontent.com/-ZldUgVhYK2I/Ui5w1xJB-GI/AAAAAAABhyw/CYqAw6qNpdU/s747-no/IMG_4199.JPG);
}
#Richard {
  background-image: url(https://lh4.googleusercontent.com/-3hWBW1tTkIs/TrAVPskLAXI/AAAAAAABYxw/tkRaEDCAhPA/w576-h575-no/DSC_0993.jpg);
}
#George {
  background-image: url(https://lh5.googleusercontent.com/-1euFoF6_C1c/T9oVScCtEMI/AAAAAAAAoEw/70Mk-gAXts4/w525-h528-no/GT+color3.jpg);
}
#Beaufort {
  background-image: url(https://lh6.googleusercontent.com/-GnkJpy0jk60/Tqar4gGdvwI/AAAAAAAA-0w/8qaP8zAm31E/s747-no/P1030524.JPG);
}
#Ana {
  background-image: url(https://lh4.googleusercontent.com/-1zxtaYRNh84/AAAAAAAAAAI/AAAAAAAAAAA/7kbNSX3kyZQ/s170-c-k-no/photo.jpg);
}
#Mara {
  background-image: url(https://lh6.googleusercontent.com/-iJYfqnaDuxg/Uc-C9u0u70I/AAAAAAAB4wI/TTxYpcfPTW8/s747-no/DSC_5282.JPG);
}

/* Local Interactions */
#contact-1:checked ~ #menu_right {
  background-image: url(https://lh3.googleusercontent.com/-_RbzbA4U-AY/T3le0hEjh5I/AAAAAAAAPbU/H6aRDFguJMY/s207-p-no/profile_pic.png);
}
#contact-2:checked ~ #menu_right {
  background-image: url(https://lh6.googleusercontent.com/-HM-tr3UEejY/UTkJfpIKB0I/AAAAAAAB4Qw/Auc20I2DW50/s207-p-no/PO8A0788.jpg);
}
#contact-3:checked ~ #menu_right {
  background-image: url(https://lh4.googleusercontent.com/-gi7ff5iHmfc/AAAAAAAAAAI/AAAAAAAAAAA/SAt2FLwWnqQ/s170-c-k-no/photo.jpg);
}
#contact-4:checked ~ #menu_right {
  background-image: url(https://lh3.googleusercontent.com/-aldjCkivfjM/UiLJGp9uWkI/AAAAAAACZI0/SzF8u1Sn9Yg/s374-no/2010inField.jpg);
}
#contact-5:checked ~ #menu_right {
  background-image: url(https://lh5.googleusercontent.com/-ZldUgVhYK2I/Ui5w1xJB-GI/AAAAAAABhyw/CYqAw6qNpdU/s747-no/IMG_4199.JPG);
}
#contact-6:checked ~ #menu_right {
  background-image: url(https://lh4.googleusercontent.com/-3hWBW1tTkIs/TrAVPskLAXI/AAAAAAABYxw/tkRaEDCAhPA/w576-h575-no/DSC_0993.jpg);
}
#contact-7:checked ~ #menu_right {
  background-image: url(https://lh5.googleusercontent.com/-1euFoF6_C1c/T9oVScCtEMI/AAAAAAAAoEw/70Mk-gAXts4/w525-h528-no/GT+color3.jpg);
}
#contact-8:checked ~ #menu_right {
  background-image: url(https://lh6.googleusercontent.com/-GnkJpy0jk60/Tqar4gGdvwI/AAAAAAAA-0w/8qaP8zAm31E/s747-no/P1030524.JPG);
}
#contact-9:checked ~ #menu_right {
  background-image: url(https://lh4.googleusercontent.com/-1zxtaYRNh84/AAAAAAAAAAI/AAAAAAAAAAA/7kbNSX3kyZQ/s170-c-k-no/photo.jpg);
}
#contact-10:checked ~ #menu_right {
  background-image: url(https://lh6.googleusercontent.com/-iJYfqnaDuxg/Uc-C9u0u70I/AAAAAAAB4wI/TTxYpcfPTW8/s747-no/DSC_5282.JPG);
}

/*bloede Bugfixes*/
.container {
  position: inherit;
  /*Beim laden mit den Ajax-Requests kam es vor, 
        dass dieser Wert vom style_login.css ueberschrieben
        wurde und dadurch der Style komplett anders war. Das wurde hiermit
        gefixt.*/
}
#menu {
  height: calc(100% - 50px);
  /*Das muss deswegen geschrieben werden, weil sonst dieser div zu gross waere.
        Die Navigation kommt vor diesem Div und nimmt 50px des Seite schon ein,
        deshalb ist es nicht mehr height=100%, sondern 100%-50px
        */
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
