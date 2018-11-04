<?php
    //TODO: reverse ! on both first ifs
    error_reporting(E_ERROR | E_PARSE);

    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/login.php");
    $_POST = json_decode(file_get_contents("php://input"),true);
    $username = "root";
    $year = "2018";

    if(userLoggedIn())
    {
        header('HTTP/1.1 403 Forbidden');
        echo "Not logged in.";
    }
    else{
        if(false)
        {
            //todo: check if user and year is set
            //header('HTTP/1.1 403 Forbidden');
            //echo "False logged in.";
        }
        else{
            if(isset($_GET) && isset($_GET['action']))
            {
                $action = $_GET['action'];
                switch($action)
                {
                    case 'getViews':
                        getViews();
                        break;
                    case 'addView':
                        addView();
                        break;
                    default:
                        actionNotSupported($action);
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo 'Bad request.'; 
            }
        }
    }

    function getViews()
    {
        global $mysqli;
        global $year;
        global $username;

        $sql = "SELECT * FROM `fv_views` WHERE `v_y_year` = '$year' AND `v_u_name` = '$username';";
        $result = $mysqli->query($sql);
        
        $resultArray = array();
        if ($result){
            while($row = $result->fetch_assoc()) 
            {
                $resultArray[] = getViewArrayByRowResult($row);
            }
            header('Content-Type: application/json');
            echo json_encode($resultArray, JSON_PRETTY_PRINT);
        }
        else{
            echo '{}';
        }
    }

    function getViewArrayByRowResult($row)
    {
        $yearData = array(
            'january' => $row['v_month_01'],
            'february' => $row['v_month_02'],
            'march' => $row['v_month_03'],
            'april' => $row['v_month_04'],
            'may' => $row['v_month_05'],
            'june' => $row['v_month_06'],
            'july' => $row['v_month_07'],
            'august' => $row['v_month_08'],
            'september' => $row['v_month_09'],
            'october' => $row['v_month_10'],
            'november' => $row['v_month_11'],
            'december' => $row['v_month_12'],
            'tempJanuary' => $row['v_month_01'],
            'tempFebruary' => $row['v_month_02'],
            'tempMarch' => $row['v_month_03'],
            'tempApril' => $row['v_month_04'],
            'tempMay' => $row['v_month_05'],
            'tempJune' => $row['v_month_06'],
            'tempJuly' => $row['v_month_07'],
            'tempAugust' => $row['v_month_08'],
            'tempSeptember' => $row['v_month_09'],
            'tempOctober' => $row['v_month_10'],
            'tempNovember' => $row['v_month_11'],
            'tempDecember' => $row['v_month_12']
        );

        $view = array(
            'count' => $row['v_id'],
            'name' => $row['v_name'],
            'description' => $row['v_description'],
            'id' => $row['v_html_id'],
            'notes' => $row['v_notes'],
            'profile_url' => $row['v_profile_url'],
            'data' => $yearData
        );

        return $view;
    }

    function addView()
    {
        global $mysqli;
        global $year;
        global $username;

        if(isset($_POST['name'])
        &&isset($_POST['description'])
        &&isset($_POST['id'])
        &&isset($_POST['notes'])
        &&isset($_POST['profile_url'])
        &&$_POST['name']!=''
        &&$_POST['name']!=' '
        &&$_POST['description']!=''
        &&$_POST['description']!=' '
        &&$_POST['id']!=''
        &&$_POST['id']!=' '
        &&$_POST['notes']!=''
        &&$_POST['notes']!=' '
        &&$_POST['profile_url']!=''
        &&$_POST['profile_url']!=' ')
        {
            $name = mysql_real_escape_string($_POST['name']);
            $description = mysql_real_escape_string($_POST['description']);
            $id = mysql_real_escape_string($_POST['id']);
            $notes = mysql_real_escape_string($_POST['notes']);
            $profile_url = mysql_real_escape_string($_POST['profile_url']);
            
            if(!strpos($id, ' ')&&!strpos($profile_url, ' '))
            {
                $sqlForID = "SELECT * FROM `fv_views` WHERE `v_y_year` = '$year' AND `v_u_name` = '$username' AND `v_html_id` = '$id';";
                $sqlForName = "SELECT * FROM `fv_views` WHERE `v_y_year` = '$year' AND `v_u_name` = '$username' AND `v_name` = '$name';";

                if(($mysqli->query($sqlForID)->num_rows)>=1 || ($mysqli->query($sqlForID)->num_rows)>=1)
                {
                    header('HTTP/1.1 400 Bad request');
                    echo "ID or Name does already exist";
                }
                else{
                    $sqlCreateView = "INSERT INTO `fv_views` (`v_id`, `v_y_year`, `v_u_name`, `v_name`, `v_description`, `v_html_id`, `v_notes`, `v_profile_url`, `v_month_01`, `v_month_02`, `v_month_03`, `v_month_04`, `v_month_05`, `v_month_06`, `v_month_07`, `v_month_08`, `v_month_09`, `v_month_10`, `v_month_11`, `v_month_12`) VALUES (NULL, '$year', '$username', '$name', '$description', '$id', '$notes', '$profile_url', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');";
                    $resultCreateView = $mysqli->query($sqlCreateView);
                    if($resultCreateView)
                    {
                        $sql = "SELECT * FROM `fv_views` WHERE `v_y_year` = '$year' AND `v_u_name` = '$username' AND `v_html_id` = '$id';";
                        $result = $mysqli->query($sql);
                        if ($result){
                            header('Content-Type: application/json');

                            while($row = $result->fetch_assoc()) 
                            {
                                echo json_encode(getViewArrayByRowResult($row), JSON_PRETTY_PRINT);
                            }
                        }
                        else{
                            header('HTTP/1.1 400 Bad request');
                            echo "View was created but cannot be accessed. SQL Execution failed."; 
                        }
                    }
                    else{
                        header('HTTP/1.1 400 Bad request');
                        echo "Could not create a new view. SQL Execution failed."; 
                    }   
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Profile_Url or id should not contain whitespaces."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
    }

    function actionNotSupported($action)
    {
       header('HTTP/1.1 400 Bad request');
       echo "Action $action is not supported."; 
    }

?>

