// mini_browser.java — Java версия

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.web.WebView;
import javafx.stage.Stage;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.KeyCombination;

public class mini_browser extends Application {
    private static String targetUrl = "https://example.com";
    private static boolean fullscreen = true;

    @Override
    public void start(Stage primaryStage) {
        WebView webView = new WebView();
        webView.getEngine().load(targetUrl);

        Scene scene = new Scene(webView, 800, 600);
        primaryStage.setScene(scene);
        primaryStage.setTitle("Mini-Browser (Kiosk)");

        if (fullscreen) {
            primaryStage.setFullScreen(true);
            primaryStage.setFullScreenExitHint("");
            primaryStage.setFullScreenExitKeyCombination(KeyCombination.NO_MATCH);
        }

        // Обработка клавиш для выхода
        scene.setOnKeyPressed((KeyEvent event) -> {
            if (event.getCode() == KeyCode.ESCAPE) {
                primaryStage.close();
            }
            if (event.getCode() == KeyCode.Q && event.isShortcutDown() && event.isShiftDown()) {
                primaryStage.close();
            }
        });

        primaryStage.show();

        System.out.println("🌐 Mini-Browser (Java)");
        System.out.println("Открыт URL: " + targetUrl);
        System.out.println("Для выхода нажмите Esc или Ctrl+Shift+Q");
    }

    public static void main(String[] args) {
        for (int i = 0; i < args.length; i++) {
            if (args[i].equals("--url")) {
                targetUrl = args[++i];
            } else if (args[i].equals("--no-fullscreen")) {
                fullscreen = false;
            }
        }
        launch(args);
    }
}
