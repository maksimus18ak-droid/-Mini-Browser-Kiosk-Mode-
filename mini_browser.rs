// mini_browser.rs — Rust версия

use webview::*;
use std::env;

fn main() {
    let args: Vec<String> = env::args().collect();
    let mut url = "https://example.com".to_string();
    let mut fullscreen = true;

    let mut i = 1;
    while i < args.len() {
        match args[i].as_str() {
            "--url" => {
                url = args[i + 1].clone();
                i += 2;
            }
            "--no-fullscreen" => {
                fullscreen = false;
                i += 1;
            }
            _ => i += 1,
        }
    }

    println!("🌐 Mini-Browser (Rust)");
    println!("Открыт URL: {}", url);
    println!("Для выхода нажмите Esc или Ctrl+Shift+Q");

    let mut webview = webview::builder()
        .title("Mini-Browser (Kiosk)")
        .content(Content::Url(&url))
        .size(800, 600)
        .resizable(true)
        .debug(false)
        .user_data(())
        .invoke_handler(|webview, arg| {
            Ok(())
        });

    if fullscreen {
        // Полноэкранный режим зависит от платформы
        // В webview crate это делается через нативные методы
        // Для упрощения используем window.set_fullscreen
        // webview.window().set_fullscreen(true);
    }

    webview.run().unwrap();
}
