[1mdiff --git a/Form1.cs b/Form1.cs[m
[1mindex 501c077..f6d571c 100644[m
[1m--- a/Form1.cs[m
[1m+++ b/Form1.cs[m
[36m@@ -46,7 +46,7 @@[m [mnamespace NetworkWinApp[m
 [m
         private void SaveData(string output)[m
         {[m
[31m-            if (output.Length > 260)[m
[32m+[m[32m            if (output.Length > 300)[m
             {[m
                 address.Clear();[m
                 int start = output.LastIndexOf("----------", StringComparison.Ordinal);[m
[36m@@ -82,22 +82,36 @@[m [mnamespace NetworkWinApp[m
             var _oldData = JsonConvert.DeserializeObject<List<NetAddress>>(oldData);[m
             var realData = netAddresses;[m
             List<NetAddress> newlist = new List<NetAddress>();[m
[31m-            int count = 0;[m
[32m+[m[41m            [m
                 foreach (var old in _oldData)[m
                 {[m
                     foreach (var real in realData)[m
                     {[m
[31m-                        if (old.PcAddress == real.PcAddress && old.PcPort == real.PcPort)[m
[32m+[m[32m                        if (old.PcAddress == real.PcAddress && old.PcPort == real.PcPort && old.ServerAddress==real.ServerAddress && old.ServerPort==real.ServerPort)[m
                         {[m
[31m-                            //var address = new NetAddress(old.ServerName,old.AccessType,old.PcAddress,old.PcPort,old.FirewallPermission,old.ServerAddress,old.ServerPort);[m
[31m-[m
                             newlist.Add(old);[m
[31m-[m
                         }[m
                     }[m
[31m- [m
                 }[m
 [m
[32m+[m[32m           // if(newlist.Contains(realData))[m
[32m+[m[32m            List<NetAddress> missing = new List<NetAddress>();[m
[32m+[m[32m           foreach (var item in realData)[m
[32m+[m[32m           {[m
[32m+[m[32m               int count = 0;[m
[32m+[m[32m               foreach (var innerItem in newlist)[m
[32m+[m[32m               {[m
[32m+[m[32m                   if (item.PcAddress != innerItem.PcAddress || item.PcPort != innerItem.PcPort || item.ServerAddress != innerItem.ServerAddress || item.ServerPort != innerItem.ServerPort)[m
[32m+[m[32m                   {[m
[32m+[m[32m                       count++;[m
[32m+[m[32m                       if (count == newlist.Count)[m
[32m+[m[32m                       {[m
[32m+[m[32m                           missing.Add(item);[m
[32m+[m[32m                       }[m
[32m+[m[32m                   }[m
[32m+[m[32m               }[m
[32m+[m[32m           }[m
[32m+[m
         }[m
 [m
 [m
[36m@@ -435,7 +449,7 @@[m [mnamespace NetworkWinApp[m
         {[m
             string host = Dns.GetHostName();[m
             IPHostEntry ip = Dns.GetHostEntry(host);[m
[31m-            return ip.AddressList[4].ToString();[m
[32m+[m[32m            return ip.AddressList[2].ToString();[m
         }[m
 [m
 [m
[1mdiff --git a/bin/Debug/NetworkWinApp.exe b/bin/Debug/NetworkWinApp.exe[m
[1mindex 6c7a77b..c159bf2 100644[m
Binary files a/bin/Debug/NetworkWinApp.exe and b/bin/Debug/NetworkWinApp.exe differ
[1mdiff --git a/bin/Debug/NetworkWinApp.pdb b/bin/Debug/NetworkWinApp.pdb[m
[1mindex c5ebff3..3200588 100644[m
Binary files a/bin/Debug/NetworkWinApp.pdb and b/bin/Debug/NetworkWinApp.pdb differ
[1mdiff --git a/bin/Debug/NetworkWinApp.vshost.exe b/bin/Debug/NetworkWinApp.vshost.exe[m
[1mindex fc5a27b..f443805 100644[m
Binary files a/bin/Debug/NetworkWinApp.vshost.exe and b/bin/Debug/NetworkWinApp.vshost.exe differ
[1mdiff --git a/bin/Debug/PortForward.txt b/bin/Debug/PortForward.txt[m
[1mindex c0dbd79..be53fe1 100644[m
[1m--- a/bin/Debug/PortForward.txt[m
[1m+++ b/bin/Debug/PortForward.txt[m
[36m@@ -1 +1 @@[m
[31m-[{"ServerName":"1","AccessType":"1","PcAddress":"1","PcPort":"1","FirewallPermission":"2","ServerAddress":null,"ServerPort":null},{"ServerName":"4","AccessType":"4","PcAddress":"4","PcPort":"4","FirewallPermission":"4","ServerAddress":null,"ServerPort":null},{"ServerName":"2","AccessType":"2","PcAddress":"2","PcPort":"2","FirewallPermission":"2","ServerAddress":"2","ServerPort":"2"},{"ServerName":"3","AccessType":"3","PcAddress":"3","PcPort":"3","FirewallPermission":"0","ServerAddress":"3","ServerPort":"3"}][m
[32m+[m[32m[{"ServerName":"1","AccessType":"1","PcAddress":"1","PcPort":"1","FirewallPermission":"2","ServerAddress":null,"ServerPort":null},{"ServerName":"2","AccessType":"2","PcAddress":"2","PcPort":"2","FirewallPermission":"2","ServerAddress":"2","ServerPort":"2"},{"ServerName":"3","AccessType":"3","PcAddress":"3","PcPort":"3","FirewallPermission":"3","ServerAddress":"3","ServerPort":"3"},{"ServerName":"4","AccessType":"4","PcAddress":"4","PcPort":"4","FirewallPermission":"4","ServerAddress":"4","ServerPort":"4"},{"ServerName":"5","AccessType":"5","PcAddress":"5","PcPort":"5","FirewallPermission":"5","ServerAddress":"5","ServerPort":"5"},{"ServerName":"6","AccessType":"6","PcAddress":"6","PcPort":"6","FirewallPermission":"6","ServerAddress":"6","ServerPort":"6"},{"ServerName":"7","AccessType":"7","PcAddress":"7","PcPort":"7","FirewallPermission":"7","ServerAddress":"7","ServerPort":"7"},{"ServerName":"8","AccessType":"8","PcAddress":"8","PcPort":"8","FirewallPermission":"8","ServerAddress":"8","ServerPort":"8"}][m
[1mdiff --git a/obj/Debug/DesignTimeResolveAssemblyReferences.cache b/obj/Debug/DesignTimeResolveAssemblyReferences.cache[m
[1mindex 51217bc..6bcd3c9 100644[m
Binary files a/obj/Debug/DesignTimeResolveAssemblyReferences.cache and b/obj/Debug/DesignTimeResolveAssemblyReferences.cache differ
[1mdiff --git a/obj/Debug/DesignTimeResolveAssemblyReferencesInput.cache b/obj/Debug/DesignTimeResolveAssemblyReferencesInput.cache[m
[1mindex 6d4f28e..ac650af 100644[m
Binary files a/obj/Debug/DesignTimeResolveAssemblyReferencesInput.cache and b/obj/Debug/DesignTimeResolveAssemblyReferencesInput.cache differ
[1mdiff --git a/obj/Debug/NetworkWinApp.csproj.FileListAbsolute.txt b/obj/Debug/NetworkWinApp.csproj.FileListAbsolute.txt[m
[1mindex ac496d6..272c382 100644[m
[1m--- a/obj/Debug/NetworkWinApp.csproj.FileListAbsolute.txt[m
[1m+++ b/obj/Debug/NetworkWinApp.csproj.FileListAbsolute.txt[m
[36m@@ -8,3 +8,12 @@[m [mD:\suman\test\Slave\NetworkWinApp\NetworkWinApp\obj\Debug\NetworkWinApp.pdb[m
 D:\suman\test\Slave\NetworkWinApp\NetworkWinApp\obj\Debug\NetworkWinApp.csprojResolveAssemblyReference.cache[m
 D:\suman\test\Slave\NetworkWinApp\NetworkWinApp\bin\Debug\Newtonsoft.Json.dll[m
 D:\suman\test\Slave\NetworkWinApp\NetworkWinApp\bin\Debug\Newtonsoft.Json.xml[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.exe[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.pdb[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\bin\Debug\NetworkWinApp.exe[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\bin\Debug\NetworkWinApp.pdb[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\bin\Debug\Newtonsoft.Json.xml[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.csprojResolveAssemblyReference.cache[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.Form1.resources[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.Properties.Resources.resources[m
[32m+[m[32mC:\Users\aoe\Documents\Visual Studio 2013\Projects\NetworkWinApp\obj\Debug\NetworkWinApp.csproj.GenerateResource.Cache[m
[1mdiff --git a/obj/Debug/NetworkWinApp.csproj.GenerateResource.Cache b/obj/Debug/NetworkWinApp.csproj.GenerateResource.Cache[m
[1mindex a9eb61e..d2a343e 100644[m
Binary files a/obj/Debug/NetworkWinApp.csproj.GenerateResource.Cache and b/obj/Debug/NetworkWinApp.csproj.GenerateResource.Cache differ
[1mdiff --git a/obj/Debug/NetworkWinApp.csprojResolveAssemblyReference.cache b/obj/Debug/NetworkWinApp.csprojResolveAssemblyReference.cache[m
[1mindex fde7ce2..251aaa6 100644[m
Binary files a/obj/Debug/NetworkWinApp.csprojResolveAssemblyReference.cache and b/obj/Debug/NetworkWinApp.csprojResolveAssemblyReference.cache differ
[1mdiff --git a/obj/Debug/NetworkWinApp.exe b/obj/Debug/NetworkWinApp.exe[m
[1mindex 6c7a77b..c159bf2 100644[m
Binary files a/obj/Debug/NetworkWinApp.exe and b/obj/Debug/NetworkWinApp.exe differ
[1mdiff --git a/obj/Debug/NetworkWinApp.pdb b/obj/Debug/NetworkWinApp.pdb[m
[1mindex c5ebff3..3200588 100644[m
Binary files a/obj/Debug/NetworkWinApp.pdb and b/obj/Debug/NetworkWinApp.pdb differ
