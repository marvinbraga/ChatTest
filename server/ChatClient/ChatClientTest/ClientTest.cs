using ChatClient;
using NUnit.Framework;
using System.Net;

namespace ChatClientTest
{
    public class Tests
    {
        private IClient client;
        private string username;

        [SetUp]
        public void Setup()
        {
            username = "test_user";
            client = Client.New(username, IPAddress.Parse("127.0.0.1"), 8081).Run();
        }

        [Test]
        [TestCase("Teste de mensagem.")]
        public void SendPublicMessage(string msg)
        {
            try
            {
                client.SendMessage($"700||{username}||null||{msg}", username);
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}