﻿
namespace MessagePassing.Products.EventPublisher.Infrastructure;

public class RabbitMqConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
