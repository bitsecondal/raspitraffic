#!/usr/bin/python

################################################################################
# Project: 	Traffic Control
# Script Usage: run_all_off.py
# Created: 	2017-04-09
# Author: 	Kenny Robinson, Bit Second Tech (www.bitsecondtech.com)
# Description:	Turns off all of the lights.
################################################################################

from time import sleep
import RPi.GPIO as GPIO
import lcddriver
import raspitraffic as rtc

display=lcddriver.lcd()

try:
	# CODE TO RUN GOES HERE

	rtc.setup()
	rtc.alloff()

except KeyboardInterrupt:
	rtc.log_message("Exiting")
	GPIO.cleanup()
	display.lcd_clear()
