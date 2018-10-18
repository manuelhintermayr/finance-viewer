<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Finance Viewer - Login</title>
    </head>
    <body>
       <?php
           include("config/checkUsername.php");
           if ( !empty( $_POST ) ) {
               if(checkCorrectUser($_POST[ 'username' ], $_POST[ 'password' ]))
               {
                   echo "Login successfull.<hr />";
               }
               else
               {
                   echo "Login unsuccessfull.<hr />";
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
    </body>
</html>