// mini_browser.go — Go версия

package main

import (
	"flag"
	"fmt"
	"os"
	"os/exec"
	"runtime"
)

func main() {
	url := flag.String("url", "https://example.com", "URL для открытия")
	fullscreen := flag.Bool("fullscreen", true, "Полноэкранный режим")
	flag.Parse()

	fmt.Println("🌐 Mini-Browser (Go)")
	fmt.Printf("Открыт URL: %s\n", *url)
	fmt.Println("Для выхода нажмите Ctrl+C")

	var cmd *exec.Cmd

	switch runtime.GOOS {
	case "windows":
		if *fullscreen {
			cmd = exec.Command("cmd", "/c", "start", "/max", *url)
		} else {
			cmd = exec.Command("cmd", "/c", "start", *url)
		}
	case "darwin":
		cmd = exec.Command("open", *url)
		if *fullscreen {
			// macOS: используем AppleScript для полноэкранного режима
			script := fmt.Sprintf(`tell application "Safari" to activate
tell application "System Events"
	tell process "Safari"
		keystroke "f" using {command down, control down}
	end tell
end tell`)
			cmd = exec.Command("osascript", "-e", script)
		}
	default: // Linux
		if *fullscreen {
			cmd = exec.Command("google-chrome", "--kiosk", *url)
		} else {
			cmd = exec.Command("xdg-open", *url)
		}
	}

	cmd.Stdout = os.Stdout
	cmd.Stderr = os.Stderr
	if err := cmd.Run(); err != nil {
		fmt.Printf("❌ Ошибка: %v\n", err)
		os.Exit(1)
	}
}
