/*
 Name:		StreamBox.ino
 Created:	7/24/2018 10:31:04 PM
 Author:	aholloway
*/

#include <RotaryEncoder.h>

#include <Wire.h>
#include <LCD.h>
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C  lcd(0x3F, 2, 1, 0, 4, 5, 6, 7);

const byte ledPin = 13;
const byte startPin = 2;  //Start toggle
const byte leftPin = 3;  //Player One flip
const byte rightPin = 6; //Player Two flip
const byte leftScoreClk = 7; //Player One Score Up
const byte leftScoreDt = 8; //Player One Score Down
const byte beeperPin = 12; //Beeper

						   //Rotary encoder for left score.
RotaryEncoder encoder(leftScoreClk, leftScoreDt);

//Flag for software.
bool softwareLinked = false;

// Variables will change:
int ledState = HIGH;         // the current state of the output pin
int buttonState;             // the current reading from the input pin
int lastButtonState = LOW;   // the previous reading from the input pin

int leftScore = 0;

int rightState;
int lastRightState;
unsigned long lastRightDebounceTime = 0;

int startState;
int lastStartState;
unsigned long lastStartDebounceTime = 0;

// the following variables are unsigned long's because the time, measured in miliseconds,
// will quickly become a bigger number than can be stored in an int.
unsigned long lastDebounceTime = 0;  // the last time the output pin was toggled
unsigned long debounceDelay = 50;    // the debounce time; increase if the output flickers

void setup() {
	pinMode(ledPin, OUTPUT);
	pinMode(beeperPin, OUTPUT);
	pinMode(leftPin, INPUT_PULLUP);
	pinMode(rightPin, INPUT_PULLUP);
	pinMode(startPin, INPUT_PULLUP);
	pinMode(leftScoreClk, INPUT_PULLUP);
	pinMode(leftScoreDt, INPUT_PULLUP);

	configLCD();
	//attachInterrupt(digitalPinToInterrupt(interruptPin), blink, FALLING);
	//attachInterrupt(digitalPinToInterrupt(interruptPinRight), blinkRight, FALLING);
	Serial.setTimeout(50);
	Serial.begin(9600);
	while (!Serial);
	Serial.println("STARTING");
	bool setupRec = false;
	bool oneTime = false;
	bool twoTime = false;
	bool activePlayer = false;
	bool oneScore = false;
	bool twoScore = false;


	while (!softwareLinked) {
		//Check for incoming serial comms.
		while (Serial.available() > 0 && !setupRec) {
			String incoming = Serial.readStringUntil('/');
			if (incoming == "SETUP") {
				matchModeLCD();
				setupRec = true;
			}
		}

		while (Serial.available() > 0) {
			String incoming = Serial.readStringUntil('/');
			if (incoming.startsWith("1T-")) {
				String timeString = incoming.substring(3);
				lcd.setCursor(2, 1);
				lcd.print(timeString);
				oneTime = true;
			}
			else if (incoming.startsWith("2T-")) {
				String timeString = incoming.substring(3);
				lcd.setCursor(2, 3);
				lcd.print(timeString);
				twoTime = true;
			}
			else if (incoming == "1P") {
				showActive(true);
				activePlayer = true;
			}
			else if (incoming == "2P") {
				showActive(false);
				activePlayer = true;
			}
			else if (incoming.startsWith("1S-")) {
				String cpString = incoming.substring(3);
				lcd.setCursor(15, 1);
				lcd.print(cpString + ' ');
				oneScore = true;
			}
			else if (incoming.startsWith("2S-")) {
				String cpString = incoming.substring(3);
				lcd.setCursor(15, 3);
				lcd.print(cpString + ' ');
				twoScore = true;
			}
		}

		if (oneTime && twoTime && setupRec && activePlayer && oneScore && twoScore) {
			softwareLinked = true;
		}
		delay(100);
	}
}

