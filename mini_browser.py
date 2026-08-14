

### 1. `mini_browser.py` (Python)

```python
# mini_browser.py — Python версия

import sys
import argparse
from PyQt5.QtCore import QUrl
from PyQt5.QtWidgets import QApplication, QMainWindow
from PyQt5.QtWebEngineWidgets import QWebEngineView
from PyQt5.QtGui import QKeyEvent, QKeySequence

class KioskBrowser(QMainWindow):
    def __init__(self, url, fullscreen=True):
        super().__init__()
        self.url = url
        self.fullscreen = fullscreen
        self.setWindowTitle("Mini-Browser (Kiosk)")
        self.initUI()

    def initUI(self):
        self.browser = QWebEngineView()
        self.browser.setUrl(QUrl(self.url))
        self.setCentralWidget(self.browser)

        if self.fullscreen:
            self.showFullScreen()
        else:
            self.resize(1024, 768)

        # Блокировка системных комбинаций
        self.browser.page().fullScreenRequested.connect(lambda req: req.accept())

    def keyPressEvent(self, event: QKeyEvent):
        # Esc для выхода из киоск-режима
        if event.key() == 16777216:  # Qt.Key_Escape
            self.close()
        # Ctrl+Shift+Q для принудительного выхода
        if event.modifiers() == (Qt.KeyboardModifier.ControlModifier | Qt.KeyboardModifier.ShiftModifier) and event.key() == 81:
            self.close()
        super().keyPressEvent(event)

def main():
    parser = argparse.ArgumentParser(description='Mini-Browser (Kiosk Mode)')
    parser.add_argument('--url', default='https://example.com', help='URL для открытия')
    parser.add_argument('--no-fullscreen', action='store_true', help='Отключить полноэкранный режим')
    args = parser.parse_args()

    app = QApplication(sys.argv)
    browser = KioskBrowser(args.url, not args.no_fullscreen)
    print(f"🌐 Mini-Browser (Python)")
    print(f"Открыт URL: {args.url}")
    print("Для выхода нажмите Esc или Ctrl+Shift+Q")
    sys.exit(app.exec_())

if __name__ == "__main__":
    main()
