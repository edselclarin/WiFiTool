# WiFi Tool
Simple C# WPF application to connect to a WiFi access point.

## Usage
* Click Connect.
* Select WiFi profile file.
* Click Open.

## WiFi Profile XML Schema
~~~
<?xml version="1.0"?>
<WLANProfile xmlns="http://www.microsoft.com/networking/WLAN/profile/v1">
    <name>--enter profile name--</name>
    <SSIDConfig>
        <SSID>
            <name>--enter SSID--</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <connectionMode>manual</connectionMode>
    <MSM>
        <security>
            <authEncryption>
                <authentication>WPA2PSK</authentication>
                <encryption>AES</encryption>
                <useOneX>false</useOneX>
            </authEncryption>
            <sharedKey>
                <keyType>passPhrase</keyType>
                <protected>false</protected>
                <keyMaterial>--enter password--</keyMaterial>
            </sharedKey>
        </security>
    </MSM>
</WLANProfile>
~~~
Replace --enter profile name--, --enter SSID--, and --enter password--.
