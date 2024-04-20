import socket
import threading
import time
from queue import Queue
from datetime import datetime
from tabulate import tabulate

# Puerto para la recepción de datos
puerto = 8888
ip_addr = "0.0.0.0"
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
    request = client_socket.recv(1024).decode('utf-8').rstrip()
    roomAndHour, sensorData = request.split(';') # Los datos nos llegan en forma -> habitación Hora;datos_sensores
    room, hour = roomAndHour.split(' ') # Obtención de la habitación y hora de actualización
    lista_sensores = sensorData.split(',')

    # Diccionario para almacenar los datos de los sensores por habitación
    sensor_dict = {}
    
    # Recopilar datos de los sensores para cada habitación

    for sensor in lista_sensores:
        try:
            if sensor != '':
                sensor_type, sensor_value = sensor.split(':')
                if sensor_value.lower() == 'true':
                    sensor_value = 1
                elif sensor_value.lower() == 'false':
                    sensor_value = 0
                if room not in sensor_dict:
                    sensor_dict[room] = {}
                if sensor_type not in sensor_dict[room]:
                    sensor_dict[room][sensor_type] = []
                sensor_dict[room][sensor_type].append(sensor_value)
        except ValueError:
            # Si el split falla, imprimir el mensaje del request directamente
            print(request)
            break  # Salir del bucle para evitar procesar más datos

    print(request)


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
    server_socket.bind((ip_addr, puerto))
    server_socket.listen(5)
    print(f"El servidor está escuchando en el puerto {puerto}")

    while True:
        client_socket, addr = server_socket.accept()
        client_handler = threading.Thread(target=handle_client, args=(client_socket,))
        client_handler.start()
            
# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()