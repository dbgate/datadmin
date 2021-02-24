<?
define('VERSION', #VERSION#);
define('CHECK', '#CHECK#');
define('EXTENDEDSAFETY', #EXTENDEDSAFETY#); 

$host = $_POST['HOST'];
$port = $_POST['PORT'];
$user = $_POST['USER'];
$password = $_POST['PASSWORD'];
$engine = $_POST['ENGINE'];
$database = $_POST['DATABASE'];
$encodingstyle = $_POST['ENCODINGSTYLE'];

header('Content-type: application/octet-stream');

function write_error($msg) {
    echo 'WERR';
    echo pack('V', VERSION);
    echo pack('V', strlen($msg));
    echo $msg;
    echo 'WEND';
    exit();
}

function write_ping()
{
    echo 'WPNG';
    echo pack('V', VERSION);    
    echo 'WEND';
    exit();
}

error_reporting(E_ERROR);

if ($_POST['OPERATION'] == 'PING') {
    write_ping();
} else if ($_POST['OPERATION'] == 'QUERY') {
    $op = 'query';
} else if ($_POST['OPERATION'] == 'INSERT') {
    $op = 'insert';
} else if ($_POST['OPERATION'] == 'DBPING') {
    $op = 'dbping';
} else if ($_POST['OPERATION'] == 'SCRIPT') {
    $op = 'script';
} else {
    echo '<h1>PHP Tunnel</h1>';
    echo 'Copy address from your browser to field URL in DatAdmin';
    exit();
}

if (EXTENDEDSAFETY && $_POST['CHECK'] != CHECK) write_error('Invalid check code');


function func_connect_mysql($host, $port, $user, $password, $database) {
    global $encodingstyle;
    
    $link = mysql_connect($host . ':' . $port, $user, $password);
    if ($link && $database) mysql_select_db($database, $link);
    
    if ($link)
    {
        if ($encodingstyle == 'DEFAULT') {
        } else if ($encodingstyle == 'DATABASE') {
            mysql_query('SET NAMES UTF8', $link);
        } else if ($encodingstyle == 'EXPLICIT') {
            mysql_query('SET character_set_results = NULL', $link);
            mysql_query('SET NAMES binary', $link);
        } 
    }
    return $link;
}

function func_query_mysql($link, $query) {
    return mysql_query($query, $link);
}

function func_escape_mysql($link, $str) {
    return mysql_real_escape_string($str, $link);
}

function func_insert_id_mysql($link, $res)
{
    return mysql_insert_id($link);
}

function func_connect_postgre($host, $port, $user, $password, $database) {
    $conn_str = "host=$host user=$user password=$password";
    if ($database) $conn_str .= "dbname=$database";
    return pg_connect($conn_str);
}

function func_escape_postgre($link, $str) {
    return pg_escape_string($str);
}

function func_connect_firebird($host, $port, $user, $password, $database) {
    $link = fbsql_connect($host, $user, $password);
    if ($link && $database) fbsql_select_db($database, $link);
    return $link;
}

function func_query_firebird($link, $query) {
    return fbsql_query($query, $link);
}

function normalize_post($value)
{
    if (get_magic_quotes_gpc()) return stripslashes($value);
    else return $value;
}

function func_affected_mysql($link, $result) {
	return mysql_affected_rows($link);
}

function func_affected_postgre($link, $result) {
	return pg_affected_rows($result);
}

function func_affected_firebird($link, $result) {
	return fbsql_affected_rows($link);
}

function func_isblob_generic($fld) {
    return $fld->blob;
}

function func_isblob_mysql($fld) {
    if (!$fld->blob) return FALSE;
    
    global $link;
    $col = $fld->name;
    $table = $fld->table;
    $res2 = mysql_query("SHOW COLUMNS FROM `$table`", $link);

    $result = TRUE;
    while ($row = mysql_fetch_array($res2)) {
        if ($row[0] == $col) {
            $type = $row[1];
            if (strstr($type, 'text') === FALSE) $result = TRUE;
            else $result = FALSE;
            break;
        }
    }
    mysql_free_result($res2);
    return $result;
}

if ($engine == 'mysql') {
    $func_connect = 'func_connect_mysql';
    $func_query = 'func_query_mysql';
    $func_escape = 'func_escape_mysql';
    $func_num_rows = 'mysql_num_rows';
    $func_num_cols = 'mysql_num_fields';
    $func_fetch_array = 'mysql_fetch_array';
    $func_get_col_name = 'mysql_field_name';
    $func_error = 'mysql_error';
    $func_insert_id = 'func_insert_id_mysql';
    $func_affected = 'func_affected_mysql';
    $func_fetch_field = 'mysql_fetch_field';
    $func_isblob = 'func_isblob_mysql';
    $func_free_result = 'mysql_free_result';
} else if ($engine == 'postgre') {
    $func_connect = 'func_connect_postgre';
    $func_query = 'pg_query';
    $func_escape = 'func_escape_postgre';
    $func_num_rows = 'pg_num_rows';
    $func_num_cols = 'pg_num_fields';
    $func_fetch_array = 'pg_fetch_array';
    $func_get_col_name = 'pg_field_name';
    $func_error = 'pg_last_error';
    $func_affected = 'func_affected_postgre';
    $func_isblob = 'func_isblob_generic';
    //$func_free_result = TODO
} else if ($engine == 'firebird') {
    $func_connect = 'func_connect_firebird';
    $func_query = 'func_query_firebird';
    $func_escape = 'func_escape_postgre';
    $func_num_rows = 'fbsql_num_rows';
    $func_num_cols = 'fbsql_num_fields';
    $func_fetch_array = 'fbsql_fetch_array';
    $func_get_col_name = 'fbsql_field_name';
    $func_error = 'fbsql_error';
    $func_affected = 'func_affected_firebird';
    $func_isblob = 'func_isblob_generic';
    //$func_free_result = TODO
} else write_error("Unknown engine: $engine");

$link = $func_connect($host, $port, $user, $password, $database);

if (!$link) write_error($func_error());

if ($op == 'dbping') write_ping();

$query = strrev(normalize_post($_POST['COMMAND']));
$limit = (int)($_POST['LIMIT']);

if ($op == 'insert') {
    echo 'WIID';
    echo pack('V', VERSION);
    $iid = $func_insert_id($link, $result);
    echo pack('V', $iid);
    echo 'WEND';
}

if ($op == 'query') {
    $result = $func_query($link, $query);
    if (!$result) write_error($func_error($link));

    echo 'WRES';
    echo pack('V', VERSION);
    echo pack('V', 0); // return code
    $affected =  $func_affected($link, $result);
    echo pack('V', $affected); // affected rows
    $numcols = $func_num_cols($result);
    echo pack('V', $numcols);
    
    if ($numcols > 0) {
        for ($col = 0; $col < $numcols; $col++) {
            $name = $func_get_col_name($result, $col);
            echo pack('V', strlen($name));
            echo $name;
            
            $fld = $func_fetch_field($result, $col);
            echo pack('V', strlen($fld->table));
            echo $fld->table;
            
            if ($fld->primary_key) echo '1'; else echo '0';
            if ($func_isblob($fld)) echo '1'; else echo '0'; 
        }
        $numrows = $func_num_rows($result);
        echo pack('V', $numrows);
        if ($limit >= 0 && $numrows > $limit) $numrows = $limit;
        for ($row = 0; $row < $numrows; $row++) {
            echo 'WROW';
            $data = $func_fetch_array($result);
            for ($col = 0; $col < $numcols; $col++) {
                $value = $data[$col];
                if ($value === NULL) {
                    echo pack('V', -1);
                } else {
                    echo pack('V', strlen($value));
                    echo $value;
                }
            }
            flush();
        }
    }
    $func_free_result($result);
    echo 'WEND';
}

if ($op == 'script') {
    $count = $_POST['CMDCOUNT'];
    for($i = 0; $i < $count; $i++) {    
        $query = strrev(normalize_post($_POST["COMMAND$i"]));
        $result = $func_query($link, $query);
        if (!$result) write_error($func_error($link));
        $func_free_result($result);
    }    
    echo 'WRUN';
    echo pack('V', VERSION);    
    echo 'WEND';
}

?>
