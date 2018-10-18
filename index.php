<?php
    include("config/generator.php");
    include("config/login.php");
    do_html_header("Login");
                       
    if(userLoggedIn())
    {
        echo "<script>window.location='dashboard.php';</script>";
        //currently both going to normal user page
    }
    else{
              
           if ( !empty( $_POST ) ) {
               if(checkCorrectUser($_POST[ 'username' ], $_POST[ 'password' ]))
               {
                   $_SESSION['m_user']=$_POST['username'];
                   $_SESSION['m_password']=$_POST['password'];
                   echo "Login successfull.";

                   if(userIsAdmin($_POST[ 'username' ]))
                   {
                       echo "<a href='admin.php'>Admin</a>";
                   }
                   else{
                       echo "<a href='normalUser.php'>NormalUser</a>";
                   }
                   echo "<hr />";
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