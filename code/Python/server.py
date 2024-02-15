import socket
import threading
import time
from queue import Queue
from datetime import datetime
import tabulate  # Necesitarás instalar esta librería con pip install tabulate

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
    request = client_socket.recv(1024).decode('utf-8')
    sensor_type, data = request.split(':', 1)  # Asumiendo que los datos vienen en formato 'Tipo:Datos'
    queues[sensor_type].put(data)  # Asegúrate que 'sensor_type' coincida con las claves del diccionario
    client_socket.close()

def print_table():
    global last_door_state  # Indica que estás usando la variable global
    while True:
        if not queues['Puertas'].empty():
            door_data = queues['Puertas'].get()
            if door_data != last_door_state:
                print(f'Puerta: {door_data}')
                last_door_state = door_data  # Actualiza el último estado

def server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(("127.0.0.1", 8052))
    server_socket.listen(5)
    print("El servidor está escuchando en el puerto 8052")

    while True:
        client_socket, addr = server_socket.accept()
        client_handler = threading.Thread(target=handle_client, args=(client_socket,))
        client_handler.start()

# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()

print_table_thread = threading.Thread(target=print_table)
print_table_thread.start()
