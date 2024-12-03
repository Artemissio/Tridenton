using System.Net;
using System.Net.Mime;
using Tridenton.Core;
using Tridenton.Core.Metadata;
using Tridenton.Core.Metadata.Tracing;
using Tridenton.Core.Utilities;

var requestId = RequestId.NewId();

var metadata = new Metadata(requestId);

var segmentHost = new TraceSegmentHost(
    host: "http://localhost",
    port: 80);

var segmentRequest = new TraceSegmentRequest(
    method: HttpMethod.Get,
    path: "/api/tests/",
    protocol: "http",
    headers: [],
    cookies: [],
    query: [],
    routeValues: []);

var segmentResponse= new TraceSegmentResponse(
    statusCode: HttpStatusCode.OK,
    contentType: new ContentType("application/json"),
    content: """{ "Test": "test" }""",
    headers: [],
    cookies: []);

var segment = new TraceSegment(
    host: segmentHost,
    request: segmentRequest,
    response: segmentResponse);
    
metadata.Trace.Append(segment);
metadata.Trace.Append(segment);

var json = Serializer.ToJson(metadata);

Console.WriteLine(json);

Console.ReadKey();