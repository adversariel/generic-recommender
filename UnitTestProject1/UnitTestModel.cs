using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Sockets;
using CustomNetworking;
using System.Text;
using System.Threading;

namespace RecEngModel
{
    [TestClass]
    public class UnitTestModel
    {
        [TestMethod]
        public void TestGetID()
        {
            Model m = new Model();
            string result = m.getID("1984");
            Assert.AreEqual(result, "17");
        }

        [TestMethod]
        public void TestGetAuthor()
        {
            Model m = new Model();
            string result = m.getAuthor("17");
            Assert.AreEqual(result, "George Orwell");
        }

        [TestMethod]
        public void TestGetTitle()
        {
            Model m = new Model();
            string result = m.getTitle("17");
            Assert.AreEqual(result, "1984");
        }

        [TestMethod]
        public void TestClientGetAuthor()
        {
            Model m = new Model();
            m.Start();
            SSClientTest tester = new SSClientTest();
            tester.send("AUTHOR 17\n");

            try
            {
                Assert.IsTrue(tester.ev.WaitOne(10000), "Client Start Failed");
                Assert.AreEqual("AUTHOR George Orwell", tester.auth);
            }
            finally
            {
                try
                {
                    tester.Close();
                    m.Stop();
                }
                catch { }
            }
        }
    }

    [TestClass()]
    public class SSClientTest
    {
        public ManualResetEvent ev = new ManualResetEvent(false);
        private StringSocket ss;
        private TcpClient tc;
        public string auth = "";

        public SSClientTest()
        {
            tc = new TcpClient("localhost", 2000);
            ss = new StringSocket(tc.Client, new UTF8Encoding());
            this.receive();
        }

        public void Close()
        {
            tc.Close();
        }

        public void send(string command)
        {
            ss.BeginSend(command, sendF, null);
        }

        private void sendF(Exception e, object payload)
        {
            if (e != null) { throw e; }
        }

        private void receive()
        {
            ss.BeginReceive(receiveF, null);
        }

        private void receiveF(string command, Exception e, object payload)
        {
            if (command.ToUpper().StartsWith("AUTHOR "))
            {
                auth = command;
                ev.Set();
            }
            this.receive();
        }
    }

}
