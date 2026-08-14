# mini_browser.rb — Ruby версия

require 'webrick'
require 'launchy'
require 'optparse'

options = {}
OptionParser.new do |opts|
  opts.banner = "Usage: ruby mini_browser.rb [options]"
  opts.on("--url URL", "URL для открытия") { |v| options[:url] = v }
  opts.on("--no-fullscreen", "Отключить полноэкранный режим") { options[:fullscreen] = false }
end.parse!

url = options[:url] || "https://example.com"
fullscreen = options[:fullscreen].nil? ? true : options[:fullscreen]

puts "🌐 Mini-Browser (Ruby)"
puts "Открыт URL: #{url}"
puts "Для выхода закройте окно браузера"

# Открываем в системном браузере с флагами
case RUBY_PLATFORM
when /linux/
  if fullscreen
    system("google-chrome --kiosk #{url} &")
  else
    system("xdg-open #{url} &")
  end
when /darwin/
  system("open #{url}")
when /mswin|mingw|windows/
  if fullscreen
    system("start /max #{url}")
  else
    system("start #{url}")
  end
end

# Ждём завершения (упрощённо)
sleep
