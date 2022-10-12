# xas.otp.console

A Command line to display OTP codes. In a unsecure way.

## Usage

Update the _appsettings.json_ file with the OTP keys, hence the security warning below.

The `Key` (your secret key) and `Name` (a description of the code) properties are required.  

The `Duration` (in seconds) and `Length` (length of the OTP code) properties are optional.  

Run the app.  

Codes will be updated according their refresh time (30s by default).

## Remarks

The app use [Spectre Console]() package, so it is better to use the app with unicode/ligatures fonts.

## Security issues

__As written almost everywhere in this repo, the secret key is stored in clear on a text file, so use the application on private computer, and be careful, nothing is safe.__

__NO RESPONSIBILITY TAKEN__
