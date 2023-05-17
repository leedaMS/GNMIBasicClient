// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: gnmi/gnmi.proto
// </auto-generated>
// Original file comments:
//
// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Gnmi {
  public static partial class gNMI
  {
    static readonly string __ServiceName = "gnmi.gNMI";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.CapabilityRequest> __Marshaller_gnmi_CapabilityRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.CapabilityRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.CapabilityResponse> __Marshaller_gnmi_CapabilityResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.CapabilityResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.GetRequest> __Marshaller_gnmi_GetRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.GetRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.GetResponse> __Marshaller_gnmi_GetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.GetResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.SetRequest> __Marshaller_gnmi_SetRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.SetRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.SetResponse> __Marshaller_gnmi_SetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.SetResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.SubscribeRequest> __Marshaller_gnmi_SubscribeRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.SubscribeRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Gnmi.SubscribeResponse> __Marshaller_gnmi_SubscribeResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Gnmi.SubscribeResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Gnmi.CapabilityRequest, global::Gnmi.CapabilityResponse> __Method_Capabilities = new grpc::Method<global::Gnmi.CapabilityRequest, global::Gnmi.CapabilityResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Capabilities",
        __Marshaller_gnmi_CapabilityRequest,
        __Marshaller_gnmi_CapabilityResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Gnmi.GetRequest, global::Gnmi.GetResponse> __Method_Get = new grpc::Method<global::Gnmi.GetRequest, global::Gnmi.GetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_gnmi_GetRequest,
        __Marshaller_gnmi_GetResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Gnmi.SetRequest, global::Gnmi.SetResponse> __Method_Set = new grpc::Method<global::Gnmi.SetRequest, global::Gnmi.SetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Set",
        __Marshaller_gnmi_SetRequest,
        __Marshaller_gnmi_SetResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Gnmi.SubscribeRequest, global::Gnmi.SubscribeResponse> __Method_Subscribe = new grpc::Method<global::Gnmi.SubscribeRequest, global::Gnmi.SubscribeResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "Subscribe",
        __Marshaller_gnmi_SubscribeRequest,
        __Marshaller_gnmi_SubscribeResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Gnmi.GnmiReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of gNMI</summary>
    [grpc::BindServiceMethod(typeof(gNMI), "BindService")]
    public abstract partial class gNMIBase
    {
      /// <summary>
      /// Capabilities allows the client to retrieve the set of capabilities that
      /// is supported by the target. This allows the target to validate the
      /// service version that is implemented and retrieve the set of models that
      /// the target supports. The models can then be specified in subsequent RPCs
      /// to restrict the set of data that is utilized.
      /// Reference: gNMI Specification Section 3.2
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Gnmi.CapabilityResponse> Capabilities(global::Gnmi.CapabilityRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Retrieve a snapshot of data from the target. A Get RPC requests that the
      /// target snapshots a subset of the data tree as specified by the paths
      /// included in the message and serializes this to be returned to the
      /// client using the specified encoding.
      /// Reference: gNMI Specification Section 3.3
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Gnmi.GetResponse> Get(global::Gnmi.GetRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Set allows the client to modify the state of data on the target. The
      /// paths to modified along with the new values that the client wishes
      /// to set the value to.
      /// Reference: gNMI Specification Section 3.4
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Gnmi.SetResponse> Set(global::Gnmi.SetRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Subscribe allows a client to request the target to send it values
      /// of particular paths within the data tree. These values may be streamed
      /// at a particular cadence (STREAM), sent one off on a long-lived channel
      /// (POLL), or sent as a one-off retrieval (ONCE).
      /// Reference: gNMI Specification Section 3.5
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task Subscribe(grpc::IAsyncStreamReader<global::Gnmi.SubscribeRequest> requestStream, grpc::IServerStreamWriter<global::Gnmi.SubscribeResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for gNMI</summary>
    public partial class gNMIClient : grpc::ClientBase<gNMIClient>
    {
      /// <summary>Creates a new client for gNMI</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gNMIClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for gNMI that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gNMIClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gNMIClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gNMIClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Capabilities allows the client to retrieve the set of capabilities that
      /// is supported by the target. This allows the target to validate the
      /// service version that is implemented and retrieve the set of models that
      /// the target supports. The models can then be specified in subsequent RPCs
      /// to restrict the set of data that is utilized.
      /// Reference: gNMI Specification Section 3.2
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.CapabilityResponse Capabilities(global::Gnmi.CapabilityRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Capabilities(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Capabilities allows the client to retrieve the set of capabilities that
      /// is supported by the target. This allows the target to validate the
      /// service version that is implemented and retrieve the set of models that
      /// the target supports. The models can then be specified in subsequent RPCs
      /// to restrict the set of data that is utilized.
      /// Reference: gNMI Specification Section 3.2
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.CapabilityResponse Capabilities(global::Gnmi.CapabilityRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Capabilities, null, options, request);
      }
      /// <summary>
      /// Capabilities allows the client to retrieve the set of capabilities that
      /// is supported by the target. This allows the target to validate the
      /// service version that is implemented and retrieve the set of models that
      /// the target supports. The models can then be specified in subsequent RPCs
      /// to restrict the set of data that is utilized.
      /// Reference: gNMI Specification Section 3.2
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.CapabilityResponse> CapabilitiesAsync(global::Gnmi.CapabilityRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CapabilitiesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Capabilities allows the client to retrieve the set of capabilities that
      /// is supported by the target. This allows the target to validate the
      /// service version that is implemented and retrieve the set of models that
      /// the target supports. The models can then be specified in subsequent RPCs
      /// to restrict the set of data that is utilized.
      /// Reference: gNMI Specification Section 3.2
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.CapabilityResponse> CapabilitiesAsync(global::Gnmi.CapabilityRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Capabilities, null, options, request);
      }
      /// <summary>
      /// Retrieve a snapshot of data from the target. A Get RPC requests that the
      /// target snapshots a subset of the data tree as specified by the paths
      /// included in the message and serializes this to be returned to the
      /// client using the specified encoding.
      /// Reference: gNMI Specification Section 3.3
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.GetResponse Get(global::Gnmi.GetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Retrieve a snapshot of data from the target. A Get RPC requests that the
      /// target snapshots a subset of the data tree as specified by the paths
      /// included in the message and serializes this to be returned to the
      /// client using the specified encoding.
      /// Reference: gNMI Specification Section 3.3
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.GetResponse Get(global::Gnmi.GetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>
      /// Retrieve a snapshot of data from the target. A Get RPC requests that the
      /// target snapshots a subset of the data tree as specified by the paths
      /// included in the message and serializes this to be returned to the
      /// client using the specified encoding.
      /// Reference: gNMI Specification Section 3.3
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.GetResponse> GetAsync(global::Gnmi.GetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Retrieve a snapshot of data from the target. A Get RPC requests that the
      /// target snapshots a subset of the data tree as specified by the paths
      /// included in the message and serializes this to be returned to the
      /// client using the specified encoding.
      /// Reference: gNMI Specification Section 3.3
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.GetResponse> GetAsync(global::Gnmi.GetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>
      /// Set allows the client to modify the state of data on the target. The
      /// paths to modified along with the new values that the client wishes
      /// to set the value to.
      /// Reference: gNMI Specification Section 3.4
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.SetResponse Set(global::Gnmi.SetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Set(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Set allows the client to modify the state of data on the target. The
      /// paths to modified along with the new values that the client wishes
      /// to set the value to.
      /// Reference: gNMI Specification Section 3.4
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Gnmi.SetResponse Set(global::Gnmi.SetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Set, null, options, request);
      }
      /// <summary>
      /// Set allows the client to modify the state of data on the target. The
      /// paths to modified along with the new values that the client wishes
      /// to set the value to.
      /// Reference: gNMI Specification Section 3.4
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.SetResponse> SetAsync(global::Gnmi.SetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Set allows the client to modify the state of data on the target. The
      /// paths to modified along with the new values that the client wishes
      /// to set the value to.
      /// Reference: gNMI Specification Section 3.4
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Gnmi.SetResponse> SetAsync(global::Gnmi.SetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Set, null, options, request);
      }
      /// <summary>
      /// Subscribe allows a client to request the target to send it values
      /// of particular paths within the data tree. These values may be streamed
      /// at a particular cadence (STREAM), sent one off on a long-lived channel
      /// (POLL), or sent as a one-off retrieval (ONCE).
      /// Reference: gNMI Specification Section 3.5
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::Gnmi.SubscribeRequest, global::Gnmi.SubscribeResponse> Subscribe(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Subscribe(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Subscribe allows a client to request the target to send it values
      /// of particular paths within the data tree. These values may be streamed
      /// at a particular cadence (STREAM), sent one off on a long-lived channel
      /// (POLL), or sent as a one-off retrieval (ONCE).
      /// Reference: gNMI Specification Section 3.5
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncDuplexStreamingCall<global::Gnmi.SubscribeRequest, global::Gnmi.SubscribeResponse> Subscribe(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_Subscribe, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override gNMIClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new gNMIClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(gNMIBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Capabilities, serviceImpl.Capabilities)
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_Set, serviceImpl.Set)
          .AddMethod(__Method_Subscribe, serviceImpl.Subscribe).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, gNMIBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Capabilities, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Gnmi.CapabilityRequest, global::Gnmi.CapabilityResponse>(serviceImpl.Capabilities));
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Gnmi.GetRequest, global::Gnmi.GetResponse>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_Set, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Gnmi.SetRequest, global::Gnmi.SetResponse>(serviceImpl.Set));
      serviceBinder.AddMethod(__Method_Subscribe, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Gnmi.SubscribeRequest, global::Gnmi.SubscribeResponse>(serviceImpl.Subscribe));
    }

  }
}
#endregion