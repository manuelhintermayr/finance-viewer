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
                    'december' => $row['v_month_12']
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
                $resultArray[] = $view;
            }
            header('Content-Type: application/json');
            echo json_encode($resultArray, JSON_PRETTY_PRINT);
        }
        else{
            echo '{}';
        }
    }

    function actionNotSupported($action)
    {
       header('HTTP/1.1 400 Bad request');
       echo "Action $action is not supported."; 
    }

?>

