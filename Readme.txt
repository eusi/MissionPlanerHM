-----------WINDOWS-------------

1) Install software

* Git
* TortuiseGit
* Visual Studio Express 2013 for Windows Desktop
* DirectX Redist (http://www.microsoft.com/en-us/download/details.aspx?id=35)
* Microsoft .NET 4.0

2) Check out

* Check out via "Git Clone" (https://github.com/eusi/MissionPlanerHM)

3) Build

* Open ArdupilotMega.sln with Visual Studio express 2013 for windows desktop.
* Compile.


**Note:**

MissionPlaner (original) may not be used to make this fork work. To be on the safe side the MP installer was copied to .installer directory. Perhaps some system32 libs might missing.


-----------MONO-------------
run using 
mono MissionPlanner.exe

run debuging
MONO_LOG_LEVEL=debug mono MissionPlanner.exe

you need prereq's
sudo apt-get install mono-runtime libmono-system-windows-forms4.0-cil libmono-system-core4.0-cil libmono-winforms2.0-cil libmono-corlib2.0-cil libmono-system-management4.0-cil libmono-system-xml-linq4.0-cil

