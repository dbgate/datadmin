<?

if (!$_POST['OPERATION']) {
    echo '<h1>DatAdmin HTTP Tunnel</h1>';
    echo 'Copy address from your browser to field URL in DatAdmin';
    exit();
}

$engine = $_POST['engine'];
$version = $_POST['VERSION'];

if ($engine == 'mysql' && function_exists('mysqli_connect') && $version >= 11) $engine = 'mysqli';

$file = 'datunnel_'. $engine . '_' . $version . '.php';

if (file_exists($file)) {
    require $file;
} else {
    $msg = 'Unsupported version or engine';
    echo 'WERR';
    echo pack('V', $version);
    echo pack('V', strlen($msg));
    echo $msg;
    echo 'WEND';
    exit();
}

?>