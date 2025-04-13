#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
#include <DHT.h>
#include <ArduinoJson.h>

// --- CONFIGURABIL ---
const char* ssid = "NumeReteaWiFi";
const char* password = "ParolaWiFi";
const char* host = "192.168.0.100"; // IP-ul serverului
const int httpsPort = 443; // Portul HTTPS
const char* endpoint = "/api/deviceData"; // ruta API
const unsigned long INTERVAL = 30000; // în milisecunde (30s)

#define DHTPIN D4     // pinul la care e conectat senzorul (ex. GPIO2)
#define DHTTYPE DHT22 // AM2302 este DHT22


DHT dht(DHTPIN, DHTTYPE);
WiFiClientSecure client;

unsigned long lastSendTime = 0;

void setup() {
  Serial.begin(115200);
  delay(100);

  dht.begin();
  WiFi.begin(ssid, password);
  Serial.print("Conectare la WiFi");

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("\nConectat la rețea!");
  client.setInsecure(); // DOAR pentru test, ignoră validarea certificatului SSL
}

void loop() {
  unsigned long now = millis();
  if (now - lastSendTime >= INTERVAL) {
    float temperature = dht.readTemperature();
    float humidity = dht.readHumidity();

    if (isnan(temperature) || isnan(humidity)) {
      Serial.println("Eroare la citirea senzorului DHT!");
      return;
    }

    Serial.printf("Temp: %.2f °C, Umid: %.2f %%\n", temperature, humidity);

    if (!client.connect(host, httpsPort)) {
      Serial.println("Conexiune eșuată la server HTTPS");
      return;
    }

    StaticJsonDocument<200> doc;
    doc["senderId"] = "esp8266-demo-001"; // sau UUID/ID hardcodat
    doc["timeStamp"] = millis(); // pentru demo, altfel ar fi timpul real
    doc["temp"] = (int)temperature;

    String jsonPayload;
    serializeJson(doc, jsonPayload);

    client.print(String("POST ") + endpoint + " HTTP/1.1\r\n" +
                 "Host: " + host + "\r\n" +
                 "Content-Type: application/json\r\n" +
                 "Content-Length: " + jsonPayload.length() + "\r\n\r\n" +
                 jsonPayload);

    while (client.connected()) {
      String line = client.readStringUntil('\n');
      if (line == "\r") break;
    }

    String response = client.readStringUntil('\n');
    Serial.println("Răspuns de la server: " + response);

    client.stop();
    lastSendTime = now;
  }
}
