import socket
import threading
import time
from queue import Queue
from datetime import datetime

# Definir un diccionario para almacenar las colas por tipo de sensor
queues = {
    'Puertas': Queue(),
    'Posición': Queue(),
    'Temperatura': Queue(),
    'Humedad': Queue(),
    'Sonido': Queue()
}

last_door_state = None  # Variable para almacenar el último estado de la puerta

def handle_client(client_socket):
    print("Cliente")
    request = client_socket.recv(1024).decode('utf-8')
    sensor_type, data = request.split(':', 1)  # Asumiendo que los datos vienen en formato 'Tipo:Datos'
    queues[sensor_type].put(data)  # Asegúrate que 'sensor_type' coincida con las claves del diccionario
    
    # Almacena los datos en un archivo de texto
    with open(f"{sensor_type}_data.txt", "a") as file:
        file.write(f"{datetime.now()}: {data}\n")

    client_socket.close()

def server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(("127.0.0.1", 8080))
    server_socket.listen(5)
    print("El servidor está escuchando en el puerto 8080")

    while True:
        client_socket, addr = server_socket.accept()
        print("Servidor")
        client_handler = threading.Thread(target=handle_client, args=(client_socket,))
        client_handler.start()

# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()
