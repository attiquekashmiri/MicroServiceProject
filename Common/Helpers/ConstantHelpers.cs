using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public class ConstantHelpers
    {
        public static string BaseUrl = null;
        public static void ApiLogs(string API, string Message, bool result)
        {
            try
            {
                if (BaseUrl != null)
                {
                    string date = DateTime.Now.ToString("dd-MM-yyyy");
                    var filePath = BaseUrl + "/ApiLogs/" + date + "_Logs.txt";

                    if (!File.Exists(filePath))
                    {
                        using (var file = File.Create(filePath)) ;
                    }
                    if (File.Exists(filePath))
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Exists)
                        {
                            // Get file size
                            long size = fi.Length;
                            if (size > 1000000000)
                            {
                                return;
                            }
                        }
                    }
                    string Log = "";
                    string dateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt");
                    string Heading = "=============  Date Time : " + dateTime + " ================ \n";
                    string ApiPath = "API : " + API + "\n";
                    string Result = "Result : " + (result == true ? "Success" : "Failure") + "\n";
                    string message = (result == true ? "Return Message: " : "Exception: ");
                    message = message + Message + "\n";


                    Log = Log + Heading + ApiPath + Result + message + "\n";
                    using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Log);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex;
            }
        }
        public static string PadNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                var x = Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
                return x;
            }
        }
    }
}
