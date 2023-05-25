# GNMIBasicClient

### This project contains a standalone GNMI Client in C#

**Summary**
This tool is used in a command-line interface and includes GET and SUBSCRIBE (sample, onchange) functionalities. Authentication currently supports INSECURE mode and client certificates (we will support username and password pending additional testing). 

**Prerequisites**

1. Download .NET 6.0 SDK (https://dotnet.microsoft.com/en-us/download)

For non-Windows operating systems, select 'All .NET 6.0 downloads'.


**Instructions**

1. Open a command prompt and navigate to the root directory of the project folder

2. Run the following command 'dotnet build'

3. Navigate to the folder 'GNMIBasicClient\bin\Debug\net6.0\' where you will find an application file named GnmiBasicClient.exe

4. In the current folder, create a file named Paths.txt; write each path you would like to get/subscribe (one path per line)

5. Run GnmiBasicClient.exe using the flags below

PS D:\GNMIBasicClient\bin\Debug\net6.0> .\GnmiBasicClient.exe --help
GnmiBasicClient 1.0.0
Copyright (C) 2023 GnmiBasicClient

  -c, --ClientCertFile            Cert file for client.

  -s, --CaCertFile                CA cert file to be passed on client side.

  -k, --ClientKeyFile             File containing private key for client

  -n, --ClientName                Name to override on client side

  -d, --Destination IP            Required. Destination IP of gRPC server.

  -p, --Destination port          Required. Destination port of gRPC server.

  -m, --IsSecureMode              Required. Whether to use secure mode. Please input: true or false

  -h, --HeartbeatIntervalInSec    Heartbeat interval in seconds. This gets applied to all paths.

  -f, --SamplingFrequencyInSec    Sampling frequency in seconds. This gets applied to all paths.

  -l, --PathFile                  Required. Name of the file containing list of paths.

  -r, --Sku                       Required. Sku of destination. Please input: Arista or Sonic

  -u, --UserName                  Username for authentication.

  -w, --Password                  Password for authentication.

  -b, --SourceDb                  Source DB which can be SysDb/Smash etc.

  -e, --SubscribeMode             Subscription mode. Please input: Sample or OnChange

  -t, --Type                      Required. Data retrieval type. Get/Subscribe

  -v, --OutputRaw                 Print raw data

  -x, --RegularExpressKey         Regular Expression of the Key to catch and output to file

  -a, --OutputFileName            The file name of the catch result by key regular expression

  --help                          Display this help screen.

  --version                       Display version information.


**Examples**
 
 a) Command for GET on Sonic device (secure mode w/ certificates)

 GnmiBasicClient.exe -d 25.10.255.10 -p 50051 -m true -h 60 -l Paths.txt -r sonic -c telemetryclient.cer -s root.cer -k telemetryclient.key -b EXAMPLE_DB -t get

 b) Command for SUBSCRIBE sample on Sonic device (20 second interval, insecure mode)

 GnmiBasicClient.exe -d 25.10.255.10 -p 50051 -m false -h 60 -f 20 -l Paths.txt -r sonic -b EXAMPLE_DB -t subscribe -e sample

 c) Command for SUBSCRIBE onchange on Sonic device (insecure mode)

  GnmiBasicClient.exe -d 25.10.255.10 -p 50051 -m false -h 60 -l Paths.txt -r sonic -b EXAMPLE_DB -t subscribe -e onchange


- To-Do List
1. test IPv6 address
2. username and password authentication