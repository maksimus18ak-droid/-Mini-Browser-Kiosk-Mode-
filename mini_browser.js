// mini_browser.js — JavaScript версия (Electron)

const { app, BrowserWindow, globalShortcut } = require('electron');
const path = require('path');
const url = require('url');

let mainWindow;

function createWindow() {
    // Парсим аргументы командной строки
    const args = process.argv.slice(2);
    let targetUrl = 'https://example.com';
    let fullscreen = true;

    for (let i = 0; i < args.length; i++) {
        if (args[i] === '--url') targetUrl = args[++i];
        else if (args[i] === '--no-fullscreen') fullscreen = false;
    }

    mainWindow = new BrowserWindow({
        width: 800,
        height: 600,
        fullscreen: fullscreen,
        frame: false,
        autoHideMenuBar: true,
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true,
            preload: path.join(__dirname, 'preload.js')
        }
    });

    mainWindow.loadURL(targetUrl);

    // Блокировка выхода
    mainWindow.on('closed', () => {
        mainWindow = null;
    });

    // Регистрация горячих клавиш для выхода
    globalShortcut.register('Escape', () => {
        app.quit();
    });
    globalShortcut.register('CommandOrControl+Shift+Q', () => {
        app.quit();
    });

    console.log(`🌐 Mini-Browser (JavaScript / Electron)`);
    console.log(`Открыт URL: ${targetUrl}`);
    console.log('Для выхода нажмите Esc или Ctrl+Shift+Q');
}

app.whenReady().then(() => {
    createWindow();
});

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
        createWindow();
    }
});

// preload.js (содержимое файла)
// window.addEventListener('DOMContentLoaded', () => {
//     console.log('Kiosk mode active');
// });
