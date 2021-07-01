using ChatClient;
using NUnit.Framework;
using System.Net;

namespace ChatClientTest
{
    public class ClientTest
    {
        private IClient client1, client2, client3;

        [SetUp]
        public void Setup()
        {
            if (client1 == null)
            {
                client1 = Client.New("username_01", IPAddress.Parse("127.0.0.1"), 8081).Run();
            }
            if (client2 == null)
            {
                client2 = Client.New("username_02", IPAddress.Parse("127.0.0.1"), 8081).Run();
            }
            if (client3 == null)
            {
                client3 = Client.New("username_03", IPAddress.Parse("127.0.0.1"), 8081).Run();
            }
        }

        [Test]
        [TestCase("Teste de mensagem pública.")]
        [TestCase("")]
        public void Test1_SendPublicMessage(string msg)
        {
            try
            {
                string username = client1.Username();
                client1.SendMessage($"700||{username}||null||{msg}", username);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        [TestCase("Teste de mensagem privada.")]
        [TestCase("")]
        public void Test2_SendPrivateMessage(string msg)
        {
            try
            {
                string username = client1.Username();
                string toUsername = client2.Username();
                client1.SendMessage($"900||{username}||{toUsername}||{msg}", toUsername);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Test3_DisconnectClients()
        {
            try
            {
                client1 = null;
                client2 = null;
                client3 = null;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
}