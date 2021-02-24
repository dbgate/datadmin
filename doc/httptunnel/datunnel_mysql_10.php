<?
define('VERSION', 10);

// connection parameters
$host = $_POST['host'];
$port = $_POST['port'];
$user = $_POST['login'];
$password = $_POST['password'];
$engine = $_POST['engine'];

// tunnel parameters
$operation = $_POST['OPERATION'];
$encodingstyle = $_POST['ENCODINGSTYLE'];
$version = $_POST['VERSION'];
$database = $_POST['DATABASE'];
$sessinit = $_POST['SESSIONINIT'];

if (!$operation) {
    echo '<h1>DatAdmin HTTP Tunnel</h1>';
    echo 'Copy address from your browser to field URL in DatAdmin';
    exit();
}

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

function write_db_ping()
{
    global $link;
    echo 'WDPG';
    echo pack('V', VERSION);
    write_string(mysql_get_server_info($link)); 
    echo 'WEND';
    exit();
}

function write_string($value) {
   if ($value === NULL) {
        echo pack('V', -1);
    } else {
        echo pack('V', strlen($value));
        echo $value;
    }
}

function mysql_field_is_blob($fld, $link) {
    if (!$fld->blob) return FALSE;
    
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

function normalize_post($value)
{
    if (get_magic_quotes_gpc()) return stripslashes($value);
    else return $value;
} 

error_reporting(E_ERROR);

if ($version != VERSION) {
    write_error("Incompatible version, Tunnel version = '" . VERSION . "', DatAdmin version='$version'"); 
}

if ($operation == 'PING') {
    write_ping();
} 

if ($engine != 'mysql') {
    write_error("Incompatible engine, 'mysql' expected, '$engine' found"); 
}

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

if (!$link) write_error(mysql_error());

if ($operation == 'DBPING') write_db_ping();

$query = strrev(normalize_post($_POST['COMMAND']));
$limit = (int)($_POST['LIMIT']);

if ($sessinit) {
    $cmd = strrev(normalize_post($sessinit));
    foreach(explode('||', $cmd) as $c) {
        $r = mysql_query($c, $link);
        mysql_free_result($r);
    }
}

if ($operation == 'INSERT') {
    echo 'WIID';
    echo pack('V', VERSION);
    $iid = mysql_insert_id($link, $result);
    echo pack('V', $iid);
    echo 'WEND';
    exit();
}


if ($operation == 'QUERY') {
    $result = mysql_query($query, $link);
    if (!$result) write_error(mysql_error($link));

    echo 'WRES';
    echo pack('V', VERSION);
    echo pack('V', 0); // return code
    $affected = mysql_affected_rows($link);
    echo pack('V', $affected); // affected rows
    $numcols = mysql_num_fields($result);
    echo pack('V', $numcols);
    
    if ($numcols > 0) {
        for ($col = 0; $col < $numcols; $col++) {
            $name = mysql_field_name($result, $col);
            write_string($name);
            
            $fld = mysql_fetch_field($result, $col);
            write_string($fld->table);
            
            if ($fld->primary_key) echo '1'; else echo '0';
            if (mysql_field_is_blob($fld, $link)) echo '1'; else echo '0'; 
        }
        $numrows = mysql_num_rows($result);
        echo pack('V', $numrows);
        if ($limit >= 0 && $numrows > $limit) $numrows = $limit;
        for ($row = 0; $row < $numrows; $row++) {
            echo 'WROW';
            $data = mysql_fetch_array($result);
            for ($col = 0; $col < $numcols; $col++) {
                $value = $data[$col];
                write_string($value);
            }
            flush();
        }
    }
    mysql_free_result($result);
    echo 'WEND';
    exit();
}

if ($operation == 'SCRIPT') {
    $count = $_POST['CMDCOUNT'];
    for($i = 0; $i < $count; $i++) {    
        $query = strrev(normalize_post($_POST["COMMAND$i"]));
        $result = mysql_query($query, $link);
        if (!$result) write_error(mysql_error($link));
        mysql_free_result($result);
    }    
    echo 'WRUN';
    echo pack('V', VERSION);    
    echo 'WEND';
    exit();
}

write_error("Undefined operation: $operation");

?> 