<?php
// mini_browser.php — PHP версия

$url = "https://example.com";
$fullscreen = true;

// Парсим аргументы командной строки
for ($i = 1; $i < $argc; $i++) {
    if ($argv[$i] == "--url") {
        $url = $argv[++$i];
    } elseif ($argv[$i] == "--no-fullscreen") {
        $fullscreen = false;
    }
}

echo "🌐 Mini-Browser (PHP)\n";
echo "Открыт URL: $url\n";
echo "Для выхода закройте окно браузера\n";

$command = "";
if (strtoupper(substr(PHP_OS, 0, 3)) === 'WIN') {
    // Windows
    if ($fullscreen) {
        $command = "start /max $url";
    } else {
        $command = "start $url";
    }
} elseif (PHP_OS === 'Darwin') {
    // macOS
    $command = "open $url";
} else {
    // Linux
    if ($fullscreen) {
        $command = "google-chrome --kiosk $url &";
    } else {
        $command = "xdg-open $url &";
    }
}

exec($command);

// Ждём завершения (упрощённо)
sleep(3600);
?>
