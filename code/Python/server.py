import socket
import threading
import time
from queue import Queue
from datetime import datetime

# Diccionario para almacenar las colas por tipo de sensor

sensor_queues = {
    'Puertas': Queue(),
    'Movimiento': Queue(),
    'Temperatura': Queue(),
    'Humedad': Queue(),
    'Sonido': Queue(),
    'Presion': Queue(),
    'Luminosidad': Queue()
}

# Diccionario que contendrá cada habitación asociada a su conjunto de sensores
room_queues = {
    'Recibidor': dict(sensor_queues),
    'Salon': dict(sensor_queues),
    'Cocina': dict(sensor_queues),
    'Habitacion_1': dict(sensor_queues),
    'Habitacion_2': dict(sensor_queues),
    'Habitacion_3': dict(sensor_queues),
    'Habitacion_4': dict(sensor_queues),
    'Servicio': dict(sensor_queues),
    'Pasillo': dict(sensor_queues)
}

def handle_client(client_socket):
    request = client_socket.recv(1024).decode('utf-8')
    room, data = request.split(';', 1) # Los datos nos llegan en forma -> habitación;datos_sensores
    
    print(f"{room} -> {data}")
    '''lista_sensores = data.split(':')
    print(f"{room}-->{data}");
    sensor_type, data = data.split(':')  # Asumiendo que los datos vienen en formato 'Tipo_Sensor:Datos,'
    queues[sensor_type].put(data)  # Asegúrate que 'sensor_type' coincida con las claves del diccionario
    
    # Almacena los datos en un archivo de texto
    
    with open(f"{sensor_type}_data.txt", "a") as file:
        file.write(f"{room}{sensor_type}: {data}\n")'''

    client_socket.close()

def server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(("127.0.0.1", 8080))
    server_socket.listen(5)
    print("El servidor está escuchando en el puerto 8080")

    while True:
        client_socket, addr = server_socket.accept()
        client_handler = threading.Thread(target=handle_client, args=(client_socket,))
        client_handler.start()

# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()
