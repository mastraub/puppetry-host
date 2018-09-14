using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PuppetryHostTests
{
    [TestClass]
    public class ProcessTests
    {
        [TestMethod]
        public void LaunchTest()
        {
            // java [options] class [args]
            // openjdk-1.8.0.131-1.b11\bin\java -Dlogback.configurationFile=conf/logback.xml -Dfile.encoding=UTF-8 -d64 -Xms256m -Xmx2g -XX:+HeapDumpOnOutOfMemoryError -classpath libs/* de.ehex.puppetry.Starter %1 %2

            var process = new Process();
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += (s, e) => Debug.WriteLine("Output -> " + e.Data);
            process.ErrorDataReceived += (s, e) => Debug.WriteLine("Error -> " + e.Data);

            //* Set your output and error (asynchronous) handlers
            //process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            //process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);

            process.StartInfo = new ProcessStartInfo(@"C:\Users\marti\repos\puppetry-host\puppetry\openjdk-1.8.0.131-1.b11\bin\java")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = "-Dlogback.configurationFile=conf/logback.xml -Dfile.encoding=UTF-8 -d64 -Xms256m -Xmx2g -XX:+HeapDumpOnOutOfMemoryError -classpath libs/* de.ehex.puppetry.Starter",
                WorkingDirectory = @"C:\Users\marti\repos\puppetry-host\puppetry"
            };

            var foo = process.Start();
            if (!foo)
            {
                throw new InvalidOperationException("??");
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();


            //int exitCode = process.ExitCode;
            //Debug.WriteLine("ExitCode: " + exitCode);

            process.Close();
        }

        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //* Do your stuff with the output (write to console/log/StringBuilder)
            Debug.WriteLine(outLine.Data);
        }
    }
}
