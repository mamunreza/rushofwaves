import pika
import json
import argparse
import time

def publish_message(queue, message, host="localhost", repeat=1, delay=1):
    """Publishes a message to RabbitMQ."""
    connection = pika.BlockingConnection(pika.ConnectionParameters(host=host))
    channel = connection.channel()
    
    # Declare the queue (ensures it exists)
    channel.queue_declare(queue=queue, durable=True)

    # Loop for repeated sending (if needed)
    for i in range(repeat):
        final_message = json.dumps(message) if isinstance(message, dict) else message
        channel.basic_publish(
            exchange="",
            routing_key=queue,
            body=final_message.encode("utf-8"),
            properties=pika.BasicProperties(
                delivery_mode=2,  # Persistent message
            )
        )
        print(f"[x] Sent to '{queue}': {final_message}")

        if repeat > 1:
            time.sleep(delay)  # Delay between messages

    connection.close()


def load_json_file(file_path):
    """Loads a message from a JSON file."""
    try:
        with open(file_path, "r", encoding="utf-8") as f:
            return json.load(f)
    except Exception as e:
        print(f"Error loading JSON file: {e}")
        return None


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="RabbitMQ Message Publisher")
    parser.add_argument("-q", "--queue", type=str, required=True, help="Queue name")
    parser.add_argument("-m", "--message", type=str, help="Message as a string")
    parser.add_argument("-f", "--file", type=str, help="JSON file with message content")
    parser.add_argument("-r", "--repeat", type=int, default=1, help="Number of times to send message")
    parser.add_argument("-d", "--delay", type=float, default=1, help="Delay (seconds) between repeated messages")
    parser.add_argument("--host", type=str, default="localhost", help="RabbitMQ host")

    args = parser.parse_args()

    # Load message from file if provided
    if args.file:
        message = load_json_file(args.file)
        if not message:
            exit(1)
    else:
        message = args.message or '{"default": "Hello RabbitMQ"}'

    # Send message
    publish_message(args.queue, message, args.host, args.repeat, args.delay)
