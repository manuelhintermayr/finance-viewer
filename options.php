<?php
    include("config/login.php");
    $_POST = json_decode(file_get_contents("php://input"), true);
    $username = "";

    if (!userLoggedIn()) {
        header('HTTP/1.1 403 Forbidden');
        echo "Not logged in.";
    } else {
        if ($inDev) {
            $username = $adminName;
        } else {
            $username = $_SESSION['m_user'];
        }

        if (isset($_GET) && isset($_GET['action'])) {
            $action = $_GET['action'];
            switch ($action) {
                case 'getInfo':
                    getInfo();
                    break;
                case 'setViewYear':
                    setViewYear();
                    break;
                default:
                    actionNotSupported($action);
            }
        } else {
            header('HTTP/1.1 400 Bad request');
            echo 'Bad request.';
        }
    }

    function getInfo()
    {
        global $username;

        $usedUsername = $username;
        if (loggedInUserIsAdmin()&&isset($_SESSION['m_view_username'])) {
            $usedUsername = $_SESSION['m_view_username'];
        }

        $result = array(
            'name' => getFirstNameForUsername($username),
            'isAdmin' => loggedInUserIsAdmin(),
            'years' => getYearsForUser($usedUsername)
        );
        echo json_encode($result);
    }

    function setViewYear()
    {
        global $username;
        $usedUsername = $username;
        if (loggedInUserIsAdmin()&&isset($_SESSION['m_view_username'])) {
            $usedUsername = $_SESSION['m_view_username'];
        }

        if (isset($_POST['year'])) {
            $years = getYearsForUser($usedUsername);
            if (in_array($_POST['year'], $years)) {
                $_SESSION['m_view_year'] = $_POST['year'];
                echo json_encode(array('message' => "Year for view set."));
            } else {
                header('HTTP/1.1 400 Bad request');
                echo "Invalid option.";
            }
        } else {
            header('HTTP/1.1 400 Bad request');
            echo "Year is not set.";
        }
    }

    function actionNotSupported($action)
    {
        header('HTTP/1.1 400 Bad request');
        echo "Action $action is not supported.";
    }

?>