using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    VirtualHost = "/",
    UserName = "guest",
    Password = "guest",
    Port = 5672
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.ExchangeDeclare(exchange: "chat.exchange", type: ExchangeType.Direct);
channel.QueueDeclare(queue: "chat_queue", durable: true, autoDelete: false);

while (true)
{
    Console.WriteLine("Enter your message: ");
    var message = Console.ReadLine();
    var messageByte = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "chat.exchange", routingKey: "chat_queue", body: messageByte);
}
