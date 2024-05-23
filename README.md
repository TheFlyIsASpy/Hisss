# Hisss
HISSS (Health Industry Shitty Scanner Software) is a basic command line app for controlling Ricoh/Fujitsu Fi series scanners using their SDK.

## Features:
Seemless activation of scanner through commandline with no gui or user interaction (Useful for StarLIMS and other management services)

Control over most capabilities of the scanner driver including: qrcode and barcode detection/decoding, full silent scanning support for essentially anything you can do in paperstream capture, full error code translations into english and step by step logging in a text file, multiscanning, multipage scanning, auto formatting, and more!

## Usage:
hisss.exe -h for all options and help

## Overrides File
You can optionally include a json file in your user directory to set scanner settings overriding any settings given by command line.

This file isn't necessary, but is useful if you need to change scanner settings for a specific machine but cannot change the command line arguments.

1. Create a file named hisss_overrides.json in your user directory {%userprofile%\hisss_overrides.json}

2. Create a single json object inside and add desired settings, the program will automatically read this file and override all settings given as command line arguments:
   
![override_example](https://github.com/TheFlyIsASpy/Hisss/assets/56007360/13a0b6d9-c33f-4624-aae1-02d839660032)

## Tech Used:
Ricoh fi series SDK

.NET
