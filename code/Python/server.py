import socket
import threading
import time
from queue import Queue
from datetime import datetime
from tabulate import tabulate

# Diccionario para almacenar las colas por tipo de sensor

sensor_queues = {
    'Puertas': Queue(),
    'Movimiento': Queue(),
    'Temperatura': Queue(),
    'Humedad': Queue(),
    'Sonido': Queue(),
    'Presion': Queue(),
    'Luminosidad': Queue(),
    'Hora': Queue(),
}

# Diccionario que contendrá cada habitación asociada a su conjunto de sensores
room_queues = {
    'PlacaRecibidor': dict(sensor_queues),
    'PlacaSalonComedor': dict(sensor_queues),
    'PlacaCocina': dict(sensor_queues),
    'PlacaHabitacion_1': dict(sensor_queues),
    'PlacaHabitacion_2': dict(sensor_queues),
    'PlacaHabitacion_3': dict(sensor_queues),
    'PlacaHabitacion_4': dict(sensor_queues),
    'PlacaServicio': dict(sensor_queues),
    'PlacaPasillo': dict(sensor_queues)
}

def handle_client(client_socket):
    # Formato del mensaje entrante: room hora;sensor_type:data_sensor,...
    request = client_socket.recv(1024).decode('utf-8')
    roomAndHour, sensorData = request.split(';') # Los datos nos llegan en forma -> habitación Hora;datos_sensores
    room, hour = roomAndHour.split(' ') # Obtención de la habitación y hora de actualización
    lista_sensores = sensorData.split(',')
    
    # Diccionario para almacenar los datos de los sensores por habitación
    sensor_dict = {}
    
    # Recopilar datos de los sensores para cada habitación
    for sensor in lista_sensores:
        sensor_type, sensor_value = sensor.split(':')
        if room not in sensor_dict:
            sensor_dict[room] = {}
        if sensor_type not in sensor_dict[room]:
            sensor_dict[room][sensor_type] = []
        sensor_dict[room][sensor_type].append(sensor_value)

    # Construir la tabla con los datos de los sensores
    for room, sensors in sensor_dict.items():
        table_headers = ["Sensor", "Valor"]
        table_rows = []
        for sensor_type, sensor_values in sensors.items():
            table_headers.append(sensor_type)
            table_rows.append([sensor_type] + sensor_values)

        with open(f"{room}.txt", "a") as file:
            file.write(f"\n{hour}\n")
            file.write(tabulate(table_rows, headers=table_headers, tablefmt="grid"))
        #print(f"Habitación: {room} a las {hour}")
        #print(tabulate(table_rows, headers=table_headers, tablefmt="grid"))
        #print()  # Añadir línea en blanco entre tablas de habitaciones

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

def guardar_datos_room_queues():
    for room, sensors in room_queues.items():
        rows = []
        for sensor, queue in sensors.items():
            valores_sensor = []
            while not queue.empty():
                value = queue.get()
                valores_sensor.append(value)
            queue.queue.clear()  # Limpiamos la cola después de imprimir sus valores
            rows.append([sensor] + valores_sensor)

        # Construir la tabla con la cabecera y los datos
        table = ""
        for row in rows:
            table += f"Sensor: {row[0]}\n"
            table += tabulate([row[1:]], headers=["Valor"], tablefmt="plain")
            table += "\n\n"  # Añadir un espacio entre cada sensor

        # Guardar la tabla en un archivo de texto
        with open(f"{room}.txt", "w") as file:
            file.write(table)
            
# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()
