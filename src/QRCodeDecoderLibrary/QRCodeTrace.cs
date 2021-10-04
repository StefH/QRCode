//using System;
//using System.IO;

//namespace QRCodeDecoderLibrary
//{
//    /// <summary>
//    /// Trace Class 
//    /// </summary>
//    static public class QRCodeTrace
//    {
//        private static TextWriter TraceFile;

//        public static void Init(TextWriter tw)
//        {
//            TraceFile = tw;

//            Write("----");
//        }

//        public static void Format(string message, params object[] args)
//        {
//            return;
//            if (args.Length == 0)
//            {
//                Write(message);
//            }
//            else
//            {
//                Write(string.Format(message, args));
//            }
//        }

//        public static void Write(string message)
//        {
//            return;
//            // write date and time
//            TraceFile.Write(string.Format("{0:yyyy}-{0:MM}-{0:dd} {0:HH}:{0:mm}:{0:ss}:{0:fff} ", DateTime.Now));

//            // write message
//            TraceFile.WriteLine(message);

//            // close the file
//            TraceFile.Close();
//        }
//    }
//}