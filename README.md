Windows keylogger and screen capturer with email saves.

References: https://gist.github.com/Scrapax/f538259a5750c0c866351c10643b4124 and http://cursoprogramacioncsharptutoriales.blogspot.com/2017/11/18curso-de-c-con-visual-studio.html.

Note: .NET 5.0 or .NET Core 3.1 can not be used because the library used for keylog (MouseKeyHook) is not compatible with them.

Note: MouseKeyHook library does not work when executing as Windows Service. Therefore the aplication is a console one. Exists the possibility of creating a Windows Service that execute this console application.