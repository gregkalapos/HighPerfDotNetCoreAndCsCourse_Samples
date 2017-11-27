using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;
using System.Text;

namespace StringParsingStringVsSpan
{
    /// <summary>
    /// This small demo is used in Video 6.3 to demonstrate the performance difference
    /// between parsing a string with System.Substring vs. ReadOnlySpan<char>
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            //WorkWithString();
        }

        [Benchmark]
        public static void WorkWithString()
        {
            String str = @"Request URL: http://kalapos.net/
						   Request Method:GET
						   Status Code:200 OK
						   Remote Address:81.10.250.159:80
						   Referrer Policy:no-referrer-when-downgrade
						   Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
						   Accept-Encoding:gzip, deflate
						   Accept-Language:de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4,hu;q=0.2
						   Connection:keep-alive
						   Cookie:__cfduid=d37ec605870fbb75a1b5b9616bf161ed01507890791; _ga=GA1.2.460922426.1476689834; _gid=GA1.2.1163677089.1508317942
						   Host:kalapos.net
						   Upgrade-Insecure-Requests:1
						   User-Agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

            ParseStringClassic(str);
        }

        [Benchmark]
        public static void WorkWithSpan()
        {
            ReadOnlySpan<char> str = @"Request URL: http://kalapos.net/
						   Request Method:GET
						   Status Code:200 OK
						   Remote Address:81.10.250.159:80
						   Referrer Policy:no-referrer-when-downgrade
						   Accept:texvt/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
						   Accept-Encoding:gzip, deflate
						   Accept-Language:de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4,hu;q=0.2
						   Connection:keep-alive
						   Cookie:__cfduid=d37ec605870fbb75a1b5b9616bf161ed01507890791; _ga=GA1.2.460922426.1476689834; _gid=GA1.2.1163677089.1508317942
						   Host:kalapos.net
						   Upgrade-Insecure-Requests:1
						   User-Agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36".AsSpan();

            ParseStringSpan(str);
        }

        /// <summary>
        /// The method based on the traditional String API
        /// </summary>
        /// <param name="str"></param>
        public static void ParseStringClassic(String str)
        {
            var url = str.Substring(13, 19);
            var method = str.Substring(58, 3);
            Int32.TryParse(str.Substring(84, 3), out int statusCode);
            var remoteAddr = str.Substring(116, 14);
            var referrerPolicy = str.Substring(157, 26);
            var accept = str.Substring(201, 85);
            var acceptEncoding = str.Substring(313, 13);
            var acceptLanguage = str.Substring(353, 44);
            var connection = str.Substring(419, 10);
            var cookie = str.Substring(447, 118);
            var host = str.Substring(581, 11);
            Int32.TryParse(str.Substring(629, 1), out int updgradeInsecureRequest);
            var userAgent = str.Substring(652, 115);

            Debug.WriteLine(url);
            Debug.WriteLine(method);
            Debug.WriteLine(statusCode);
            Debug.WriteLine(remoteAddr);
            Debug.WriteLine(accept);
            Debug.WriteLine(acceptEncoding);
            Debug.WriteLine(acceptLanguage);

            Debug.WriteLine(connection);
            Debug.WriteLine(cookie);
            Debug.WriteLine(host);
            Debug.WriteLine(updgradeInsecureRequest);
            Debug.WriteLine(userAgent);
        }

        /// <summary>
        /// The method based on ReadOnlySpan
        /// </summary>
        /// <param name="str"></param>
        public static void ParseStringSpan(ReadOnlySpan<char> str)
        {
            var url = str.Slice(13, 19);
            var method = str.Slice(58, 3);
            PrimitiveParser.TryParseInt32(str.Slice(84, 3).AsBytes(), out int statusCode, out int bytesConsumed);
            var remoteAddr = str.Slice(116, 14);
            var referrerPolicy = str.Slice(157, 26);
            var accept = str.Slice(201, 85);
            var acceptEncoding = str.Slice(313, 13);
            var acceptLanguage = str.Slice(353, 44);
            var connection = str.Slice(419, 10);
            var cookie = str.Slice(447, 118);
            var host = str.Slice(581, 11);
            PrimitiveParser.TryParseInt32(str.Slice(629, 1).AsBytes(), out int updgradeInsecureRequest, out int bytesConsumed2);
            var userAgent = str.Slice(652, 115);

            Debug.WriteLine(url);
            Debug.WriteLine(method);
            Debug.WriteLine(statusCode);
            Debug.WriteLine(remoteAddr);
            Debug.WriteLine(accept);
            Debug.WriteLine(acceptEncoding);
            Debug.WriteLine(acceptLanguage);
            Debug.WriteLine(connection);
            Debug.WriteLine(cookie);
            Debug.WriteLine(host);
            Debug.WriteLine(updgradeInsecureRequest);
            Debug.WriteLine(userAgent);
        }
    }
}
