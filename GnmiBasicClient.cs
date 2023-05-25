using Gnmi;
using Grpc.Core;
using Grpc.Net.Client;
using CommandLine;
using Google.Protobuf.WellKnownTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace GnmiBasicClient
{
    public class Options
    {
        [Option('c', "ClientCertFile", Required = false, HelpText = "Cert file for client.")]
        public string? ClientCertFile { get; set; }

        [Option('s', "CaCertFile", Required = false, HelpText = "CA cert file to be passed on client side.")]
        public string? CaCertFile { get; set; }

        [Option('k', "ClientKeyFile", Required = false, HelpText = "File containing private key for client")]
        public string? ClientKeyFile { get; set; }

        [Option('n', "ClientName", Required = false, HelpText = "Name to override on client side")]
        public string? ClientName { get; set; }

        [Option('d', "Destination IP", Required = true, HelpText = "Destination IP of gRPC server.")]
        public string? DestinationIp { get; set; }

        [Option('p', "Destination port", Required = true, HelpText = "Destination port of gRPC server.")]
        public string? DestinationPort { get; set; }

        [Option('m', "IsSecureMode", Required = true, HelpText = "Whether to use secure mode. Please input: true or false")]
        public string? IsSecureMode { get; set; }

        [Option('h', "HeartbeatIntervalInSec", Required = false, HelpText = "Heartbeat interval in seconds. This gets applied to all paths.")]
        public string? HeartbeatIntervalInSec { get; set; }

        [Option('f', "SamplingFrequencyInSec", Required = false, HelpText = "Sampling frequency in seconds. This gets applied to all paths.")]
        public string? SamplingFrequency { get; set; }

        [Option('l', "PathFile", Required = true, HelpText = "Name of the file containing list of paths.")]
        public string? PathFile { get; set; }

        [Option('r', "Sku", Required = true, HelpText = "Sku of destination. Please input: Arista or Sonic")]
        public string? Sku { get; set; }

        [Option('u', "UserName", Required = false, HelpText = "Username for authentication.")]
        public string? UserName { get; set; }

        [Option('w', "Password", Required = false, HelpText = "Password for authentication.")]
        public string? Password { get; set; }

        [Option('b', "SourceDb", Required = false, HelpText = "Source DB which can be SysDb/Smash etc.")]
        public string? SourceDb { get; set; }

        [Option('e', "SubscribeMode", Required = false, HelpText = "Subscription mode. Please input: Sample or OnChange")]
        public string? SubscribeMode { get; set; }

        [Option('t', "Type", Required = true, HelpText = "Data retrieval type. Get/Subscribe")]
        public string? Type { get; set; }

        [Option('v', "OutputRaw", Required = false, HelpText = "Print raw data")]
        public string? OutputRaw { get; set; }

        [Option('x', "RegularExpressKey", Required = false, HelpText = "Regular Expression of the Key to catch and output to file")]
        public string? RegularExpressKeyToFile { get; set; }

        [Option('a', "OutputFileName", Required = false, HelpText = "The file name of the catch result by key regular expression")]
        public string? OutputFileName { get; set; }
    }

    public static class GnmiBasicTool
    {
        private static SkuType skuType;
        private static SubscribeMode subscribeMode;

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(DisplayOptionsAndExecute);
        }

        private static void DisplayOptionsAndExecute(Options options)
        {
            GrpcChannel channel;
            Console.WriteLine("Destination IP: {0}", options.DestinationIp);
            Console.WriteLine("Destination port: {0}", options.DestinationPort);
            Console.WriteLine("Secure mode: {0}", options.IsSecureMode);
            Console.WriteLine("File containing list of paths: {0}", options.PathFile);

            skuType = GetSkuType(options);
            if (options.Type!.ToLower() == "subscribe")
            {
                subscribeMode = GetSubscribeMode(options);
            }
            else
            {
                subscribeMode = SubscribeMode.None;
            }

            bool secure;
            if (!bool.TryParse(options.IsSecureMode, out secure))
            {
                Console.WriteLine("Could not parse {0} into bool", options.IsSecureMode);
            }

            if (!secure)
            {
                if (options.DestinationIp!.Contains(":"))
                {
                    Console.WriteLine("Destination host is V6 address");
                    channel = GrpcChannel.ForAddress($"http://[{options.DestinationIp}]:{options.DestinationPort}", new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
                }
                else
                {
                    Console.WriteLine("Destination host is V4 address");
                    channel = GrpcChannel.ForAddress($"http://{options.DestinationIp}:{options.DestinationPort}", new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
                }
            }
            else
            {
                Console.WriteLine("CA cert file name: {0}", options.CaCertFile);
                Console.WriteLine("client cert file name: {0}", options.ClientCertFile);
                Console.WriteLine("client key file name: {0}", options.ClientKeyFile);
                Console.WriteLine("client name to override: {0}", options.ClientName);


                SocketsHttpHandler socketsHttpHandler = new()
                {
                    SslOptions = new SslClientAuthenticationOptions
                    {
                        EncryptionPolicy = EncryptionPolicy.RequireEncryption,
                        ClientCertificates = new X509CertificateCollection(),
                        RemoteCertificateValidationCallback = (_, cert, chain, _) =>
                        {
                            chain!.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                            chain.ChainPolicy.CustomTrustStore.Add(new X509Certificate2(options.CaCertFile!));
                            return chain.Build(new X509Certificate2(cert!));
                        },
                        TargetHost = options.ClientName
                    },
                };

                // Pass the client certificate so the server can authenticate the client
                var ephemeral = X509Certificate2.CreateFromPemFile(options.ClientCertFile!, options.ClientKeyFile);
                var clientCert = ephemeral.Export(X509ContentType.Pkcs12);
                socketsHttpHandler.SslOptions.ClientCertificates.Add(new X509Certificate2(clientCert));

                if (options.DestinationIp!.Contains(":"))
                {
                    Console.WriteLine("Destination host is V6 address");
                    channel = GrpcChannel.ForAddress($"https://[{options.DestinationIp}]:{options.DestinationPort}", new GrpcChannelOptions { HttpHandler = socketsHttpHandler });
                }
                else
                {
                    Console.WriteLine("Destination host is V4 address");
                    channel = GrpcChannel.ForAddress($"https://{options.DestinationIp}:{options.DestinationPort}", new GrpcChannelOptions { HttpHandler = socketsHttpHandler });
                }
            }
            var client = new gNMI.gNMIClient(channel);
            Console.WriteLine("Successfully created GRPC client");

            switch (options.Type!.ToLower())
            {
                case "get":
                    Get(client, options);
                    break;

                case "subscribe":
                    Subscribe(channel, client, options);
                    break;

                default:
                    Console.WriteLine("Request type {0} not supported", options.Type);
                    Environment.Exit(-1);
                    break;
            }

            static void Get(gNMI.gNMIClient client, Options options)
            {
                GetRequest request = new GetRequest
                {
                    Prefix = GetPrefix(options),
                    Type = GetRequest.Types.DataType.All,
                };

                var pathStrings = File.ReadAllLines(options.PathFile!);

                foreach (var pathString in pathStrings)
                {
                    Gnmi.Path convertedPath = ConvertStringToPath(pathString.Trim());
                    request.Path.Add(convertedPath);
                }

                CallOptions callOptions;

                if (!string.IsNullOrWhiteSpace(options.UserName) && !string.IsNullOrWhiteSpace(options.Password))
                {
                    Metadata metadata = new Metadata
                    {
                        new Metadata.Entry("username", options.UserName),
                        new Metadata.Entry("password", options.Password)
                    };

                    callOptions = new CallOptions(metadata);
                }
                else
                {
                    callOptions = new CallOptions();
                }

                var responseStream = client.GetAsync(request, callOptions);
                var result = responseStream.ResponseAsync.Result;

                Console.WriteLine("Notification count: {0}", result.Notification.Count);

                int count = 0;

                foreach (var notification in result.Notification)
                {
                    count++;
                    Console.WriteLine("\nNotification no: {0}", count);
                    ProcessNotification(notification, subscribeMode, options, skuType);
                }
            }

            static SubscriptionList GetSubscriptions(Options options)
            {
                const ulong milliseconds = 1000;
                const ulong nanoseconds = 1000000;
                SubscriptionList subList = new SubscriptionList
                {
                    Mode = SubscriptionList.Types.Mode.Stream
                };

                if (!uint.TryParse(options.HeartbeatIntervalInSec, out uint heartbeatInSec))
                {
                    Console.WriteLine("Could not parse Heartbeat interval: {0}, using default 60 seconds.", options.HeartbeatIntervalInSec);
                    heartbeatInSec = 60;
                }

                ulong samplingRate = 30 * milliseconds * nanoseconds;
                if (skuType == SkuType.Sonic)
                {
                    if (!uint.TryParse(options.SamplingFrequency, out uint samplingRateInSec))
                    {
                        Console.WriteLine("Could not parse Sampling frequency: {0}, using default 30 seconds.", options.HeartbeatIntervalInSec);
                        samplingRateInSec = 30;
                    }

                    samplingRate = samplingRateInSec * milliseconds * nanoseconds;
                }

                ulong heartbeatInterval = heartbeatInSec * milliseconds * nanoseconds;
       
                var pathStrings = File.ReadAllLines(options.PathFile!);
                if (!string.IsNullOrWhiteSpace(options.SourceDb))
                {
                    subList.Prefix = GetPrefix(options);

                    if (skuType == SkuType.Sonic)
                    {
                        subList.Encoding = Gnmi.Encoding.JsonIetf;
                    }

                    foreach (var pathString in pathStrings)
                    {
                        Gnmi.Path convertedPath = ConvertStringToPath(pathString.Trim());
                        if (subscribeMode == SubscribeMode.Sample)
                        {
                            if (skuType == SkuType.Sonic)
                            {
                                subList.Subscription.Add(new Subscription() { Mode = SubscriptionMode.Sample, Path = convertedPath, HeartbeatInterval = heartbeatInterval, SampleInterval = samplingRate });
                            }
                            else
                            {
                                subList.Subscription.Add(new Subscription() { Path = convertedPath, HeartbeatInterval = heartbeatInterval, SampleInterval = samplingRate });
                            }
                        }
                        else
                        {
                            subList.Subscription.Add(new Subscription() { Mode = SubscriptionMode.OnChange, Path = convertedPath, HeartbeatInterval = heartbeatInterval });
                        }
                    }
                }
                else
                {
                    foreach (var pathString in pathStrings)
                    {
                        Gnmi.Path convertedPath = ConvertStringToPath(pathString.Trim());

                        if (subscribeMode == SubscribeMode.Sample)
                        {
                            if (skuType == SkuType.Sonic)
                            {
                                subList.Subscription.Add(new Subscription() { Mode = SubscriptionMode.Sample, Path = convertedPath, HeartbeatInterval = heartbeatInterval, SampleInterval = samplingRate });
                            }
                            else
                            {
                                subList.Subscription.Add(new Subscription() { Path = convertedPath, HeartbeatInterval = heartbeatInterval, SampleInterval = samplingRate });
                            }
                        }
                        else
                        {
                            var subscription = new Subscription
                            {
                                Mode = Gnmi.SubscriptionMode.OnChange,
                                Path = convertedPath,
                            };

                            subList.Subscription.Add(subscription);
                        }
                    }
                }
                return subList;
            }

            static void Subscribe(GrpcChannel grpcChannel, gNMI.gNMIClient client, Options options)
            {
                var sl = GetSubscriptions(options);
                SubscribeRequest subReq = new SubscribeRequest { Subscribe = sl };

                IAsyncStreamReader<SubscribeResponse> responseStream = null!;
                try
                {
                    AsyncDuplexStreamingCall<SubscribeRequest, SubscribeResponse> call;

                    if (!string.IsNullOrWhiteSpace(options.UserName) && !string.IsNullOrWhiteSpace(options.Password))
                    {
                        Metadata metadata = new Metadata
                        {
                            new Metadata.Entry("username", options.UserName),
                            new Metadata.Entry("password", options.Password)
                        };

                        call = client.Subscribe(metadata);
                    }
                    else
                    {
                        call = client.Subscribe();
                    }

                    call.RequestStream.WriteAsync(subReq).Wait();
                    call.RequestStream.CompleteAsync().Wait();
                    responseStream = call.ResponseStream;
                }
                catch (RpcException exception)
                {
                    Console.WriteLine("{0}", exception);
                }

                int count = 0;

                while (grpcChannel.State == ConnectivityState.Ready) {
                    if (responseStream == null)
                    {
                        Console.WriteLine("Response stream is null");
                        Environment.Exit(-1);
                    }

                    if (!responseStream.MoveNext().Result)
                    {
                        Console.WriteLine("Could not get subscription response.");
                        Environment.Exit(-1);
                    }

                    SubscribeResponse currResponse = responseStream.Current;

                    if (currResponse?.Update != null)
                    {
                        count++;
                        Console.WriteLine("\nNotification no: {0}", count);
                        Console.WriteLine("Response type: {0}", currResponse.ResponseCase.ToString());
                        ProcessNotification(currResponse.Update, subscribeMode, options, skuType);
                    }
                    else
                    {
                        if (currResponse != null)
                        {
                            Console.WriteLine(currResponse.ResponseCase.ToString());
                            continue;
                        }
                    }
                }
            }

            static Gnmi.Path GetPrefix(Options options)
            {
                Gnmi.Path prefix = null!;

                switch (skuType)
                {
                    case SkuType.Arista:
                        PathElem prefixElem = new PathElem { Name = options.SourceDb };
                        prefix = new Gnmi.Path();
                        prefix.Elem.Add(prefixElem);
                        break;

                    case SkuType.Sonic:
                        prefix = new Gnmi.Path { Target = options.SourceDb };
                        break;

                    default:
                        Console.WriteLine("Cannot get prefix for Sku : {0}", options.Sku);
                        Environment.Exit(-1);
                        break;
                }

                return prefix;
            }

            static Gnmi.Path ConvertStringToPath(string pathString)
            {
                Gnmi.Path subscriptionPath = new Gnmi.Path();
                Console.WriteLine("Adding path {0} to request.", pathString.Trim());

                if (!pathString.Contains("/"))
                {
                    PathElem pthElem = new PathElem { Name = pathString.Trim() };
                    subscriptionPath.Elem.Add(pthElem);
                }
                else
                {
                    foreach (var element in pathString.Split('/').ToList())
                    {
                        PathElem pthElem = new PathElem();
                        string fixElement = element.Trim();

                        if (fixElement.Contains("[") && fixElement.Contains("]"))
                        {
                            pthElem.Name = fixElement.Split('[')[0].Trim();
                            string kvString = BetweenStrings(fixElement, "[", "]");
                            var kvs = kvString.Split('=');
                            pthElem.Key.Add(kvs[0].Trim(), kvs[1].Trim());
                        }
                        else
                        {
                            pthElem.Name = fixElement;
                        }

                        subscriptionPath.Elem.Add(pthElem);
                    }
                }

                return subscriptionPath;
            }

            static string BetweenStrings(string parentString, string predecessor, string successor)
            {
                int predecessorIndex = parentString.IndexOf(predecessor);

                if (predecessorIndex == -1)
                {
                    return string.Empty;
                }

                int successorIndex = parentString.LastIndexOf(successor);

                if (successorIndex == -1)
                {
                    return string.Empty;
                }

                int adjustedpredecessorIndex = predecessorIndex + predecessor.Length;

                if (adjustedpredecessorIndex >= successorIndex)
                {
                    return string.Empty;
                }

                return parentString.Substring(adjustedpredecessorIndex, successorIndex - adjustedpredecessorIndex);
            }
        }
        private static void PrintAllUpdatesInNotification(
            Notification notification,
            string timeStamp,
            SubscribeMode subscribeMode = SubscribeMode.None,
            Options options = null!,
            SkuType skuType = SkuType.None)
        {

            Console.WriteLine("Notification at {0}", timeStamp);
            if (subscribeMode == SubscribeMode.Sample)
            {
                if (notification.Update != null && notification.Update.Count != 0)
                {
                    Console.WriteLine("Total updates: {0}", notification.Update.Count);

                    uint updateIndex = 0;
                    foreach (var update in notification.Update)
                    {
                        if (update != null && update.Path != null)
                        {
                            var pathStr = ConvertPath(update.Path);
                            PrintGnmiUpdateValue(update.Val, pathStr, updateIndex);
                            OutputToFileIfConfigured(options, update, pathStr, timeStamp, updateIndex);
                        }

                        updateIndex += 1;
                    }
                }
                else
                {
                    Console.WriteLine("Updates are null/empty");
                }
            }
            else
            {
                uint updateIndex = 0;
                foreach (var update in notification.Update)
                {
                    var pathStr = ConvertPath(update.Path);
                    if (update.Val != null)
                    {
                        Console.WriteLine("{0}:{1} UpdateIndex:{2}", pathStr, update.Val.JsonIetfVal.ToStringUtf8(), updateIndex);
                    }
                    else
                    {
                        PrintGnmiUpdateValue(update.Val!, pathStr, updateIndex);
                    }

                    OutputToFileIfConfigured(options, update, pathStr, timeStamp, updateIndex);

                    updateIndex += 1;
                }
            }
        }
        private static string ConvertPathToString(Gnmi.Path path)
        {
            if (path == null || path.Elem == null || path.Elem.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder pathStringBuilder = new StringBuilder();

            foreach (var pathElem in path.Elem)
            {
                if (!string.IsNullOrWhiteSpace(pathElem.Name))
                {
                    pathStringBuilder.Append($"{pathElem.Name}/");
                }
            }

            return pathStringBuilder.ToString();
        }
        private static string ConvertPath(Gnmi.Path path)
        {
            if (path.Elem != null && path.Elem.Count > 0)
            {
                return ConvertPathToStringInOldFormat(path);
            }
            else
            {
                return ConvertPathToString(path).TrimEnd('/');
            }
        }

        private static void ProcessNotification(
            Notification notification,
            SubscribeMode subscribeMode = SubscribeMode.None,
            Options options = null!,
            SkuType skuType = SkuType.None)
        {
            if (options != null && !string.IsNullOrWhiteSpace(options.OutputRaw))
            {
                if (!bool.TryParse(options.OutputRaw, out bool outputRaw))
                {
                    Console.WriteLine("Could not parse {0} into bool", options.OutputRaw);
                }
                else
                {
                    if (outputRaw)
                    {
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(notification));
                    }
                }
            }

            var timeStamp = ConvertEpochToDateTime((ulong)notification.Timestamp, DateTimeKind.Utc, EpochGranularity.Nanoseconds);
            PrintAllUpdatesInNotification(notification, timeStamp.ToString(), subscribeMode, options!, skuType);
        }

        private const uint miliToNanoSeconds = 1000000;
        private static DateTime ConvertEpochToDateTime(ulong epoch, DateTimeKind timezone, EpochGranularity granularity)
        {
            switch (granularity)
            {
                case EpochGranularity.Nanoseconds:
                    ulong epochInMil = epoch / miliToNanoSeconds;
                    return new DateTime(1970, 1, 1, 0, 0, 0, timezone).AddMilliseconds(epochInMil);

                case EpochGranularity.Milliseconds:
                    return new DateTime(1970, 1, 1, 0, 0, 0, timezone).AddMilliseconds(epoch);

                case EpochGranularity.Seconds:
                    return new DateTime(1970, 1, 1, 0, 0, 0, timezone).AddSeconds(epoch);

                default:
                    throw new NotSupportedException("Please enter valid epoch granularity.");
            }
        }
        private static string ConvertPathToStringInOldFormat(Gnmi.Path path)
        {
            if (path.Elem == null || path.Elem.Count == 0)
            {
                return string.Empty;
            }

            var pathStringBuilder = new StringBuilder();

            foreach (var pathElem in path.Elem)
            {
                pathStringBuilder.Append($"{pathElem}/");
            }

            return pathStringBuilder.ToString().TrimEnd('/');
        }
        private static void OutputToFileIfConfigured(Options options, Update update, string pathStr, string timeStamp, uint updateIndex)
        {
            if (!string.IsNullOrWhiteSpace(options.RegularExpressKeyToFile))
            {
                if (string.IsNullOrWhiteSpace(options.OutputFileName))
                {
                    throw new ArgumentException("The OutputFileName is required if RegularExpressKeyToFile");
                }

                if (Regex.IsMatch(pathStr, options.RegularExpressKeyToFile))
                {
                    string msg;
                    if (update.Val != null)
                    {
                        msg = update.Val.ToString();
                    }
                    else
                    {
                        msg = GetGnmiUpdateValue(update.Val!);
                    }

                    File.AppendAllText(options.OutputFileName, $"{timeStamp} {pathStr}:{msg} UpdateIndex:{updateIndex}{System.Environment.NewLine}");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{timeStamp} {pathStr}:{msg}  UpdateIndex:{updateIndex}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        private static void PrintGnmiUpdateValue(TypedValue update, string path, uint updateIndex)
        {
            string val = GetGnmiUpdateValue(update);
            Console.WriteLine("{0} : [{1}] UpdateIndex:{2}", path, val, updateIndex);
        }
        private static string GetGnmiUpdateValue(TypedValue updateValue)
        {
            switch (updateValue.ValueCase)
            {
                case TypedValue.ValueOneofCase.BoolVal:
                    return string.Format("{0} [{1}]", updateValue.BoolVal, updateValue.ValueCase);
                case TypedValue.ValueOneofCase.IntVal:
                    return string.Format("{0} [{1}]", updateValue.IntVal, updateValue.ValueCase);
                case TypedValue.ValueOneofCase.UintVal:
                    return string.Format("{0} [{1}]", updateValue.UintVal, updateValue.ValueCase);
                case TypedValue.ValueOneofCase.StringVal:
                    return string.Format("{0} [{1}]", updateValue.StringVal, updateValue.ValueCase);
                case TypedValue.ValueOneofCase.AnyVal:
                    return string.Format("{0} [{1}]", updateValue.AnyVal, updateValue.ValueCase);
                case TypedValue.ValueOneofCase.JsonVal:
                    return string.Format("{0} [{1}]", updateValue.JsonVal.ToStringUtf8(), updateValue.ValueCase);
                case TypedValue.ValueOneofCase.JsonIetfVal:
                    return string.Format("{0} [{1}]", updateValue.JsonIetfVal.ToStringUtf8(), updateValue.ValueCase);
                default:
                    return string.Format("Not identified data. [{0}]", updateValue.ValueCase);
            }
        }
        static SubscribeMode GetSubscribeMode(Options options)
        {
            if (string.IsNullOrWhiteSpace(options.SubscribeMode))
            {
                Console.WriteLine("Please include 'SubscribeMode' flag (e)");
                Environment.Exit(-1);
            }
            switch (options.SubscribeMode!.ToLower())
            {
                case "sample":
                    return SubscribeMode.Sample;

                case "onchange":
                    return SubscribeMode.OnChange;

                default:
                    Console.WriteLine("SubscribeMode {0} not supported", options.SubscribeMode);
                    Environment.Exit(-1);
                    break;
            }

            return SubscribeMode.None;
        }
        static SkuType GetSkuType(Options options)
        {
            switch (options.Sku!.ToLower())
            {
                case "arista":
                    return SkuType.Arista;

                case "sonic":
                    return SkuType.Sonic;

                default:
                    Console.WriteLine("Sku {0} not supported", options.Sku);
                    Environment.Exit(-1);
                    break;
            }

            return SkuType.None;
        }
    }

    public enum SkuType
    {
        None,
        Sonic,
        Arista
    }
    public enum EpochGranularity
    {
        Seconds,
        Milliseconds,
        Nanoseconds
    }
    public enum SubscribeMode
    {
        None,
        Sample,
        OnChange
    }
}