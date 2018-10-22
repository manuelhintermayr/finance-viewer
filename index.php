<?php
    include("config/login.php");
                             
    if(userLoggedIn())
    {
        redirectToCorrectDashboard();
    }
    else
    {
        if ( !empty( $_POST ) && !empty($_POST['username']) && !empty($_POST['password']) ) 
        {
            $username = $_POST[ 'username' ];
            $password = encrypt($_POST['password']);

            if(checkCorrectUser($username, $password))
            {
                setUserCredentials($username, $password);
                redirectToCorrectDashboard();
            }
            else
            {
                echo "Login unsuccessfull.<hr />";
            }
        }      
    }     


    //START INSERT GENERATED CODE HERE
?> 
<link href="css/style_login.css" rel="stylesheet" type="text/css" />
<!-- Background -->
<ul class="bg-bubbles">
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
	<li></li>
</ul>
<!-- End Background -->


	<div id="login-container" class="container">
			<h1 class="welcome-message">Welcome <span id="messageForTheUser"></span></h1>
			<form class="form" action="index.php" method="post">
				<input type="text" name="username" id="user" placeholder="Username">
				<input type="password" name="password" id="pass" placeholder="Password">
				<button type="submit" id="login-button">Login</button>
			</form>
		</div>
<?php 
 //END INSERT GENERATED CODE HERE
 ?>