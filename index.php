<?php
    include("config/login.php");
    do_html_header("Login");
                             
    if(userLoggedIn())
    {
        redirectToCorrectDashboard();
    }
    else
    {
        if ( !empty( $_POST ) && !empty($_POST['username']) && !empty($_POST['password']) ) 
        {
            $username = $_POST[ 'username' ];
            $password = $_POST[ 'password' ];

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
?> 
        <form action="index.php" method="post">
            <label for='username'>Username: </label><br />
            <input type="text" name="username"/><br /><br />
            <label for='password'>Password: </label><br />
            <input type="password" name="password"/><br /><br />
            <br/>
            <button type="submit">Submit</button>
        </form>
<?php do_html_footer(); ?>