void loop() {
	encoder.tick();
	sendLeftScore(encoder.getPosition());


	int reading = digitalRead(leftPin);
	if (reading != lastButtonState) {
		lastDebounceTime = millis();
	}

	if ((millis() - lastDebounceTime) > debounceDelay) {
		if (reading != buttonState) {
			buttonState = reading;
			if (buttonState == LOW) {
				ledState = !ledState;
				sendLeft();
			}
		}
	}

	lastButtonState = reading;

	int readingRight = digitalRead(rightPin);
	if (readingRight != lastRightState) {
		lastRightDebounceTime = millis();
	}

	if ((millis() - lastRightDebounceTime) > debounceDelay) {
		if (readingRight != rightState) {
			rightState = readingRight;
			if (rightState == LOW) {
				ledState = !ledState;
				sendRight();
			}
		}
	}

	lastRightState = readingRight;

	int readingStart = digitalRead(startPin);
	if (readingStart != lastStartState) {
		lastStartDebounceTime = millis();
	}

	if ((millis() - lastStartDebounceTime) > debounceDelay) {
		if (readingStart != startState) {
			startState = readingStart;
			if (startState == HIGH) {
				ledState = !ledState;
				sendPause();
			}
			else {
				sendStart();
			}
		}
	}

	lastStartState = readingStart;

	//Check for incoming serial comms.
	while (Serial.available() > 0) {
		String incoming = Serial.readStringUntil('/');
		if (incoming == "BEEP") {
			beep();
		}
		else if (incoming.startsWith("1T-")) {
			String timeString = incoming.substring(3);
			lcd.setCursor(2, 1);
			lcd.print(timeString);
		}
		else if (incoming.startsWith("2T-")) {
			String timeString = incoming.substring(3);
			lcd.setCursor(2, 3);
			lcd.print(timeString);
		}
		else if (incoming == "1P") {
			showActive(true);
			beep();
		}
		else if (incoming == "2P") {
			showActive(false);
			beep();
		}
		else if (incoming.startsWith("1S-")) {
			String cpString = incoming.substring(3);
			lcd.setCursor(15, 1);
			lcd.print(cpString + ' ');
		}
		else if (incoming.startsWith("2S-")) {
			String cpString = incoming.substring(3);
			lcd.setCursor(15, 3);
			lcd.print(cpString + ' ');
		}
		else if (incoming.startsWith("INIT")) {
			setup();
		}
	}

	//Change the LED.
	//digitalWrite(ledPin, ledState);
}

void beep() {
	/*tone(beeperPin, 1000);
	digitalWrite(ledPin, HIGH);
	delay(100);
	noTone(beeperPin);
	digitalWrite(ledPin, LOW);*/
}

void configLCD() {
	// activate LCD module
	lcd.begin(20, 4); // for 16 x 2 LCD module
	lcd.setBacklightPin(3, POSITIVE);
	lcd.setBacklight(HIGH);

	lcd.home(); // set cursor to 0,0
	lcd.print("Warlords of Walsall");
	lcd.setCursor(0, 1);       // go to start of 2nd line
	lcd.print("Stream Box v3");
}

void matchModeLCD() {
	lcd.clear();
	lcd.home();
	lcd.print("> Player One");
	lcd.setCursor(0, 1);
	lcd.print("> 00:00     CP:0");  //CP is at index 15.  Clock at index 2.
	lcd.setCursor(0, 2);
	lcd.print("  Player Two");
	lcd.setCursor(0, 3);
	lcd.print("  00:00     CP:0");
}

void showActive(bool playerOneActive) {
	if (playerOneActive) {
		lcd.setCursor(0, 0);
		lcd.print(">");
		lcd.setCursor(0, 1);
		lcd.print(">");
		lcd.setCursor(0, 2);
		lcd.print(" ");
		lcd.setCursor(0, 3);
		lcd.print(" ");
	}
	else {
		lcd.setCursor(0, 0);
		lcd.print(" ");
		lcd.setCursor(0, 1);
		lcd.print(" ");
		lcd.setCursor(0, 2);
		lcd.print(">");
		lcd.setCursor(0, 3);
		lcd.print(">");
	}
}

void sendAck() {
	Serial.println("ACK");
}

void sendStart() {
	Serial.println("START");
}

void sendPause() {
	Serial.println("PAUSE");
}

void sendTurn() {
	Serial.println("FLIP");
}

void sendRight() {
	Serial.println("LEFT");
}

void sendLeft() {
	Serial.println("RIGHT");
}

void sendLeftScore(int leftScoreNew) {
	if (leftScore != leftScoreNew) {
		leftScore = leftScoreNew;

		Serial.println("LSCORE" + leftScore);
	}
}


